using D328.MultiRecording.Domain;

namespace D328.MultiRecording.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        public User GetCurrentUser()
        {
            return new User(UserName.CreateDefault()); // todo: now, return a fixed value.
        }
    }
}
