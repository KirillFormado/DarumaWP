using System.Collections.Generic;
using DarumaBLLPortable.Domain;

namespace DarumaDAL.WP.Abstraction
{
    public interface IQuotationSource
    {
        KeyValuePair<string, string> GetQuotationSourse(DarumaWishTheme theme);
        string GetQuotationByKey(DarumaWishTheme theme, string key);
    }
}