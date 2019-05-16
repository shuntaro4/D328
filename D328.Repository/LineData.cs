using D328.Domain.Model;
using Realms;

namespace D328.Repository
{
    internal class LineData : RealmObject
    {
        [PrimaryKey]
        public int Id { get; set; }

        public int SortNumber { get; set; }

        public string AudioPath { get; set; }

        public RecordData OwnerRecordObject { get; set; }

        public LineData()
        {
        }

        private LineData(Line line, Record ownerRecord)
        {
            Id = line.Id;
            SortNumber = line.SortNumber;
            AudioPath = line.AudioPath;
            OwnerRecordObject = RecordData.CreateNew(ownerRecord);
        }

        public static LineData CreateNew(Line line, Record ownerRecord)
        {
            return new LineData(line, ownerRecord);
        }

        public Line ToDomainModel()
        {
            // Do not convert OwnerRecordObject. Because an infinite loop occurs.
            return Line.CreateNew(Id, SortNumber, AudioPath);
        }
    }
}
