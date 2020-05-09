using D328.MultiRecording.Domain;
using System;
using System.Threading.Tasks;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;

namespace D328.MultiRecording.UseCase
{
    public class RecordingUseCase : IRecordingUseCase
    {
        private readonly IFileCreator fileCreator;

        private MediaCapture mediaCapture;
        private string audioFilePath;

        public RecordingUseCase(IFileCreator fileCreator)
        {
            this.fileCreator = fileCreator;
        }

        public async Task StartAsync()
        {
            mediaCapture = new MediaCapture();

            var settings = new MediaCaptureInitializationSettings
            {
                StreamingCaptureMode = StreamingCaptureMode.Audio
            };
            await mediaCapture.InitializeAsync(settings);

            var audioStorageFile = await fileCreator.CreateAudioFileAsync();
            audioFilePath = audioStorageFile.Path;

            await mediaCapture.StartRecordToStorageFileAsync(
                MediaEncodingProfile.CreateMp3(AudioEncodingQuality.High),
                audioStorageFile);
        }

        public async Task<Recording> StopAsync()
        {
            await mediaCapture.StopRecordAsync();

            var user = new User(new UserName("hoge"));
            var audioFile = new AudioFile(audioFilePath);
            return Recording.CreateNew(user, audioFile);
        }
    }
}
