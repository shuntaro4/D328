using System;
using System.Globalization;
using System.Windows.Data;
using static D328.WPF.ViewModels.MainWindowViewModel;

namespace D328.WPF.Converter
{
    public class WindowModeToRecordingStopButtonEnabledConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var windowMode = value as MainWindowMode?;
            if (windowMode == null)
            {
                return false;
            }
            return windowMode == MainWindowMode.Recording || windowMode == MainWindowMode.Pause;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
