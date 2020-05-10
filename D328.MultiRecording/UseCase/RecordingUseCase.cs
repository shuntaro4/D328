using D328.MultiRecording.Domain;
using System.Threading.Tasks;

namespace D328.MultiRecording.UseCase
{
    public class RecordingUseCase : IRecordingUseCase
    {
        private readonly IFileCreator fileCreator;
        private readonly IRecorder recorder;

        public RecordingUseCase(IFileCreator fileCreator, IRecorder recorder)
        {
            this.fileCreator = fileCreator;
            this.recorder = recorder;
        }

        public async Task StartAsync()
        {
            var audioStorageFile = await fileCreator.CreateAudioFileAsync();
            await recorder.StartAsync(audioStorageFile);
        }

        public async Task<Recording> StopAsync()
        {
            var user = new User(new UserName("hoge")); // todo 仮
            var audioFile = await recorder.StopAsync();
            return Recording.CreateNew(user, audioFile);
        }
    }
}
