using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace AdionFA.UI.Station.Infrastructure.Contracts
{
    public interface IMetaTraderService
    {
        Task ConnectAsync(string ipAddress, int port);

        void Disconnect();

        Task<string> DownloadHistoricalDataAsync(string symbol, int timeframe, DateTime start, DateTime? end);

        Task<IList<string>> LoadSymbolListAsync();

        EndPoint RemoteEndPoint { get; }
    }
}