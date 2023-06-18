using AdionFA.Infrastructure.Common.Extractor.Model;
using AdionFA.Infrastructure.Common.Helpers;
using AdionFA.Infrastructure.Common.IofC;
using AdionFA.Infrastructure.Common.MetaTrader.Contracts;
using AdionFA.Infrastructure.Common.MetaTrader.Model;
using AdionFA.Infrastructure.Common.Weka.Model;
using AdionFA.Infrastructure.Enums;
using AdionFA.Infrastructure.Enums.Model;
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
using DynamicData;
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

        public MetaTraderViewModel(MainViewModel mainViewModel) : base(mainViewModel)
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

            PopulateViewModel();
        }

        public ICommand SelectItemHamburgerMenuCommand => new DelegateCommand<string>(item =>
        {
            if (item == HamburgerMenuItems.MetaTrader.Replace(" ", string.Empty))
            {
                PopulateViewModel();
            }
        });

        public DelegateCommand StopProcessBtnCommand => new DelegateCommand(() =>
        {
            _cancellationTokenSource.Cancel();
        }, () => IsTransactionActive).ObservesProperty(() => IsTransactionActive);

        public DelegateCommand ProcessBtnCommand => new DelegateCommand(async () =>
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

                await SubSocket(subscriberSocketProgress);
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
        }, () => !IsTransactionActive).ObservesProperty(() => IsTransactionActive);

        private async Task SubSocket(IProgress<ZmqMsgModel> progress)
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
                            var zmqModel = JsonConvert.DeserializeObject<ZmqMsgModel>(message);
                            Debug.WriteLine("SubscriberSocket-Receive:" + message);

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

                    if (Node != null || AssembledNode != null)
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

                        if (Node != null)
                        {
                            isTrade = _tradeService.IsTrade(
                                Node,
                                candles,
                                _currentCandle);

                            label = Node.Label;
                        }
                        else if (AssembledNode != null)
                        {
                            isTrade = _tradeService.IsTrade(
                                AssembledNode,
                                candles,
                                _currentCandle);

                            label = AssembledNode.ParentNode.Label;
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

        public ICommand CleanMessageInputCommand => new DelegateCommand(() =>
        {
            MessageInput.Clear();
        }, () => MessageInput.Count > 0).ObservesProperty(() => MessageInput);

        public ICommand CleanMessageOutputCommand => new DelegateCommand(() =>
        {
            MessageOutput.Clear();
        }, () => MessageOutput.Count > 0).ObservesProperty(() => MessageOutput);

        public ICommand AddNodeToMetaTrader => new DelegateCommand<object>(node =>
        {
            if (node is REPTreeNodeModel singleNode)
            {
                Node = singleNode;
            }

            if (node is AssembledNodeModel assembledNode)
            {
                AssembledNode = assembledNode;
            }
        });

        public ICommand RemoveNodeFromMetaTrader => new DelegateCommand<object>(node =>
        {
            if (node is REPTreeNodeModel singleNode)
            {
                Node = null;
            }

            if (node is AssembledNodeModel assembledNode)
            {
                AssembledNode = null;
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

        private async void PopulateViewModel()
        {
            try
            {
                _project = await _projectService.GetProjectAsync(ProcessArgs.ProjectId, true);
                Configuration = _project?.ProjectConfigurations.FirstOrDefault();

                ExpertAdvisor = await _serviceAgent.GetExpertAdvisor(_project.ProjectId);
                ExpertAdvisor ??= new();

                if (!Timeframes.Any())
                {
                    Timeframes.AddRange(EnumUtil.ToEnumerable<TimeframeEnum>()
                        .Where(c => c.Id != (int)TimeframeEnum.W1 && c.Id != (int)TimeframeEnum.MN1)
                        .Select(c => new Metadata
                        {
                            Id = c.Id,
                            Name = c.Name
                        }).ToList());

                    TimeframeId = Configuration?.TimeframeId ?? (int)TimeframeEnum.H1;
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);

                throw;
            }
        }

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

        private ProjectConfigurationVM _configuration;
        public ProjectConfigurationVM Configuration
        {
            get => _configuration;
            set => SetProperty(ref _configuration, value);
        }

        private REPTreeNodeModel _node;
        public REPTreeNodeModel Node
        {
            get => _node;
            set => SetProperty(ref _node, value);
        }

        private AssembledNodeModel _assembledNode;
        public AssembledNodeModel AssembledNode
        {
            get => _assembledNode;
            set => SetProperty(ref _assembledNode, value);
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

        private int? _timeframeId;
        public int? TimeframeId
        {
            get => _timeframeId;
            set => SetProperty(ref _timeframeId, value);
        }

        public ObservableCollection<Metadata> Timeframes { get; } = new ObservableCollection<Metadata>();

        private ExpertAdvisorVM _expertAdvisor;
        public ExpertAdvisorVM ExpertAdvisor
        {
            get => _expertAdvisor;
            set => SetProperty(ref _expertAdvisor, value);
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