using D328.Application.Services;
using D328.Audio.Windows;
using D328.Domain;
using D328.Domain.Enum;
using D328.Domain.Model;
using D328.Repository;
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

        private ObservableCollection<RecordViewModel> _recordList;

        public ObservableCollection<RecordViewModel> RecordList
        {
            get => _recordList;
            set => SetProperty(ref _recordList, value);
        }

        private RecordViewModel _selectedRecord;

        public RecordViewModel SelectedRecord
        {
            get => _selectedRecord;
            set => SetProperty(ref _selectedRecord, value);
        }

        private LineViewModel _selectedLine;

        public LineViewModel SelectedLine
        {
            get => _selectedLine;
            set => SetProperty(ref _selectedLine, value);
        }

        public DelegateCommand RecordingStartCommand { get; }

        public DelegateCommand RecordingStopCommand { get; }

        public DelegateCommand WindowClosedCommand { get; }

        public DelegateCommand RecordListSelectionChangedCommand { get; }

        public DelegateCommand CloseCommand { get; }

        public DelegateCommand CreateNewRecordCommand { get; }

        public DelegateCommand LineListSelectionChangedCommand { get; }


        private IAudioDeviceService AudioDeviceService = new AudioDeviceService();

        private IAudioRecorderService AudioRecorderService;

        private IAudioPlayerService AudioPlayerService;

        private IRecordRepository RecordRepository = new RecordRepository();

        public MainWindowViewModel()
        {
            var inputAudioDevices = AudioDeviceService.GetInputAudioDevices();
            InputAudioDevices = new ObservableCollection<AudioDevice>(inputAudioDevices);
            SelectedInputAudioDevice = AudioDeviceService.GetSelectedInputAudioDevice(InputAudioDevices);

            var outputAudioDevice = AudioDeviceService.GetOutputAudioDevices();
            OutputAudioDevices = new ObservableCollection<AudioDevice>(outputAudioDevice);
            SelectedOutputAudioDevice = AudioDeviceService.GetSelectedOutputAudioDevice(OutputAudioDevices);

            RecordList = new ObservableCollection<RecordViewModel>(RecordRepository.FindAll().Select(x => new RecordViewModel(x)));

            RecordingStartCommand = new DelegateCommand(RecordingStartCommandExecute);
            RecordingStopCommand = new DelegateCommand(RecordingStopCommandExecute);
            WindowClosedCommand = new DelegateCommand(WindowClosedCommandExecute);
            RecordListSelectionChangedCommand = new DelegateCommand(RecordListSelectionChangedCommandExecute);
            CloseCommand = new DelegateCommand(CloseCommandExecute);
            CreateNewRecordCommand = new DelegateCommand(CreateNewRecordCommandExecute);
            LineListSelectionChangedCommand = new DelegateCommand(RecordListSelectionChangedCommandExecute);

            RecordingReadyCommandExecute();
        }

        private void RecordingReadyCommandExecute()
        {
            if (SelectedInputAudioDevice == null)
            {
                return;
            }

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
            SelectedLine.AudioMode = AudioMode.Recording;

            AudioRecorderService?.Start();
        }

        private void RecordingStopCommandExecute()
        {
            SelectedLine.AudioMode = AudioMode.Normal;

            AudioRecorderService?.Stop();
            SelectedLine.AudioPath = AudioRecorderService.GetAudioPath();

            RecordingReadyCommandExecute();
        }

        private void WindowClosedCommandExecute()
        {
            AudioRecorderService?.Dispose();
            AudioRecorderService = null;

            AudioPlayerService?.Dispose();
            AudioPlayerService = null;
        }

        private void RecordListSelectionChangedCommandExecute()
        {
            AudioPlayerService?.Dispose();
            AudioPlayerService = null;

            SelectedRecord.ClearLinesAudioModeCommand.Execute();

            SelectedRecord?.SubscriveEventOnSaveFinished((_, __) =>
            {
                RecordList.Clear();
                RecordList = new ObservableCollection<RecordViewModel>(RecordRepository.FindAll().Select(x => new RecordViewModel(x)));
            });
        }

        private void CloseCommandExecute()
        {
            System.Windows.Application.Current.MainWindow.Close();
        }

        private void CreateNewRecordCommandExecute()
        {
            var record = Record.CreateNew();
            record.Lines.Add(Line.CreateNew(sortNumber: 1));
            SelectedRecord = new RecordViewModel(record);
            SelectedLine = SelectedRecord.Lines.FirstOrDefault();
        }
    }
}