using DarumaBLLPortable.ApplicationServices.Abstractions;
using DarumaBLLPortable.ApplicationServices.Services;
using DarumaBLLPortable.Common.Abstractions;
using DarumaDAL.WP.Storages;
using Ninject;

namespace DarumaDAL.WP.Infrastructure
{
    public class IoCContainter
    {
        private static readonly IKernel Kernel = new StandardKernel();

        static IoCContainter()
        {
            Kernel.Bind<IDarumaStorage>().To<DarumaStorage>();
            Kernel.Bind<IFavoritStorage>().To<FavoritStorage>();
            Kernel.Bind<IDarumaApplicationService>().To<DarumaApplicationService>();
            Kernel.Bind<IFavoritApplicationService>().To<FavoritApplicationService>();
            Kernel.Bind<ISettingsStorage>().To<SettingsStorage>();
            Kernel.Bind<IDarumaImageUriResolver>().To<DarumaImageUriResolver>();
            Kernel.Bind<IQuotationSource>().To<QuotationSource>();
        }

        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }
    }
}
