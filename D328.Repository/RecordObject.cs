using D328.Domain.Model;
using Realms;
using System.Linq;

namespace D328.Repository
{
    internal class RecordObject : RealmObject
    {
        [PrimaryKey]
        public int Id { get; set; }

        public string AudioPath { get; set; }

        public string Title { get; set; }

        [Backlink(nameof(LineObject.OwnerRecordObject))]
        public IQueryable<LineObject> Lines { get; }

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
            var list = Lines.ToList();
            return Record.CreateNew(Id, Title, AudioPath, list.Select(x => x.ToDomainModel()).ToList());
        }
    }
}
