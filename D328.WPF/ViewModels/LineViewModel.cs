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

        public LineViewModel(Line line)
        {
            Id = line.Id;
            SortNumber = line.SortNumber;
            AudioPath = line.AudioPath;

            ClearAudioCommand = new DelegateCommand(ClearAudioModeCommandExecute);
        }

        public Line ToDomainModel()
        {
            return Line.CreateNew(Id, SortNumber, AudioPath);
        }

        private void ClearAudioModeCommandExecute()
        {
            AudioMode = AudioMode.Normal;
        }
    }
}
