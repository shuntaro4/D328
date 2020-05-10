using Windows.Devices.Enumeration;

namespace D328.MultiRecording.Domain
{
    public class AudioDevice
    {
        public string Id { get; private set; }

        public string Name { get; private set; }

        public DeviceInformation Value { get; private set; }

        public AudioDevice(DeviceInformation deviceInformation)
        {
            Id = deviceInformation.Id;
            Name = deviceInformation.Name;
        }
    }
}
