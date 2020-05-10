using Windows.Devices.Enumeration;

namespace D328.MultiRecording.Domain
{
    public class AudioDevice
    {
        public string Id { get; private set; }

        public string Name { get; private set; }

        public bool IsDefault { get; private set; }

        public DeviceInformation Value { get; private set; }

        public AudioDevice(DeviceInformation deviceInformation, bool isDefault = false)
        {
            Id = deviceInformation.Id;
            Name = deviceInformation.Name;
            IsDefault = isDefault;
            Value = deviceInformation;
        }
    }
}
