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
        {
            var list = new List<AudioDevice>();
            deviceInformationCollection.ToList().ForEach(x => list.Add(new AudioDevice(x)));
        }
    }
}
