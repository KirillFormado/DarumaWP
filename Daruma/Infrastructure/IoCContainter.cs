using DarumaBLLPortable.Common.Abstractions;
using DarumaDAL.WP;
using DarumaDAL.WP.Abstraction;
using DarumaDAL.WP.Storages;
using Ninject;

namespace Daruma.Infrastructure
{
    internal class IoCContainter
    {
        private static readonly IKernel Kernel = new StandardKernel();

        static IoCContainter()
        {
            Kernel.Bind<IDarumaStorage>().To<DarumaStorage>();
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
