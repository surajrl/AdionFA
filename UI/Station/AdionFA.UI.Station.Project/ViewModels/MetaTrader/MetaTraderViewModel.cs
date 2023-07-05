using AdionFA.Infrastructure.Common.Extractor.Model;
using AdionFA.Infrastructure.Common.IofC;
using AdionFA.Infrastructure.Common.MetaTrader.Contracts;
using AdionFA.Infrastructure.Common.MetaTrader.Model;
using AdionFA.Infrastructure.Common.Modules.Weka.Model;
using AdionFA.Infrastructure.Common.Weka.Model;
using AdionFA.Infrastructure.Enums;
using AdionFA.Infrastructure.I18n.Resources;
using AdionFA.UI.Station.Infrastructure;
using AdionFA.UI.Station.Infrastructure.Contracts.AppServices;
using AdionFA.UI.Station.Infrastructure.Helpers;
using AdionFA.UI.Station.Infrastructure.Model.MetaTrader;
using AdionFA.UI.Station.Infrastructure.Model.Project;
using AdionFA.UI.Station.Project.Commands;
using AdionFA.UI.Station.Project.EventAggregator;
using AdionFA.UI.Station.Project.Features;
using AdionFA.UI.Station.Project.Model.MetaTrader;
using AdionFA.UI.Station.Project.Validators.MetaTrader;
using NetMQ;
using NetMQ.Sockets;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AdionFA.UI.Station.Project.ViewModels
{
    public class MetaTraderViewModel : MenuItemViewModel, IDisposable
    {
        private readonly ITradeService _tradeService;

        private readonly IProjectServiceAgent _projectService;
        private readonly IServiceAgent _serviceAgent;
        private readonly IMarketDataServiceAgent _marketDataService;

        private readonly IEventAggregator _eventAggregator;

        private CancellationTokenSource _cancellationTokenSource;
        private ManualResetEventSlim _manualResetEventSlim;
        private Candle _currentCandle;
        private bool _disposedValue;
        private string _mainSymbol;

        public MetaTraderViewModel(MainViewModel mainViewModel)
            : base(mainViewModel)
        {
            _tradeService = IoC.Get<ITradeService>();

            _serviceAgent = ContainerLocator.Current.Resolve<IServiceAgent>();
            _projectService = ContainerLocator.Current.Resolve<IProjectServiceAgent>();
            _eventAggregator = ContainerLocator.Current.Resolve<IEventAggregator>();
            _marketDataService = ContainerLocator.Current.Resolve<IMarketDataServiceAgent>();

            ContainerLocator.Current.Resolve<IAppProjectCommands>().SelectItemHamburgerMenuCommand.RegisterCommand(SelectItemHamburgerMenuCommand);
            ContainerLocator.Current.Resolve<IApplicationCommands>().AddNodeToMetaTrader.RegisterCommand(AddNodeToMetaTrader);
            ContainerLocator.Current.Resolve<IApplicationCommands>().RemoveNodeFromMetaTrader.RegisterCommand(RemoveNodeFromMetaTrader);

            _eventAggregator.GetEvent<AppProjectCanExecuteEvent>().Subscribe(p => CanExecute = p);

            MessageInput = new();
            MessageOutput = new();

            _manualResetEventSlim = new(false);
        }

        public ICommand SelectItemHamburgerMenuCommand => new DelegateCommand<string>(async item =>
        {
            if (item == HamburgerMenuItems.MetaTrader.Replace(" ", string.Empty))
            {
                try
                {
                    ProjectConfiguration = await _projectService.GetProjectConfigurationAsync(ProcessArgs.ProjectId).ConfigureAwait(true);
                    var s = await _marketDataService.GetSymbolAsync(ProjectConfiguration.SymbolId).ConfigureAwait(true);
                    _mainSymbol = s.Value;

                    ExpertAdvisor = await _serviceAgent.GetExpertAdvisor(ProcessArgs.ProjectId);
                    ExpertAdvisor ??= new();
                }
                catch (Exception ex)
                {
                    Trace.TraceError(ex.Message);

                    throw;
                }
            }
        });

        public ICommand StopCommand => new DelegateCommand(() =>
        {
            _cancellationTokenSource.Cancel();
        }, () => IsTransactionActive).ObservesProperty(() => IsTransactionActive);

        public ICommand ProcessCommand => new DelegateCommand(async () =>
        {
            try
            {
                IsTransactionActive = true;
                _eventAggregator.GetEvent<AppProjectCanExecuteEvent>().Publish(false);

                _cancellationTokenSource = new();

                Task.Factory.StartNew(() => Subscriber());
                await Task.Factory.StartNew(() => Requester()).ConfigureAwait(true);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                Debug.WriteLine($"Process-{ex.Message}");
            }
            finally
            {
                _cancellationTokenSource?.Dispose();
                _cancellationTokenSource = null;

                IsTransactionActive = false;
                _eventAggregator.GetEvent<AppProjectCanExecuteEvent>().Publish(true);
            }
        }, () => !IsTransactionActive && (TestNodes || TestAssemblyNode || TestStrategyNode))
            .ObservesProperty(() => IsTransactionActive)
            .ObservesProperty(() => TestNodes)
            .ObservesProperty(() => TestAssemblyNode)
            .ObservesProperty(() => TestStrategyNode);

        private void Subscriber()
        {
            using var subscriber = new SubscriberSocket($">tcp://{ExpertAdvisor.Host}:{ExpertAdvisor.PublisherPort}");

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

                        zmqModel.Id = MessageInput.Count;
                        zmqModel.DateFormat = zmqModel.Date.AddSeconds(TimeSpan.Parse(zmqModel.Time, CultureInfo.InvariantCulture).TotalSeconds).ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);

                        if (zmqModel.IsCurrentCandle)
                        {
                            _currentCandle = new Candle
                            {
                                Date = zmqModel.Date.AddSeconds((long)TimeSpan.Parse(zmqModel.Time, CultureInfo.InvariantCulture).TotalSeconds),
                                Time = (long)TimeSpan.Parse(zmqModel.Time, CultureInfo.InvariantCulture).TotalSeconds,
                                Open = (double)zmqModel.Open,
                                High = (double)zmqModel.Open,
                                Low = (double)zmqModel.Open,
                                Close = (double)zmqModel.Open,
                            };

                            // Signal requester to check for trade
                            _manualResetEventSlim.Set();
                        }
                        else
                        {
                            Application.Current.Dispatcher.Invoke(() => MessageInput.Insert(0, zmqModel));
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

        private void Requester()
        {
            using var requester = new RequestSocket($">tcp://{ExpertAdvisor.Host}:{ExpertAdvisor.ResponsePort}");

            try
            {
                while (true)
                {
                    _manualResetEventSlim.Reset();   // Block

                    _manualResetEventSlim.Wait(_cancellationTokenSource.Token);   // Wait until signal received from subscriber (i.e. candle received)
                    _cancellationTokenSource.Token.ThrowIfCancellationRequested();

                    // Perform algorithm --------------------------------------------------
                    var candles = MessageInput.Select(message => new Candle
                    {
                        Date = message.Date.AddSeconds((long)TimeSpan.Parse(message.Time, CultureInfo.InvariantCulture).TotalSeconds),
                        Time = (long)TimeSpan.Parse(message.Time, CultureInfo.InvariantCulture).TotalSeconds,
                        Open = (double)message.Open,
                        High = (double)message.High,
                        Low = (double)message.Low,
                        Close = (double)message.Close
                    })
                    .OrderBy(m => m.Date)
                    .ToList();

                    candles.Add(_currentCandle);

                    var isTrade = false;
                    var label = string.Empty;

                    if (TestNodes)
                    {
                        foreach (var node in Nodes)
                        {
                            isTrade = _tradeService.IsTrade(
                                node.NodeData.Node,
                                candles,
                                _currentCandle);

                            if (isTrade)
                            {
                                break;
                            }
                        }

                        label = Nodes[0].NodeData.Label;
                    }
                    else if (TestAssemblyNode)
                    {
                        // Test for parent node
                        isTrade = _tradeService.IsTrade(
                            AssemblyNode.ParentNodeData.Node,
                            candles,
                            _currentCandle);

                        if (!isTrade)
                        {
                            continue;
                        }

                        // Test for one child node
                        foreach (var childNode in AssemblyNode.ChildNodesData)
                        {
                            isTrade = _tradeService.IsTrade(
                                childNode.Node,
                                candles,
                                _currentCandle);

                            if (isTrade)
                            {
                                break;
                            }
                        }

                        label = AssemblyNode.ParentNodeData.Label;
                    }
                    else if (TestStrategyNode)
                    {
                        // Test for parent node
                        isTrade = _tradeService.IsTrade(
                            StrategyNode.ParentNodeData.Node,
                            candles,
                            _currentCandle);

                        if (!isTrade)
                        {
                            continue;
                        }

                        // Test for every crossing node
                        foreach (var crossingNode in StrategyNode.CrossingNodesData)
                        {
                            isTrade = _tradeService.IsTrade(
                                crossingNode.Item1.Node,
                                /* crossing candles */ null,
                                /* current crossing candle*/ null);

                            if (!isTrade)
                            {
                                break;
                            }
                        }

                        // Test for one child node
                        foreach (var childNode in StrategyNode.ChildNodesData)
                        {
                            isTrade = _tradeService.IsTrade(
                                childNode.Node,
                                candles,
                                _currentCandle);

                            if (isTrade)
                            {
                                break;
                            }
                        }

                        label = StrategyNode.ParentNodeData.Label;
                    }

                    if (isTrade)
                    {
                        // Open operation request -----------------------------------------------
                        var operationRequest = label.ToLowerInvariant() == "up"
                        ? _tradeService.OpenOperation(_mainSymbol, OrderTypeEnum.Buy)
                        : _tradeService.OpenOperation(_mainSymbol, OrderTypeEnum.Sell);

                        requester.SendFrame(JsonConvert.SerializeObject(operationRequest));
                        Debug.WriteLine($"RequestSocket-Send:{operationRequest}");
                        //-----------------------------------------------------------------------

                        // Open operation response ----------------------------------------------
                        var openOperationRep = requester.ReceiveFrameString();
                        Debug.WriteLine($"RequestSocket-Receive:{openOperationRep}");

                        var response = JsonConvert.DeserializeObject<ZmqMsgRequestModel>(openOperationRep);

                        Application.Current.Dispatcher.Invoke(() => MessageOutput.Insert(0,
                            new ZmqMsgModel
                            {
                                Id = MessageOutput.Count + 1,
                                Date = _currentCandle.Date,
                                DateFormat = _currentCandle.Date.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                                Volume = decimal.Parse(response.Volume, CultureInfo.InvariantCulture)
                            }));
                        //-----------------------------------------------------------------------
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
                Debug.WriteLine($"RequestSocket:{ex.Message}");
            }
        }

        public ICommand CleanMessageInputCommand => new DelegateCommand(MessageInput.Clear);

        public ICommand CleanMessageOutputCommand => new DelegateCommand(MessageOutput.Clear);

        public ICommand AddNodeToMetaTrader => new DelegateCommand<object>(item =>
        {
            if (item is NodeModel singleNode)
            {
                if (Nodes.IndexOf(singleNode) == -1)
                {
                    Nodes.Add(singleNode);
                }
            }

            if (item is AssemblyNodeModel assembledNode)
            {
                AssemblyNode = assembledNode;
            }

            if (item is StrategyNodeModel strategyNode)
            {
                StrategyNode = strategyNode;
            }
        });

        public ICommand RemoveNodeFromMetaTrader => new DelegateCommand<object>(item =>
        {
            if (item is NodeModel singleNode)
            {
                Nodes.Remove(singleNode);
            }

            if (item is AssemblyNodeModel assembledNode)
            {
                AssemblyNode = null;
            }

            if (item is StrategyNodeModel strategyNode)
            {
                StrategyNode = null;
            }
        });

        public ICommand SaveEACommand => new DelegateCommand(async () =>
        {
            try
            {
                var validator = Validate(new ExpertAdvisorValidator());
                if (!validator.IsValid)
                {
                    IsTransactionActive = false;
                    MessageHelper.ShowMessages(this,
                        "MetaTrader - Expert Advisor",
                        validator.Errors.Select(msg => msg.ErrorMessage).ToArray());

                    return;
                }

                ExpertAdvisor.ProjectId = ProcessArgs.ProjectId;
                ExpertAdvisor.Name = $"{ProcessArgs.ProjectName}.EA.{ExpertAdvisor.MagicNumber}";

                var response = await _serviceAgent.UpdateExpertAdvisor(ExpertAdvisor);
                MessageHelper.ShowMessage(this,
                    "MetaTrader - Expert Advisor",
                    response.IsSuccess
                    ? MessageResources.EntitySaveSuccess
                    : MessageResources.EntityErrorTransaction);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);

                throw;
            }
        });

        // Bindable Model

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

        private ExpertAdvisorVM _expertAdvisor;
        public ExpertAdvisorVM ExpertAdvisor
        {
            get => _expertAdvisor;
            set => SetProperty(ref _expertAdvisor, value);
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

        private ObservableCollection<ZmqMsgModel> _messageInput;
        public ObservableCollection<ZmqMsgModel> MessageInput
        {
            get => _messageInput;
            set => SetProperty(ref _messageInput, value);
        }

        private ObservableCollection<ZmqMsgModel> _messageOutput;
        public ObservableCollection<ZmqMsgModel> MessageOutput
        {
            get => _messageOutput;
            set => SetProperty(ref _messageOutput, value);
        }

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