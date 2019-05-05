﻿namespace D328.Domain.Model
{
    public class Line
    {
        public int Id { get; } = -1;

        public int SortNumber { get; } = -1;

        public string AudioPath { get; } = "";

        private Line(int id, int sortNumber, string audioPath)
        {
            Id = id;
            SortNumber = sortNumber;
            AudioPath = audioPath;
        }

        public static Line CreateNew(int id = -1, int sortNumber = -1, string audioPath = "")
        {
            return new Line(id, sortNumber, audioPath);
        }
    }
}
