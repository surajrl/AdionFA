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

## Weka Configuration

Instances -> Number of trees created per extractor
NTotals ->

## Strategy Builder Configuration

- Winning Strategy UP
- Winning Strategy DOWN
- Transactions Target
- Variation Percent
- Correlation Percent
- Transactions (IS)
- Transactions (OS)
- Success Rate Percent (IS)
- Success Rate Percent (OS)
