using D328.Domain.Model;
using Realms;

namespace D328.Repository
{
    internal class LineObject : RealmObject
    {
        public int Id { get; set; }

        public int SortNumber { get; set; }

        public string AudioPath { get; set; }

        public RecordObject RecordObject { get; set; }

        public LineObject()
        {
        }

        public LineObject(Line line)
        {
            Id = line.Id;
            SortNumber = line.SortNumber;
            AudioPath = line.AudioPath;
        }

        public Line ToLine()
        {
            return Line.CreateNew(Id, SortNumber, AudioPath);
        }
    }
}
