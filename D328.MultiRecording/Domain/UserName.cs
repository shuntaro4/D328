using System;

namespace D328.MultiRecording.Domain
{
    public class UserName
    {
        public string Value { get; private set; }

        public static readonly string DefaultValue = "NoName";

        public UserName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException();
            }

            Value = value;
        }

        public static UserName CreateDefault()
        {
            return new UserName(DefaultValue);
        }
    }
}
