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
input int ReceiveTimeout = 100;
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
   Close = 8
  };

//+------------------------------------------------------------------+
//| Expert initialization function                                   |
//+------------------------------------------------------------------+
int OnInit()
  {
//---
   ResponseSocket.bind("tcp://" + Host + ":" + ResponsePort);
   PublisherSocket.bind("tcp://" + Host + ":" + PublisherPort);
   
   ResponseSocket.setReceiveTimeout(ReceiveTimeout);
//---
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
   OrderTypeEnum buy = Buy;
   OrderTypeEnum sell = Sell;
   CTrade trade;
   
   if(PendingClose)
     {
       if(trade.PositionClose(PositionGetTicket(PositionsTotal()-1)))
         {
          CloseRequest = false;
          PendingClose = false;
         }
     }
   
   if(PendingBuy)
     {
       CJAVal responseJson;
       
       if(trade.Buy(PositionVolume))
         {
          responseJson["OrderType"] = 0;
          responseJson["Volume"] = PositionVolume;
          
          ResponseSocket.send(responseJson.Serialize());
          CloseRequest = true;
          PendingBuy = false;
         }
     }
   
   if(PendingSell)
     {
       CJAVal responseJson;

       if(trade.Sell(PositionVolume))
         {
          responseJson["OrderType"] = 1;
          responseJson["Volume"] = PositionVolume;
          
          ResponseSocket.send(responseJson.Serialize());
          CloseRequest = true;
          PendingSell = false;
         } 
     }
     
   static datetime dtBarCurrent  = WRONG_VALUE;
   datetime dtBarPrevious = dtBarCurrent;
   
   dtBarCurrent = iTime(_Symbol, _Period, 0); 
   
   if(dtBarCurrent != dtBarPrevious)
     {
       if(CloseRequest)
         {
          if(trade.PositionClose(PositionGetTicket(PositionsTotal()-1)))
            {
             CloseRequest = false;
            }
          else
            {
             PendingClose = true;
            }
         }
       
       MqlRates rates[];
       CopyRates(Symbol(), PERIOD_CURRENT, 0 , 2, rates);
       
       // Complete Candle ---------------------------------------------------------
       CJAVal jsonCompleteCandle;
       
       jsonCompleteCandle["IsNewCandle"] = false;
       
       jsonCompleteCandle["Open"] = rates[0].open;
       jsonCompleteCandle["High"] = rates[0].high;
       jsonCompleteCandle["Low"] = rates[0].low;
       jsonCompleteCandle["Close"] = rates[0].close;
       
       jsonCompleteCandle["Volume"] = rates[0].real_volume;
       jsonCompleteCandle["Spread"] = rates[0].spread;
       
       jsonCompleteCandle["Time"] = TimeToString(rates[0].time, TIME_MINUTES);
       jsonCompleteCandle["Date"] = TimeToString(rates[0].time, TIME_DATE);
       
       jsonCompleteCandle["Temporality"] = 16388;
       // -------------------------------------------------------------------------
       
       // Current Candle ----------------------------------------------------------       
       CJAVal jsonCurrentCandle;
       
       jsonCurrentCandle["IsNewCandle"] = true;
       
       jsonCurrentCandle["Open"] = rates[1].open;
      
       jsonCurrentCandle["Time"] = TimeToString(rates[0].time, TIME_MINUTES);
       jsonCurrentCandle["Date"] = TimeToString(rates[0].time, TIME_DATE);
       
       jsonCurrentCandle["Temporality"] = 16388;
       // -------------------------------------------------------------------------
       
       PublisherSocket.send(jsonCurrentCandle.Serialize());
       printf("[PUBLISHER]\t[SEND] %s", jsonCurrentCandle.Serialize());
       
       PublisherSocket.send(jsonCompleteCandle.Serialize());
       printf("[PUBLISHER]\t[SEND] %s", jsonCompleteCandle.Serialize());
     
     
       // Receive OPEN/CLOSE operation and process
       ZmqMsg requestMsg;
       if(ResponseSocket.recv(requestMsg))
         {
          CJAVal requestJson;
          requestJson.Deserialize(requestMsg.getData());
      
          OrderTypeEnum orderType = (OrderTypeEnum)requestJson["OrderType"].ToInt();
      
          // BUY
          if(orderType == Buy)
            {
             if(trade.Buy(PositionVolume))
               {
                CJAVal responseJson;
                responseJson["OrderType"] = 0;
                responseJson["Volume"] = PositionVolume;
            
                ResponseSocket.send(responseJson.Serialize());
                CloseRequest = true;
                return;
               }
         
             PendingBuy = true;
             return;
            }
      
          // SELL  
          if(orderType == Sell)
            {
             if(trade.Sell(PositionVolume))
               {
                CJAVal responseJson;
                responseJson["OrderType"] = 1;
                responseJson["Volume"] = PositionVolume;
            
                ResponseSocket.send(responseJson.Serialize());
                CloseRequest = true;
                return;
               }
         
             PendingSell = true;
             return;
            }
       }
     }
  }
//+------------------------------------------------------------------+
