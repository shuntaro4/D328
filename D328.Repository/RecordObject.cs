using D328.Domain.Model;
using Realms;

namespace D328.Repository
{
    internal class RecordObject : RealmObject
    {
        [PrimaryKey]
        public int Id { get; set; }

        public string AudioPath { get; set; }

        public string Title { get; set; }

        public RecordObject()
        {
            // Please do not use. This constructor is realm library only.
        }

        private RecordObject(Record record)
        {
            Id = record.Id;
            AudioPath = record.AudioPath;
            Title = record.Title;
        }

        public static RecordObject CreateNew(Record record)
        {
            return new RecordObject(record);
        }

        public Record ToDomainModel()
        {
            return Record.CreateNew(Id, Title, AudioPath);
        }
    }
}
