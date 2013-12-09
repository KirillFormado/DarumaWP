using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarumaBLL.Domain
{
    public enum DarumaStatus
    {
        Empty,
        MakedWish,
        ExecutedWish,
        /// <summary>
        ///   After year since makes a wish, Daruma will burned
        /// </summary>
        TimeExpired
    }
}
