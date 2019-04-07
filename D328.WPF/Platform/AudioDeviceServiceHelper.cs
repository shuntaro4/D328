using NAudio.Wave;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace D328.WPF.Platform
{
    public class AudioDeviceServiceHelper
    {
        public AudioDeviceServiceHelper()
        {

        }

        public int GetInputAudioDeviceNumber(WaveInCapabilities inputAudioDevice)
        {
            for (var i = 0; i < WaveInEvent.DeviceCount; i++)
            {
                var device = WaveInEvent.GetCapabilities(i);
                if (inputAudioDevice.ProductGuid == device.ProductGuid)
                {
                    return i;
                }
            }
            return 0;
        }

        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        public ImageSource ImageToImageSource(Image source)
        {
            var drawingBitmap = (new Bitmap(source)).GetHbitmap();
            try
            {
                return Imaging.CreateBitmapSourceFromHBitmap(
                    drawingBitmap,
                    IntPtr.Zero,
                    Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions());
            }
            finally
            {
                DeleteObject(drawingBitmap);
            }
        }
    }
}
