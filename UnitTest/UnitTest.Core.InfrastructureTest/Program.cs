using Adion.FA.Core.Application.Contracts.Markets;
using Adion.FA.Infrastructure.Common.Extractor.Model;
using Adion.FA.Infrastructure.Common.Helpers;
using Adion.FA.Infrastructure.Core.Data.Persistence;
using Adion.FA.Infrastructure.Core.Data.Persistence.EFCore;
using Adion.FA.Infrastructure.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Adion.FA.TransferObject.Market;
using Adion.FA.Infrastructure.Common.Directories.Contracts;
using Adion.FA.TransferObject.Project;
using Adion.FA.Infrastructure.Common.IofC;
using Adion.FA.Infrastructure.Core.IofCExt;
using Adion.FA.Infrastructure.Common.Security.Model;
using Adion.FA.Core.API.Contracts.Projects;
using Adion.FA.Core.API.Contracts.Markets;
using Adion.FA.Infrastructure.Common.Logger.Contracts;
using Adion.FA.Infrastructure.Common.Security.Helper;
using Adion.FA.TransferObject.Base;

namespace UnitTest.Core.InfrastructureTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                #region IoC
                new IoC().Setup();
                using (var ctx = new AdionFADbContext()) { ctx.Database.Migrate(); }
                using (var ctx = new AdionSecurityDbContext()) { ctx.Database.Migrate(); }
                #endregion

                #region Log
                ILoggerHandler Log = IoC.Get<ILoggerHandler>();
                #endregion

                #region Identity
                AdionIdentity Identity = new AdionIdentity(SecurityHelper.DefaultTenantId, SecurityHelper.DefaultOwnerId, SecurityHelper.DefaultOwner);
                AdionPrincipal Principal = new AdionPrincipal();
                Principal.Identity = Identity;
                AppDomain.CurrentDomain.SetThreadPrincipal(Principal);
                #endregion

                //ProgramTest.CreateHistory();
                //ProgramTest.CreateProjects();

                #region History
                /*
                var historyAPI = IoC.Get<IMarketDataAPI>();
                var history = historyAPI.GetMarketData(1, true);

                IEnumerable<Candle> candles = history.MarketDataDetails.Select(
                        h => new Candle
                        {
                            date = h.StartDate,
                            time = h.StartTime,
                            open = h.OpenPrice,
                            max = h.MaxPrice,
                            min = h.MinPrice,
                            close = h.ClosePrice,
                            volumen = h.Volumen
                        }
                    ).OrderBy(d => d.date).ThenBy(d => d.time).ToList();

                var fromDT = DateTime.Parse("2019-01-17T12:00:00");
                IEnumerable<Candle> candlesRange = from c in candles
                                                   let dt = DateTimeHelper.BuildDateTime((int)CurrencyPeriodEnum.H1, c.date, c.time)
                                                   where dt >= fromDT
                                                   select c;

                foreach (var c in candlesRange)
                {
                    var dt = DateTimeHelper.BuildDateTime((int)CurrencyPeriodEnum.H1, c.date, c.time);
                    if (dt >= fromDT)
                        Console.WriteLine($"{dt} - {fromDT}");
                }*/
                #endregion
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
            }
                       
        }
    }

    public static class ProgramTest
    {
        public static void CreateHistory()
        {
            string historyFilePath = "C:\\Trade\\EURUSD60.csv";

            List<Candle> result = CandleHelper.GetHistoryCandles(historyFilePath).ToList();

            if (result.Count > 0)
            {
                var marketHistoryData = new MarketDataDTO
                {
                    MarketId = (int)MarketEnum.Forex,
                    CurrencyPairId = (int)CurrencyPairEnum.EURUSD,
                    CurrencyPeriodId = (int)CurrencyPeriodEnum.H1,
                    MarketDataDetails = Array.Empty<MarketDataDetailDTO>().ToList()
                };

                foreach (var item in result)
                {
                    marketHistoryData.MarketDataDetails.Add(new MarketDataDetailDTO
                    {
                        StartDate = item.date,
                        StartTime = item.time,
                        OpenPrice = item.open,
                        MaxPrice = item.max,
                        MinPrice = item.min,
                        ClosePrice = item.close,
                        Volumen = item.volumen
                    });
                }

                IoC.Get<IMarketDataAppService>().CreateMarketData(marketHistoryData);
            }
        }

        public static ResponseDTO CreateProjects()
        {
            ResponseDTO response = new ResponseDTO { IsSuccess = false };
            IProjectDirectoryService pds = IoC.Get<IProjectDirectoryService>();
            if (pds.CreateDefaultWorkspace())
            {
                
                MarketDataDTO dataDTO = IoC.Get<IMarketDataAPI>().GetMarketData(
                            (int)MarketEnum.Forex, (int)CurrencyPairEnum.EURUSD, (int)CurrencyPeriodEnum.H1);

                string pname = CurrencyPairEnum.GBPUSD.GetMetadata().Name;

                response = IoC.Get<IProjectAPI>().CreateProject(new ProjectDTO
                {
                    ProjectName = pname,
                    ProjectStepId = (int)ProjectStepEnum.InitialConfiguration,

                }, 0, dataDTO.MarketDataId);

                if(response.IsSuccess)
                    pds.CreateDefaultProjectWorkspace(pname);
            }
            return response;
        }

        public static async Task CreateExtraction() 
        {/*
            IDirectoryService directoryService = new DirectoryService();
            IProjectService projectService = new ProjectService();
            IMarketHistoryDataService historyService = new MarketHistoryDataService();

            ProjectVM project = await projectService.GetProject(1, false);
            ProjectConfigurationVM pconfig = await projectService.GetProjectConfiguration(project.ProjectId, true);
            MarketHistoryDataVM history = await historyService.GetMarketHistoryData(project.MarketHistoryDataId ?? 0, true);

            IEnumerable<Candle> candles = history.MarketHistoryDataDetails.Select(
                    h => new Candle
                    {
                        date = h.StartDate,
                        time = h.StartTime,
                        open = h.OpenPrice,
                        max = h.MaxPrice,
                        min = h.MinPrice,
                        close = h.ClosePrice,
                        volumen = h.Volumen
                    }
                ).OrderBy(d => d.date).ThenBy(d => d.time).ToList();
            */

            #region Parse Indicator Templates 
            /*FileInfo[] files = directoryService.GetFilesInPath("C:\\Trade\\Indicator Tempates\\");
            int index = 0;
            foreach (var fi in files)
            {
                index++;
                using (var reader = new StreamReader(fi.FullName))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Configuration.HasHeaderRecord = false;
                    int count = 0;
                    while (csv.Read())
                    {
                        count++;
                    }

                    List<IndicatorBase> i = ExtractorApi.BuildIndicators(fi.FullName);

                    Console.WriteLine($"{index}-{fi.Name}-{count}--{i.Count()}");
                }
            }
            return;*/
            #endregion
            /*
            List<IndicatorBase> indicators = ExtractorApi.BuildIndicators("C:\\Trade\\Indicator Tempates\\Ext-031616.csv");
            */
            #region Test Indicator Template
            /*foreach (var indicator in indicators)
            {
                var idx = indicators.IndexOf(indicator)+1;
                switch (indicator.Type)
                {
                    case IndicatorEnum.ADX:
                        ADX adx = (ADX)indicator;
                        Console.WriteLine($"{idx}-{adx.Name}-{adx.OptInTimePeriod}");
                        break;
                    case IndicatorEnum.ADXR:
                        ADXR adxr = (ADXR)indicator;
                        Console.WriteLine($"{idx}-{adxr.Name}-{adxr.OptInTimePeriod}");
                        break;
                    case IndicatorEnum.APO:
                        APO apo = (APO)indicator;
                        Console.WriteLine($"{idx}-APO-{Enum.GetName(typeof(PriceTypeEnum), apo.PriceType)}-{apo.OptInFastPeriod}-{apo.OptInSlowPeriod}-{Enum.GetName(typeof(MATypeEnum), apo.MAType)}");
                        break;
                    case IndicatorEnum.AROON:
                        AROON aroon = (AROON)indicator;
                        Console.WriteLine($"{idx}-AROON-{((AROONDownUpEnum)aroon.AROONDownUp).GetDescription()}");
                        break;
                    case IndicatorEnum.ATR:
                        ATR atr = (ATR)indicator;
                        Console.WriteLine($"{idx}-ATR-{atr.OptInTimePeriod}");
                        break;
                    case IndicatorEnum.BOP:
                        BOP BOP = (BOP)indicator;
                        Console.WriteLine($"{idx}-BOP-Empty");
                        break;
                    case IndicatorEnum.CCI:
                        CCI cci = (CCI)indicator;
                        Console.WriteLine($"{idx}-CCI-{cci.OptInTimePeriod}");
                        break;
                    case IndicatorEnum.CMO:
                        CMO cmo = (CMO)indicator;
                        Console.WriteLine($"{idx}-CMO-{Enum.GetName(typeof(PriceTypeEnum), cmo.PriceType)}-{cmo.OptInTimePeriod}");
                        break;
                    case IndicatorEnum.DX:
                        DX dx = (DX)indicator;
                        Console.WriteLine($"{idx}-DX-{dx.OptInTimePeriod}");
                        break;
                    case IndicatorEnum.MACD:
                        MACD macd = (MACD)indicator;
                        Console.WriteLine($"{idx}-MACD-{Enum.GetName(typeof(PriceTypeEnum), macd.PriceType)}-{macd.OptInFastPeriod}-{macd.OptInSlowPeriod}-{macd.OptInSignalPeriod}-{((MACDOutputEnum)macd.MACDOutput).GetDescription()}");
                        break;
                    case IndicatorEnum.MINUS_DI:
                        MINUS_DI MINUS_DI = (MINUS_DI)indicator;
                        Console.WriteLine($"{idx}-MINUS_DI-{MINUS_DI.OptInTimePeriod}");
                        break;
                    case IndicatorEnum.MINUS_DM:
                        MINUS_DM MINUS_DM = (MINUS_DM)indicator;
                        Console.WriteLine($"{idx}-MINUS_DM-{MINUS_DM.OptInTimePeriod}");
                        break;
                    case IndicatorEnum.MOM:
                        MOM MOM = (MOM)indicator;
                        Console.WriteLine($"{idx}-MOM-{Enum.GetName(typeof(PriceTypeEnum), MOM.PriceType)}-{MOM.OptInTimePeriod}");
                        break;
                    case IndicatorEnum.PLUS_DI:
                        PLUS_DI PLUS_DI = (PLUS_DI)indicator;
                        Console.WriteLine($"{idx}-PLUS_DI-{PLUS_DI.OptInTimePeriod}");
                        break;
                    case IndicatorEnum.PLUS_DM:
                        PLUS_DM PLUS_DM = (PLUS_DM)indicator;
                        Console.WriteLine($"{idx}-PLUS_DM-{PLUS_DM.OptInTimePeriod}");
                        break;
                    case IndicatorEnum.PPO:
                        PPO PPO = (PPO)indicator;
                        Console.WriteLine($"{idx}-PPO-{Enum.GetName(typeof(PriceTypeEnum), PPO.PriceType)}-{PPO.OptInFastPeriod}-{PPO.OptInSlowPeriod}-{Enum.GetName(typeof(MATypeEnum), PPO.MAType)}");
                        break;
                    case IndicatorEnum.ROC:
                        ROC ROC = (ROC)indicator;
                        Console.WriteLine($"{idx}-ROC-{Enum.GetName(typeof(PriceTypeEnum), ROC.PriceType)}-{ROC.OptInTimePeriod}");
                        break;
                    case IndicatorEnum.RSI:
                        RSI RSI = (RSI)indicator;
                        Console.WriteLine($"{idx}-RSI-{Enum.GetName(typeof(PriceTypeEnum), RSI.PriceType)}-{RSI.OptInTimePeriod}");
                        break;
                    case IndicatorEnum.STDDEV:
                        STDDEV STDDEV = (STDDEV)indicator;
                        Console.WriteLine($"{idx}-STDDEV-{Enum.GetName(typeof(PriceTypeEnum), STDDEV.PriceType)}-{STDDEV.OptInTimePeriod}-{STDDEV.OptInNbDev}");
                        break;
                    case IndicatorEnum.STOCHF:
                        STOCHF STOCHF = (STOCHF)indicator;
                        Console.WriteLine($"{idx}-STOCHF-{STOCHF.OptInFastKPeriod}-{STOCHF.OptInFastDPeriod}-{Enum.GetName(typeof(MATypeEnum), STOCHF.OptInFastDMAType)}-{((STOCHFOutputEnum)STOCHF.STOCHFOutput).GetDescription()}");
                        break;
                    case IndicatorEnum.STOCH:
                        STOCH STOCH = (STOCH)indicator;
                        Console.WriteLine($"{idx}-STOCH-{STOCH.OptInFastKPeriod}-{STOCH.OptInSlowKPeriod}-{Enum.GetName(typeof(MATypeEnum), STOCH.OptInSlowKMAType)}-{STOCH.OptInSlowDPeriod}-{Enum.GetName(typeof(MATypeEnum), STOCH.OptInSlowDMAType)}");
                        break;
                    case IndicatorEnum.STOCHRSI:
                        STOCHRSI STOCHRSI = (STOCHRSI)indicator;
                        Console.WriteLine($"{idx}-STOCHRSI-{Enum.GetName(typeof(PriceTypeEnum), STOCHRSI.PriceType)}-{STOCHRSI.OptInTimePeriod}-{STOCHRSI.OptInFastKPeriod}-{STOCHRSI.OptInFastDPeriod}-{Enum.GetName(typeof(MATypeEnum), STOCHRSI.OptInFastDMAType)}-{((STOCHRSIOutputEnum)STOCHRSI.STOCHRSIOutput).GetDescription()}");
                        break;
                    case IndicatorEnum.ULTOSC:
                        ULTOSC ULTOSC = (ULTOSC)indicator;
                        Console.WriteLine($"{idx}-ULTOSC-{ULTOSC.OptInTimePeriod1}-{ULTOSC.OptInTimePeriod2}-{ULTOSC.OptInTimePeriod3}");
                        break;
                    case IndicatorEnum.VAR:
                        VAR VAR = (VAR)indicator;
                        Console.WriteLine($"{idx}-VAR-{Enum.GetName(typeof(PriceTypeEnum), VAR.PriceType)}-{VAR.OptInTimePeriod}");
                        break;
                    case IndicatorEnum.WILLR:
                        WILLR WILLR = (WILLR)indicator;
                        Console.WriteLine($"{idx}-WILLR-{WILLR.OptInTimePeriod}");
                        break;
                }
            }*/
            #endregion
            /*
            DateTime fromDate = new DateTime(2016, 01, 04);

            DateTime toDate = new DateTime(2016, 01, 6);

            List<IndicatorBase> extractions = ExtractorApi.ExtractorExecute(fromDate, toDate, indicators, candles, 1);

            //var europe = pconfig.
            ExtractorApi.ExtractorWrite(
                project.ProjectName.ProjectExtractorEuropeDirectory($"Ext-010957_{DateTime.UtcNow.ToString("yyyy.MM.dd.HH.mm.ss")}.csv"),
                extractions,
                9,18
            );
            */
        }
    }
}
