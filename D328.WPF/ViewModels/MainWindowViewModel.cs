using D328.Platform;
using D328.WPF.Platform;
using D328.WPF.Repository;
using NAudio.Wave;
using Prism.Commands;
using Prism.Mvvm;
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

        private ObservableCollection<WaveInCapabilities> _audioDevices;

        public ObservableCollection<WaveInCapabilities> AudioDevices
        {
            get => _audioDevices;
            set => SetProperty(ref _audioDevices, value);
        }

        private WaveInCapabilities _selectedAudioDevice;

        public WaveInCapabilities SelectedAudioDevice
        {
            get => _selectedAudioDevice;
            set => SetProperty(ref _selectedAudioDevice, value);
        }

        public DelegateCommand RecordingStartCommand { get; }

        public DelegateCommand RecordingStopCommand { get; }

        private IAudioDeviceService<WaveInCapabilities> AudioDeviceService;

        private AudioDeviceServiceHelper AudioDeviceServiceHelper;

        private IAudioRecorder AudioRecorder;

        public MainWindowViewModel()
        {
            AudioDeviceService = new AudioDeviceService();
            AudioDeviceServiceHelper = new AudioDeviceServiceHelper();
            var audioDevices = AudioDeviceService.GetInputAudioDevices();
            AudioDevices = new ObservableCollection<WaveInCapabilities>(audioDevices);
            SelectedAudioDevice = audioDevices.FirstOrDefault();

            RecordingStartCommand = new DelegateCommand(RecordingStartCommandExecute);
            RecordingStopCommand = new DelegateCommand(RecordingStopCommandExecute);
        }

        private void RecordingStartCommandExecute()
        {
            var fileName = @"test.wav";

            AudioRecorder = new AudioRecorder(fileName, AudioDeviceServiceHelper.GetInputAudioDeviceNumber(SelectedAudioDevice));
            AudioRecorder.Start();
        }

        private void RecordingStopCommandExecute()
        {
            AudioRecorder.Stop();
        }
    }
}