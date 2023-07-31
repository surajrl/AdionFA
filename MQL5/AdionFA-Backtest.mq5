//+------------------------------------------------------------------+
//|                                             AdionFA-Backtest.mq5 |
//|                                  Copyright 2023, MetaQuotes Ltd. |
//|                                             https://www.mql5.com |
//+------------------------------------------------------------------+
#property copyright "Copyright 2023, MetaQuotes Ltd."
#property link      "https://www.mql5.com"
#property version   "1.00"

#include <ZMQ\Zmq.mqh>
#include <Json\JAson.mqh>
#include <Trade\Trade.mqh>

input string Host = "192.168.50.137";
input string ResponsePort = "5550";
input string PublisherPort = "5551";
input string Symbols = "EURUSD";
input double PositionVolume = 0.1;

Context ServerContext("backtest");
Socket ResponseSocket(ServerContext, ZMQ_REP);
Socket PublisherSocket(ServerContext, ZMQ_PUB);

bool CloseRequest = false;

bool PendingBuy = false;
bool PendingSell = false;
bool PendingClose = false;

enum OrderTypeEnum
  {
   Buy = 0,
   Sell = 1,
   Close = 8,
   None = 9,
  };

int NumberOfSymbols;
string SymbolsArray[];
static datetime dtCurrentBar[];

//+------------------------------------------------------------------+
//| Expert initialization function                                   |
//+------------------------------------------------------------------+
int OnInit()
  {
//---
   ResponseSocket.bind("tcp://" + Host + ":" + ResponsePort);
   PublisherSocket.bind("tcp://" + Host + ":" + PublisherPort);
   
   ResponseSocket.setTimeout(5000);
   
   NumberOfSymbols = StringSplit(Symbols, '|', SymbolsArray);
   ArrayResize(dtCurrentBar, NumberOfSymbols);
   
   for(int i=0;i<NumberOfSymbols;i++)
     {
      dtCurrentBar[i] = WRONG_VALUE;
     }
   
   return(INIT_SUCCEEDED);
  }
//+------------------------------------------------------------------+
//| Expert deinitialization function                                 |
//+------------------------------------------------------------------+
void OnDeinit(const int reason)
  {
//---
   
  }
//+------------------------------------------------------------------+
//| Expert tick function                                             |
//+------------------------------------------------------------------+
void OnTick()
  {
//---
   // Operation not executed due to market being closed
   CheckPendingClose();
   CheckPendingBuy();
   CheckPendingSell();
   
   // Check for new bar on every symbol being used
   for(int i=0;i<NumberOfSymbols;i++)
     {
      datetime dtPreviousBar = dtCurrentBar[i];
      dtCurrentBar[i] = iTime(SymbolsArray[i], _Period, 0);
      
      if(dtCurrentBar[i]!=dtPreviousBar)
        {
         if(SymbolsArray[i] == _Symbol)
           {
            TryClosePosition();
           }
         
         // Get bar details and send them
         MqlRates rates[];
         CopyRates(SymbolsArray[i], _Period, 0 , 2, rates);

         CJAVal jsonPreviousBar;
         jsonPreviousBar["Symbol"] = SymbolsArray[i];
         jsonPreviousBar["Date"] = TimeToString(rates[0].time, TIME_DATE);
         jsonPreviousBar["Time"] = TimeToString(rates[0].time, TIME_MINUTES);
         jsonPreviousBar["Open"] = rates[0].open;
         jsonPreviousBar["High"] = rates[0].high;
         jsonPreviousBar["Low"] = rates[0].low;
         jsonPreviousBar["Close"] = rates[0].close;
         jsonPreviousBar["Volume"] = rates[0].real_volume;
         jsonPreviousBar["Spread"] = rates[0].spread;
         
         CJAVal jsonCurrentBar;
         jsonCurrentBar["IsCurrentBar"] = true;
         jsonCurrentBar["Symbol"] = SymbolsArray[i];
         jsonCurrentBar["Date"] = TimeToString(rates[1].time, TIME_DATE);
         jsonCurrentBar["Time"] = TimeToString(rates[1].time, TIME_MINUTES);
         jsonCurrentBar["Open"] = rates[1].open;
         jsonCurrentBar["High"] = rates[1].high;
         jsonCurrentBar["Low"] = rates[1].low;
         jsonCurrentBar["Close"] = rates[1].close;
         jsonCurrentBar["Volume"] = rates[1].real_volume;
         jsonCurrentBar["Spread"] = rates[1].spread;
         
         PublisherSocket.send(jsonPreviousBar.Serialize());
         printf("[PUBLISHER]\t[SEND] %s", jsonPreviousBar.Serialize());
         
         PublisherSocket.send(jsonCurrentBar.Serialize());
         printf("[PUBLISHER]\t[SEND] %s", jsonCurrentBar.Serialize());
        
         if(i==NumberOfSymbols-1)
           {
            CJAVal jsonCheckTrade;
            jsonCheckTrade["CheckTrade"] = true;
            
            PublisherSocket.send(jsonCheckTrade.Serialize());
            printf("[PUBLISHER]\t[SEND] %s", jsonCheckTrade.Serialize());
        
            // Check for requested trades
            ZmqMsg requestMsg;
            if(ResponseSocket.recv(requestMsg))
              {
               CJAVal requestJson;
               requestJson.Deserialize(requestMsg.getData());
      
               OrderTypeEnum orderType = (OrderTypeEnum)requestJson["OrderType"].ToInt();
              
               if(orderType == OrderTypeEnum::Buy)
                 {
                  TryOpenBuyPosition();
                 }
               else if(orderType == OrderTypeEnum::Sell)
                 {
                  TryOpenSellPosition();
                 }
               else if(orderType == OrderTypeEnum::None)
                 {
                  NoOperation();
                 }
              }
           }
        }
     }
  }
