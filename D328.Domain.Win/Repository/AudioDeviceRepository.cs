using NAudio.CoreAudioApi;
using System.Collections.Generic;
using System.Linq;

namespace D328.Domain.Win.Repository
{
    public class AudioDeviceRepository : IAudioDeviceRepository<MMDevice>
    {
        public List<MMDevice> GetAudioDevices()
        {
            var devices = new MMDeviceEnumerator().EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);
            return devices.ToList();
        }
    }
}
