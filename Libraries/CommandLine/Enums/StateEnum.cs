using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestDoctors.DirectInsurance.Infrastructure.Enums
{
    public enum StateEnum
    {
        Unchanged = 0,
        Added,
        Modified,
        Deleted,
        /// <summary>
        /// Used in ProducConf for validation and visual purpose, so you can use it, too.
        /// </summary>
        Closed
    }
}
