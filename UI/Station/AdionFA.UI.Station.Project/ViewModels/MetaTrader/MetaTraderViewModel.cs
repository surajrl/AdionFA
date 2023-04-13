using AdionFA.Infrastructure.Common.IofC;
using AdionFA.Infrastructure.Common.Extensions;
using AdionFA.Infrastructure.Common.Extractor.Attributes;
using AdionFA.Infrastructure.Common.Extractor.Contracts;
using AdionFA.Infrastructure.Common.Helpers;
using AdionFA.Infrastructure.Common.Infrastructures.MetaTrader.Model;
using AdionFA.Infrastructure.Common.Infrastructures.MetaTrader.Contracts;
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
using AdionFA.UI.Station.Infrastructure.Model.MetaTrader;
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
using AdionFA.UI.Station.Project.Model.StrategyBuilder;

namespace AdionFA.UI.Station.Project.ViewModels
{
    public class MetaTraderViewModel : MenuItemViewModel
    {
        private readonly IMapper _mapper;

        private readonly IExtractorService _extractorService;
        private readonly ITradeService _tradeService;

        private readonly IProjectServiceAgent _projectService;
        private readonly IMarketDataServiceAgent _historicalDataService;
        private readonly IServiceAgent _serviceAgent;

        private readonly IEventAggregator _eventAggregator;

        private ProjectVM _project;
        private CancellationTokenSource _cancellationTokenSrc;

        public MetaTraderViewModel(MainViewModel mainViewModel) : base(mainViewModel)
        {
            _extractorService = IoC.Get<IExtractorService>();
            _tradeService = IoC.Get<ITradeService>();

            _serviceAgent = ContainerLocator.Current.Resolve<IServiceAgent>();
            _projectService = ContainerLocator.Current.Resolve<IProjectServiceAgent>();
            _historicalDataService = ContainerLocator.Current.Resolve<IMarketDataServiceAgent>();

            _eventAggregator = ContainerLocator.Current.Resolve<IEventAggregator>();
            _eventAggregator.GetEvent<AppProjectCanExecuteEventAggregator>().Subscribe(p => CanExecute = p);
            ContainerLocator.Current.Resolve<IAppProjectCommands>().SelectItemHamburgerMenuCommand.RegisterCommand(SelectItemHamburgerMenuCommand);

            AddNodeForTestCommand = new DelegateCommand<BacktestModelVM>(AddNode);
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
        {
            _cancellationTokenSrc.Cancel();
        }, () => IsTransactionActive).ObservesProperty(() => IsTransactionActive);

        public DelegateCommand ProcessBtnCommand => new DelegateCommand(async () =>
        {
            try
            {
                IsTransactionActive = true;
                _eventAggregator.GetEvent<AppProjectCanExecuteEventAggregator>().Publish(false);

                _cancellationTokenSrc = new CancellationTokenSource();
                CancellationToken token = _cancellationTokenSrc.Token;

                var requestSocketProgress = new Progress<ZmqMsgModel>();
                requestSocketProgress.ProgressChanged += (senderOfProgressChanged, nextItem) =>
                {
                    MessageOutput.Insert(0, nextItem);
                };

                var pullSocketProgress = new Progress<ZmqMsgModel>();
                pullSocketProgress.ProgressChanged += async (senderOfProgressChanged, nextItem) =>
                {
                    MessageInput.Insert(0, nextItem);
                    await RequestSocketAsync(requestSocketProgress, _cancellationTokenSrc);
                };

                await PullSocketAsync(pullSocketProgress, _cancellationTokenSrc);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
            }
            finally
            {
                _cancellationTokenSrc.Dispose();
                IsTransactionActive = false;
                _eventAggregator.GetEvent<AppProjectCanExecuteEventAggregator>().Publish(true);
            }
        }, () => !IsTransactionActive).ObservesProperty(() => IsTransactionActive);

