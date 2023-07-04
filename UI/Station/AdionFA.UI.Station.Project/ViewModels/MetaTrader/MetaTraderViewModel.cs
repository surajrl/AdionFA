using AdionFA.Infrastructure.Common.Extractor.Model;
using AdionFA.Infrastructure.Common.Helpers;
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
using AdionFA.UI.Station.Project.Enums;
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
using System.Windows.Input;

namespace AdionFA.UI.Station.Project.ViewModels
{
    public class MetaTraderViewModel : MenuItemViewModel, IDisposable
    {
        private readonly ITradeService _tradeService;

        private readonly IProjectServiceAgent _projectService;
        private readonly IServiceAgent _serviceAgent;

        private readonly IEventAggregator _eventAggregator;

        private ProjectVM _project;
        private CancellationTokenSource _cancellationTokenSource;
        private bool _firstCompleteCandleReceived;
        private Candle _currentCandle;
        private bool _disposedValue;

        public MetaTraderViewModel(MainViewModel mainViewModel)
            : base(mainViewModel)
        {
            _tradeService = IoC.Get<ITradeService>();

            _serviceAgent = ContainerLocator.Current.Resolve<IServiceAgent>();
            _projectService = ContainerLocator.Current.Resolve<IProjectServiceAgent>();
            _eventAggregator = ContainerLocator.Current.Resolve<IEventAggregator>();

            ContainerLocator.Current.Resolve<IAppProjectCommands>().SelectItemHamburgerMenuCommand.RegisterCommand(SelectItemHamburgerMenuCommand);
            ContainerLocator.Current.Resolve<IApplicationCommands>().AddNodeToMetaTrader.RegisterCommand(AddNodeToMetaTrader);
            ContainerLocator.Current.Resolve<IApplicationCommands>().RemoveNodeFromMetaTrader.RegisterCommand(RemoveNodeFromMetaTrader);

            _eventAggregator.GetEvent<AppProjectCanExecuteEvent>().Subscribe(p => CanExecute = p);

            MessageInput = new();
            MessageOutput = new();
        }

