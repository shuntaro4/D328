﻿using D328.Domain.Enum;
using D328.Domain.Model;
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

        private LineMode _lineMode = LineMode.Normal;
        public LineMode LineMode
        {
            get => _lineMode;
            set => SetProperty(ref _lineMode, value);
        }

        public LineViewModel(Line line)
        {
            Id = line.Id;
            SortNumber = line.SortNumber;
            AudioPath = line.AudioPath;
        }

        public Line ToDomainModel()
        {
            return Line.CreateNew(Id, SortNumber, AudioPath);
        }
    }
}