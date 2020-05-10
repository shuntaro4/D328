using D328.MultiRecording.Domain;
using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Media.Audio;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Media.Render;
using Windows.Media.Transcoding;
using Windows.Storage;

namespace D328.MultiRecording.Infrastructure
{
    public class Recorder : IRecorder
    {
        private StorageFile audioStorageFile;
        private AudioGraph audioGraph;
        private AudioFileOutputNode audioFileOutputNode;

        public async Task StartAsync(AudioDevice inputAudioDevice, StorageFile audioStorageFile)
        {
            if (inputAudioDevice == null || audioStorageFile == null || !File.Exists(audioStorageFile.Path))
            {
                throw new ArgumentException();
            }

            this.audioStorageFile = audioStorageFile;

            var settings = new AudioGraphSettings(AudioRenderCategory.Media);
            var graphCreateResult = await AudioGraph.CreateAsync(settings);
            if (graphCreateResult.Status != AudioGraphCreationStatus.Success)
            {
                throw new Exception($"Failed to create AudioGraph: {graphCreateResult.Status}");
            }

            audioGraph = graphCreateResult.Graph;

            var inputNodeResult = await audioGraph.CreateDeviceInputNodeAsync(
                MediaCategory.Media,
                audioGraph.EncodingProperties,
                inputAudioDevice.Value);
            if (inputNodeResult.Status != AudioDeviceNodeCreationStatus.Success)
            {
                throw new Exception($"Failed to create input AudioDeviceNode: {inputNodeResult.Status}");
            }
            var inputNode = inputNodeResult.DeviceInputNode;

            var mediaEncodingProfile = MediaEncodingProfile.CreateMp3(AudioEncodingQuality.High);
            var fileOutputNodeResult = await audioGraph.CreateFileOutputNodeAsync(this.audioStorageFile, mediaEncodingProfile);
            if (fileOutputNodeResult.Status != AudioFileNodeCreationStatus.Success)
            {
                throw new Exception($"Failed to create output AudioFileNode: {fileOutputNodeResult.Status}");
            }
            audioFileOutputNode = fileOutputNodeResult.FileOutputNode;
            inputNode.AddOutgoingConnection(audioFileOutputNode);

            audioGraph.Start();
        }

        public async Task<AudioFile> StopAsync()
        {
            audioGraph.Stop();
            var finalizeResult = await audioFileOutputNode.FinalizeAsync();
            if (finalizeResult != TranscodeFailureReason.None)
            {
                throw new Exception($"Failed to transcode output audio file: {finalizeResult}");
            }
            return new AudioFile(audioStorageFile.Path);
        }
    }
}