        public ICommand SelectItemHamburgerMenuCommand => new DelegateCommand<string>(async item =>
        {
            if (item == HamburgerMenuItems.MetaTrader.Replace(" ", string.Empty))
            {
                try
                {
                    ProjectConfiguration = await _projectService.GetProjectConfigurationAsync(ProcessArgs.ProjectId).ConfigureAwait(true);

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

                _cancellationTokenSource = new CancellationTokenSource();

                _firstCompleteCandleReceived = false;

                var requestSocketProgress = new Progress<ZmqMsgModel>();
                requestSocketProgress.ProgressChanged += (senderOfProgressChanged, nextItem) =>
                {
                    MessageOutput.Insert(0, nextItem);
                };

                var subscriberSocketProgress = new Progress<ZmqMsgModel>();
                subscriberSocketProgress.ProgressChanged += async (senderOfProgressChanged, nextItem) =>
                {
                    if (!_firstCompleteCandleReceived && !nextItem.IsNewCandle)
                    {
                        _firstCompleteCandleReceived = true;

                        return;
                    }

                    if (nextItem.IsNewCandle)
                    {
                        // Current candle with only the open price
                        _currentCandle = new Candle
                        {
                            Date = nextItem.Date.AddSeconds((long)TimeSpan.Parse(nextItem.Time, CultureInfo.InvariantCulture).TotalSeconds),
                            Time = (long)TimeSpan.Parse(nextItem.Time, CultureInfo.InvariantCulture).TotalSeconds,
                            Open = (double)nextItem.Open,
                            High = (double)nextItem.Open,
                            Low = (double)nextItem.Open,
                            Close = (double)nextItem.Open,
                        };
                    }
                    else
                    {
                        // Complete candle
                        MessageInput.Insert(0, nextItem);
                        await RequestSocketAsync(requestSocketProgress);
                    }
                };

                await SubscriberSocket(subscriberSocketProgress);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                Debug.WriteLine($"Process-{ex.Message}");
            }
            finally
            {
                _cancellationTokenSource.Dispose();
                IsTransactionActive = false;
                _eventAggregator.GetEvent<AppProjectCanExecuteEvent>().Publish(true);
            }
        }, () => !IsTransactionActive && (TestNodes || TestAssemblyNode || TestStrategyNode))
            .ObservesProperty(() => IsTransactionActive)
            .ObservesProperty(() => TestNodes)
            .ObservesProperty(() => TestAssemblyNode)
            .ObservesProperty(() => TestStrategyNode);

        private async Task SubscriberSocket(IProgress<ZmqMsgModel> progress)
        {
            await Task.Factory.StartNew(() =>
            {
                using var subscriber = new SubscriberSocket($">tcp://{ExpertAdvisor.Host}:{ExpertAdvisor.PublisherPort}");
                subscriber.Subscribe("");

                try
                {
                    _cancellationTokenSource ??= new CancellationTokenSource();

                    while (true)
                    {
                        _cancellationTokenSource.Token.ThrowIfCancellationRequested();

                        var ts = TimeSpan.FromMilliseconds(1000);

                        if (subscriber.TryReceiveFrameString(ts, out var message) && !string.IsNullOrWhiteSpace(message))
                        {
                            // Candle data received
                            var zmqModel = JsonConvert.DeserializeObject<ZmqMsgModel>(message);
                            Debug.WriteLine($"SubscriberSocket-Receive:{message}");

                            zmqModel.Id = MessageInput.Count;
                            zmqModel.TemporalityName = EnumUtil.GetTimeframeEnum(zmqModel.Temporality).GetMetadata().Name;
                            zmqModel.DateFormat = zmqModel.Date.AddSeconds(TimeSpan.Parse(zmqModel.Time, CultureInfo.InvariantCulture).TotalSeconds).ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                            zmqModel.PutType = (int)MessageZMQPutType.Input;
                            zmqModel.PutTypeName = MessageZMQPutType.Input.GetMetadata().Name;

                            progress.Report(zmqModel);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Trace.TraceError(ex.Message);
                    Debug.WriteLine($"SubscriberSocket:{ex.Message}");

                    _cancellationTokenSource.Cancel();
                }
            });
        }

        private async Task RequestSocketAsync(IProgress<ZmqMsgModel> progress)
        {
            await Task.Factory.StartNew(() =>
            {
                using var requester = new RequestSocket($">tcp://{ExpertAdvisor.Host}:{ExpertAdvisor.ResponsePort}");

                try
                {
                    _cancellationTokenSource ??= new CancellationTokenSource();

                    _cancellationTokenSource.Token.ThrowIfCancellationRequested();

                    if (Nodes != null || AssemblyNode != null)
                    {
                        // Perform algorithm --------------------------------------------------
                        var messageInputCopy = MessageInput.ToList(); // Copy in case it gets modified

                        var lastMessageInput = messageInputCopy.FirstOrDefault();
                        var candles = messageInputCopy.Select(m => new Candle
                        {
                            Date = m.Date.AddSeconds((long)TimeSpan.Parse(m.Time, CultureInfo.InvariantCulture).TotalSeconds),
                            Time = (long)TimeSpan.Parse(m.Time, CultureInfo.InvariantCulture).TotalSeconds,
                            Open = (double)m.Open,
                            High = (double)m.High,
                            Low = (double)m.Low,
                            Close = (double)m.Close
                        })
                        .OrderBy(m => m.Date).ToList();

                        var isTrade = false;
                        var label = string.Empty;

                        if (TestNodes)
                        {
                            candles.Add(_currentCandle);

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
                            candles.Add(_currentCandle);

                            // Test for parent node
                            isTrade = _tradeService.IsTrade(
                                AssemblyNode.ParentNodeData.Node,
                                candles,
                                _currentCandle);

                            if (!isTrade)
                            {
                                return;
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
                                return;
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
                                    return;
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
                            if (label.ToLowerInvariant() == "up")
                            {
                                requester.SendFrame(JsonConvert.SerializeObject(_tradeService.OpenOperation(OrderTypeEnum.Buy)));
                                Debug.WriteLine($"RequestSocket-Send:{JsonConvert.SerializeObject(_tradeService.OpenOperation(OrderTypeEnum.Buy))}");
                            }
                            else
                            {
                                requester.SendFrame(JsonConvert.SerializeObject(_tradeService.OpenOperation(OrderTypeEnum.Sell)));
                                Debug.WriteLine($"RequestSocket-Send:{JsonConvert.SerializeObject(_tradeService.OpenOperation(OrderTypeEnum.Sell))}");
                            }
                            //-----------------------------------------------------------------------

                            // Open operation response ----------------------------------------------
                            var openOperationRep = requester.ReceiveFrameString();
                            Debug.WriteLine($"RequestSocket-Receive:{openOperationRep}");

                            var response = JsonConvert.DeserializeObject<ZmqMsgRequestModel>(openOperationRep);

                            var model = new ZmqMsgModel
                            {
                                Id = MessageOutput.Count + 1,
                                Date = _currentCandle.Date,
                                DateFormat = _currentCandle.Date.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                                PutType = (int)MessageZMQPutType.Output,
                                PutTypeName = MessageZMQPutType.Output.GetMetadata().Name,
                                PositionType = (int)response.OrderType,
                                PositionTypeName = response.OrderType.GetMetadata().Name,
                                Volume = decimal.Parse(response.Volume, CultureInfo.InvariantCulture)
                            };

                            progress.Report(model);
                            //-----------------------------------------------------------------------
                        }
                    }
                }
                catch (Exception ex)
                {
                    Trace.TraceError(ex.Message);
                    Debug.WriteLine($"RequestSocket:{ex.Message}");

                    _cancellationTokenSource.Cancel();
                }
            });
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

                ExpertAdvisor.ProjectId = _project.ProjectId;
                ExpertAdvisor.Name = $"{_project.ProjectName}.EA.{ExpertAdvisor.MagicNumber}";

                var response = await _serviceAgent.UpdateExpertAdvisor(ExpertAdvisor);
                MessageHelper.ShowMessage(this,
                    "MetaTrader - Expert Advisor",
                    response.IsSuccess ? MessageResources.EntitySaveSuccess : MessageResources.EntityErrorTransaction);
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

        private ProjectConfigurationVM __projectConfiguration;
        public ProjectConfigurationVM ProjectConfiguration
        {
            get => __projectConfiguration;
            set => SetProperty(ref __projectConfiguration, value);
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

        // IDisposable Implementation

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