using D328.Domain.Repository;
using NAudio.CoreAudioApi;
using System.Collections.Generic;
using System.Linq;

namespace D328.WPF.Repository
{
    public class AudioDeviceWinRepository : IAudioDeviceRepository<MMDevice>
    {
        public List<MMDevice> GetAudioDevices()
        {
            var devices = new MMDeviceEnumerator().EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);
            return devices.ToList();
        }
    }
}
