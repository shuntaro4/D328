using System;
using System.Collections.Generic;

namespace D328.Domain.Model
{
    public class Record
    {
        public int Id { get; } = -1;

        public string AudioPath { get; } = "";

        public string Title { get; set; } = "";

        public List<Line> Lines { get; } = new List<Line>();

        private Record(int id, string audioPath, string title)
        {
            Id = id;
            AudioPath = audioPath;
            Title = title;
        }

        public static Record CreateNew(int Id = -1, string title = "", string audioPath = "")
        {
            title = title == "" ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") : title;
            return new Record(Id, audioPath, title);
        }

        public void AddLine(Line line)
        {
            Lines.Add(line);
        }
    }
}
