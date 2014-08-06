using System.Collections.Generic;
using DarumaBLLPortable.Domain;

namespace DarumaBLLPortable.Common.Abstractions
{
    public interface IQuotationSource
    {
        KeyValuePair<string, string> GetQuotationSourse(DarumaWishTheme theme);
        string GetQuotationByKey(DarumaWishTheme theme, string key);
    }
}