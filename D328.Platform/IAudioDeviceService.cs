using System.Collections.Generic;

namespace D328.Platform
{
    public interface IAudioDeviceService<T>
    {
        List<T> GetInputAudioDevices();

        T GetDefaultInputAudioDevice();

        List<T> GetOutputAudioDevices();
    }
}
