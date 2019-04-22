using Realms;

namespace D328.Infrastructure.Repository
{
    public class Record : RealmObject
    {
        public int Id { get; set; }

        public string AudioPath { get; set; }
    }
}
