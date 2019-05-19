using D328.Domain.Enum;
using System;
using System.Globalization;
using System.Windows.Data;

namespace D328.WPF.Converter
{
    public class AudioModeToRecordingStopButtonEnabledConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var lineMode = value as AudioMode?;
            if (lineMode == null)
            {
                return false;
            }
            return lineMode == AudioMode.Recording || lineMode == AudioMode.Pause;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
