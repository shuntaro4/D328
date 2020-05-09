using System;

namespace D328.MultiRecording.Domain
{
    public class MultiRecordingId
    {
        public long Value { get; private set; }

        public MultiRecordingId(long value)
        {
            if (value <= 0)
            {
                throw new ArgumentException();
            }

            Value = value;
        }

        public static MultiRecordingId CreateNew()
        {
            return new MultiRecordingId(DateTime.Now.Ticks);
        }
    }
}
