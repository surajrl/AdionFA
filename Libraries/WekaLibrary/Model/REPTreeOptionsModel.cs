using System;

namespace WekaLibrary.Model
{
    public class REPTreeOptionsModel
    {
        public string BatchSize => "100";
        public bool Debug => false;
        public bool DoNotCheckCapabilities => false;
        public double InitialCount => 0.0;
        public int MaxDepth { get; set; }
        public double MinNum => 2.0;
        public double MinVarianceProp => 0.001;
        public bool NoPruning => true;
        public int NumDecimalPlaces { get; set; }
        public int NumFolds => 3;
        public int Seed { get; set; }
        public bool SpreadInitialCount => false;

        public REPTreeOptionsModel(int maxDepth = 10, int numDecimalPlaces = 8, int seed = 0)
        {
            MaxDepth = maxDepth;
            NumDecimalPlaces = numDecimalPlaces;
            Seed = seed;
        }
    }
}
