using D328.Domain;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace D328.Application.Services
{
    public interface IAudioDeviceService
    {
        List<AudioDevice> GetInputAudioDevices();

        AudioDevice GetSelectedInputAudioDevice(ObservableCollection<AudioDevice> devices);

        List<AudioDevice> GetOutputAudioDevices();

        AudioDevice GetSelectedOutputAudioDevice(ObservableCollection<AudioDevice> devices);
    }

    public interface IAudioDeviceServiceAsync
    {
        Task<List<AudioDevice>> GetInputAudioDevicesAsync();

        Task<AudioDevice> GetSelectedInputAudioDeviceAsync(ObservableCollection<AudioDevice> devices);

        Task<List<AudioDevice>> GetOutputAudioDevicesAsync();

        Task<AudioDevice> GetSelectedOutputAudioDeviceAsync(ObservableCollection<AudioDevice> devices);
    }
}