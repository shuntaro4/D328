using D328.Domain.Model;
using Realms;
using System.Collections.Generic;
using System.Linq;

namespace D328.Repository
{
    public class RecordRepository : RealmObject, IRepository<Record>
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
                var id = GetMaxId();
                Id = id + 1;
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

        public IEnumerable<Record> FindAll()
        {
            var realm = RealmHelper.GetInstance();
            return realm.All<RecordRepository>()
                // "Select" is not supported by Realm. So, convert it to List type.
                .ToList()
                .Select(x => x.ToRecord());
        }

        private Record ToRecord()
        {
            return Record.CreateNew(AudioPath, Id);
        }
    }
}