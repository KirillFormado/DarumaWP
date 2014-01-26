using System.Collections.Generic;
using DarumaBLLPortable.Domain;
using DarumaResourcesPortable.LocalizationResources;

namespace DarumaResourcesPortable.Infrastructure
{
    public class DarumaWishThemeToLocalizationString
    {
        private Dictionary<DarumaWishTheme, string> _dict;

        public DarumaWishThemeToLocalizationString()
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

        public Dictionary<DarumaWishTheme, string> Dictionary
        {
            get
            {
                return _dict;
            }
        }

        public string GetLocalizationByTheme(DarumaWishTheme theme)
        {
            return _dict[theme];
        }
    }
}
