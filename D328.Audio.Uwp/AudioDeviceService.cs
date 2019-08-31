using D328.Application.Services;
using D328.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;

namespace D328.Audio.UWP
{
    public class AudioDeviceService : IAudioDeviceServiceAsync
    {
        public async Task<List<AudioDevice>> GetInputAudioDevicesAsync()
        {
            var devices = await DeviceInformation.FindAllAsync(DeviceClass.AudioCapture);
            return devices.Select(x => AudioDevice.CreateNewDevice(x.Id, x.Name)).ToList();
        }

        public async Task<List<AudioDevice>> GetOutputAudioDevicesAsync()
        {
            var devices = await DeviceInformation.FindAllAsync(DeviceClass.AudioRender);
            return devices.Select(x => AudioDevice.CreateNewDevice(x.Id, x.Name)).ToList();
        }

        public async Task<AudioDevice> GetSelectedInputAudioDeviceAsync(ObservableCollection<AudioDevice> devices)
        {
            var defaltDevice = (await DeviceInformation.FindAllAsync(DeviceClass.AudioCapture))
                .Where(x => x.IsDefault)
                .FirstOrDefault();
            return devices.FirstOrDefault(x => x.Id == defaltDevice.Id);
        }

        public async Task<AudioDevice> GetSelectedOutputAudioDeviceAsync(ObservableCollection<AudioDevice> devices)
        {
            var defaltDevice = (await DeviceInformation.FindAllAsync(DeviceClass.AudioRender))
                .Where(x => x.IsDefault)
                .FirstOrDefault();
            return devices.FirstOrDefault(x => x.Id == defaltDevice.Id);
        }
    }
}
