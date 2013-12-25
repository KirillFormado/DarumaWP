using System.Collections.Generic;
using Daruma.Resources;
using DarumaBLL.Domain;

namespace Daruma.Infrastructure.Converters
{
    public class DarumaWishThemeToStringConverter : EnumToStringConverter<DarumaWishTheme>
    {
        public DarumaWishThemeToStringConverter()
        {
            _dict = new Dictionary<DarumaWishTheme, string>
            {
                {DarumaWishTheme.Friendship, AppResources.Friendship},
                {DarumaWishTheme.Funny, AppResources.Funny},
                {DarumaWishTheme.Health, AppResources.Health},
                {DarumaWishTheme.Love, AppResources.Love},
                {DarumaWishTheme.Luck, AppResources.Luck},
                {DarumaWishTheme.Rich, AppResources.Rich}
            };
        }
    }
}
