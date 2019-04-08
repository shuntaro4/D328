using D328.Platform;
using NAudio.CoreAudioApi;
using System.Collections.Generic;
using System.Linq;

namespace D328.WPF.Repository
{
    public class AudioDeviceService : IAudioDeviceService<MMDevice>
    {
        public List<MMDevice> GetInputAudioDevices()
        {
            var enumerator = new MMDeviceEnumerator();
            return enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active).ToList();
        }
    }
}