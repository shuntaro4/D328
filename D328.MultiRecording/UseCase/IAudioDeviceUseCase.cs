using D328.MultiRecording.Domain;
using System.Threading.Tasks;

namespace D328.MultiRecording.UseCase
{
    public interface IAudioDeviceUseCase
    {
        Task<AudioDeviceCollection> GetInputAudioDevicesAsync();

        Task<AudioDeviceCollection> GetOutputAudioDevicesAsync();
    }
}
