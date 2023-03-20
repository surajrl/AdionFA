using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Threading
{
    class Program
    {
        static async Task Main(string[] args)
        {
            List<int> values = new List<int>();

            var progress = new Progress<int>();
            progress.ProgressChanged += (senderOfProgressChanged, nextItem) => { 
                Console.WriteLine($"{senderOfProgressChanged}-{nextItem}-{values.Count()}"); 
            };

            await DownloadFileAsync(progress);

            Console.WriteLine("End");

            async Task DownloadFileAsync(IProgress<int> progress)
            {
                int i = 0;
                while (true)
                {
                    values.Add(i);
                    progress.Report(i);
                    Thread.Sleep(1000);
                    i++;
                }
            }


            Console.ReadKey();
        }

    }
}
