using System.Collections.Generic;
using Daruma.Resources;
using DarumaBLL.Domain;

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
