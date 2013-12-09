using Daruma.Infrastructure.Storages;
using DarumaBLL.Common.Abstractions;
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
        }

        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }
    }
}
