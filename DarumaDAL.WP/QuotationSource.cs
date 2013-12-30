using System;
using System.Globalization;
using DarumaBLLPortable.Domain;
using DarumaDAL.WP.Abstraction;
using DarumaResourcesPortable.Infrastructure;
using Microsoft.Phone.Info;

namespace DarumaDAL.WP
{
    public class QuotationSource : IQuotationSource
    {
        public string GetCitationSourse(DarumaWishTheme theme)
        {
            var resourseStorage = new LocalizationResourseStorage();
            var resource = resourseStorage.GetByDarumsWishTheme(theme);
            var resolver = new RandomCitationResolver(resource.GetResourceSet(CultureInfo.CurrentUICulture, true, true));

            var seed = DateTime.Now.Second + (int)DeviceStatus.ApplicationCurrentMemoryUsage;
            var quaotation = resolver.RenturnRandomCitation(seed);

            return quaotation;
        }
    }
}
