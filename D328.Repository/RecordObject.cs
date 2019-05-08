using D328.Domain.Model;
using Realms;
using System.Collections.Generic;

namespace D328.Repository
{
    internal class RecordObject : RealmObject
    {
        public int Id { get; set; }

        public string AudioPath { get; set; }

        public string Title { get; set; }

        public IList<LineObject> Lines { get; }


        public RecordObject()
        {
        }

        public RecordObject(Record record)
        {
            Id = record.Id;
            AudioPath = record.AudioPath;
            Title = record.Title;
            Lines = new List<LineObject>();
            foreach (var line in record.Lines)
            {
                Lines.Add(new LineObject(line));
            }
        }

        public Record ToRecord()
        {
            var record = Record.CreateNew(Id, Title, AudioPath);
            foreach (var lineObject in Lines)
            {
                record.AddLine(lineObject.ToLine());
            }
            return record;
        }
    }
}
