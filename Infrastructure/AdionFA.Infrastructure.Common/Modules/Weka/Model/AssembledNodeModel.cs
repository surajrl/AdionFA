using System.Collections.Generic;

namespace AdionFA.Infrastructure.Common.Weka.Model
{
    public class AssembledNodeModel
    {
        public REPTreeNodeModel ParentNode { get; set; }
        public List<REPTreeNodeModel> ChildNodes { get; set; }
    }
}
