using D328.Domain.Enum;

namespace D328.Domain.Model
{
    public class Line
    {
        public int Id { get; } = -1;

        public int SortNumber { get; set; } = 1;

        public string AudioPath { get; } = "";

        public LineMode LineMode { get; set; } = LineMode.Normal;

        public Record Record { get; set; }

        private Line(int id, int sortNumber, string audioPath, Record record)
        {
            Id = id;
            SortNumber = sortNumber;
            AudioPath = audioPath;
            Record = record;
        }

        public static Line CreateNew(int id = -1, int sortNumber = 1, string audioPath = "", Record record = null)
        {
            return new Line(id, sortNumber, audioPath, record);
        }
    }
}
