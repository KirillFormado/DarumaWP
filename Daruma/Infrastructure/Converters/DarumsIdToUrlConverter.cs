using System;
using System.Globalization;
using System.Windows.Data;

namespace Daruma.Infrastructure.Converters
{
    public class DarumsIdToUrlConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var url = string.Format("{0}?id={1}",
                ViewUrlRouter.DarumaShakeViewUrl,
                value);
            return url;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new Guid(value.ToString());
        }
    }
}
