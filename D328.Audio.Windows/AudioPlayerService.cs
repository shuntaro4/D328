using D328.Application.Services;
using D328.Domain.Model;
using NAudio.Wave;
using System;

namespace D328.Audio.Windows
{
    public class AudioPlayerService : IAudioPlayerService
    {
        private IWavePlayer _wavePlayer;

        private WaveStream _waveStream;

        private EventHandler _onCurrentTimeChanged;

        public AudioPlayerService(Record record) : this(record.AudioPath)
        {
        }

        public AudioPlayerService(Line line) : this(line.AudioPath)
        {
        }

        private AudioPlayerService(string audioPath)
        {
            if (audioPath == "")
            {
                return;
            }

            var waveStream = new AudioFileReader(audioPath);
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

        public TimeSpan GetTotalTime()
        {
            return _waveStream?.TotalTime ?? new TimeSpan(0);
        }

        public TimeSpan GetCurrentTime()
        {
            return _waveStream?.CurrentTime ?? new TimeSpan(0);
        }

        public void SubscriveEventOnCurrentTimeChanged(EventHandler subscriveEvent)
        {
            _onCurrentTimeChanged += subscriveEvent;
        }

        private void OnDataAvailableHandler(EventArgs e)
        {
            if (_onCurrentTimeChanged == null)
            {
                return;
            }
            _onCurrentTimeChanged(this, e);
        }
    }
}
