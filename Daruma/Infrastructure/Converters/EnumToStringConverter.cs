using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Daruma.Infrastructure.Converters
{
    public class EnumToStringConverter<T> : IValueConverter
    {
        protected Dictionary<T, string> _dict;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var stat = (T)value;
            return _dict[stat];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return _dict.FirstOrDefault(d => d.Value == value.ToString()).Key;
        }
    }
}