using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace D328.MultiRecording.Domain
{
    public class MultiRecording
    {
        public MultiRecordingId Id { get; private set; }
        public MultiRecordingTitle Title { get; private set; }
        public ReadOnlyCollection<Recording> Recordings => recordings.AsReadOnly();

        private List<Recording> recordings = new List<Recording>();

        public MultiRecording(MultiRecordingId id, MultiRecordingTitle title)
        {
            Id = id ?? throw new ArgumentException();
            Title = title ?? throw new ArgumentException();
        }

        public static MultiRecording CreateNew(MultiRecordingTitle title)
        {
            var id = MultiRecordingId.CreateNew();
            return new MultiRecording(id, title);
        }
    }
}
