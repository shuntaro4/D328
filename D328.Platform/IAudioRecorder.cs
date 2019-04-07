using System;

namespace D328.Platform
{
    public interface IAudioRecorder : IDisposable
    {
        void Start();

        void Stop();
    }
}
