using Daruma.Infrastructure.Localization;
using DarumaBLL.Domain;

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
