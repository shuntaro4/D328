using D328.Platform;
using D328.WPF.Platform;
using D328.WPF.Repository;
using NAudio.CoreAudioApi;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace D328.WPF.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "D328";

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private ObservableCollection<MMDevice> _audioDevices;

        public ObservableCollection<MMDevice> AudioDevices
        {
            get => _audioDevices;
            set => SetProperty(ref _audioDevices, value);
        }

        private MMDevice _selectedAudioDevice;

        public MMDevice SelectedAudioDevice
        {
            get => _selectedAudioDevice;
            set => SetProperty(ref _selectedAudioDevice, value);
        }

        private float _peek;

        public float Peak
        {
            get => _peek;
            set => SetProperty(ref _peek, value);
        }

        public DelegateCommand RecordingStartCommand { get; }

        public DelegateCommand RecordingStopCommand { get; }

        private IAudioDeviceService<MMDevice> AudioDeviceService;

        private AudioRecorder AudioRecorder;

        public MainWindowViewModel()
        {
            AudioDeviceService = new AudioDeviceService();
            var audioDevices = AudioDeviceService.GetInputAudioDevices();
            AudioDevices = new ObservableCollection<MMDevice>(audioDevices);
            SelectedAudioDevice = AudioDevices.FirstOrDefault(c => c.ID == AudioDeviceService.GetDefaultInputAudioDevice()?.ID);

            RecordingStartCommand = new DelegateCommand(RecordingStartCommandExecute);
            RecordingStopCommand = new DelegateCommand(RecordingStopCommandExecute);
        }

        private void RecordingStartCommandExecute()
        {
            var fileName = @"test.wav";

            AudioRecorder = new AudioRecorder(fileName, SelectedAudioDevice);
            AudioRecorder.Start();
            AudioRecorder.OnDataAvailable += (s, _) =>
            {
                var audioRecorder = s as AudioRecorder;
                if (audioRecorder == null)
                {
                    return;
                }
                Peak = audioRecorder.GetPeak();
                Console.WriteLine(Peak);
            };
        }

        private void RecordingStopCommandExecute()
        {
            AudioRecorder.Stop();
        }
    }
}