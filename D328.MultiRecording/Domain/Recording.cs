using System;

namespace D328.MultiRecording.Domain
{
    public class Recording
    {
        public User User { get; private set; }

        public Recording(User user)
        {
            User = user ?? throw new ArgumentException();
        }
    }
}
