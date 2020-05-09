using System;

namespace D328.MultiRecording.Domain
{
    public class Recording
    {
        public RecordingId Id { get; private set; }
        public User User { get; private set; }

        public Recording(RecordingId id, User user)
        {
            Id = id ?? throw new ArgumentException();
            User = user ?? throw new ArgumentException();
        }

        public static Recording CreateNew(User user)
        {
            var id = RecordingId.CreateNew();
            return new Recording(id, user);
        }
    }
}
