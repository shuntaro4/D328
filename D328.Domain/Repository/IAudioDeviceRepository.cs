using System.Collections.Generic;

namespace D328.Domain.Repository
{
    public interface IAudioDeviceRepository<T>
    {
        List<T> GetAudioDevices();
    }
}
