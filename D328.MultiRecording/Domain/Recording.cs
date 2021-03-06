﻿using System;

namespace D328.MultiRecording.Domain
{
    public class Recording
    {
        public RecordingId Id { get; private set; }
        public User User { get; private set; }
        public AudioFile AudioFile { get; private set; }
        public IsMute IsMute { get; set; }

        public Recording(RecordingId id, User user, AudioFile audioFile, IsMute isMute)
        {
            Id = id ?? throw new ArgumentException();
            User = user ?? throw new ArgumentException();
            AudioFile = audioFile ?? throw new ArgumentException();
            isMute = isMute ?? throw new ArgumentException();
        }

        public static Recording CreateNew(User user, AudioFile audioFile)
        {
            var id = RecordingId.CreateNew();
            var isMute = new IsMute(false);
            return new Recording(id, user, audioFile, isMute);
        }
    }
}
