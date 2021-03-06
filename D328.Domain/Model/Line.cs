﻿using D328.Domain.Enum;

namespace D328.Domain.Model
{
    public class Line
    {
        public int Id { get; }

        public int SortNumber { get; set; }

        public string AudioPath { get; }

        public AudioMode AudioMode { get; set; } = AudioMode.Normal;

        private Line(int id, int sortNumber, string audioPath)
        {
            Id = id;
            SortNumber = sortNumber;
            AudioPath = audioPath;
        }

        public static Line CreateNew(int id = -1, int sortNumber = 1, string audioPath = "")
        {
            return new Line(id, sortNumber, audioPath);
        }
    }
}
