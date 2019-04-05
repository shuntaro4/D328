using D328.Platform;
using NAudio.Wave;
using System;

namespace D328.WPF.Platform
{
    public class AudioRecorder : IAudioRecorder, IDisposable
    {
        private readonly string _outputFilePath;

        private WaveInEvent _waveInEvent;

        private WaveFileWriter _waveFileWriter;

        public AudioRecorder(string outputFilePath)
        {
            _outputFilePath = outputFilePath;

            if (string.IsNullOrWhiteSpace(_outputFilePath))
            {
                _outputFilePath = $"{DateTime.Now.ToString("yyyy-MM-dd-hhmmss")}.wav";
            }

            _waveInEvent = new WaveInEvent()
            {
                DeviceNumber = 0
            };
            _waveInEvent.DataAvailable += DataAvailable;
            _waveInEvent.RecordingStopped += RecordingStopped;
            _waveFileWriter = new WaveFileWriter(outputFilePath, _waveInEvent.WaveFormat);
        }

        private void DataAvailable(object sender, WaveInEventArgs e)
        {
            _waveFileWriter.Write(e.Buffer, 0, e.BytesRecorded);
            _waveFileWriter.Flush();
        }

        private void RecordingStopped(object sender, StoppedEventArgs e)
        {
            _waveFileWriter.Flush();
            Dispose();
        }

        public void Start()
        {
            _waveInEvent.StartRecording();
        }

        public void Stop()
        {
            _waveInEvent.StopRecording();
        }

        public void Dispose()
        {
            _waveInEvent.DataAvailable -= DataAvailable;
            _waveInEvent.RecordingStopped -= RecordingStopped;

            _waveInEvent?.Dispose();
            _waveFileWriter?.Dispose();
        }

    }
}
