﻿using System.Collections.Generic;

namespace D328.Platform
{
    public interface IAudioDeviceService<T>
    {
        List<T> GetAudioDevices();
    }
}