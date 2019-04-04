using D328.Platform;
using D328.WPF.Repository;
using NAudio.CoreAudioApi;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;

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

        private MMDevice _selectedAudioDevice = null;

        public MMDevice SelectedAudioDevice
        {
            get => _selectedAudioDevice;
            set => SetProperty(ref _selectedAudioDevice, value);
        }

        public DelegateCommand RecordingStartCommand { get; }

        public MainWindowViewModel()
        {
            IAudioDeviceService<MMDevice> audioDeviceRepository = new AudioDeviceWinRepository();
            var audioDevices = audioDeviceRepository.GetAudioDevices();
            AudioDevices = new ObservableCollection<MMDevice>(audioDevices);

            RecordingStartCommand = new DelegateCommand(RecordingCommandExecute);
        }

        private void RecordingCommandExecute()
        {
            // todo 録音処理
        }
    }
}