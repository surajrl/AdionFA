using AdionFA.Infrastructure.Common.IofC;
using AdionFA.Infrastructure.Common.Extensions;
using AdionFA.Infrastructure.Common.Extractor.Attributes;
using AdionFA.Infrastructure.Common.Extractor.Contracts;
using AdionFA.Infrastructure.Common.Helpers;
using AdionFA.Infrastructure.Common.MetaTrader.Model;
using AdionFA.Infrastructure.Common.MetaTrader.Contracts;
using AdionFA.Infrastructure.Common.Extractor.Model;
using AdionFA.Infrastructure.Enums;
using AdionFA.Infrastructure.Enums.Model;

using AdionFA.UI.Station.Project.Commands;
using AdionFA.UI.Station.Project.Enums;
using AdionFA.UI.Station.Project.EventAggregator;
using AdionFA.UI.Station.Project.Features;
using AdionFA.UI.Station.Project.Model.MetaTrader;
using AdionFA.UI.Station.Project.AutoMapper;
using AdionFA.UI.Station.Infrastructure;
using AdionFA.UI.Station.Infrastructure.Contracts.AppServices;
using AdionFA.UI.Station.Infrastructure.Model.Base;
using AdionFA.UI.Station.Infrastructure.Model.Project;

using NetMQ;
using NetMQ.Sockets;
using AutoMapper;
using Newtonsoft.Json;
using DynamicData;

using Prism.Commands;
using Prism.Events;
using Prism.Ioc;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using AdionFA.Infrastructure.I18n.Resources;
using AdionFA.UI.Station.Infrastructure.Helpers;
using AdionFA.UI.Station.Infrastructure.Model.Weka;
using AdionFA.Infrastructure.Common.Weka.Model;
using AdionFA.UI.Station.Project.Validators.MetaTrader;
using AdionFA.UI.Station.Infrastructure.Model.MetaTrader;
using System.Windows.Data;

namespace AdionFA.UI.Station.Project.ViewModels
{
    public class MetaTraderViewModel : MenuItemViewModel
    {
        private readonly IMapper _mapper;

        private readonly IExtractorService _extractorService;
        private readonly ITradeService _tradeService;

        private readonly IProjectServiceAgent _projectService;
        private readonly IServiceAgent _serviceAgent;

        private readonly IEventAggregator _eventAggregator;

        private ProjectVM _project;
        private CancellationTokenSource _cancellationTokenSrc;
        private bool _firstCompleteCandleReceived;
        private Candle _currentCandle;

