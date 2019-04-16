using System;
using System.Globalization;
using System.Windows.Data;
using static D328.WPF.ViewModels.MainWindowViewModel;

namespace D328.WPF.Converter
{
    public class WindowModeToRecordingButtonEnabledConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var windowMode = value as MainWindowMode?;
            if (windowMode == null)
            {
                return false;
            }
            return windowMode == MainWindowMode.Normal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
