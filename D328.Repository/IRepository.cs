using System.Collections.Generic;

namespace D328.Repository
{
    public interface IRepository<T>
    {
        int NextIdentity();

        IEnumerable<T> FindAll();
    }
}
