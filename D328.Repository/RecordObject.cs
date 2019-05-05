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
        }

        public Record ToRecord()
        {
            return Record.CreateNew(Id, Title, AudioPath);
        }
    }
}
