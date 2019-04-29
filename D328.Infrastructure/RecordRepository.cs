using D328.Domain.Model;
using Realms;

namespace D328.Repository
{
    public class RecordRepository : RealmObject, IRepository
    {
        public int Id { get; set; }

        public string AudioPath { get; set; }

        public RecordRepository()
        {

        }

        public RecordRepository(Record record)
        {
            Id = record.Id;
            AudioPath = record.AudioPath;
        }

        public void Save()
        {
            var realm = RealmHelper.GetInstance();
            realm.Write(() =>
            {
                realm.Add(this);
            });
        }
    }
}
