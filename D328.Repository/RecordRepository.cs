﻿using D328.Domain.Model;
using System.Collections.Generic;
using System.Linq;

namespace D328.Repository
{
    public class RecordRepository : IRepository<Record>
    {
        public void Save(Record record)
        {
            var realm = RealmHelper.GetInstance();
            realm.Write(() =>
            {
                var recordObject = RecordObject.CreateNew(record);
                if (recordObject.Id < 0)
                {
                    var id = NextIdentity();
                    recordObject.Id = id;
                }
                realm.Add(recordObject);
            });
        }

        public int NextIdentity()
        {
            var realm = RealmHelper.GetInstance();
            return realm.All<RecordObject>()
                .OrderByDescending(x => x.Id)
                .FirstOrDefault()?.Id + 1 ?? 1;
        }

        public IEnumerable<Record> FindAll()
        {
            var realm = RealmHelper.GetInstance();
            return realm.All<RecordObject>()
                // "Select" is not supported by Realm. So, convert it to List type.
                .ToList()
                .Select(x => x.ToDomainModel());
        }
    }
}