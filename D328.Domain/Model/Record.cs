namespace D328.Domain.Model
{
    public class Record
    {
        public int Id { get; } = -1;

        public string AudioPath { get; } = "";

        public Record(string audioPath)
        {
            AudioPath = audioPath;
        }
    }
}
