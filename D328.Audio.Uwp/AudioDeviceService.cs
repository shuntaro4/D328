using D328.Application.Services;
using D328.Domain;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.Devices.Enumeration;

namespace D328.Audio.UWP
{
    public class AudioDeviceService : IAudioDeviceService
    {
        public List<AudioDevice> GetInputAudioDevices()
        {
            var devices = DeviceInformation.FindAllAsync(DeviceClass.AudioCapture).GetResults();
            return devices.Select(x => AudioDevice.CreateNewDevice(x.Id, x.Name)).ToList();
        }

        public List<AudioDevice> GetOutputAudioDevices()
        {
            var devices = DeviceInformation.FindAllAsync(DeviceClass.AudioRender).GetResults();
            return devices.Select(x => AudioDevice.CreateNewDevice(x.Id, x.Name)).ToList();
        }

        public AudioDevice GetSelectedInputAudioDevice(ObservableCollection<AudioDevice> devices)
        {
            throw new System.NotImplementedException();
        }

        public AudioDevice GetSelectedOutputAudioDevice(ObservableCollection<AudioDevice> devices)
        {
            throw new System.NotImplementedException();
        }
    }
}
