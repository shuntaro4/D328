using D328.Platform;
using D328.WPF.Platform;
using D328.WPF.Repository;
using NAudio.CoreAudioApi;
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

        public DelegateCommand WindowCloseCommand { get; }

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
            WindowCloseCommand = new DelegateCommand(WindowClosedCommandExecute);

            RecordingReadyCommandExecute();
        }

        private void RecordingReadyCommandExecute()
        {
            if (SelectedAudioDevice == null)
            {
                return;
            }

            AudioRecorder = new AudioRecorder("", SelectedAudioDevice);
            AudioRecorder.SubscriveEventOnDataAvailable((s, _) =>
            {
                var audioRecorder = s as AudioRecorder;
                if (audioRecorder == null)
                {
                    return;
                }
                Peak = audioRecorder.GetPeak();
            });
            AudioRecorder.Ready();
        }

        private void RecordingStartCommandExecute()
        {
            AudioRecorder?.Start();
        }

        private void RecordingStopCommandExecute()
        {
            AudioRecorder?.Stop();

            RecordingReadyCommandExecute();
        }

        private void WindowClosedCommandExecute()
        {
            AudioRecorder.Dispose();
            AudioRecorder = null;
        }
    }
}