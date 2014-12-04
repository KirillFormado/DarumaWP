using System;
using System.Collections.Generic;
using System.Globalization;
using DarumaBLLPortable.Domain;
using DarumaResourcesPortable.Infrastructure;
using Microsoft.Phone.Info;
using DarumaBLLPortable.Common.Abstractions;

namespace DarumaDAL.WP
{
    public class QuotationSource : IQuotationSource
    {
        private LocalizationResourseStorage resourseStorage = new LocalizationResourseStorage();

        public KeyValuePair<string, string> GetQuotationSourse(DarumaWishTheme theme)
        {
            var resource = resourseStorage.GetByDarumsWishTheme(theme);
            var resolver = new RandomQuotationResolver(resource.GetResourceSet(CultureInfo.CurrentUICulture, true, true));

            var seed = DateTime.Now.Second +
                //change app memory usage determine type to comparable with WinRT api
                (int)Windows.System.MemoryManager.AppMemoryUsage;
            var quaotation = resolver.RenturnRandomQuotation(seed);

            return quaotation;
        }

        public string GetQuotationByKey(DarumaWishTheme theme, string key)
        {
            var resource = resourseStorage.GetByDarumsWishTheme(theme);
            var text = resource.GetString(key);
            return text;
        }
    }
}
