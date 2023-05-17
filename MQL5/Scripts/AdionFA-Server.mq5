//+------------------------------------------------------------------+
//|                                               AdionFA-Server.mq5 |
//|                                  Copyright 2023, MetaQuotes Ltd. |
//|                                             https://www.mql5.com |
//+------------------------------------------------------------------+
#property copyright "Copyright 2023, MetaQuotes Ltd."
#property link      "https://www.mql5.com"
#property version   "1.00"

input string Host = "192.168.50.137";
input string Port = "5555";

#include <ZMQ/Zmq.mqh>
#include <Json/JAson.mqh>
#include <Files\FileTxt.mqh>

Context ServerContext("server");
Socket ResponseSocket(ServerContext, ZMQ_REP);

//+------------------------------------------------------------------+
//| Script program start function                                    |
//+------------------------------------------------------------------+
void OnStart()
  {
//---
   ResponseSocket.bind("tcp://" + Host + ":" + Port);
   
   printf("Server [tcp://" + Host + ":" + Port + "] started");
   
   while(!IsStopped())
     {
      CJAVal jsonRequest;
      ZmqMsg zmqRequest;
      
      if(ResponseSocket.recv(zmqRequest, true))
      {
        printf(zmqRequest.getData());
        
        jsonRequest.Deserialize(zmqRequest.getData());
        string cmd = jsonRequest["CMD"].ToStr();
        
        if(cmd == "LSL")
          {
           LoadSymbolList();
           continue;
          }
          
        if(cmd == "DHD")
          {
           string symbol = jsonRequest["Symbol"].ToStr();
           string timeframe = jsonRequest["Timeframe"].ToStr();
           string start = jsonRequest["Start"].ToStr();
           string end = jsonRequest["End"].ToStr();
           
           DownloadHistoricalData(symbol, timeframe, start, end);
           continue;
          }
       
       CJAVal jsonResponse;
       jsonResponse["Status"] = 0;
       jsonResponse["Message"] = "Unknown Command!";
       jsonResponse["Data"] = "";
       
       ResponseSocket.send(jsonResponse.Serialize(), true);
       printf(jsonResponse.Serialize());
      }
     }
  }
//+------------------------------------------------------------------+

//+------------------------------------------------------------------+
//| Load Symbol List                                                 |
//+------------------------------------------------------------------+
void LoadSymbolList()
  {
//---
    CJAVal jsonResponse;  
    int total = SymbolsTotal(true);
    string selectedSymbols;
    for(int i=total-1;i>=0;i--)
      {
       StringAdd(selectedSymbols, SymbolName(i, true));
       if(i!=0)
         {
          StringAdd(selectedSymbols, ",");
         }
      }
      
    jsonResponse["Status"] = 1;
    jsonResponse["Message"] = "Success";
    jsonResponse["Data"] = selectedSymbols;
        
    ResponseSocket.send(jsonResponse.Serialize(), true);
    printf(jsonResponse.Serialize());
  }
//+------------------------------------------------------------------+


//+------------------------------------------------------------------+
//| Download Historical Data                                         |
//+------------------------------------------------------------------+
void DownloadHistoricalData(string symbol, string timeframe, string start, string end)
  {
//---
    MqlRates rates[];
    CFileTxt file;
    
    ENUM_TIMEFRAMES timeframeEnum = (ENUM_TIMEFRAMES)timeframe;
    datetime startDt = StringToTime(start);
    datetime endDt = StringToTime(end);
    
    int totalBars = Bars(symbol, timeframeEnum, startDt, endDt);
    int copiedBars = CopyRates(symbol, timeframeEnum, 0, totalBars, rates);
    
    // TODO: Create custom filename ...
    string filename;
    string s = TimeToString(startDt, TIME_DATE); StringReplace(s, ".", "");
    string e = TimeToString(endDt, TIME_DATE); StringReplace(e, ".", "");
    StringConcatenate(filename, symbol, "_", EnumToString(timeframeEnum), "_", s, "-", e, ".csv");
    
    file.Open(filename, FILE_WRITE | FILE_COMMON);
    
    int digits = 5;
    for(int i=0;i<copiedBars;i++)
      {
       string write = "";
       StringConcatenate(write,
       TimeToString(rates[i].time, TIME_DATE),
       "," , TimeToString(rates[i].time, TIME_MINUTES),
       "," , DoubleToString(rates[i].open, digits),
       "," , DoubleToString(rates[i].high, digits),
       "," , DoubleToString(rates[i].low, digits),
       "," , DoubleToString(rates[i].close, digits),
       "," , DoubleToString(rates[i].real_volume, 0),
       "," , DoubleToString(rates[i].spread, 0),
       "\n");
       file.WriteString(write);
      }
      
    file.Close();
    
    string filepath;
    filepath = TerminalInfoString(TERMINAL_COMMONDATA_PATH) + "\\Files\\" + filename;
    CJAVal jsonResponse;
    jsonResponse["Status"] = 1;  // 1
    jsonResponse["Message"] = "Download Historical Data!";  // Success
    jsonResponse["Data"] = filepath;   // Absolute path where historical data file has been exported

    ResponseSocket.send(jsonResponse.Serialize(), true);
    printf(jsonResponse.Serialize());
  }
//+------------------------------------------------------------------+