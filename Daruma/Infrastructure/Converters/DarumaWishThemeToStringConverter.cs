using DarumaBLLPortable.Domain;
using DarumaResourcesPortable.Infrastructure;

namespace Daruma.Infrastructure.Converters
{
    public class DarumaWishThemeToStringConverter : EnumToStringConverter<DarumaWishTheme>
    {
        public DarumaWishThemeToStringConverter()
        {
            _dict = new DarumaWishThemeToLocalizationString().Dictionary;
        }
    }
}
