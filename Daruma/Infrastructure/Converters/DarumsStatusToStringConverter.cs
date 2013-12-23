using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using Daruma.Resources;
using DarumaBLL.Domain;

namespace Daruma.Infrastructure.Converters
{
    public class DarumsStatusToStringConverter : IValueConverter
    {
        private Dictionary<DarumaStatus, string> _dict;

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

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var stat = (DarumaStatus) value;
            return _dict[stat];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return _dict.FirstOrDefault(d => d.Value == value.ToString()).Key;
        }
    }
}
