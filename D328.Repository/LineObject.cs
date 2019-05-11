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

        public RecordObject RecordObject { get; set; }

        public LineObject()
        {
        }

        private LineObject(Line line)
        {
            Id = line.Id;
            SortNumber = line.SortNumber;
            AudioPath = line.AudioPath;
            RecordObject = RecordObject.CreateNew(line.Record);
        }

        public static LineObject CreateNew(Line line)
        {
            return new LineObject(line);
        }

        public Line ToLine()
        {
            return Line.CreateNew(Id, SortNumber, AudioPath, RecordObject.ToRecord());
        }
    }
}
