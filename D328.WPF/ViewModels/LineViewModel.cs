using D328.Application.Services;
using D328.Audio.Windows;
using D328.Domain.Enum;
using D328.Domain.Model;
using Prism.Commands;
using Prism.Mvvm;

namespace D328.WPF.ViewModels
{
    public class LineViewModel : BindableBase
    {
        private int _id = -1;

        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private int _sortNumber = 1;

        public int SortNumber
        {
            get => _sortNumber;
            set => SetProperty(ref _sortNumber, value);
        }

        private string _audioPath = "";

        public string AudioPath
        {
            get => _audioPath;
            set => SetProperty(ref _audioPath, value);
        }

        private AudioMode _audioMode = AudioMode.Normal;
        public AudioMode AudioMode
        {
            get => _audioMode;
            set => SetProperty(ref _audioMode, value);
        }

        public DelegateCommand ClearAudioCommand { get; }

        public DelegateCommand PlaybackStartCommand { get; }

        public DelegateCommand PlaybackPauseCommand { get; }

        public DelegateCommand PlaybackStopCommand { get; }

        private IAudioPlayerService AudioPlayerService;

        public LineViewModel(Line line)
        {
            Id = line.Id;
            SortNumber = line.SortNumber;
            AudioPath = line.AudioPath;

            ClearAudioCommand = new DelegateCommand(ClearAudioModeCommandExecute);
            PlaybackStartCommand = new DelegateCommand(PlaybackStartCommandExecute);
            PlaybackPauseCommand = new DelegateCommand(PlaybackPauseCommandExecute);
            PlaybackStopCommand = new DelegateCommand(PlaybackStopCommandExecute);
        }

        public Line ToDomainModel()
        {
            return Line.CreateNew(Id, SortNumber, AudioPath);
        }

        private void ClearAudioModeCommandExecute()
        {
            AudioMode = AudioMode.Normal;
        }

        private void PlaybackStartCommandExecute()
        {
            if (AudioPlayerService == null)
            {
                AudioPlayerService = new AudioPlayerService(ToDomainModel());
            }
            AudioMode = AudioMode.Playing;
            AudioPlayerService.Play();
        }

        private void PlaybackPauseCommandExecute()
        {
            AudioMode = AudioMode.Pause;
            AudioPlayerService?.Pause();
        }

        private void PlaybackStopCommandExecute()
        {
            AudioMode = AudioMode.Normal;
            AudioPlayerService?.Stop();
        }
    }
}
