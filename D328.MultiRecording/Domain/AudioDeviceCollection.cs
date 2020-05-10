using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.Devices.Enumeration;

namespace D328.MultiRecording.Domain
{
    public class AudioDeviceCollection : Collection<AudioDevice>
    {
        public AudioDeviceCollection(IList<AudioDevice> list) : base(list) { }

        public AudioDeviceCollection(DeviceInformationCollection deviceInformationCollection)
            : this(deviceInformationCollection.Select(x => new AudioDevice(x)).ToList()) { }
    }
}
