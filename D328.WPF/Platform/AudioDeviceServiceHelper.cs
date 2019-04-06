using NAudio.Wave;

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
    }
}
