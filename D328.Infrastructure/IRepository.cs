namespace D328.Repository
{
    public interface IRepository
    {
        void Save();

        int GetMaxId();
    }
}
