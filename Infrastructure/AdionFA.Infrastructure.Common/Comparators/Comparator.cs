using KellermanSoftware.CompareNetObjects;
using System;
using System.Collections.Generic;

namespace AdionFA.Infrastructure.Common.Comparators
{
    public class Comparator : IComparator
    {
        public readonly CompareLogic compareLogic;
        public Comparator()
        {
            compareLogic = new CompareLogic();
        }
        public bool LogicAssert(Object obj1, Object obj2, IList<string> MembersToInclude = null)
        {
            compareLogic.Config.Reset();
            if (MembersToInclude?.Count > 0)
            {
                compareLogic.Config.MembersToInclude.AddRange(MembersToInclude);
            }
            
            return compareLogic.Compare(obj1, obj2).AreEqual;
        }
    }
}
