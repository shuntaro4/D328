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
            return new AudioDeviceCollection(await DeviceInformation.FindAllAsync(MediaDevice.GetAudioCaptureSelector()));
        }

        public async Task<AudioDeviceCollection> GetOutputAudioDevicesAsync()
        {
            return new AudioDeviceCollection(await DeviceInformation.FindAllAsync(MediaDevice.GetAudioRenderSelector()));
        }
    }
}
