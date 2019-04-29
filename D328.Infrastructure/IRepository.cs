using System.Collections.Generic;

namespace D328.Repository
{
    public interface IRepository<T>
    {
        void Save();

        int GetMaxId();

        IEnumerable<T> FindAll();
    }
}
