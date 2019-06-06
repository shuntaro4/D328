using D328.Domain.Enum;
using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;

namespace D328.WPF.Converter
{
    public class AudioModeToPlayButtonEnabledConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length != 2)
            {
                return false;
            }

            var audioMode = values[0] as AudioMode?;
            var audioPath = values[1] as string;
            if (audioMode == null || !File.Exists(audioPath))
            {
                return false;
            }
            return audioMode == AudioMode.Pause || audioMode == AudioMode.Normal;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
