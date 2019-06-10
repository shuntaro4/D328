using D328.Application.Services;
using D328.Audio.Windows;
using D328.Domain;
using D328.Domain.DomainService;
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

        public DelegateCommand RecordingCommand { get; }

        public DelegateCommand WindowClosedCommand { get; }

        public DelegateCommand RecordListSelectionChangedCommand { get; }

        public DelegateCommand CloseCommand { get; }

        public DelegateCommand CreateNewRecordCommand { get; }

        public DelegateCommand LineListSelectionChangedCommand { get; }

        public DelegateCommand<RecordViewModel> RemoveRecordCommand { get; }

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

            RecordingCommand = new DelegateCommand(RecordingCommandExecute);

            WindowClosedCommand = new DelegateCommand(WindowClosedCommandExecute);
            RecordListSelectionChangedCommand = new DelegateCommand(RecordListSelectionChangedCommandExecute);
            RemoveRecordCommand = new DelegateCommand<RecordViewModel>(RemoveRecordCommandExecute);
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

        private void RecordingCommandExecute()
        {
            if (SelectedRecord.AudioMode == AudioMode.Normal)
            {
                RecordingStartCommandExecute();
                return;
            }

            if (SelectedRecord.AudioMode == AudioMode.Recording)
            {
                RecordingStopCommandExecute();
                return;
            }
        }

        private void RecordingStartCommandExecute()
        {
            SelectedRecord.AudioMode = AudioMode.Recording;

            AudioRecorderService?.Start();
        }

        private void RecordingStopCommandExecute()
        {
            SelectedRecord.AudioMode = AudioMode.Normal;

            AudioRecorderService?.Stop();

            var sortNumber = LineDomainService.CalcNewSortNumber(SelectedRecord.Lines.Select(x => x.ToDomainModel()));
            SelectedRecord.Lines.Add(
                new LineViewModel(Line.CreateNew(sortNumber: sortNumber, audioPath: AudioRecorderService.GetAudioPath())));

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

            SelectedRecord?.ClearLinesAudioModeCommand.Execute();

            SelectedRecord?.SubscriveEventOnSaveFinished((_, __) =>
            {
                RecordList = new ObservableCollection<RecordViewModel>(RecordRepository.FindAll().Select(x => new RecordViewModel(x)));
                SelectedRecord = RecordList.FirstOrDefault();
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
            SelectedRecord.SelectedLine = SelectedRecord.Lines.FirstOrDefault();
        }

        private void RemoveRecordCommandExecute(RecordViewModel recordViewModel)
        {
            RecordRepository.Remove(recordViewModel.ToDomainModel());

            RecordList.Clear();
            RecordList = new ObservableCollection<RecordViewModel>(RecordRepository.FindAll().Select(x => new RecordViewModel(x)));

            SelectedRecord = null;
        }
    }
}
