using D328.Application.Services;
using D328.Audio.Windows;
using D328.Domain.Enum;
using D328.Domain.Model;
using D328.Repository;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace D328.WPF.ViewModels
{
    public class RecordViewModel : BindableBase
    {
        private int _id = -1;

        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private string _title = "";

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string _audioPath = "";

        public string AudioPath
        {
            get => _audioPath;
            set => SetProperty(ref _audioPath, value);
        }

        private TimeSpan _currentTime = new TimeSpan(0);

        public TimeSpan CurrentTime
        {
            get => _currentTime;
            set => SetProperty(ref _currentTime, value);
        }

        private TimeSpan _totalTime = new TimeSpan(0);

        public TimeSpan TotalTime
        {
            get => _totalTime;
            set => SetProperty(ref _totalTime, value);
        }

        private AudioMode _audioMode = AudioMode.Normal;
        public AudioMode AudioMode
        {
            get => _audioMode;
            set => SetProperty(ref _audioMode, value);
        }

        private ObservableCollection<LineViewModel> _lines = new ObservableCollection<LineViewModel>();

        public ObservableCollection<LineViewModel> Lines
        {
            get => _lines;
            set => SetProperty(ref _lines, value);
        }

        private LineViewModel _selectedLine;

        public LineViewModel SelectedLine
        {
            get => _selectedLine;
            set => SetProperty(ref _selectedLine, value);
        }

        public DelegateCommand SaveRecordCommand { get; }

        public DelegateCommand PlaybackStartCommand { get; }

        public DelegateCommand PlaybackPauseCommand { get; }

        public DelegateCommand PlaybackStopCommand { get; }

        public DelegateCommand ClearLinesAudioModeCommand { get; }

        public DelegateCommand RemoveLineCommand { get; }

        private RecordRepository RecordRepository = new RecordRepository();

        private IAudioMixerService AudioMixerService;

        private IAudioPlayerService AudioPlayerService;

        private EventHandler _onSaveFinished;

        public RecordViewModel(Record record)
        {
            Id = record.Id;
            Title = record.Title;
            AudioPath = record.AudioPath;
            Lines = new ObservableCollection<LineViewModel>(record.Lines.Select(x => new LineViewModel(x)));
            SaveRecordCommand = new DelegateCommand(SaveRecordCommandExecute);
            PlaybackStartCommand = new DelegateCommand(PlaybackStartCommandExecute);
            PlaybackPauseCommand = new DelegateCommand(PlaybackPauseCommandExecute);
            PlaybackStopCommand = new DelegateCommand(PlaybackStopCommandExecute);
            ClearLinesAudioModeCommand = new DelegateCommand(ClearLinesAudioModeCommandExecute);
            RemoveLineCommand = new DelegateCommand(RemoveLineCommandExecute);
        }

        private void SaveRecordCommandExecute()
        {
            var record = ToDomainModel();
            AudioMixerService = new AudioMixerService(record);
            AudioPath = AudioMixerService.MixLines();

            record = ToDomainModel();
            RecordRepository.Save(record);

            OnSaveFinishedHandler(new EventArgs());
        }

        private void PlaybackStartCommandExecute()
        {
            if (AudioPlayerService == null)
            {
                AudioPlayerService = new AudioPlayerService(ToDomainModel());
                AudioPlayerService.SubscriveEventOnCurrentTimeChanged((s, _) =>
                {
                    IAudioPlayerService audioPlayerService = s as AudioPlayerService;
                    if (audioPlayerService == null)
                    {
                        return;
                    }
                    CurrentTime = audioPlayerService.GetCurrentTime();
                });
            }
            TotalTime = AudioPlayerService.GetTotalTime();
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

        public Record ToDomainModel()
        {
            var record = Record.CreateNew(Id, Title, AudioPath);
            foreach (var line in Lines)
            {
                record.Lines.Add(line.ToDomainModel());
            }
            return record;
        }

        public void SubscriveEventOnSaveFinished(EventHandler subscriveEvent)
        {
            _onSaveFinished += subscriveEvent;
        }

        private void OnSaveFinishedHandler(EventArgs e)
        {
            if (_onSaveFinished == null)
            {
                return;
            }
            _onSaveFinished(this, e);
        }

        private void ClearLinesAudioModeCommandExecute()
        {
            foreach (var line in Lines)
            {
                line.ClearAudioCommand.Execute();
            }
        }

        private void RemoveLineCommandExecute()
        {
            Lines.Remove(SelectedLine);
        }
    }
}
