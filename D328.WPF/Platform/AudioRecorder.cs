﻿using D328.Domain.Model;
using D328.Platform;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using System.Threading;

namespace D328.WPF.Platform
{
    public class AudioRecorder : IAudioRecorder
    {
        private MMDevice _inputAudioDevice;

        private readonly string _outputFilePath;

        private WaveFileWriter _waveFileWriter;

        private WasapiCapture _wasapiCapture;

        private readonly SynchronizationContext _synchronizationContext;

        private float _peak;

        private EventHandler _onDataAvailable;

        private bool _isRecording;


        public AudioRecorder(string outputFilePath, MMDevice inputAudioDevice)
        {
            _outputFilePath = outputFilePath;

            if (string.IsNullOrWhiteSpace(_outputFilePath))
            {
                _outputFilePath = $"{DateTime.Now.ToString("yyyy-MM-dd-HHmmss")}.wav";
            }

            _inputAudioDevice = inputAudioDevice;
            if (_inputAudioDevice == null)
            {
                throw new ArgumentNullException(nameof(inputAudioDevice));
            }

            _wasapiCapture = new WasapiCapture(_inputAudioDevice)
            {
                ShareMode = AudioClientShareMode.Shared,
                WaveFormat = WaveFormat.CreateIeeeFloatWaveFormat(48000, 2)
            };
            _wasapiCapture.DataAvailable += DataAvailable;
            _wasapiCapture.RecordingStopped += RecordingStopped;

            _synchronizationContext = SynchronizationContext.Current;
        }

        private void DataAvailable(object sender, WaveInEventArgs e)
        {
            if (_isRecording)
            {
                if (_waveFileWriter == null)
                {
                    _waveFileWriter = new WaveFileWriter(_outputFilePath, _wasapiCapture.WaveFormat);
                }
                _waveFileWriter.Write(e.Buffer, 0, e.BytesRecorded);
            }

            OnDataAvailableHandler(new EventArgs());
        }

        private void RecordingStopped(object sender, StoppedEventArgs e)
        {
            Dispose();
        }

        public void Ready()
        {
            _wasapiCapture?.StartRecording();
        }

        public void Start()
        {
            _isRecording = true;
        }

        public void Stop()
        {
            _wasapiCapture?.StopRecording();
        }

        public void Dispose()
        {
            _waveFileWriter?.Dispose();
            _waveFileWriter = null;

            _wasapiCapture?.Dispose();
            _wasapiCapture = null;
        }

        public float GetPeak()
        {
            _synchronizationContext.Post(_ => _peak = _inputAudioDevice.AudioMeterInformation.MasterPeakValue, null);
            return _peak;
        }

        public void SubscriveEventOnDataAvailable(EventHandler subscriveEvent)
        {
            _onDataAvailable += subscriveEvent;
        }

        public D328Record GetRecordData()
        {
            return new D328Record(_outputFilePath);
        }

        private void OnDataAvailableHandler(EventArgs e)
        {
            if (_onDataAvailable == null)
            {
                return;
            }
            _onDataAvailable(this, e);
        }
    }
}
