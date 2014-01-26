using System.Collections.Generic;
using DarumaBLLPortable.Domain;
using DarumaResourcesPortable.LocalizationResources;

namespace Daruma.Infrastructure.Converters
{
    public class DarumsStatusToStringConverter : EnumToStringConverter<DarumaStatus>
    {
        public DarumsStatusToStringConverter()
        {
            _dict = new Dictionary<DarumaStatus, string>
            {
                {DarumaStatus.MakedWish, AppResources.Maked},
                {DarumaStatus.ExecutedWish, AppResources.Executed},
                {DarumaStatus.TimeExpired, AppResources.Expired},
                {DarumaStatus.Empty, AppResources.Empty}
            };
        }
    }
}
