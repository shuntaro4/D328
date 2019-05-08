using System.Collections.Generic;

namespace D328.Repository
{
    public interface IRepository<T>
    {
        void Save(T target);

        int NextIdentity();

        IEnumerable<T> FindAll();
    }
}
