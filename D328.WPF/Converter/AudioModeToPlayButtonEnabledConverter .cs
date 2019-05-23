using D328.Domain.Enum;
using System;
using System.Globalization;
using System.Windows.Data;

namespace D328.WPF.Converter
{
    public class AudioModeToPlayButtonEnabledConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var audioMode = value as AudioMode?;
            if (audioMode == null)
            {
                return false;
            }
            return audioMode == AudioMode.Pause || audioMode == AudioMode.Normal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
