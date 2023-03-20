using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WekaLibrary.ApiRestClient;
using WekaLibrary.Model;

namespace WekaLibrary
{
    public class WekaLib
    {
        static async Task Main(string[] args)
        {
            string sourcePath = @"C:\Users\guerr\OneDrive\Documents\FA.Workspace\Projects\EUR.USD.1h\Extractions\\WithoutSchedule\Ext-010957.2021.01.17.13.37.25.csv";

            #region Old
            /*
            var r = new Random();
            var classifier = new REPTreeClassifier(new REPTreeOptionsModel(), 1);

            await classifier.BuildClassifier(sourcePath);

            Console.BufferHeight = 10000;
            foreach (var model in classifier.Output)
            {
                foreach (var node in model.NodeOutput)
                {
                    Console.WriteLine($"{model.Seed}--{node.Node}");
                }
            }*/
            #endregion

            var httpClient = new WekaApiClient();
            HttpOperationResponse<IList<REPTreeOutputModel>> response = 
                await httpClient.GetREPTreeClassifierWithHttpMessagesAsync(sourcePath, instances: 5, ratio: 1.5, total: 300);
            if (response.Response.IsSuccessStatusCode)
            {
                IList<REPTreeOutputModel> result = response.Body;

                foreach (var n in result.SelectMany(m => m.NodeOutput.Where(n => n.Winner)))
                {
                    foreach (var s in n.Node)
                    {
                        Console.WriteLine(s);
                    }
                    Console.WriteLine($"{n.Label}-{n.Winner}-{n.RatioMax}-{n.Total}");
                }
            }
        }


        
    }
}
