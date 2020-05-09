using System;

namespace D328.MultiRecording.Domain
{
    public class Title
    {
        public string Value { get; private set; }

        public Title(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException();
            }

            Value = value;
        }
    }
}
