using System.Collections.Generic;

namespace AdionFA.Infrastructure.Common.Weka.Model
{
    public class AssemblyNodeModel
    {
        public REPTreeNodeModel ParentNode { get; set; }
        public List<REPTreeNodeModel> ChildNodes { get; set; }
    }
}
