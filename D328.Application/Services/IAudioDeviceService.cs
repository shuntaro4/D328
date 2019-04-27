using D328.Domain;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace D328.Application.Services
{
    public interface IAudioDeviceService
    {
        List<AudioDevice> GetInputAudioDevices();

        AudioDevice GetSelectedInputAudioDevice(ObservableCollection<AudioDevice> devices);

        List<AudioDevice> GetOutputAudioDevices();

        AudioDevice GetSelectedOutputAudioDevice(ObservableCollection<AudioDevice> devices);
    }
}