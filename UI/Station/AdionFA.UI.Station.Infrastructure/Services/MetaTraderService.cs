using AdionFA.UI.Station.Infrastructure.Contracts;
using AdionFA.UI.Station.Infrastructure.EventAggregator;
using AdionFA.UI.Station.Infrastructure.Helpers;
using AdionFA.UI.Station.Infrastructure.Model.Base;
using Prism.Events;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AdionFA.UI.Station.Module.Dashboard.Services
{
    public class MetaTraderService : IMetaTraderService, IDisposable
    {
        private Socket _socket;
        private Task _readTask;
        private bool disposedValue;

        private readonly IEventAggregator _eventAggregator;

        public MetaTraderService(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            _socket = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ResponseQueue = new();
        }

        public BlockingCollection<string> ResponseQueue;

        public EndPoint RemoteEndPoint => _socket.RemoteEndPoint;

        public bool IsConnected { get; set; }

        public async Task ConnectAsync(string ipAddress, int port)
        {
            _socket ??= new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ResponseQueue ??= new();

            await _socket.ConnectAsync(ipAddress, port);
            _eventAggregator.GetEvent<MetaTraderConnectedEvent>().Publish(true);

            _readTask = Task.Run(ReadFromSocketAsync);
        }

        private async Task ReadFromSocketAsync()
        {
            try
            {
                while (true)
                {
                    var buffer = new byte[1024];
                    var bytesRead = await _socket.ReceiveAsync(buffer, SocketFlags.None).ConfigureAwait(false);

                    if (bytesRead == 0) // Graceful close
                    {
                        Disconnect();
                        break;
                    }

                    var response = new byte[bytesRead];
                    Buffer.BlockCopy(buffer, 0, response, 0, bytesRead);

                    if (ResponseQueue.TryAdd(Encoding.ASCII.GetString(response)))
                    {
                        Debug.WriteLine($"ResponseQueue Add: {Encoding.ASCII.GetString(response)}");
                    }
                }
            }
            catch (SocketException ex)
            {
                Trace.TraceError(ex.Message);
                Disconnect();
                throw;
            }
        }

        private async Task WriteToSocketAsync(string request)
        {
            while (ResponseQueue.Count != 0)
            {
                // Wait for responses to be empty
            }

            try
            {
                await _socket.SendAsync(Encoding.ASCII.GetBytes(request), SocketFlags.None);
            }
            catch (SocketException ex)
            {
                Trace.TraceError(ex.Message);
                Disconnect();
                throw;
            }
        }

        public void Disconnect()
        {
            try
            {
                if (_socket.Connected)
                    _socket?.Shutdown(SocketShutdown.Both);
                _socket?.Close();
                _socket = null;

                _eventAggregator.GetEvent<MetaTraderConnectedEvent>().Publish(false);
            }
            catch (SocketException ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        public async Task<string> DownloadHistoricalDataAsync(string symbol, int timeframe, DateTime start, DateTime? end)
        {
            var request = "DHD"
                + " " + symbol
                + " " + timeframe
                + " " + start.ToString("yyyy.MM.dd HH:mm")
                + " " + end.ToString()
                + "\r";

            await WriteToSocketAsync(request).ConfigureAwait(false);

            string response;
            while (!ResponseQueue.TryTake(out response))
            {
                // Wait until response received
            }

            Debug.WriteLine($"ResponseQueue Take:{response}");

            // Parse the response
            var result = response.Split(' ')[1];

            return result;
        }

        public async Task<IList<string>> LoadSymbolListAsync()
        {
            var result = new List<string>();
            var request = "LSL\r";
            await WriteToSocketAsync(request).ConfigureAwait(false);

            string response;
            while (!ResponseQueue.TryTake(out response))
            {
                // Wait until response received
            }

            Debug.WriteLine($"ResponseQueue Take:{response}");

            // Parse the response
            var echoCommand = response.Split(' ')[0];
            var dataField = response
                .Replace(echoCommand, string.Empty, StringComparison.InvariantCulture)
                .Replace("\r", string.Empty, StringComparison.InvariantCulture)
                .Split(' ', StringSplitOptions.RemoveEmptyEntries);

            foreach (var value in dataField)
            {
                result.Add(value);
            }

            return result;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~MetaTraderSocketsService()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
