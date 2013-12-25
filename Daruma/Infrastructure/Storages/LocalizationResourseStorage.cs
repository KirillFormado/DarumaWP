using System;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using Daruma.Resources;
using DarumaBLL.Domain;

namespace Daruma.Infrastructure.Storages
{
    public class LocalizationResourseStorage
    {
        private Dictionary<DarumaWishTheme, ResourceManager> _dict;

        public LocalizationResourseStorage()
        {
            _dict = new Dictionary<DarumaWishTheme, ResourceManager>
            {
                {DarumaWishTheme.NoSet, CitationLocalizationResources.ResourceManager},
                {DarumaWishTheme.Friendship, Funy.ResourceManager},
                {DarumaWishTheme.Funny, Funy.ResourceManager},
                {DarumaWishTheme.Health, Health.ResourceManager},
                {DarumaWishTheme.Love, Love.ResourceManager},
                {DarumaWishTheme.Luck, Luck.ResourceManager},
                {DarumaWishTheme.Rich, Rich.ResourceManager}
            };
        }

        public ResourceSet GetByDarumsWishTheme(DarumaWishTheme theme)
        {
            var resource = _dict[theme];
            if (resource != null)
            {
                var resourceSet = resource.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
                return resourceSet;
            }
            return null;
        }
    }
}
