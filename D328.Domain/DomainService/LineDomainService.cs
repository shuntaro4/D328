using D328.Domain.Model;
using System.Collections.Generic;
using System.Linq;

namespace D328.Domain.DomainService
{
    public class LineDomainService
    {
        public static int CalcNewSortNumber(IEnumerable<Line> lines)
        {
            if (lines == null || lines.Count() < 1)
            {
                return 1;
            }
            return lines.Max(x => x.SortNumber) + 1;
        }
    }
}
