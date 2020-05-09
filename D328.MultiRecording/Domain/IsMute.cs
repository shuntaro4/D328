namespace D328.MultiRecording.Domain
{
    public class IsMute
    {
        public bool Value { get; private set; }

        public IsMute(bool value)
        {
            Value = value;
        }
    }
}
