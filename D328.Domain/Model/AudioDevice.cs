namespace D328.Domain
{
    public class AudioDevice
    {
        public string Id { get; private set; }

        public string Name { get; private set; }

        private AudioDevice()
        {
        }

        public static AudioDevice CreateNewDevice(string id, string name)
        {
            return new AudioDevice
            {
                Id = id,
                Name = name
            };
        }
    }
}