using System;
using System.Globalization;
using System.Windows.Data;

namespace D328.Presentation.Converters
{
    public class InverseBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var target = value as bool? ?? false;
            return !target;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var target = value as bool? ?? false;
            return !target;
        }
    }
}
