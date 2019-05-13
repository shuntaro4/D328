using System;
using System.Collections.Generic;

namespace D328.Domain.Model
{
    public class Record : IAggregateRoot
    {
        public int Id { get; }

        public string AudioPath { get; }

        public string Title { get; set; }

        public List<Line> Lines { get; }

        private Record(int id, string audioPath, string title, List<Line> lines)
        {
            Id = id;
            AudioPath = audioPath;
            Title = title;
            Lines = lines;
        }

        public static Record CreateNew(int Id = -1, string title = "", string audioPath = "", List<Line> lines = null)
        {
            title = title == "" ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") : title;
            lines = lines ?? new List<Line>();
            return new Record(Id, audioPath, title, lines);
        }
    }
}
