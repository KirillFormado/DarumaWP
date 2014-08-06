using DarumaDAL.WP.Infrastructure;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Daruma.Infrastructure.Converters
{
    public class DarumsIdToUrlConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var guid = Guid.Parse(value.ToString());
            var url = ViewUrlRouter.DarumaShakeViewByIdUrl(guid);
            return url;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new Guid(value.ToString());
        }
    }
}
