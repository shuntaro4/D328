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

        private LineData(Line line, RecordData ownerRecordData)
        {
            Id = line.Id;
            SortNumber = line.SortNumber;
            AudioPath = line.AudioPath;
            OwnerRecordObject = ownerRecordData;
        }

        public static LineData CreateNew(Line line, RecordData ownerRecordData)
        {
            return new LineData(line, ownerRecordData);
        }

        public Line ToDomainModel()
        {
            // Do not convert OwnerRecordObject. Because an infinite loop occurs.
            return Line.CreateNew(Id, SortNumber, AudioPath);
        }
    }
}
