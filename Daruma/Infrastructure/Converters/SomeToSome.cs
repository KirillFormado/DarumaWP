using System;
using System.Globalization;
using System.Windows.Data;

namespace Daruma.Infrastructure.Converters
{
    public class SomeToSome : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = value;
            return v;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
