using D328.Platform;
using NAudio.CoreAudioApi;
using System.Collections.Generic;
using System.Linq;

namespace D328.WPF.Repository
{
    public class AudioDeviceService : IAudioDeviceService<MMDevice>
    {
        private readonly MMDeviceEnumerator _emurator;

        public AudioDeviceService()
        {
            _emurator = new MMDeviceEnumerator();
        }

        public List<MMDevice> GetInputAudioDevices()
        {
            return _emurator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active).ToList();
        }

        public MMDevice GetDefaultInputAudioDevice()
        {
            return _emurator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Console);
        }

        public List<MMDevice> GetOutputAudioDevices()
        {
            return _emurator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active).ToList();
        }

        public MMDevice GetDefaultOutputAudioDevice()
        {
            return _emurator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console);
        }
    }
}