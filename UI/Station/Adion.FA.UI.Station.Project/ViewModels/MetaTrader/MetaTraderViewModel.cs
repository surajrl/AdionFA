using Adion.FA.Infrastructure.Common.Directories.Contracts;
using Adion.FA.Infrastructure.Common.Extensions;
using Adion.FA.Infrastructure.Common.Extractor.Attributes;
using Adion.FA.Infrastructure.Common.Extractor.Contracts;
using Adion.FA.Infrastructure.Common.Helpers;
using Adion.FA.Infrastructure.Common.Templates.MetaTrader;
using Adion.FA.Infrastructure.Enums;
using Adion.FA.Infrastructure.Enums.Model;
using Adion.FA.UI.Station.Project.Commands;
using Adion.FA.UI.Station.Project.Enums;
using Adion.FA.UI.Station.Project.EventAggregator;
using Adion.FA.UI.Station.Project.Features;
using Adion.FA.UI.Station.Project.Model.MetaTrader;
using Adion.FA.UI.Station.Project.Model.StrategyBuilder;
using Adion.FA.UI.Station.Project.Services;
using Adion.FA.UI.Station.Infrastructure;
using Adion.FA.UI.Station.Infrastructure.Contracts.AppServices;
using Adion.FA.UI.Station.Infrastructure.Model.Base;
using Adion.FA.UI.Station.Infrastructure.Model.Project;
using Adion.FA.Infrastructure.Common.IofC;
using NetMQ;
using NetMQ.Sockets;
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
using System.Windows;
using System.Windows.Input;
using Adion.FA.Infrastructure.Common.Infrastructures.MetaTrader.Model;
using Adion.FA.Infrastructure.Common.Infrastructures.MetaTrader.Contracts;
using Adion.FA.Infrastructure.Common.Extractor.Model;

namespace Adion.FA.UI.Station.Project.ViewModels
{
    public class MetaTraderViewModel : MenuItemViewModel
    {
        #region Services

        private readonly IProjectDirectoryService ProjectDirectoryService;
        private readonly IExtractorService ExtractorService;
        private readonly ITradeService TradeService;

        private readonly IAppProjectService AppProjectService;
        private readonly IMarketDataServiceAgent MarketDataService;

        private readonly IEventAggregator eventAggregator;
        
        #endregion

        private ProjectVM Project;

        #region Ctor

        public MetaTraderViewModel(MainViewModel mainViewModel) : base(mainViewModel)
        {
            ProjectDirectoryService = IoC.Get<IProjectDirectoryService>();
            ExtractorService = IoC.Get<IExtractorService>();
            TradeService = IoC.Get<ITradeService>();

            AppProjectService = ContainerLocator.Current.Resolve<IAppProjectService>();
            MarketDataService = ContainerLocator.Current.Resolve<IMarketDataServiceAgent>();
            
            eventAggregator = ContainerLocator.Current.Resolve<IEventAggregator>();
            eventAggregator.GetEvent<AppProjectCanExecuteEventAggregator>().Subscribe(p => CanExecute = p);
            ContainerLocator.Current.Resolve<IAppProjectCommands>().SelectItemHamburgerMenuCommand.RegisterCommand(SelectItemHamburgerMenuCommand);

            AddNodeForTestCommand = new DelegateCommand<REPTreeNodeModelVM>(AddNode);
            ContainerLocator.Current.Resolve<IApplicationCommands>().NodeTestInMetatraderCommand.RegisterCommand(AddNodeForTestCommand);

            PopulateViewModel();
        }

        #endregion

        #region Commands

