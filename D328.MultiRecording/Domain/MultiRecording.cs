using System;

namespace D328.MultiRecording.Domain
{
    public class MultiRecording
    {
        public Title Title { get; private set; }

        public MultiRecording(Title title)
        {
            Title = title ?? throw new ArgumentException();
        }
    }
}
