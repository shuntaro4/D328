using D328.Application.Services;
using D328.Domain.Model;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.IO;
using System.Linq;

namespace D328.Audio.Windows
{
    public class AudioMixerService : IAudioMixerService
    {
        private Record _record;

        public AudioMixerService(Record record)
        {
            _record = record;
        }

        public string MixLines()
        {
            var audioFiles = _record.Lines
                .Where(x => File.Exists(x.AudioPath))
                .Select(x => new AudioFileReader(x.AudioPath)).ToList();

            if (audioFiles.Count == 0)
            {
                return "";
            }

            var mixer = new MixingSampleProvider(audioFiles);
            var filePath = _record.AudioPath;
            if (string.IsNullOrEmpty(filePath))
            {
                filePath = $"mix-{DateTime.Now.ToString("yyyy-MM-dd-HHmmss")}.wav";
            }
            WaveFileWriter.CreateWaveFile16(filePath, mixer);

            foreach (var fileReader in audioFiles)
            {
                fileReader.Dispose();
            }
            audioFiles = null;

            return filePath;
        }
    }
}
