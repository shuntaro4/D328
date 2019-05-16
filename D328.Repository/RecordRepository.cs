using D328.Domain.Model;
using System.Collections.Generic;
using System.Linq;

namespace D328.Repository
{
    public class RecordRepository : IRepository<Record>
    {
        public Record Save(Record record)
        {
            var realm = RealmHelper.GetInstance();
            var recordObject = RecordData.CreateNew(record);
            realm.Write(() =>
            {
                if (recordObject.Id < 0)
                {
                    var id = NextIdentity();
                    recordObject.Id = id;
                }
                realm.Add(recordObject, true);
            });
            return recordObject.ToDomainModel();
        }

        public int NextIdentity()
        {
            var realm = RealmHelper.GetInstance();
            return realm.All<RecordData>()
                .OrderByDescending(x => x.Id)
                .FirstOrDefault()?.Id + 1 ?? 1;
        }

        public IEnumerable<Record> FindAll()
        {
            var realm = RealmHelper.GetInstance();
            // "Select" is not supported by Realm. So, convert it to List type.
            var list = realm.All<RecordData>().ToList();
            var result = list.Select(x => x.ToDomainModel());
            return result;
        }
    }
}