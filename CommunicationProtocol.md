## General

ASCII encoding

## Request

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

## Response

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