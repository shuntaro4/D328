using D328.Domain.Repository;
using D328.WPF.Repository;
using NAudio.CoreAudioApi;
using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace D328.WPF.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "D328";

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private ObservableCollection<MMDevice> _audioDevices;

        public ObservableCollection<MMDevice> AudioDevices
        {
            get => _audioDevices;
            set => SetProperty(ref _audioDevices, value);
        }

        public MainWindowViewModel()
        {
            IAudioDeviceRepository<MMDevice> audioDeviceRepository = new AudioDeviceWinRepository();
            var audioDevices = audioDeviceRepository.GetAudioDevices();
            AudioDevices = new ObservableCollection<MMDevice>(audioDevices);
        }
    }
}