        public MetaTraderViewModel(MainViewModel mainViewModel) : base(mainViewModel)
        {
            _extractorService = IoC.Get<IExtractorService>();
            _tradeService = IoC.Get<ITradeService>();

            _serviceAgent = ContainerLocator.Current.Resolve<IServiceAgent>();
            _projectService = ContainerLocator.Current.Resolve<IProjectServiceAgent>();

            _eventAggregator = ContainerLocator.Current.Resolve<IEventAggregator>();
            _eventAggregator.GetEvent<AppProjectCanExecuteEventAggregator>().Subscribe(p => CanExecute = p);

            ContainerLocator.Current.Resolve<IAppProjectCommands>().SelectItemHamburgerMenuCommand.RegisterCommand(SelectItemHamburgerMenuCommand);
            ContainerLocator.Current.Resolve<IApplicationCommands>().NodeTestInMetatraderCommand.RegisterCommand(AddNodeForTestCommand);

            _mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingAppProjectProfile());
            }).CreateMapper();

            PopulateViewModel();
        }

        public ICommand SelectItemHamburgerMenuCommand => new DelegateCommand<string>(item =>
        {
            try
            {
                if (item == HamburgerMenuItems.MetaTrader.Replace(" ", string.Empty))
                {
                    PopulateViewModel();
                }
            }
            catch (Exception ex)
            {
                IsTransactionActive = false;

                Trace.TraceError(ex.Message);

                throw;
            }
        }, (s) => true);

        public DelegateCommand StopProcessBtnCommand => new DelegateCommand(() =>
            _cancellationTokenSrc.Cancel(),
            () => IsTransactionActive).ObservesProperty(() => IsTransactionActive);

        public DelegateCommand ProcessBtnCommand => new DelegateCommand(async () =>
        {
            try
            {
                IsTransactionActive = true;
                _eventAggregator.GetEvent<AppProjectCanExecuteEventAggregator>().Publish(false);

                _cancellationTokenSrc = new CancellationTokenSource();

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
                            Date = nextItem.Date.AddSeconds((long)TimeSpan.Parse(nextItem.Time).TotalSeconds),
                            Time = (long)TimeSpan.Parse(nextItem.Time).TotalSeconds,
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
                _cancellationTokenSrc.Dispose();
                IsTransactionActive = false;
                _eventAggregator.GetEvent<AppProjectCanExecuteEventAggregator>().Publish(true);
            }
        }, () => !IsTransactionActive).ObservesProperty(() => IsTransactionActive);

        private async Task SubSocket(IProgress<ZmqMsgModel> progress)
        {
            await Task.Factory.StartNew(() =>
            {
                using var subscriber = new SubscriberSocket($">tcp://{ExpertAdvisor.Host}:{ExpertAdvisor.PushPort}");
                subscriber.Subscribe("");

                try
                {
                    _cancellationTokenSrc ??= new CancellationTokenSource();

                    while (true)
                    {
                        _cancellationTokenSrc.Token.ThrowIfCancellationRequested();

                        var ts = TimeSpan.FromMilliseconds(1000);

                        if (subscriber.TryReceiveFrameString(ts, out var message) && !string.IsNullOrWhiteSpace(message))
                        {
                            var zmqModel = JsonConvert.DeserializeObject<ZmqMsgModel>(message);
                            Debug.WriteLine("SubscriberSocket-Receive:" + message);

                            zmqModel.Id = MessagesFromCurrentPeriod;
                            zmqModel.TemporalityName = EnumUtil.GetTimeframeEnum(zmqModel.Temporality).GetMetadata().Name;
                            zmqModel.DateFormat = zmqModel.Date.AddSeconds(TimeSpan.Parse(zmqModel.Time).TotalSeconds).ToString("dd/MM/yyyy HH:mm:ss");
                            zmqModel.PutType = (int)MessageZMQPutTypeEnum.Input;
                            zmqModel.PutTypeName = MessageZMQPutTypeEnum.Input.GetMetadata().Name;

                            progress.Report(zmqModel);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Trace.TraceError(ex.Message);
                    Debug.WriteLine($"SubscriberSocket:{ex.Message}");

                    _cancellationTokenSrc.Cancel();
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
                    _cancellationTokenSrc ??= new CancellationTokenSource();

                    _cancellationTokenSrc.Token.ThrowIfCancellationRequested();

                    if (Nodes.Any())
                    {
                        // Perform algorithm --------------------------------------------------
                        var messageInputCopy = MessageInput.ToList(); // Copy in case it gets modified

                        var lastMessageInput = messageInputCopy.FirstOrDefault();
                        var candles = messageInputCopy.Select(m => new Candle
                        {
                            Date = m.Date.AddSeconds((long)TimeSpan.Parse(m.Time).TotalSeconds),
                            Time = (long)TimeSpan.Parse(m.Time).TotalSeconds,
                            Open = (double)m.Open,
                            High = (double)m.High,
                            Low = (double)m.Low,
                            Close = (double)m.Close
                        })
                        .OrderBy(m => m.Date).ToList();

                        var isTrade = _tradeService.IsTrade(
                                _mapper.Map<REPTreeNodeVM, REPTreeNodeModel>(Nodes.FirstOrDefault()),
                                candles,
                                _currentCandle);
                        //---------------------------------------------------------------------

                        if (isTrade)
                        {
                            // Open operation request -----------------------------------------------
                            if (Nodes.First().Label.ToLower() == "up")
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
                                DateFormat = _currentCandle.Date.ToString("dd/MM/yyyy HH:mm:ss"),
                                PutType = (int)MessageZMQPutTypeEnum.Output,
                                PutTypeName = MessageZMQPutTypeEnum.Output.GetMetadata().Name,
                                PositionType = (int)response.OrderType,
                                PositionTypeName = response.OrderType.GetMetadata().Name,
                                Volume = decimal.Parse(response.Volume)
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

                    _cancellationTokenSrc.Cancel();
                }
            });
        }

        public DelegateCommand CleanMessageInputCommand => new DelegateCommand(() =>
        {
            MessageInput?.Clear();
            MessagesFromCurrentPeriod = 0;
        }).ObservesCanExecute(() => MessageInputAny);

        public DelegateCommand CleanMessageOutputCommand => new DelegateCommand(() =>
        {
            MessageOutput?.Clear();
        }).ObservesCanExecute(() => MessageOutputAny);

        private ICommand AddNodeForTestCommand => new DelegateCommand<REPTreeNodeVM>(node =>
        {
            try
            {
                Nodes ??= new ObservableCollection<REPTreeNodeVM>();

                foreach (var n in Nodes)
                {
                    if (n.Node == node.Node)
                    {
                        Nodes.Remove(node);
                        return;
                    }
                }

                Nodes.Add(node);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
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
                // Find the configuration of the project
                _project = await _projectService.GetProjectAsync(ProcessArgs.ProjectId, true);
                Configuration = _project?.ProjectConfigurations.FirstOrDefault();

                // Find the EA of the project
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

                if (Nodes == null)
                {
                    Nodes = new ObservableCollection<REPTreeNodeVM>();
                    Nodes.CollectionChanged += (object sender, NotifyCollectionChangedEventArgs e) =>
                    {
                        NodesAny = Nodes.Count > 0;
                    };
                }

                if (MessageInput == null)
                {
                    MessageInput = new ObservableCollection<ZmqMsgModel>();
                    MessageInput.CollectionChanged += (object sender, NotifyCollectionChangedEventArgs e) =>
                    {
                        MessagesFromCurrentPeriod++;
                        MessageInputAny = MessageInput.Count > 0;
                    };
                }

                if (MessageOutput == null)
                {
                    MessageOutput = new ObservableCollection<ZmqMsgModel>();
                    MessageOutput.CollectionChanged += (object sender, NotifyCollectionChangedEventArgs e) =>
                    {
                        MessageOutputAny = MessageOutput.Count > 0;
                    };
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

        private ConfigurationBaseVM _configuration;

        public ConfigurationBaseVM Configuration
        {
            get => _configuration;
            set => SetProperty(ref _configuration, value);
        }

        private int _maximumMessagesRequired;

        public int MaximumMessagesRequired
        {
            get => _maximumMessagesRequired;
            set => SetProperty(ref _maximumMessagesRequired, value);
        }

        private int _messagesFromCurrentPeriod;

        public int MessagesFromCurrentPeriod
        {
            get => _messagesFromCurrentPeriod;
            set => SetProperty(ref _messagesFromCurrentPeriod, value);
        }

        private bool _nodesAny;

        public bool NodesAny
        {
            get => _nodesAny;
            set => SetProperty(ref _nodesAny, value);
        }

        private ObservableCollection<REPTreeNodeVM> _nodes;

        public ObservableCollection<REPTreeNodeVM> Nodes
        {
            get => _nodes;
            set => SetProperty(ref _nodes, value);
        }

        private bool _messageInputAny;

        public bool MessageInputAny
        {
            get => _messageInputAny;
            set => SetProperty(ref _messageInputAny, value);
        }

        private ObservableCollection<ZmqMsgModel> _messageInput;

        public ObservableCollection<ZmqMsgModel> MessageInput
        {
            get => _messageInput;
            set => SetProperty(ref _messageInput, value);
        }

        private bool _messageOutputAny;

        public bool MessageOutputAny
        {
            get => _messageOutputAny;
            set => SetProperty(ref _messageOutputAny, value);
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
    }
}