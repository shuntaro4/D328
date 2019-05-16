using D328.Domain.Model;
using Realms;
using System.Collections.Generic;
using System.Linq;

namespace D328.Repository
{
    public class RecordRepository : IRepository<Record>
    {
        private readonly Realm db;

        public RecordRepository()
        {
            db = RealmHelper.GetInstance();
        }

        public Record Save(Record record)
        {
            var recordData = RecordData.CreateNew(record);
            db.Write(() =>
            {
                if (recordData.Id < 0)
                {
                    var id = NextIdentity();
                    recordData.Id = id;
                }
                db.Add(recordData, true);
            });
            return recordData.ToDomainModel();
        }

        public int NextIdentity()
        {
            return db.All<RecordData>()
                .OrderByDescending(x => x.Id)
                .FirstOrDefault()?.Id + 1 ?? 1;
        }

        public int ChildNextIdentity()
        {
            return db.All<LineData>()
                .OrderByDescending(x => x.Id)
                .FirstOrDefault()?.Id + 1 ?? 1;
        }

        public IEnumerable<Record> FindAll()
        {
            // "Select" is not supported by Realm. So, convert it to List type.
            var list = db.All<RecordData>().ToList();
            var result = list.Select(x => x.ToDomainModel());
            return result;
        }
    }
}