using D328.Domain.Model;
using Realms;
using System.Collections.Generic;
using System.Linq;

namespace D328.Repository
{
    public class RecordRepository : IRecordRepository
    {
        private readonly Realm db;

        public RecordRepository()
        {
            db = RealmHelper.GetInstance();
        }

        public void Save(Record record)
        {
            db.Write(() =>
            {
                var recordData = RecordData.CreateNew(record);
                if (recordData.Id < 0)
                {
                    var id = NextIdentity();
                    recordData.Id = id;
                }
                db.Add(recordData, true);
                var lineDataList = record.Lines.Select(line => LineData.CreateNew(line, recordData)).ToList();
                foreach (var lineData in lineDataList)
                {
                    if (lineData.Id < 0)
                    {
                        var id = ChildNextIdentity();
                        lineData.Id = id;
                    }
                    db.Add(lineData, true);
                }
            });
        }

        private int NextIdentity()
        {
            return db.All<RecordData>()
                .OrderByDescending(x => x.Id)
                .FirstOrDefault()?.Id + 1 ?? 1;
        }

        private int ChildNextIdentity()
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

        public void Remove(Record record)
        {
            db.Write(() =>
            {
                var target = db.Find<RecordData>(record.Id);
                db.Remove(target);
            });
        }
    }
}