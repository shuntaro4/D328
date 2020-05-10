using D328.MultiRecording.Domain;
using System;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Media.Devices;

namespace D328.MultiRecording.UseCase
{
    public class AudioDeviceUseCase : IAudioDeviceUseCase
    {
        public async Task<AudioDeviceCollection> GetInputAudioDevicesAsync()
        {
            var result = new AudioDeviceCollection();

            var deviceCollection = await DeviceInformation.FindAllAsync(MediaDevice.GetAudioCaptureSelector());
            var defaultDevice = await DeviceInformation.CreateFromIdAsync(MediaDevice.GetDefaultAudioCaptureId(AudioDeviceRole.Default));
            foreach (var device in deviceCollection)
            {
                result.Add(new AudioDevice(device, device.Id == defaultDevice.Id));
            }

            return result;
        }

        public async Task<AudioDeviceCollection> GetOutputAudioDevicesAsync()
        {
            var result = new AudioDeviceCollection();

            var deviceCollection = await DeviceInformation.FindAllAsync(MediaDevice.GetAudioRenderSelector());
            var defaultDevice = await DeviceInformation.CreateFromIdAsync(MediaDevice.GetDefaultAudioRenderId(AudioDeviceRole.Default));
            foreach (var device in deviceCollection)
            {
                result.Add(new AudioDevice(device, device.Id == defaultDevice.Id));
            }

            return result;
        }
    }
}
