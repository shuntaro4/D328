﻿using D328.Domain.Model;
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

        private ObservableCollection<MMDevice> _inputAudioDevices;

        public ObservableCollection<MMDevice> InputAudioDevices
        {
            get => _inputAudioDevices;
            set => SetProperty(ref _inputAudioDevices, value);
        }

        private MMDevice _selectedInputAudioDevice;

        public MMDevice SelectedInputAudioDevice
        {
            get => _selectedInputAudioDevice;
            set => SetProperty(ref _selectedInputAudioDevice, value);
        }

        private float _peek;

        public float Peak
        {
            get => _peek;
            set => SetProperty(ref _peek, value);
        }

        private ObservableCollection<D328Record> _recordList;

        public ObservableCollection<D328Record> RecordList
        {
            get => _recordList;
            set => SetProperty(ref _recordList, value);
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

        private IAudioDeviceService<MMDevice> AudioDeviceService;

        private IAudioRecorderService AudioRecorderService;

        public MainWindowViewModel()
        {
            AudioDeviceService = new AudioDeviceService();
            var inputAudioDevices = AudioDeviceService.GetInputAudioDevices();
            InputAudioDevices = new ObservableCollection<MMDevice>(inputAudioDevices);
            SelectedInputAudioDevice = InputAudioDevices.FirstOrDefault(c => c.ID == AudioDeviceService.GetDefaultInputAudioDevice()?.ID);
            RecordList = new ObservableCollection<D328Record>();

            RecordingStartCommand = new DelegateCommand(RecordingStartCommandExecute);
            RecordingStopCommand = new DelegateCommand(RecordingStopCommandExecute);
            WindowCloseCommand = new DelegateCommand(WindowClosedCommandExecute);

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
            RecordList.Add(AudioRecorderService.GetRecordData());

            RecordingReadyCommandExecute();
        }

        private void WindowClosedCommandExecute()
        {
            AudioRecorderService.Dispose();
            AudioRecorderService = null;
        }
    }
}