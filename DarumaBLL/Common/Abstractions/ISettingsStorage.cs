using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarumaBLL.Common.Abstractions
{
    public interface ISettingsStorage
    {
        bool IsContains(string key);
        void Add(string key, object item);
    }
}
