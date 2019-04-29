namespace D328.Domain.Model
{
    public class Record
    {
        public int Id { get; } = -1;

        public string AudioPath { get; } = "";

        private Record(int Id, string audioPath)
        {
            AudioPath = audioPath;
        }

        public static Record CreateNew(string audioPath, int Id = -1)
        {
            return new Record(Id, audioPath);
        }
    }
}
