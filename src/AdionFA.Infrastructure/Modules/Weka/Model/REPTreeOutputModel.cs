using System;
using System.Collections.Generic;

namespace AdionFA.Infrastructure.Weka.Model
{
    public class REPTreeOutputModel
    {
        public int Seed { get; set; }
        public string TreeOutput { get; set; }
        public List<REPTreeNodeModel> NodeOutput { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
