using D328.Application.Services;
using D328.Domain;
using NAudio.CoreAudioApi;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace D328.Audio.Windows
{
    public class AudioDeviceService : IAudioDeviceService
    {
        private readonly MMDeviceEnumerator _emurator;


        public AudioDeviceService()
        {
            _emurator = new MMDeviceEnumerator();
        }

        public List<AudioDevice> GetInputAudioDevices()
        {
            return _emurator
                .EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active)
                .Select(x => AudioDevice.CreateNewDevice(x.ID, x.FriendlyName))
                .ToList();
        }

        public AudioDevice GetSelectedInputAudioDevice(ObservableCollection<AudioDevice> devices)
        {
            var mmdevice = _emurator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Console);
            return devices.FirstOrDefault(c => c.Id == mmdevice?.ID);
        }

        public List<AudioDevice> GetOutputAudioDevices()
        {
            return _emurator
                .EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active)
                .Select(x => AudioDevice.CreateNewDevice(x.ID, x.FriendlyName))
                .ToList();
        }

        public AudioDevice GetSelectedOutputAudioDevice(ObservableCollection<AudioDevice> devices)
        {
            var mmdevice = _emurator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console);
            return devices.FirstOrDefault(c => c.Id == mmdevice?.ID);
        }

        internal MMDevice InputAudioDeviceToMMDevice(AudioDevice inputDevice)
        {
            if (inputDevice == null)
            {
                return null;
            }
            var devices = _emurator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active);
            return devices.FirstOrDefault(c => c.ID == inputDevice?.Id);
        }
    }
}
