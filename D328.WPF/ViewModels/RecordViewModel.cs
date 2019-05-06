﻿using D328.Domain.Model;
using Prism.Mvvm;
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

        public RecordViewModel(Record record)
        {
            Id = record.Id;
            Title = record.Title;
            AudioPath = record.AudioPath;
            Lines = new ObservableCollection<LineViewModel>(record.Lines.Select(x => new LineViewModel(x)));
        }

        public Record ToDomainModel()
        {
            var record = Record.CreateNew(Id, Title, AudioPath);
            foreach (var line in Lines)
            {
                record.AddLine(line.ToDomainModel());
            }
            return record;
        }
    }
}
