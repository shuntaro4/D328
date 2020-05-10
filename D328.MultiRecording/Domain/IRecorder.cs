using System.Threading.Tasks;
using Windows.Storage;

namespace D328.MultiRecording.Domain
{
    public interface IRecorder
    {
        Task StartAsync(AudioDevice inputAudioDevice, StorageFile audioStorageFile);
        Task<AudioFile> StopAsync();
    }
}
