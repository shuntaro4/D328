using D328.MultiRecording.Domain;
using System.Threading.Tasks;

namespace D328.MultiRecording.UseCase
{
    public class RecordingUseCase : IRecordingUseCase
    {
        private readonly IFileCreator fileCreator;
        private readonly IRecorder recorder;
        private readonly IUserRepository userRepository;

        public RecordingUseCase(IFileCreator fileCreator, IRecorder recorder, IUserRepository userRepository)
        {
            this.fileCreator = fileCreator;
            this.recorder = recorder;
            this.userRepository = userRepository;
        }

        public async Task StartAsync(AudioDevice inputAudioDevice)
        {
            var audioStorageFile = await fileCreator.CreateAudioFileAsync();
            await recorder.StartAsync(inputAudioDevice, audioStorageFile);
        }

        public async Task<Recording> StopAsync()
        {
            var user = userRepository.GetCurrentUser();
            var audioFile = await recorder.StopAsync();
            return Recording.CreateNew(user, audioFile);
        }
    }
}