        #region SelectItemHamburgerMenuCommand
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
        }, (s) => true);//item => !IsTransactionActive).ObservesProperty(() => IsTransactionActive);
        #endregion

        #region ProcessBtnCommand

        private CancellationTokenSource ctsTask;
        public DelegateCommand StopProcessBtnCommand => new DelegateCommand(() => { ctsTask.Cancel(); }, () => true);
        public DelegateCommand ProcessBtnCommand => new DelegateCommand(async () => 
        {
            try
            {
                IsTransactionActive = true;
                eventAggregator.GetEvent<AppProjectCanExecuteEventAggregator>().Publish(false);

                ctsTask = new CancellationTokenSource();
                CancellationToken token = ctsTask.Token;

                var requestSocketProgress = new Progress<ZmqMsgModel>();
                requestSocketProgress.ProgressChanged += (senderOfProgressChanged, nextItem) =>
                {
                    MessageOutput.Insert(0, nextItem);
                };

                var pullSocketProgress = new Progress<ZmqMsgModel>();
                pullSocketProgress.ProgressChanged += async (senderOfProgressChanged, nextItem) =>
                {
                    MessageInput.Insert(0, nextItem);

                    await RequestSocket(requestSocketProgress, ctsTask);
                };

                await PullSocket(pullSocketProgress, ctsTask);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
            }
            finally
            {
                ctsTask.Dispose();
                IsTransactionActive = false;
                eventAggregator.GetEvent<AppProjectCanExecuteEventAggregator>().Publish(true);
            }

        }, () => !IsTransactionActive).ObservesProperty(() => IsTransactionActive);

        private async Task PullSocket(IProgress<ZmqMsgModel> progress, CancellationTokenSource ctsTask)
        {
            await Task.Factory.StartNew(() =>
            {
                using (var receiver = new PullSocket(">tcp://localhost:5556"))
                {
                    try
                    {
                        ctsTask ??= new CancellationTokenSource();
                        CancellationToken token = ctsTask.Token;

                        while (true)
                        {
                            if (token.IsCancellationRequested)
                                token.ThrowIfCancellationRequested();

                            //Start our clock now
                            var watch = Stopwatch.StartNew();

                            var ts = TimeSpan.FromMilliseconds(1000);
                            string workload;
                            if (receiver.TryReceiveFrameString(ts, out workload) && !string.IsNullOrWhiteSpace(workload))
                            {
                                watch.Stop();
                                ConsoleWriteLine(string.Format("Total elapsed time {0} msec", watch.ElapsedMilliseconds));

                                var model = Newtonsoft.Json.JsonConvert.DeserializeObject<ZmqMsgModel>(workload);
                                model.Id = CountMessagesCurrentPeriod() + 1;
                                model.TemporalityName = EnumUtil.GetPeriodEnum(model.Temporality).GetMetadata().Name;
                                model.DateFormat = model.Date.AddSeconds(TimeSpan.Parse(model.Time).TotalSeconds).ToString("dd/MM/yyyy HH:mm:ss");
                                model.PutType = (int)MessageZMQPutTypeEnum.Input;
                                model.PutTypeName = MessageZMQPutTypeEnum.Input.GetMetadata().Name;
                                model.Description = workload;
                                model.IsRequired = true;

                                model.ElapsedMilliseconds = watch.ElapsedMilliseconds;
                                model.ElapsedTimeFormated = TimeSpan.FromMilliseconds(model.ElapsedMilliseconds).ToString(@"hh\:mm\:ss");

                                progress.Report(model);
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
                }
            });
        }

        private async Task RequestSocket(IProgress<ZmqMsgModel> progress, CancellationTokenSource ctsTask)
        {
            await Task.Factory.StartNew(() =>
            {
                using (var requestSocket = new RequestSocket(">tcp://localhost:5555"))
                {
                    try
                    {
                        ctsTask ??= new CancellationTokenSource();
                        CancellationToken token = ctsTask.Token;

                        if (token.IsCancellationRequested)
                            token.ThrowIfCancellationRequested();

                        ZmqMsgModel lastMessageInput = MessagesFromCurrentPeriod > MaximumMessagesRequired ? MessageInput.FirstOrDefault() : null;

                        if (Nodes.Any())
                        {
                            #region CloseAll

                            var closeAllMsg = TradeService.CloseAllOperationMessage();
                            requestSocket.SendFrame(closeAllMsg);
                            string messageCloseAll = requestSocket.ReceiveFrameString();
                                
                            #endregion

                            IEnumerable<Candle> candles = MessageInput.Take(MaximumMessagesRequired + 1).Select(m => new Candle
                            {
                                date = m.Date.AddSeconds((long)TimeSpan.Parse(m.Time).TotalSeconds),
                                time = (long)TimeSpan.Parse(m.Time).TotalSeconds,
                                open = (double)m.Open,
                                max = (double)m.High,
                                min = (double)m.Low,
                                close = (double)m.Close,
                                volumen = (double)m.Volume,
                                label = m.Label
                            }).OrderBy(m => m.date);

                            //var isTrade = TradeService.IsTrade((CurrencyPeriodEnum)currencyPeriodId, Nodes, candles);
                            if (true)
                            {
                                requestSocket.SendFrame(TradeService.OpenOperationMessage());
                                string message = requestSocket.ReceiveFrameString();

                                try
                                {
                                    ZmqMsgRequestModel response = Newtonsoft.Json.JsonConvert.DeserializeObject<ZmqMsgRequestModel>(message);

                                    var model = new ZmqMsgModel
                                    {
                                        Id = MessageOutput.Count + 1,

                                        Date = lastMessageInput.Date,
                                        DateFormat = lastMessageInput.DateFormat,

                                        PutType = (int)MessageZMQPutTypeEnum.Output,
                                        PutTypeName = MessageZMQPutTypeEnum.Output.GetMetadata().Name,
                                        PositionType = response.OrderType == "BUY" ? (int)MessageZMQPositionTypeEnum.Buy : (int)MessageZMQPositionTypeEnum.Sell,
                                        PositionTypeName = response.OrderType == "BUY" ? MessageZMQPositionTypeEnum.Buy.GetMetadata().Name : MessageZMQPositionTypeEnum.Sell.GetMetadata().Name,
                                        Volume = (decimal)0.5,
                                        Description = "Open Position",
                                    };

                                    progress.Report(model);
                                }
                                catch (Exception ex)
                                {
                                    Trace.TraceError(ex.Message);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Trace.TraceError(ex.Message);
                        ctsTask.Cancel();
                    }
                }
            });
        }

        /*
        public DelegateCommand ProcessBtnCommand => new DelegateCommand(async () =>
        {
            try
            {
                ctsTask ??= new CancellationTokenSource();
                CancellationToken token = ctsTask.Token;

                LoopStop = false;

                IsTransactionActive = true;
                eventAggregator.GetEvent<AppProjectCanExecuteEventAggregator>().Publish(false);

                MessageInput.Clear();

                await Task.Factory.StartNew(() => 
                {
                    var messageInputTask = new Task(() =>
                    {
                        DateTime startTime = DateTime.Now;
                        List<ZmqMsgModel> inputQueue = new List<ZmqMsgModel>();

                        using (var receiver = new PullSocket(">tcp://localhost:5556"))
                        {
                            try
                            {
                                while (!LoopStop)
                                {
                                    if (token.IsCancellationRequested)
                                        token.ThrowIfCancellationRequested();

                                    string workload = receiver.ReceiveFrameString();

                                    if (!string.IsNullOrWhiteSpace(workload))
                                    {
                                        var model = Newtonsoft.Json.JsonConvert.DeserializeObject<ZmqMsgModel>(workload);
                                        model.Id = CountMessagesCurrentPeriod() + 1;
                                        model.TemporalityName = EnumUtil.GetPeriodEnum(model.Temporality).GetMetadata().Name;
                                        model.DateFormat = model.Date.AddSeconds(TimeSpan.Parse(model.Time).TotalSeconds).ToString("dd/MM/yyyy HH:mm:ss");
                                        model.PutType = (int)MessageZMQPutTypeEnum.Input;
                                        model.PutTypeName = MessageZMQPutTypeEnum.Input.GetMetadata().Name;
                                        model.Description = workload;
                                        model.IsRequired = true;

                                        if (CurrencyPeriodId == (int)EnumUtil.GetPeriodEnum(model.Temporality))
                                        {
                                            inputQueue.Insert(0, model);

                                            if (true || Math.Round((DateTime.Now - startTime).TotalSeconds) % 3 == 0)
                                            {
                                                UpdateMessageInputQueue(inputQueue);
                                                inputQueue.Clear();
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Trace.TraceError(ex.Message);
                                ctsTask.Cancel();
                            }
                        }
                    }, token);
                    messageInputTask.Start();
                    messageInputTask.Await();

                    #region MessageOutputTask
                    
                    //var messageOutputTask = new Task(() => 
                    //{
                    //    DateTime startTime = DateTime.Now;
                    //    List<ZmqMsgModel> outputQueue = new List<ZmqMsgModel>();

                    //    using (var requestSocket = new RequestSocket(">tcp://localhost:5555"))
                    //    {
                    //        try
                    //        {
                    //            ZmqMsgModel lastMinp = null;
                    //            while (!LoopStop)
                    //            {
                    //                if (token.IsCancellationRequested)
                    //                    token.ThrowIfCancellationRequested();

                    //                ZmqMsgModel lastMessageInput = MessagesFromCurrentPeriod > MaximumMessagesRequired ? MessageInput.FirstOrDefault() : null; 

                    //                if ((lastMessageInput?.Id ?? 0) > (lastMinp?.Id ?? 0)  && Nodes.Any())
                    //                {
                    //                    #region CloseAll
                    //                    var msgCloseAllObj = new ZmqMsgRequestModel
                    //                    {
                    //                        UUID = Guid.NewGuid().ToString(),
                    //                        SYMBOL = "EURUSD",
                    //                        Request = "TRADE",
                    //                        OrderType = string.Empty,
                    //                        Action = "CLOSE_ALL"
                    //                    };

                    //                    string msgCloseAllReq = string.Join("|", msgCloseAllObj.GetType().GetProperties()
                    //                    .Where(p => Attribute.IsDefined(p, typeof(OrderAttribute)))
                    //                    .OrderBy(p => ((OrderAttribute)p.GetCustomAttributes(typeof(OrderAttribute), false).Single()).Order)
                    //                    .Select(p => p.GetValue(msgCloseAllObj)).ToList());

                    //                    requestSocket.SendFrame(msgCloseAllReq);
                    //                    string messageCloseAll = requestSocket.ReceiveFrameString();
                    //                    #endregion

                    //                    lastMinp = MessageInput.FirstOrDefault();


                    //                    IEnumerable<Candle> candles = MessageInput.Take(MaximumMessagesRequired + 1).Select(m => new Candle 
                    //                    {
                    //                        date = m.Date.AddSeconds((long)TimeSpan.Parse(m.Time).TotalSeconds),
                    //                        time = (long)TimeSpan.Parse(m.Time).TotalSeconds,
                    //                        open = (double)m.Open,
                    //                        max = (double)m.High,
                    //                        min = (double)m.Low,
                    //                        close = (double)m.Close,
                    //                        volumen = (double)m.Volume,
                    //                        label = m.Label
                    //                    }).OrderBy(m => m.date);

                    //                    List<IndicatorBase> indicators = ExtractorService.BuildIndicatorsFromNode(Nodes.SelectMany(n => n.Node.Select(_n => _n)).ToList());

                    //                    DateTime fromDateIS = candles.LastOrDefault().date.AddSeconds(-((CurrencyPeriodEnum)CurrencyPeriodId).ToSeconds());
                    //                    DateTime toDateOS = candles.LastOrDefault().date.AddSeconds(((CurrencyPeriodEnum)CurrencyPeriodId).ToSeconds());
                    //                    List<IndicatorBase> extractorResultIs = ExtractorService.ExtractorExecute(fromDateIS, toDateOS, indicators, candles, 0);

                    //                    int nodeTotalRulesIs = extractorResultIs.Count;
                    //                    if (nodeTotalRulesIs > 0)
                    //                    {
                    //                        var temporalIndicator = extractorResultIs.FirstOrDefault();
                    //                        int length = temporalIndicator.Output.Length;

                    //                        int counter = 0;
                    //                        while (counter < length)
                    //                        {
                    //                            int passed = 0;
                    //                            string upOrDown = temporalIndicator.IntervalLabels[counter].Label;

                    //                            foreach (var indicator in extractorResultIs)
                    //                            {
                    //                                double output = indicator.Output[counter];

                    //                                switch (indicator.Operator)
                    //                                {
                    //                                    case MathOperatorEnum.GreaterThanOrEqual:
                    //                                        passed += output >= indicator.Value ? 1 : 0;
                    //                                        break;
                    //                                    case MathOperatorEnum.LessThanOrEqual:
                    //                                        passed += output <= indicator.Value ? 1 : 0;
                    //                                        break;
                    //                                    case MathOperatorEnum.GreaterThan:
                    //                                        passed += output > indicator.Value ? 1 : 0;
                    //                                        break;
                    //                                    case MathOperatorEnum.LessThan:
                    //                                        passed += output < indicator.Value ? 1 : 0;
                    //                                        break;
                    //                                    case MathOperatorEnum.Equal:
                    //                                        passed += output == indicator.Value ? 1 : 0;
                    //                                        break;
                    //                                }
                    //                            }

                    //                            if (passed == nodeTotalRulesIs)
                    //                            {
                    //                                var msgObj = new ZmqMsgRequestModel
                    //                                {
                    //                                    UUID = Guid.NewGuid().ToString(),
                    //                                    SYMBOL = "EURUSD",
                    //                                    Request = "TRADE",
                    //                                    OrderType = "BUY",
                    //                                    Action = "CLOSE_ALL"
                    //                                };

                    //                                string msgReq = string.Join("|", msgObj.GetType().GetProperties()
                    //                                .Where(p => Attribute.IsDefined(p, typeof(OrderAttribute)))
                    //                                .OrderBy(p => ((OrderAttribute)p.GetCustomAttributes(typeof(OrderAttribute), false).Single()).Order)
                    //                                .Select(p => p.GetValue(msgObj)).ToList());

                    //                                requestSocket.SendFrame(msgReq);
                    //                                string message = requestSocket.ReceiveFrameString();

                    //                                try
                    //                                {
                    //                                    ZmqMsgRequestModel response = Newtonsoft.Json.JsonConvert.DeserializeObject<ZmqMsgRequestModel>(message);

                    //                                    outputQueue.Add(new ZmqMsgModel
                    //                                    {
                    //                                        Id = MessageOutput.Count + 1,

                    //                                        Date = lastMessageInput.Date,
                    //                                        DateFormat = lastMessageInput.DateFormat,

                    //                                        PutType = (int)MessageZMQPutTypeEnum.Output,
                    //                                        PutTypeName = MessageZMQPutTypeEnum.Output.GetMetadata().Name,
                    //                                        PositionType = response.OrderType == "BUY" ? (int)MessageZMQPositionTypeEnum.Buy : (int)MessageZMQPositionTypeEnum.Sell,
                    //                                        PositionTypeName = response.OrderType == "BUY" ? MessageZMQPositionTypeEnum.Buy.GetMetadata().Name : MessageZMQPositionTypeEnum.Sell.GetMetadata().Name,
                    //                                        Volume = (decimal)0.5,
                    //                                        Description = "Open Position",
                    //                                    });
                    //                                }
                    //                                catch (Exception ex)
                    //                                {
                    //                                    Trace.TraceError(ex.Message);
                    //                                }
                    //                            }

                    //                            counter++;
                    //                        }
                    //                    }
                                        
                    //                    if (true || Math.Round((DateTime.Now - startTime).TotalSeconds) % 1 == 0)
                    //                    {
                    //                        UpdateMessageOutputQueue(outputQueue);
                    //                        outputQueue.Clear();
                    //                    }
                    //                }
                    //            }
                    //        }
                    //        catch (Exception ex)
                    //        {
                    //            Trace.TraceError(ex.Message);
                    //            ctsTask.Cancel();
                    //        }
                    //    }
                    //}, token);
                    //messageOutputTask.Start();
                    //messageOutputTask.Await();
                    
                    #endregion

                }, token, TaskCreationOptions.None, TaskScheduler.Default);

                //IsTransactionActive = false;
            }
            catch (OperationCanceledException e)
            {
                Console.WriteLine($"{nameof(OperationCanceledException)} thrown with message: {e.Message}");
            }
            catch (Exception ex)
            {
                IsTransactionActive = false;
                Trace.TraceError(ex.Message);
            }
            finally
            {
                //ctsTask.Dispose();
                eventAggregator.GetEvent<AppProjectCanExecuteEventAggregator>().Publish(true);
            }
        }, () => !IsTransactionActive).ObservesProperty(() => IsTransactionActive);
        */
        #endregion

        #region CleanMessageOutputCommand
        public DelegateCommand CleanMessageInputCommand => new DelegateCommand(() => 
        {
            CleanMessageInput();
        }).ObservesCanExecute(() => MessageInputAny);
        public DelegateCommand CleanMessageOutputCommand => new DelegateCommand(() => { MessageOutput?.Clear(); }).ObservesCanExecute(() => MessageOutputAny);
        #endregion

        #region CurrencyPeriodCommand
        public ICommand CurrencyPeriodCommand => new DelegateCommand(() =>
        {
            foreach (var item in MessageInput.Where(m => m.IsRequired))
            {
                item.IsRequired = (int)EnumUtil.GetPeriodEnum(item.Temporality) == CurrencyPeriodId;
            }

            MessagesFromCurrentPeriod = CountMessagesCurrentPeriod();
        });
        #endregion

        #region AddNodeForTestCommand
        private ICommand AddNodeForTestCommand { get; set; }
        public void AddNode(REPTreeNodeModelVM node) 
        {
            try
            {
                if (Nodes == null)
                    Nodes = new ObservableCollection<REPTreeNodeModelVM>();

                var n = Nodes.FirstOrDefault(_n => _n.NodeWithoutFormat == node.NodeWithoutFormat);
                if (n != null)
                {
                    Nodes.Remove(node);
                }
                else
                {
                    Nodes.Add(node);
                }

                MaximumMessagesRequired = MaxMessagesRequired();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }
        #endregion

        #endregion

        private async void PopulateViewModel()
        {
            try
            {
                Project = await AppProjectService.GetProject(ProcessArgs.ProjectId, true);
                Configuration = Project?.ProjectConfigurations.FirstOrDefault();

                if (!CurrencyPeriods.Any())
                {
                    CurrencyPeriods.AddRange(EnumUtil.ToEnumerable<CurrencyPeriodEnum>()
                        .Where(c => c.Id != (int)CurrencyPeriodEnum.W && c.Id != (int)CurrencyPeriodEnum.MN)
                        .Select(c => new Metadata
                        {
                            Id = c.Id,
                            Name = c.Name
                        }).ToList());

                    CurrencyPeriodId = Configuration?.CurrencyPeriodId ?? (int)CurrencyPeriodEnum.H1;
                }

                if (!IsTransactionActive)
                {
                    ProjectMetaTraderExpertTemplate template = new ProjectMetaTraderExpertTemplate();
                    String pageContent = template.TransformText();
                    System.IO.File.WriteAllText("outputPage.txt", pageContent);
                }

                if (Nodes == null)
                {
                    Nodes = new ObservableCollection<REPTreeNodeModelVM>();
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
                        MessagesFromCurrentPeriod = CountMessagesCurrentPeriod();
                        int max = MaxMessagesRequired();
                        MessageInputAny = MessagesFromCurrentPeriod > max;

                        if (MessageInput.Count > 0 && max > 0 &&  MessageInput.Count > MaximumMessagesRequired * 2)
                        {
                            CleanMessageInput();
                        }
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

        private void ConsoleWriteLine(string msg)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Console.WriteLine(msg);
            });
        }

        private void UpdateMessageInputQueue(List<ZmqMsgModel> inputQueue) 
        {
            Application.Current.Dispatcher.Invoke(() => 
            {
                if (inputQueue.Count > 0)
                {
                    if (MessageInput.Count > 0)
                    {
                        /*var firstInputQueue = inputQueue.FirstOrDefault();
                        var firstMessageInput = MessageInput.FirstOrDefault();

                        DateTime fiq = firstInputQueue.Date.AddSeconds(TimeSpan.Parse(firstInputQueue.Time).TotalSeconds);
                        DateTime fmi = firstMessageInput.Date.AddSeconds(TimeSpan.Parse(firstMessageInput.Time).TotalSeconds);

                        if ((fiq - fmi).TotalSeconds != ((CurrencyPeriodEnum)CurrencyPeriodId).ToSeconds())
                        {
                            foreach (var item in MessageInput.Where(m => m.IsRequired))
                            {
                                item.IsRequired = false;
                            }
                        }*/
                    }

                    foreach (var m in inputQueue)
                    {
                        MessageInput.Insert(0, m);
                    }
                }
            });
        }
        private void UpdateMessageOutputQueue(List<ZmqMsgModel> outputQueue)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                foreach (var m in outputQueue)
                {
                    MessageOutput.Insert(0, m);
                }
            });
        }

        private int MaxMessagesRequired()
        {
            if (Nodes.Count > 0)
            {
                var indicators = ExtractorService.BuildIndicatorsFromNode(Nodes.SelectMany(_n => _n.Node.Select(__n => __n)).ToList());

                List<int> valueProperties = indicators.SelectMany(_i => _i.GetType().GetByAttributeProperties(typeof(IndicatorPeriodAttribute))
                    .Where(prop => prop.PropertyType.Name == typeof(int).Name).Select(prop => (int)prop.GetValue(_i))).OrderByDescending(pv => pv).ToList();

                return valueProperties.OrderByDescending(vp => vp)?.FirstOrDefault() ?? 0;
            }
            return 0;
        }
        private int CountMessagesCurrentPeriod()
        { 
            return MessageInput.Where(_m => (int)EnumUtil.GetPeriodEnum(_m.Temporality) == CurrencyPeriodId && _m.IsRequired).Count();
        }

        private void CleanMessageInput() 
        {
            int max = MaxMessagesRequired();
            var queue = MessageInput.Where(_m => (int)EnumUtil.GetPeriodEnum(_m.Temporality) == CurrencyPeriodId).ToList();

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

        #region Bindable Model
        private bool istransactionActive;
        public bool IsTransactionActive
        {
            get => istransactionActive;
            set => SetProperty(ref istransactionActive, value);
        }


        private bool canExecute = true;
        public bool CanExecute
        {
            get => canExecute;
            set => SetProperty(ref canExecute, value);
        }


        private ConfigurationBaseVM _configuration;
        public ConfigurationBaseVM Configuration
        {
            get => _configuration;
            set => SetProperty(ref _configuration, value);
        }


        private int maximumMessagesRequired;
        public int MaximumMessagesRequired 
        {
            get => maximumMessagesRequired;
            set => SetProperty(ref maximumMessagesRequired, value);
        }


        private int messagesFromCurrentPeriod;
        public int MessagesFromCurrentPeriod
        { 
            get => messagesFromCurrentPeriod;
            set => SetProperty(ref messagesFromCurrentPeriod, value);
        }


        private bool nodesAny;
        public bool NodesAny 
        {
            get => nodesAny;
            set => SetProperty(ref nodesAny, value);
        }


        private ObservableCollection<REPTreeNodeModelVM> nodes;
        public ObservableCollection<REPTreeNodeModelVM> Nodes
        {
            get => nodes;
            set => SetProperty(ref nodes, value);
        }


        private bool messageInputAny;
        public bool MessageInputAny
        {
            get => messageInputAny;
            set => SetProperty(ref messageInputAny, value);
        }


        private ObservableCollection<ZmqMsgModel> messageInput;
        public ObservableCollection<ZmqMsgModel> MessageInput 
        {
            get => messageInput;
            set => SetProperty(ref messageInput, value);
        }


        private bool messageOutputAny;
        public bool MessageOutputAny 
        {
            get => messageOutputAny;
            set => SetProperty(ref messageOutputAny, value);
        }


        private ObservableCollection<ZmqMsgModel> messageOutput;
        public ObservableCollection<ZmqMsgModel> MessageOutput
        {
            get => messageOutput;
            set => SetProperty(ref messageOutput, value);
        }


        private int? currencyPeriodId;
        public int? CurrencyPeriodId
        {
            get { return currencyPeriodId; }
            set { SetProperty(ref currencyPeriodId, value); }
        }


        public ObservableCollection<Metadata> CurrencyPeriods { get; } = new ObservableCollection<Metadata>();
        #endregion
    }
}
