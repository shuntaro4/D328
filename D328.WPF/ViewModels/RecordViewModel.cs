﻿using D328.Application.Services;
using D328.Audio.Windows;
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

        private ObservableCollection<LineViewModel> _lines = new ObservableCollection<LineViewModel>();

        public ObservableCollection<LineViewModel> Lines
        {
            get => _lines;
            set => SetProperty(ref _lines, value);
        }

        public DelegateCommand SaveRecordCommand { get; }

        public DelegateCommand PlaybackStartCommand { get; }

        public DelegateCommand PlaybackPauseCommand { get; }


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
        }

        private void SaveRecordCommandExecute()
        {
            var record = ToDomainModel();
            RecordRepository.Save(record);

            OnSaveFinishedHandler(new EventArgs());
        }

        private void PlaybackStartCommandExecute()
        {
            // todo It is necessary to fix timing to mix audio.

            var record = ToDomainModel();
            AudioMixerService = new AudioMixerService(record);
            AudioPath = AudioMixerService.MixLines();

            if (AudioPlayerService == null)
            {
                AudioPlayerService = new AudioPlayerService(ToDomainModel());
            }
            AudioPlayerService.Play();
        }

        private void PlaybackPauseCommandExecute()
        {
            AudioPlayerService?.Pause();
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
    }
}
