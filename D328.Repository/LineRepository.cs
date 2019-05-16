using D328.Domain.Model;
using System.Collections.Generic;
using System.Linq;

namespace D328.Repository
{
    public class LineRepository : IRepository<Line>
    {
        public Line Save(Line line, Record record)
        {
            var realm = RealmHelper.GetInstance();
            var lineObject = LineData.CreateNew(line, record);
            realm.Write(() =>
            {
                if (lineObject.Id < 0)
                {
                    var id = NextIdentity();
                    lineObject.Id = id;
                }
                realm.Add(lineObject, true);
            });
            return lineObject.ToDomainModel();
        }

        public int NextIdentity()
        {
            var realm = RealmHelper.GetInstance();
            return realm.All<LineData>()
                .OrderByDescending(x => x.Id)
                .FirstOrDefault()?.Id + 1 ?? 1;
        }

        public IEnumerable<Line> FindAll()
        {
            var realm = RealmHelper.GetInstance();
            return realm.All<LineData>()
                // "Select" is not supported by Realm. So, convert it to List type.
                .ToList()
                .Select(x => x.ToDomainModel());
        }
    }
}