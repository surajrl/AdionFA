using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using AdionFA.Infrastructure.Enums;
using System.IO;
using System.Diagnostics;

namespace UnitTest.Core.GenericHost
{
    public class MetaTraderConnection
    {
        private readonly TcpClient _tcpClient = new();
        public async Task TestTcpClient()
        {
            try
            {
                _tcpClient.Connect("192.168.50.137", 5000);
            }
            catch (SocketException)
            {
                Console.WriteLine("Server is closed");
                return;
            }

            var portReadTask = Task.Run(() => PortReadAsync());

            while (_tcpClient.Connected)
            {
                var symbol = "EURUSD..";
                var timeframe = TimeframeEnum.H4;
                var start = "2020.01.01 00:00";
                var end = "2023.01.01 00:00";
                var downloadMsg = symbol + "," + ((int)timeframe) + "," + start + "," + end;

                byte[] msg_bytes = Encoding.ASCII.GetBytes(downloadMsg);

                Console.WriteLine("DISCONNECT?");
                string close = Console.ReadLine();
                if (close == "Y" || close == "y")
                {
                    _tcpClient.Close();
                    return;
                }

                Console.WriteLine("PRESS ANY KEY TO SEND DOWNLOAD REQUEST");
                Console.ReadLine();
                
                try
                {
                    await _tcpClient.GetStream().WriteAsync(msg_bytes);
                    Console.WriteLine($"WriteAsync: {downloadMsg}");
                }
                catch (IOException)
                {
                    await Console.Out.WriteLineAsync("The server has been closed");
                    _tcpClient.Close();
                    return;
                }
            }

            _tcpClient.Close();
        }

        public async Task PortReadAsync()
        {
            while (_tcpClient.Connected)
            {
                var buffer = new byte[1024];
                
                try
                {
                    var recvBytes = await _tcpClient.GetStream().ReadAsync(buffer);
                    var recvData = new byte[recvBytes];
                    Array.Copy(buffer, recvData, recvBytes);
                    var recvMsg = Encoding.ASCII.GetString(recvData);
                    await Console.Out.WriteLineAsync($"ReadAsync:{recvMsg}");
                }
                catch (IOException)
                {
                    await Console.Out.WriteLineAsync("PortReadAsync: The server has been closed");
                    _tcpClient.Close();
                    return;
                }
            }
        }
    }

    public class Program
    {

        public async static Task Main(string[] args)
        {
            MetaTraderConnection connection = new MetaTraderConnection();
            await connection.TestTcpClient();
            return;
        }

    }
}
