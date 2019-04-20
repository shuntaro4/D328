using System;

namespace D328.Platform
{
    public interface IAudioPlayerService : IDisposable
    {
        void Play();

        void Pause();

        void Stop();
    }
}
