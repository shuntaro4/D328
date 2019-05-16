using D328.Domain.Model;
using Realms;

namespace D328.Repository
{
    internal class LineObject : RealmObject
    {
        [PrimaryKey]
        public int Id { get; set; }

        public int SortNumber { get; set; }

        public string AudioPath { get; set; }

        public RecordData OwnerRecordObject { get; set; }

        public LineObject()
        {
        }

        private LineObject(Line line, Record ownerRecord)
        {
            Id = line.Id;
            SortNumber = line.SortNumber;
            AudioPath = line.AudioPath;
            OwnerRecordObject = RecordData.CreateNew(ownerRecord);
        }

        public static LineObject CreateNew(Line line, Record ownerRecord)
        {
            return new LineObject(line, ownerRecord);
        }

        public Line ToDomainModel()
        {
            // Do not convert OwnerRecordObject. Because an infinite loop occurs.
            return Line.CreateNew(Id, SortNumber, AudioPath);
        }
    }
}
