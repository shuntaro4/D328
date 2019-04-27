using D328.Application.Services;
using D328.Domain.Model;
using NAudio.Wave;

namespace D328.Audio.Windows
{
    public class AudioPlayerService : IAudioPlayerService
    {
        private readonly Record _recordData;

        private IWavePlayer _wavePlayer;

        private WaveStream _waveStream;

        public AudioPlayerService(Record record)
        {
            _recordData = record;
            var waveStream = new AudioFileReader(_recordData.AudioPath);
            _waveStream = waveStream;
            var sampleProvider = new SampleProvider(waveStream);

            _wavePlayer = new WaveOut { DesiredLatency = 200 };
            _wavePlayer.Init(sampleProvider);
        }

        public void Play()
        {
            if (_waveStream == null
                || _wavePlayer == null
                || _wavePlayer.PlaybackState == PlaybackState.Playing)
            {
                return;
            }

            _wavePlayer.Play();
        }

        public void Pause()
        {
            _wavePlayer?.Pause();
        }

        public void Stop()
        {
            _wavePlayer?.Stop();
            if (_waveStream != null)
            {
                _waveStream.Position = 0;
            }
        }

        public void Dispose()
        {
            Stop();
            _wavePlayer?.Dispose();
            _wavePlayer = null;
        }
    }
}
