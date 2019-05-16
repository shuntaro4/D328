using D328.Domain.Model;
using System.Collections.Generic;

namespace D328.Repository
{
    public interface IRecordRepository
    {
        void Save(Record record);

        IEnumerable<Record> FindAll();
    }
}
