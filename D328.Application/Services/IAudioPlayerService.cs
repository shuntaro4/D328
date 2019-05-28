using System;

namespace D328.Application.Services
{
    public interface IAudioPlayerService : IDisposable
    {
        void Play();

        void Pause();

        void Stop();

        TimeSpan GetTotalTime();
    }
}
