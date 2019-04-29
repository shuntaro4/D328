using D328.Domain.Model;
using Realms;
using System.Linq;

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

        public int GetMaxId()
        {
            var realm = RealmHelper.GetInstance();
            return realm.All<RecordRepository>()
                .OrderByDescending(x => x.Id)
                .FirstOrDefault()?.Id ?? 0;
        }
    }
}
