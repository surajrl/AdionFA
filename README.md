# AdionFA

Adion Financial Automat

## Strategy Builder

Weka
- Creates a REP tree for each extractor (IS data) using the Weka configuration (Depth, total decimal, total instances, max ratio, ntotal, and minimum and maximum seed). Each REP tree is considered an "Instance".
- Pruning -> Selects the winning nodes (i.e. the nodes that meet the requirements from the Strategy Builder configuration?)

Strategy builder
- Runs a backtest on each node (IS data and OS data).