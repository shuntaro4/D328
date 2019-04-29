using D328.Application.Services;
using D328.Audio.Windows;
using D328.Domain;
using D328.Domain.Model;
using D328.Repository;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace D328.WPF.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public enum MainWindowMode
        {
            Normal,
            Recording,
            Pause,
            Playing
        };

        private string _title = "D328";


        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private ObservableCollection<AudioDevice> _inputAudioDevices;

        public ObservableCollection<AudioDevice> InputAudioDevices
        {
            get => _inputAudioDevices;
            set => SetProperty(ref _inputAudioDevices, value);
        }

        private AudioDevice _selectedInputAudioDevice;

        public AudioDevice SelectedInputAudioDevice
        {
            get => _selectedInputAudioDevice;
            set => SetProperty(ref _selectedInputAudioDevice, value);
        }

        private ObservableCollection<AudioDevice> _outputAudioDevices;

        public ObservableCollection<AudioDevice> OutputAudioDevices
        {
            get => _outputAudioDevices;
            set => SetProperty(ref _outputAudioDevices, value);
        }

        private AudioDevice _selectedOutputAudioDevice;

        public AudioDevice SelectedOutputAudioDevice
        {
            get => _selectedOutputAudioDevice;
            set => SetProperty(ref _selectedOutputAudioDevice, value);
        }

        private float _peek;

        public float Peak
        {
            get => _peek;
            set => SetProperty(ref _peek, value);
        }

        private ObservableCollection<Record> _recordList;

        public ObservableCollection<Record> RecordList
        {
            get => _recordList;
            set => SetProperty(ref _recordList, value);
        }

        private Record _selectedRecord;

        public Record SelectedRecord
        {
            get => _selectedRecord;
            set => SetProperty(ref _selectedRecord, value);
        }

        private MainWindowMode _windowMode;

        public MainWindowMode WindowMode
        {
            get => _windowMode;
            set => SetProperty(ref _windowMode, value);
        }

        public DelegateCommand RecordingStartCommand { get; }

        public DelegateCommand RecordingStopCommand { get; }

        public DelegateCommand WindowCloseCommand { get; }

        public DelegateCommand PlaybackStartCommand { get; }

        public DelegateCommand PlaybackPauseCommand { get; }

        public DelegateCommand PlaybackStopCommand { get; }

        public DelegateCommand RecordListSelectionChangedCommand { get; }

        private IAudioDeviceService AudioDeviceService = new AudioDeviceService();

        private IAudioRecorderService AudioRecorderService;

        private IAudioPlayerService AudioPlayerService;

        public MainWindowViewModel()
        {
            var inputAudioDevices = AudioDeviceService.GetInputAudioDevices();
            InputAudioDevices = new ObservableCollection<AudioDevice>(inputAudioDevices);
            SelectedInputAudioDevice = AudioDeviceService.GetSelectedInputAudioDevice(InputAudioDevices);

            var outputAudioDevice = AudioDeviceService.GetOutputAudioDevices();
            OutputAudioDevices = new ObservableCollection<AudioDevice>(outputAudioDevice);
            SelectedOutputAudioDevice = AudioDeviceService.GetSelectedOutputAudioDevice(OutputAudioDevices);

            var recordRepository = new RecordRepository();
            RecordList = new ObservableCollection<Record>(recordRepository.FindAll());

            RecordingStartCommand = new DelegateCommand(RecordingStartCommandExecute);
            RecordingStopCommand = new DelegateCommand(RecordingStopCommandExecute);
            WindowCloseCommand = new DelegateCommand(WindowClosedCommandExecute);
            PlaybackStartCommand = new DelegateCommand(PlaybackStartCommandExecute);
            PlaybackPauseCommand = new DelegateCommand(PlaybackPauseCommandExecute);
            PlaybackStopCommand = new DelegateCommand(PlaybackStopCommandExecute);
            RecordListSelectionChangedCommand = new DelegateCommand(RecordListSelectionChangedCommandExecute);

            RecordingReadyCommandExecute();
        }

        private void RecordingReadyCommandExecute()
        {
            if (SelectedInputAudioDevice == null)
            {
                return;
            }

            WindowMode = MainWindowMode.Normal;

            AudioRecorderService = new AudioRecorderService(SelectedInputAudioDevice);
            AudioRecorderService.SubscriveEventOnDataAvailable((s, _) =>
            {
                IAudioRecorderService audioRecorderService = s as AudioRecorderService;
                if (audioRecorderService == null)
                {
                    return;
                }
                Peak = audioRecorderService.GetPeak();
            });
            AudioRecorderService.Ready();
        }

        private void RecordingStartCommandExecute()
        {
            WindowMode = MainWindowMode.Recording;

            AudioRecorderService?.Start();
        }

        private void RecordingStopCommandExecute()
        {
            WindowMode = MainWindowMode.Normal;

            AudioRecorderService?.Stop();
            var record = AudioRecorderService.GetRecordData();
            var recordRepository = new RecordRepository(record);
            recordRepository.Save();

            RecordList = new ObservableCollection<Record>(recordRepository.FindAll());

            RecordingReadyCommandExecute();
        }

        private void WindowClosedCommandExecute()
        {
            AudioRecorderService?.Dispose();
            AudioRecorderService = null;

            AudioPlayerService?.Dispose();
            AudioPlayerService = null;
        }

        private void PlaybackStartCommandExecute()
        {
            if (SelectedRecord == null)
            {
                return;
            }
            AudioPlayerService.Play();
        }

        private void PlaybackPauseCommandExecute()
        {
            AudioPlayerService?.Pause();
        }

        private void PlaybackStopCommandExecute()
        {
            AudioPlayerService?.Stop();
        }

        private void RecordListSelectionChangedCommandExecute()
        {
            AudioPlayerService?.Dispose();
            AudioPlayerService = null;
            AudioPlayerService = new AudioPlayerService(SelectedRecord);
        }
    }
}