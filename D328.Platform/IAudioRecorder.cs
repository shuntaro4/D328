using System;

namespace D328.Platform
{
    public interface IAudioRecorder : IDisposable
    {
        void Ready();

        void Start();

        void Stop();

        float GetPeak();

        void SubscriveEventOnDataAvailable(EventHandler subscriveEvent);
    }
}
