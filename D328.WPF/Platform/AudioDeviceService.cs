using D328.Platform;
using NAudio.Wave;
using System.Collections.Generic;

namespace D328.WPF.Repository
{
    public class AudioDeviceService : IAudioDeviceService<WaveInCapabilities>
    {
        public List<WaveInCapabilities> GetInputAudioDevices()
        {
            var devices = new List<WaveInCapabilities>();
            for (var i = 0; i < WaveInEvent.DeviceCount; i++)
            {
                devices.Add(WaveInEvent.GetCapabilities(i));
            }
            return devices;
        }
    }
}