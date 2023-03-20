using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;

namespace MLDotNET
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //string time = "01:00:00";
            //double seconds = TimeSpan.Parse(time).TotalSeconds;
            //Console.WriteLine(seconds);
            ZeroMQ();
        }

        public async static void ZeroMQ()
        {
            try
            {
                using (var requestSocket = new RequestSocket(">tcp://localhost:5555"))
                {
                    int count = 0;
                    while (count < 10000)
                    {
                        Console.WriteLine("requestSocket : Sending Order");

                        var msgObj = new ZmqMsgRequestModel
                        {
                            UUID = System.Guid.NewGuid().ToString(),
                            SYMBOL = "EURUSD",
                            Request = "TRADE",
                            OrderType = string.Empty,//"SELL",
                            Action = "CLOSE_ALL"
                        };

                        string msgReq = string.Join("|",msgObj.GetType().GetProperties()
                        .Where(p => Attribute.IsDefined(p, typeof(OrderAttribute)))
                        .OrderBy(p => ((OrderAttribute)p.GetCustomAttributes(typeof(OrderAttribute), false).Single()).Order)
                        .Select(p => p.GetValue(msgObj)).ToList());

                        Console.WriteLine(msgReq);
                    
                        requestSocket.SendFrame(msgReq);
                        string message = requestSocket.ReceiveFrameString();
                        Console.WriteLine(string.Format("requestSocket : Received '{0}'", message));
                        count++;
                        Thread.Sleep(1000);
                    }
                }
                }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
            /*
            var workerA = Task.Factory.StartNew(() => {
                using (var receiver = new PullSocket(">tcp://localhost:5556"))
                {
                    while (true)
                    {
                        string workload = receiver.ReceiveFrameString();
                        Console.WriteLine("A" + workload);
                    }
                }
            });

            var workerB = Task.Factory.StartNew(() => {
                using (var requestSocket = new RequestSocket(">tcp://localhost:5555"))
                {
                    int count = 0;
                    while (count < 10000)
                    {
                        console("requestSocket : Sending 'Hello'");
                        requestSocket.SendFrame("Hello");
                        string message = requestSocket.ReceiveFrameString();
                        console(string.Format("requestSocket : Received '{0}'", message));
                        count++;
                        Thread.Sleep(1000);
                    }
                }
            });
            
            await Task.WhenAll(workerB);*/
        }

        public static void console(string msg) 
        {
            Console.WriteLine(msg);
        }

        public static bool PortInUse(int port)
        {
            bool inUse = false;

            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            TcpConnectionInformation[] ipEndPoints = ipProperties.GetActiveTcpConnections();


            foreach (TcpConnectionInformation endPoint in ipEndPoints)
            {
                if (endPoint.LocalEndPoint.Port == port)
                {
                    inUse = true;
                    break;
                }
            }

            return inUse;
        }
    }
}
