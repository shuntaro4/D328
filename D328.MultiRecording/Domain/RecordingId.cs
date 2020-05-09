using System;

namespace D328.MultiRecording.Domain
{
    public class RecordingId
    {
        public long Value { get; private set; }

        public RecordingId(long value)
        {
            if (value <= 0)
            {
                throw new ArgumentException();
            }

            Value = value;
        }

        public static RecordingId CreateNew()
        {
            return new RecordingId(DateTime.Now.Ticks);
        }
    }
}
