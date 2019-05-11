using D328.Domain.Model;
using System.Collections.Generic;
using System.Linq;

namespace D328.Repository
{
    public class LineObjectRepository : IRepository<Line>
    {
        public void Save(Line line)
        {
            var realm = RealmHelper.GetInstance();
            realm.Write(() =>
            {
                var lineObject = new LineObject(line);
                if (lineObject.Id < 0)
                {
                    var id = NextIdentity();
                    lineObject.Id = id;
                }
                realm.Add(lineObject);
            });
        }

        public int NextIdentity()
        {
            var realm = RealmHelper.GetInstance();
            return realm.All<LineObject>()
                .OrderByDescending(x => x.Id)
                .FirstOrDefault()?.Id + 1 ?? 1;
        }

        public IEnumerable<Line> FindAll()
        {
            var realm = RealmHelper.GetInstance();
            return realm.All<LineObject>()
                // "Select" is not supported by Realm. So, convert it to List type.
                .ToList()
                .Select(x => x.ToLine());
        }
    }
}