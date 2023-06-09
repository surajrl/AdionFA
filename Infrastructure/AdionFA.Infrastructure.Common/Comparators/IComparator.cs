﻿using System;
using System.Collections.Generic;

namespace AdionFA.Infrastructure.Common.Comparators
{
    public interface IComparator
    {
        bool LogicAssert(Object obj1, Object obj2, IList<string> TypesToIgnore = null);
    }
}
