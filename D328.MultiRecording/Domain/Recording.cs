using System;

namespace D328.MultiRecording.Domain
{
    public class Recording
    {
        public RecordingId Id { get; private set; }
        public User User { get; private set; }
        public AudioFile AudioFile { get; private set; }


        public Recording(RecordingId id, User user, AudioFile audioFile)
        {
            Id = id ?? throw new ArgumentException();
            User = user ?? throw new ArgumentException();
            AudioFile = audioFile ?? throw new ArgumentException();
        }

        public static Recording CreateNew(User user, AudioFile audioFile)
        {
            var id = RecordingId.CreateNew();
            return new Recording(id, user, audioFile);
        }
    }
}