        private async Task PullSocketAsync(IProgress<ZmqMsgModel> progress, CancellationTokenSource ctsTask)
        {
            await Task.Factory.StartNew(() =>
            {
                using var subscriber = new SubscriberSocket($">tcp://{ExpertAdvisor.Host}:{ExpertAdvisor.PushPort}");
                subscriber.Subscribe("");

                try
                {
                    _cancellationTokenSrc ??= new CancellationTokenSource();
                    CancellationToken token = _cancellationTokenSrc.Token;

                    while (true)
                    {
                        if (token.IsCancellationRequested)
                            token.ThrowIfCancellationRequested();

                        var ts = TimeSpan.FromMilliseconds(1000);

                        if (subscriber.TryReceiveFrameString(ts, out var message) && !string.IsNullOrWhiteSpace(message))
                        {
                            var zmqModel = JsonConvert.DeserializeObject<ZmqMsgModel>(message);
                            Debug.WriteLine("SubscriberSocket-Receive:" + message);

                            zmqModel.Id = CountMessagesCurrentPeriod() + 1;
                            zmqModel.TemporalityName = EnumUtil.GetTimeframeEnum(zmqModel.Temporality).GetMetadata().Name;
                            zmqModel.DateFormat = zmqModel.Date.AddSeconds(TimeSpan.Parse(zmqModel.Time).TotalSeconds).ToString("dd/MM/yyyy HH:mm:ss");
                            zmqModel.PutType = (int)MessageZMQPutTypeEnum.Input;
                            zmqModel.PutTypeName = MessageZMQPutTypeEnum.Input.GetMetadata().Name;
                            zmqModel.Description = message;
                            zmqModel.IsRequired = true;

                            progress.Report(zmqModel);
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (ex is OperationCanceledException)
                        Console.WriteLine($"{nameof(OperationCanceledException)} thrown with message: {ex.Message}");
                    else
                        Console.WriteLine(ex.Message);
                    ctsTask.Cancel();
                }
            });
        }

        private async Task RequestSocketAsync(IProgress<ZmqMsgModel> progress, CancellationTokenSource ctsTask)
        {
            await Task.Factory.StartNew(() =>
            {
                using var requester = new RequestSocket($">tcp://{ExpertAdvisor.Host}:{ExpertAdvisor.ResponsePort}");

                try
                {
                    ctsTask ??= new CancellationTokenSource();
                    CancellationToken token = ctsTask.Token;

                    if (token.IsCancellationRequested)
                        token.ThrowIfCancellationRequested();

                    ZmqMsgModel lastMessageInput = MessageInput.FirstOrDefault();

                    if (Nodes.Any())
                    {
                        // Close operation request --------------------------------------------
                        requester.SendFrame(JsonConvert.SerializeObject(_tradeService.CloseAllOperation()));
                        Debug.WriteLine($"RequestSocket-Send:{JsonConvert.SerializeObject(_tradeService.CloseAllOperation())}");

                        // Close operation response -------------------------------------------
                        string closeAllRep = requester.ReceiveFrameString();
                        Debug.WriteLine($"RequestSocket-Receive:{closeAllRep}");
                        //---------------------------------------------------------------------

                        // Perform algorithm --------------------------------------------------
                        IEnumerable<Candle> candles = MessageInput.Select(m => new Candle
                        {
                            Date = m.Date.AddSeconds((long)TimeSpan.Parse(m.Time).TotalSeconds),
                            Time = (long)TimeSpan.Parse(m.Time).TotalSeconds,
                            Open = (double)m.Open,
                            High = (double)m.High,
                            Low = (double)m.Low,
                            Close = (double)m.Close,
                            Volume = (double)m.Volume,
                            Label = m.Label
                        }).OrderBy(m => m.Date);

                        var isTrade = _tradeService.IsTrade(
                            (TimeframeEnum)TimeframeId,
                            Nodes.FirstOrDefault().BacktestModel,
                            candles);
                        //---------------------------------------------------------------------

                        if (isTrade)
                        {
                            // Open operation request -----------------------------------------------
                            if (Nodes.First().BacktestModel.Label.ToLower() == "up")
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
                            string openOperationRep = requester.ReceiveFrameString();
                            Debug.WriteLine($"RequestSocket-Receive:{openOperationRep}");

                            ZmqMsgRequestModel response = JsonConvert.DeserializeObject<ZmqMsgRequestModel>(openOperationRep);

                            var model = new ZmqMsgModel
                            {
                                Id = MessageOutput.Count + 1,

                                Date = lastMessageInput.Date,
                                DateFormat = lastMessageInput.DateFormat,

                                PutType = (int)MessageZMQPutTypeEnum.Output,
                                PutTypeName = MessageZMQPutTypeEnum.Output.GetMetadata().Name,
                                PositionType = (int)response.OrderType,
                                PositionTypeName = response.OrderType.GetMetadata().Name,
                                Volume = decimal.Parse(response.Volume),
                                Description = response.Comment,
                            };

                            progress.Report(model);
                            //-----------------------------------------------------------------------
                        }
                    }
                }
                catch (Exception ex)
                {
                    Trace.TraceError(ex.Message);
                    ctsTask.Cancel();
                }
            });
        }

        public DelegateCommand CleanMessageInputCommand => new DelegateCommand(() =>
        {
            MessageInput?.Clear();
        }).ObservesCanExecute(() => MessageInputAny);

        public DelegateCommand CleanMessageOutputCommand => new DelegateCommand(() =>
        {
            MessageOutput?.Clear();
        }).ObservesCanExecute(() => MessageOutputAny);

        public ICommand TimeframeCommand => new DelegateCommand(() =>
        {
            foreach (var item in MessageInput.Where(m => m.IsRequired))
            {
                item.IsRequired = (int)EnumUtil.GetTimeframeEnum(item.Temporality) == TimeframeId;
            }

            MessagesFromCurrentPeriod = CountMessagesCurrentPeriod();
        });

        private ICommand AddNodeForTestCommand { get; set; }
        public void AddNode(BacktestModelVM node)
        {
            try
            {
                Nodes ??= new ObservableCollection<BacktestModelVM>();

                foreach (var n in Nodes)
                {
                    if (n.BacktestModel.Node == node.BacktestModel.Node)
                    {
                        Nodes.Remove(node);
                        MaximumMessagesRequired = MaxMessagesRequired();
                        return;
                    }
                }

                Nodes.Add(node);
                MaximumMessagesRequired = MaxMessagesRequired();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public ICommand SaveEACommand => new DelegateCommand(async () =>
        {
            try
            {
                // TODO: Validate all required boxes are filled

                ExpertAdvisor.ProjectId = _project.ProjectId;
                ExpertAdvisor.Name = $"{_project.ProjectName}.EA.{ExpertAdvisor.MagicNumber}";

                var response = await _serviceAgent.UpdateExpertAdvisor(ExpertAdvisor);
                MessageHelper.ShowMessage(this,
                    "Expert Advisor Save",
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
                _project = await _projectService.GetProject(ProcessArgs.ProjectId, true);
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
                    Nodes = new ObservableCollection<BacktestModelVM>();
                    Nodes.CollectionChanged += (object sender, NotifyCollectionChangedEventArgs e) =>
                    {
                        NodesAny = Nodes.Count > 0;
                    };
                    MaximumMessagesRequired = MaxMessagesRequired();
                }

                if (MessageInput == null)
                {
                    MessageInput = new ObservableCollection<ZmqMsgModel>();
                    MessageInput.CollectionChanged += (object sender, NotifyCollectionChangedEventArgs e) =>
                    {
                        MessagesFromCurrentPeriod = CountMessagesCurrentPeriod();
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

        private int MaxMessagesRequired()
        {
            if (Nodes.Count > 0)
            {
                var indicators = _extractorService.BuildIndicatorsFromNode(Nodes.SelectMany(_n => _n.BacktestModel.Node.Select(__n => __n)).ToList());

                List<int> valueProperties = indicators
                    .SelectMany(_i => _i.GetType().GetByAttributeProperties(typeof(IndicatorPeriodAttribute))
                    .Where(prop => prop.PropertyType.Name == typeof(int).Name)
                    .Select(prop => (int)prop.GetValue(_i))).OrderByDescending(pv => pv).ToList();

                return valueProperties.OrderByDescending(vp => vp)?.FirstOrDefault() ?? 0;
            }
            return 0;
        }

        private int CountMessagesCurrentPeriod()
        {
            return MessageInput.Where(_m => (int)EnumUtil.GetTimeframeEnum(_m.Temporality) == TimeframeId && _m.IsRequired).Count();
        }

        private void CleanMessageInput()
        {
            int max = MaxMessagesRequired();
            var queue = MessageInput.Where(_m => (int)EnumUtil.GetTimeframeEnum(_m.Temporality) == TimeframeId).ToList();

            MessageInput = queue.Count >= max ? new ObservableCollection<ZmqMsgModel>(queue.Take(max).ToList())
            : new ObservableCollection<ZmqMsgModel>(queue);

            MessagesFromCurrentPeriod = MessageInput.Count;
            MessageInputAny = MessagesFromCurrentPeriod > max;
            MessageInput.CollectionChanged += (object sender, NotifyCollectionChangedEventArgs e) =>
            {
                MessagesFromCurrentPeriod = CountMessagesCurrentPeriod();
                int max = MaxMessagesRequired();
                MessageInputAny = MessagesFromCurrentPeriod > max;

                if (MessageInput.Count > 0 && max > 0 && MessageInput.Count > MaximumMessagesRequired * 2)
                {
                    CleanMessageInput();
                }
            };
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

        private ObservableCollection<BacktestModelVM> _nodes;
        public ObservableCollection<BacktestModelVM> Nodes
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

        // Expert Advisor Configuration

        private ExpertAdvisorVM _expertAdvisor;
        public ExpertAdvisorVM ExpertAdvisor
        {
            get => _expertAdvisor;
            set => SetProperty(ref _expertAdvisor, value);
        }
    }
}
