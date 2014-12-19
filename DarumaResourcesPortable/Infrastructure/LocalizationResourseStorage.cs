using System.Collections.Generic;
using System.Resources;
using DarumaBLLPortable.Domain;
using DarumaResourcesPortable.LocalizationResources;

namespace DarumaResourcesPortable.Infrastructure
{
    public class LocalizationResourseStorage
    {
        private Dictionary<DarumaWishTheme, ResourceManager> _dict;

        public LocalizationResourseStorage()
        {
            _dict = new Dictionary<DarumaWishTheme, ResourceManager>
            {
                {DarumaWishTheme.NoSet, CitationLocalizationResources.ResourceManager},
                {DarumaWishTheme.Friendship, Friendship2.ResourceManager},
                {DarumaWishTheme.Funny, Funy.ResourceManager},
                {DarumaWishTheme.Health, Health2.ResourceManager},
                {DarumaWishTheme.Love, Love2.ResourceManager},
                {DarumaWishTheme.Luck, Luck2.ResourceManager},
                {DarumaWishTheme.Rich, Rich2.ResourceManager}
            };
        }

        public ResourceManager GetByDarumsWishTheme(DarumaWishTheme theme)
        {
            if (_dict.ContainsKey(theme))
            {
                var resource = _dict[theme];
                if (resource != null)
                {
                    //var resourceSet = resource.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
                    return resource;
                }
            }
            return null;
        }
    }
}
