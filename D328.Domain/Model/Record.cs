namespace D328.Domain.Model
{
    public class Record
    {
        public int Id { get; } = -1;

        public string AudioPath { get; } = "";

        private Record(string audioPath)
        {
            AudioPath = audioPath;
        }

        public static Record CreateNew(string audioPath)
        {
            return new Record(audioPath);
        }
    }
}
