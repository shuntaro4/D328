﻿using D328.Application.Services;
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

        public DelegateCommand<LineViewModel> RecordingStartCommand { get; }

        public DelegateCommand<LineViewModel> RecordingStopCommand { get; }

        public DelegateCommand WindowClosedCommand { get; }

        public DelegateCommand<LineViewModel> PlaybackStartCommand { get; }

        public DelegateCommand<LineViewModel> PlaybackPauseCommand { get; }

        public DelegateCommand<LineViewModel> PlaybackStopCommand { get; }

        public DelegateCommand RecordListSelectionChangedCommand { get; }

        public DelegateCommand CloseCommand { get; }

        public DelegateCommand CreateNewRecordCommand { get; }

        public DelegateCommand LineListSelectionChangedCommand { get; }


        private IAudioDeviceService AudioDeviceService = new AudioDeviceService();

        private IAudioRecorderService AudioRecorderService;

        private IAudioPlayerService AudioPlayerService;

        private IRepository<Record> RecordRepository = new RecordRepository();

        public MainWindowViewModel()
        {
            var inputAudioDevices = AudioDeviceService.GetInputAudioDevices();
            InputAudioDevices = new ObservableCollection<AudioDevice>(inputAudioDevices);
            SelectedInputAudioDevice = AudioDeviceService.GetSelectedInputAudioDevice(InputAudioDevices);

            var outputAudioDevice = AudioDeviceService.GetOutputAudioDevices();
            OutputAudioDevices = new ObservableCollection<AudioDevice>(outputAudioDevice);
            SelectedOutputAudioDevice = AudioDeviceService.GetSelectedOutputAudioDevice(OutputAudioDevices);

            RecordList = new ObservableCollection<RecordViewModel>(RecordRepository.FindAll().Select(x => new RecordViewModel(x)));

            RecordingStartCommand = new DelegateCommand<LineViewModel>(RecordingStartCommandExecute);
            RecordingStopCommand = new DelegateCommand<LineViewModel>(RecordingStopCommandExecute);
            WindowClosedCommand = new DelegateCommand(WindowClosedCommandExecute);
            PlaybackStartCommand = new DelegateCommand<LineViewModel>(PlaybackStartCommandExecute);
            PlaybackPauseCommand = new DelegateCommand<LineViewModel>(PlaybackPauseCommandExecute);
            PlaybackStopCommand = new DelegateCommand<LineViewModel>(PlaybackStopCommandExecute);
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

        private void RecordingStartCommandExecute(LineViewModel line)
        {
            line.LineMode = LineMode.Recording;

            AudioRecorderService?.Start();
        }

        private void RecordingStopCommandExecute(LineViewModel line)
        {
            line.LineMode = LineMode.Normal;

            AudioRecorderService?.Stop();
            line.AudioPath = AudioRecorderService.GetAudioPath();

            RecordingReadyCommandExecute();
        }

        private void WindowClosedCommandExecute()
        {
            AudioRecorderService?.Dispose();
            AudioRecorderService = null;

            AudioPlayerService?.Dispose();
            AudioPlayerService = null;
        }

        private void PlaybackStartCommandExecute(LineViewModel line)
        {
            if (AudioPlayerService == null)
            {
                AudioPlayerService = new AudioPlayerService(line.ToDomainModel());
            }
            AudioPlayerService.Play();
        }

        private void PlaybackPauseCommandExecute(LineViewModel line)
        {
            line.LineMode = LineMode.Pause;
            AudioPlayerService?.Pause();
        }

        private void PlaybackStopCommandExecute(LineViewModel line)
        {
            line.LineMode = LineMode.Normal;
            AudioPlayerService?.Stop();
        }

        private void RecordListSelectionChangedCommandExecute()
        {
            AudioPlayerService?.Dispose();
            AudioPlayerService = null;
        }

        private void CloseCommandExecute()
        {
            System.Windows.Application.Current.MainWindow.Close();
        }

        private void CreateNewRecordCommandExecute()
        {
            var record = Record.CreateNew();
            record.AddLine(Line.CreateNew(sortNumber: 1, record: record));
            record.AddLine(Line.CreateNew(sortNumber: 2, record: record));
            record.AddLine(Line.CreateNew(sortNumber: 3, record: record));
            record.AddLine(Line.CreateNew(sortNumber: 4, record: record));
            record.AddLine(Line.CreateNew(sortNumber: 5, record: record));
            SelectedRecord = new RecordViewModel(record);
            SelectedLine = SelectedRecord.Lines.FirstOrDefault();
        }
    }
}