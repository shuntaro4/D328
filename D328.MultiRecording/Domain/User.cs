using System;

namespace D328.MultiRecording.Domain
{
    public class User
    {
        public UserName Name { get; private set; }

        public User(UserName name)
        {
            Name = name ?? throw new ArgumentException();
        }
    }
}