//+------------------------------------------------------------------+
//| OPEN SELL POSITION                                               |
//+------------------------------------------------------------------+
void TryOpenSellPosition()
  {
//---
   CTrade trade;
   if(trade.Sell(PositionVolume))
     {
      CJAVal responseJson;
      responseJson["OrderType"] = (int)OrderTypeEnum::Sell;
      responseJson["Volume"] = PositionVolume;
                  
      ResponseSocket.send(responseJson.Serialize());
      printf("OPEN SELL SUCCESSFUL!");
                  
      CloseRequest = true;
                  
      return;
     }
   
   PendingSell = true;
  }
//+------------------------------------------------------------------+
//| OPEN BUY POSITION                                                |
//+------------------------------------------------------------------+
void TryOpenBuyPosition()
  {
//---
   CTrade trade;
   if(trade.Buy(PositionVolume))
     {
      CJAVal responseJson;
      responseJson["OrderType"] = (int)OrderTypeEnum::Buy;
      responseJson["Volume"] = PositionVolume;
                  
      ResponseSocket.send(responseJson.Serialize());
      printf("OPEN BUY SUCCESSFUL!");
                  
      CloseRequest = true;
                  
      return;
     }
   
   PendingBuy = true;
  }
//+------------------------------------------------------------------+
//| CLOSE POSITION                                                   |
//+------------------------------------------------------------------+
void TryClosePosition()
  {
//---
   CTrade trade;
   if(!CloseRequest)
     {
      return;
     }
   
   if(CloseRequest && trade.PositionClose(PositionGetTicket(PositionsTotal()-1)))
     {
      CloseRequest = false;
      return;
     }
   
   PendingClose = true;
  }
//+------------------------------------------------------------------+
//| NO OPERATION                                                     |
//+------------------------------------------------------------------+
void NoOperation()
  {
//---
   CJAVal responseJson;
   responseJson["OrderType"] = (int)OrderTypeEnum::None;
                  
   ResponseSocket.send(responseJson.Serialize());
  }
//+------------------------------------------------------------------+
//| Check Pending SELL                                               |
//+------------------------------------------------------------------+
void CheckPendingSell()
  {
//---
   CTrade trade;
   if(PendingSell && trade.Sell(PositionVolume))
     {
      CJAVal responseJson;
      responseJson["OrderType"] = (int)OrderTypeEnum::Sell;
      responseJson["Volume"] = PositionVolume;
          
      ResponseSocket.send(responseJson.Serialize());
      printf("PENDING SELL SUCCESSFUL!");
      
      CloseRequest = true;
      PendingSell = false;
     }
  }
//+------------------------------------------------------------------+
//| Check Pending BUY                                                |
//+------------------------------------------------------------------+
void CheckPendingBuy()
  {
//---
   CTrade trade;
   if(PendingBuy && trade.Buy(PositionVolume))
     {
       CJAVal responseJson;
       responseJson["OrderType"] = (int)OrderTypeEnum::Buy;
       responseJson["Volume"] = PositionVolume;
          
       ResponseSocket.send(responseJson.Serialize());
       printf("PENDING BUY SUCCESSFUL!");
          
       CloseRequest = true;
       PendingBuy = false;
     }
  }
//+------------------------------------------------------------------+
//| Check Pending CLOSE                                              |
//+------------------------------------------------------------------+
void CheckPendingClose()
  {
//---
   CTrade trade;
   if(PendingClose && trade.PositionClose(PositionGetTicket(PositionsTotal()-1)))
     {
      CloseRequest = false;
      PendingClose = false;
      printf("PENDING CLOSE SUCCESSFUL!");
     }
  }
     