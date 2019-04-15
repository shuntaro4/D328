namespace D328.Domain.Model
{
    public class D328Record
    {
        public int Id { get; } = -1;

        public string AudioPath { get; } = "";

        public D328Record(string audioPath)
        {
            AudioPath = audioPath;
        }
    }
}
