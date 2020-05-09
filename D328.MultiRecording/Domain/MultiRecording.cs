using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace D328.MultiRecording.Domain
{
    public class MultiRecording
    {
        public Title Title { get; private set; }
        public ReadOnlyCollection<Recording> Recordings => recordings.AsReadOnly();

        private List<Recording> recordings = new List<Recording>();

        public MultiRecording(Title title)
        {
            Title = title ?? throw new ArgumentException();
        }
    }
}
