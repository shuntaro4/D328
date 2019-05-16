using D328.Domain.Model;
using Realms;
using System.Linq;

namespace D328.Repository
{
    internal class RecordData : RealmObject
    {
        [PrimaryKey]
        public int Id { get; set; }

        public string AudioPath { get; set; }

        public string Title { get; set; }

        [Backlink(nameof(LineData.OwnerRecordObject))]
        public IQueryable<LineData> Lines { get; }

        public RecordData()
        {
            // Please do not use. This constructor is realm library only.
        }

        private RecordData(Record record)
        {
            Id = record.Id;
            AudioPath = record.AudioPath;
            Title = record.Title;
        }

        public static RecordData CreateNew(Record record)
        {
            return new RecordData(record);
        }

        public Record ToDomainModel()
        {
            var list = Lines.ToList();
            return Record.CreateNew(Id, Title, AudioPath, list.Select(x => x.ToDomainModel()).ToList());
        }
    }
}
