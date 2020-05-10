using D328.MultiRecording.Domain;
using System.Threading.Tasks;

namespace D328.MultiRecording.UseCase
{
    public interface IRecordingUseCase
    {
        Task StartAsync(AudioDevice inputAudioDevice);
        Task<Recording> StopAsync();
    }
}
