using D328.Platform;
using NAudio.CoreAudioApi;
using System.Collections.Generic;
using System.Linq;

namespace D328.WPF.Repository
{
    public class AudioDeviceWinRepository : IAudioDeviceService<MMDevice>
    {
        public List<MMDevice> GetAudioDevices()
        {
            var devices = new MMDeviceEnumerator().EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);
            return devices.ToList();
        }
    }
}
