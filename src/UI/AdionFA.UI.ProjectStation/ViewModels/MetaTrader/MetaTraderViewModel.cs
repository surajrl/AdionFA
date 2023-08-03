using AdionFA.Application.Contracts;
using AdionFA.Domain.Enums;
using AdionFA.Infrastructure.Extractor.Model;
using AdionFA.Infrastructure.IofC;
using AdionFA.Infrastructure.MetaTrader.Contracts;
using AdionFA.Infrastructure.MetaTrader.Model;
using AdionFA.Infrastructure.Modules.Strategy;
using AdionFA.TransferObject.Project;
using AdionFA.UI.Infrastructure;
using AdionFA.UI.Infrastructure.AutoMapper;
using AdionFA.UI.Infrastructure.Model.Project;
using AdionFA.UI.ProjectStation.Commands;
using AdionFA.UI.ProjectStation.EventAggregator;
using AdionFA.UI.ProjectStation.Features;
using AdionFA.UI.ProjectStation.Model.MetaTrader;
using AutoMapper;
using NetMQ;
using NetMQ.Sockets;
using Newtonsoft.Json;
using Ninject;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AdionFA.UI.ProjectStation.ViewModels
{
    public class MetaTraderViewModel : MenuItemViewModel, IDisposable
    {
        private readonly IMapper _mapper;

        private readonly ITradeService _tradeService;
        private readonly IProjectService _projectService;
        private readonly IMarketDataService _marketDataService;

        private readonly IEventAggregator _eventAggregator;

        private CancellationTokenSource _cancellationTokenSource;
        private bool _disposedValue;

        private Task _subscriberTask;

        private readonly Dictionary<string, List<Candle>> _completeCandles;
        private readonly Dictionary<string, Candle> _currentCandles;
        private string _mainSymbol;
        private readonly List<string> _crossingSymbols;

        public MetaTraderViewModel(MainViewModel mainViewModel)
            : base(mainViewModel)
        {
            _tradeService = IoC.Kernel.Get<ITradeService>();
            _projectService = IoC.Kernel.Get<IProjectService>();
            _marketDataService = IoC.Kernel.Get<IMarketDataService>();

            _eventAggregator = ContainerLocator.Current.Resolve<IEventAggregator>();

            ContainerLocator.Current.Resolve<IAppProjectCommands>().SelectItemHamburgerMenuCommand.RegisterCommand(SelectItemHamburgerMenuCommand);
            ContainerLocator.Current.Resolve<IApplicationCommands>().AddNodeToMetaTrader.RegisterCommand(AddNodeToMetaTrader);
            ContainerLocator.Current.Resolve<IApplicationCommands>().RemoveNodeFromMetaTrader.RegisterCommand(RemoveNodeFromMetaTrader);

            _eventAggregator.GetEvent<AppProjectCanExecuteEvent>().Subscribe(p => CanExecute = p);

            _mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingInfrastructureProfile());
            }).CreateMapper();

            Nodes = new();
            DataInput = new();
            DataOutput = new();

            _completeCandles = new();
            _currentCandles = new();
            _crossingSymbols = new();
        }

        public ICommand SelectItemHamburgerMenuCommand => new DelegateCommand<string>(item =>
        {
            if (item == HamburgerMenuItems.MetaTraderTrim)
            {
                ProjectConfiguration = _mapper.Map<ProjectConfigurationDTO, ProjectConfigurationVM>(_projectService.GetProjectConfiguration(ProcessArgs.ProjectId, true));
            }
        });
        public ICommand StopCommand => new DelegateCommand(() =>
        {
            _cancellationTokenSource.Cancel();

            while (!_subscriberTask.IsCompleted)
            {
                // Wait for the task to cancel
            }

            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;

            IsTransactionActive = false;
            _eventAggregator.GetEvent<AppProjectCanExecuteEvent>().Publish(true);

        }, () => IsTransactionActive)
            .ObservesProperty(() => IsTransactionActive);

        public ICommand ProcessCommand => new DelegateCommand(() =>
        {
            try
            {
                IsTransactionActive = true;
                _eventAggregator.GetEvent<AppProjectCanExecuteEvent>().Publish(false);

                _cancellationTokenSource = new();

                if (TestStrategyNode)
                {
                    _completeCandles.Clear();
                    _crossingSymbols.Clear();
                    _currentCandles.Clear();

                    var symbol = _marketDataService.GetSymbol(ProcessArgs.SymbolId);

                    _completeCandles.Add(symbol.Name, new());
                    _mainSymbol = symbol.Name;

                    foreach (var crossingNode in StrategyNode.CrossingNodesData)
                    {
                        var historicalData = _marketDataService.GetHistoricalData(crossingNode.Item2, includeGraph: false);
                        symbol = _marketDataService.GetSymbol(historicalData.SymbolId);
                        _completeCandles.Add(symbol.Name, new());
                        _crossingSymbols.Add(symbol.Name);
                    }
                }
                else
                {
                    _completeCandles.Clear();
                    _currentCandles.Clear();

                    var symbol = _marketDataService.GetSymbol(ProcessArgs.SymbolId);

                    _completeCandles.Add(symbol.Name, new());
                    _mainSymbol = symbol.Name;
                }

                _subscriberTask = Task.Factory.StartNew(Subscriber);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
            }
        }, () => !IsTransactionActive
        && (TestNodes && Nodes.Count > 0
        || TestAssemblyNode && AssemblyNode != null
        || TestStrategyNode && StrategyNode != null))
            .ObservesProperty(() => IsTransactionActive)
            .ObservesProperty(() => TestNodes)
            .ObservesProperty(() => TestAssemblyNode)
            .ObservesProperty(() => TestStrategyNode)
            .ObservesProperty(() => Nodes.Count)
            .ObservesProperty(() => AssemblyNode)
            .ObservesProperty(() => StrategyNode);

        private void Subscriber()
        {
            using var subscriber = new SubscriberSocket($">tcp://{ProjectConfiguration.ExpertAdvisorHost}:{ProjectConfiguration.ExpertAdvisorPublisherPort}");

            try
            {
                subscriber.Subscribe("");

                while (true)
                {
                    _cancellationTokenSource.Token.ThrowIfCancellationRequested();

                    if (subscriber.TryReceiveFrameString(TimeSpan.FromMilliseconds(1000), out var message) && !string.IsNullOrWhiteSpace(message))
                    {
                        // Candle data received
                        var zmqModel = JsonConvert.DeserializeObject<ZmqMsgModel>(message);
                        Debug.WriteLine($"SubscriberSocket-Receive:{message}");

                        if (zmqModel.CheckTrade)
                        {
                            Requester();
                            continue;
                        }

                        zmqModel.DateFormat = zmqModel.Date.AddSeconds(TimeSpan.Parse(zmqModel.Time, CultureInfo.InvariantCulture).TotalSeconds).ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);

                        if (zmqModel.IsCurrentBar)
                        {
                            _currentCandles[zmqModel.Symbol] = new Candle
                            {
                                Date = zmqModel.Date.AddSeconds((long)TimeSpan.Parse(zmqModel.Time, CultureInfo.InvariantCulture).TotalSeconds),
                                Time = (long)TimeSpan.Parse(zmqModel.Time, CultureInfo.InvariantCulture).TotalSeconds,
                                Open = zmqModel.Open,
                                High = zmqModel.High,
                                Low = zmqModel.Low,
                                Close = zmqModel.Close
                            };
                        }
                        else
                        {
                            _completeCandles[zmqModel.Symbol].Add(new Candle
                            {
                                Date = zmqModel.Date.AddSeconds((long)TimeSpan.Parse(zmqModel.Time, CultureInfo.InvariantCulture).TotalSeconds),
                                Time = (long)TimeSpan.Parse(zmqModel.Time, CultureInfo.InvariantCulture).TotalSeconds,
                                Open = zmqModel.Open,
                                High = zmqModel.High,
                                Low = zmqModel.Low,
                                Close = zmqModel.Close
                            });

                            System.Windows.Application.Current.Dispatcher.Invoke(() => DataInput.Insert(0, zmqModel));
                        }
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // Stop pressed ...
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                Debug.WriteLine($"SubscriberSocket:{ex.Message}");
            }
        }

        private bool IsTradeNodes()
        {
            foreach (var node in Nodes)
            {
                var isTrade = _tradeService.IsTrade(
                    node.NodeData.Node,
                    _completeCandles[_mainSymbol],
                    _currentCandles[_mainSymbol]);

                if (isTrade)
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsTradeAssemblyNode()
        {
            // Test for parent node
            var isTrade = _tradeService.IsTrade(
                AssemblyNode.ParentNodeData.Node,
                _completeCandles[_mainSymbol],
                _currentCandles[_mainSymbol]);

            if (!isTrade)
            {
                return false;
            }

            // Test for one child node
            foreach (var childNode in AssemblyNode.ChildNodesData)
            {
                isTrade = _tradeService.IsTrade(
                    childNode.Node,
                    _completeCandles[_mainSymbol],
                    _currentCandles[_mainSymbol]);

                if (isTrade)
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsTradeStrategyNode()
        {
            // Test parent node
            var isTrade = _tradeService.IsTrade(
                StrategyNode.ParentNodeData.Node,
                _completeCandles[_mainSymbol],
                _currentCandles[_mainSymbol]);

            if (!isTrade)
            {
                return false;
            }

            // Test for every crossing node
            for (var idx = 0; idx < StrategyNode.CrossingNodesData.Count; idx++)
            {
                isTrade = _tradeService.IsTrade(
                    StrategyNode.CrossingNodesData[idx].Item1.Node,
                    _completeCandles[_crossingSymbols[idx]],
                    _currentCandles[_crossingSymbols[idx]]);

                if (!isTrade)
                {
                    return false;
                }
            }

            // Test for one child node
            foreach (var childNode in StrategyNode.ChildNodesData)
            {
                isTrade = _tradeService.IsTrade(
                    childNode.Node,
                    _completeCandles[_mainSymbol],
                    _currentCandles[_mainSymbol]);

                if (isTrade)
                {
                    return true;
                }
            }

            return false;
        }

        private void Requester()
        {
            using var requester = new RequestSocket($">tcp://{ProjectConfiguration.ExpertAdvisorHost}:{ProjectConfiguration.ExpertAdvisorResponsePort}");

            try
            {
                _cancellationTokenSource.Token.ThrowIfCancellationRequested();

                var isTrade = false;
                var label = string.Empty;

                if (TestNodes && IsTradeNodes())
                {
                    isTrade = true;
                    label = Nodes[0].NodeData.Label;
                }
                else if (TestAssemblyNode && IsTradeAssemblyNode())
                {
                    isTrade = true;
                    label = AssemblyNode.ParentNodeData.Label;
                }
                else if (TestStrategyNode && IsTradeStrategyNode())
                {
                    isTrade = true;
                    label = StrategyNode.ParentNodeData.Label;
                }

                if (isTrade)
                {
                    // request --------------------------------------------------------------
                    var operationRequest = label.ToLowerInvariant() == "up"
                    ? _tradeService.OperationRequest(OrderTypeEnum.Buy)
                    : _tradeService.OperationRequest(OrderTypeEnum.Sell);

                    requester.SendFrame(JsonConvert.SerializeObject(operationRequest));
                    Debug.WriteLine($"RequestSocket-Send:{JsonConvert.SerializeObject(operationRequest)}");
                    //-----------------------------------------------------------------------

                    // response -------------------------------------------------------------
                    var openOperationRep = requester.ReceiveFrameString();
                    Debug.WriteLine($"RequestSocket-Receive:{openOperationRep}");

                    var response = JsonConvert.DeserializeObject<ZmqMsgRequestModel>(openOperationRep);

                    System.Windows.Application.Current.Dispatcher.Invoke(() => DataOutput.Insert(0,
                        new ZmqMsgModel
                        {
                            Id = DataOutput.Count + 1,
                            Date = _currentCandles[_mainSymbol].Date,
                            DateFormat = _currentCandles[_mainSymbol].Date.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                            OrderType = response.OrderType,
                            Volume = response.Volume
                        }));
                    //-----------------------------------------------------------------------
                }
                else
                {
                    // request --------------------------------------------------------------
                    var operationRequest = _tradeService.OperationRequest(OrderTypeEnum.None);

                    requester.SendFrame(JsonConvert.SerializeObject(operationRequest));
                    Debug.WriteLine($"RequestSocket-Send:{JsonConvert.SerializeObject(operationRequest)}");
                    //-----------------------------------------------------------------------

                    // response -------------------------------------------------------------
                    var operationResponse = requester.ReceiveFrameString();
                    Debug.WriteLine($"RequestSocket-Receive:{operationResponse}");
                    //-----------------------------------------------------------------------
                }
            }
            catch (OperationCanceledException)
            {
                // Stop pressed ...
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                Debug.WriteLine($"RequestSocket:{ex.Message}");
            }
        }

        public ICommand CleanMessageInputCommand => new DelegateCommand(() =>
        {
            DataInput.Clear();
        }, () => DataInput.Count > 0)
            .ObservesProperty(() => DataInput.Count);

        public ICommand CleanMessageOutputCommand => new DelegateCommand(() =>
        {
            DataOutput.Clear();
        }, () => DataOutput.Count > 0)
            .ObservesProperty(() => DataOutput.Count);

        public ICommand AddNodeToMetaTrader => new DelegateCommand<object>(item =>
        {
            if (item is NodeModel node)
            {
                if (Nodes.IndexOf(node) == -1)
                {
                    Nodes.Add(node);
                }
            }

            if (item is AssemblyNodeModel assemblyNode)
            {
                AssemblyNode = assemblyNode;
            }

            if (item is StrategyNodeModel strategyNode)
            {
                StrategyNode = strategyNode;
            }
        });

        public ICommand RemoveNodeFromMetaTrader => new DelegateCommand<object>(item =>
        {
            if (item is NodeModel node)
            {
                Nodes.Remove(node);
            }

            if (item is AssemblyNodeModel assemblyNode)
            {
                AssemblyNode = null;
            }

            if (item is StrategyNodeModel strategyNode)
            {
                StrategyNode = null;
            }
        });

        // View Bindings

        private bool _isTransactionActive;
        public bool IsTransactionActive
        {
            get => _isTransactionActive;
            set => SetProperty(ref _isTransactionActive, value);
        }

        private bool _canExecute = true;
        public bool CanExecute
        {
            get => _canExecute;
            set => SetProperty(ref _canExecute, value);
        }

        private ProjectConfigurationVM _projectConfiguration;
        public ProjectConfigurationVM ProjectConfiguration
        {
            get => _projectConfiguration;
            set => SetProperty(ref _projectConfiguration, value);
        }

        private bool _testStrategyNode;
        public bool TestStrategyNode
        {
            get => _testStrategyNode;
            set => SetProperty(ref _testStrategyNode, value);
        }

        private bool _testAssemblyNode;
        public bool TestAssemblyNode
        {
            get => _testAssemblyNode;
            set => SetProperty(ref _testAssemblyNode, value);
        }

        private bool _testNodes;
        public bool TestNodes
        {
            get => _testNodes;
            set => SetProperty(ref _testNodes, value);
        }

        private ObservableCollection<NodeModel> _nodes;
        public ObservableCollection<NodeModel> Nodes
        {
            get => _nodes;
            set => SetProperty(ref _nodes, value);
        }

        private AssemblyNodeModel _assembledNode;
        public AssemblyNodeModel AssemblyNode
        {
            get => _assembledNode;
            set => SetProperty(ref _assembledNode, value);
        }

        private StrategyNodeModel _strategyNode;
        public StrategyNodeModel StrategyNode
        {
            get => _strategyNode;
            set => SetProperty(ref _strategyNode, value);
        }

        public ObservableCollection<ZmqMsgModel> DataInput { get; set; }
        public ObservableCollection<ZmqMsgModel> DataOutput { get; set; }

        // IDisposable

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                _disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~MetaTraderViewModel()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}