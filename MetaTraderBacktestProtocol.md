AdionFA
- Test Node 1 Server running on 127.0.0.1:5000
    - Start the server --> Wait for tick information from MetaTrader --> Receive tick information --> Logic to decide on trade --> Request trade order
- Test Node 2 Server running on 127.0.0.1:5001

MetaTrader
- Strategy Tester running on 127.0.0.1:3000
    - EA client connected to 127.0.0.1:5000
    - Backtest of Node 1
    - Connect to AdionFA --> Tick information received --> Send tick information to AdionFA --> Wait for a response from AdionFA --> Receive the response --> Execute an order based on response --> ...

- Strategy Tester running on 127.0.0.1:3001
    - EA client connected to 127.0.0.1:5001
    - Backtest of Node 2