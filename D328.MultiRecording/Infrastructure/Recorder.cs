using D328.MultiRecording.Domain;
using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage;

namespace D328.MultiRecording.Infrastructure
{
    public class Recorder : IRecorder
    {
        private MediaCapture mediaCapture;
        private StorageFile audioStorageFile;

        public async Task StartAsync(StorageFile audioStorageFile)
        {
            if (audioStorageFile == null || File.Exists(audioStorageFile.Path))
            {
                throw new ArgumentException();
            }

            this.audioStorageFile = audioStorageFile;

            mediaCapture = new MediaCapture();

            var settings = new MediaCaptureInitializationSettings
            {
                StreamingCaptureMode = StreamingCaptureMode.Audio
            };
            await mediaCapture.InitializeAsync(settings);


            await mediaCapture.StartRecordToStorageFileAsync(
                MediaEncodingProfile.CreateMp3(AudioEncodingQuality.High),
                audioStorageFile);
        }

        public async Task<AudioFile> StopAsync()
        {
            if (mediaCapture == null)
            {
                throw new InvalidOperationException();
            }

            await mediaCapture.StopRecordAsync();

            mediaCapture.Dispose();
            mediaCapture = null;

            return new AudioFile(audioStorageFile.Path);
        }
    }
}
