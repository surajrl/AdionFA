# AdionFA

Adion Financial Automat

## Strategy Builder

Weka
- Creates a REP tree for each extractor (IS data) using the Weka configuration (Depth, total decimal, total instances, max ratio, ntotal, and minimum and maximum seed). Each REP tree is considered an "Instance".
- Pruning -> Selects the winning nodes (i.e. the nodes that meet the requirements from the Strategy Builder configuration?)

Backtest Model
- Total Opportunity: Number of candles used to calculate one node.
- Total Transactions: Number of candles that met the node conditions.

Strategy builder
- Runs a backtest on each node (IS data and OS data).

## AdionFA Station Project to MetaTrader Communication Protocol 

AdionFA
- Pull Socket: Receives data from MetaTrader and adds to an input message queue 
- Request Socket: Takes data from input message queue, sends trade order to MetaTrader, receives trade execution from MetaTrader and adds it to an output queue

MetaTrader
- Push Socket: Sends data to AdionFA
- Response Socket: Receives trade order from AdionFA, send trade execution to AdionFA

All the sockets use the same host (e.g. 127.0.0.1)
Pull-Push sockets use the same port (e.g. 5556)
Request-Response sockets use the same port (e.g. 5555)

## AdionFA Station to MetaTrader Communication Protocol

ASCII encoding

### Request

Request Format
- 3 Bytes:  Command
- X Bytes:  Data Field (Each data value is preceded by a white space)
- 1 Byte:   CR

Command
- DHD (Download Historical Data)
    - Symbol
    - Timeframe
    - Start datetime
    - End datetime

- LSL (Load Symbol List)
    - No data field

### Response

Response Format
- 3 Bytes - Command
- X Bytes - Data Field (Each data value is preceded by a white space)
- 1 Byte  - CR

Command
- DHD (Download Historical Data)
    - Absolute path of where the historical data was downloaded

- LSL (Load Symbol List)
    - Decimal value of N symbols
    - Symbol n1
    - Symbol n2
    - Symbol nx
    - ...
    - Symbol N