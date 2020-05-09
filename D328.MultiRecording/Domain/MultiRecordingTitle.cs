using System;

namespace D328.MultiRecording.Domain
{
    public class MultiRecordingTitle
    {
        public string Value { get; private set; }

        public MultiRecordingTitle(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException();
            }

            Value = value;
        }
    }
}
