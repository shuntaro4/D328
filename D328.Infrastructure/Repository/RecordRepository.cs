using D328.Domain.Model;
using D328.Domain.Repository;
using Realms;

namespace D328.Infrastructure.Repository
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
            var realm = Realm.GetInstance();
            realm.Write(() =>
            {
                realm.Add(this);
            });
        }
    }
}
