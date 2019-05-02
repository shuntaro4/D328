namespace D328.Domain.Model
{
    public class Record
    {
        public int Id { get; } = -1;

        public string AudioPath { get; } = "";

        public string Title { get; set; } = "";

        private Record(int Id, string audioPath, string title)
        {
            AudioPath = audioPath;
            Title = title;
        }

        public static Record CreateNew(string audioPath, int Id = -1, string title = "")
        {
            return new Record(Id, audioPath, title);
        }
    }
}
