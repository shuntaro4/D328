using Realms;

namespace D328.Repository
{
    public class LineObject : RealmObject
    {
        public int Id { get; set; }

        public int SortNumber { get; set; }

        public string AudioPath { get; set; }

        public LineObject()
        {
        }
    }
}
