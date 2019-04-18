using D328.Domain.Model;
using System;

namespace D328.Platform
{
    public interface IAudioRecorderService : IDisposable
    {
        void Ready();

        void Start();

        void Stop();

        float GetPeak();

        void SubscriveEventOnDataAvailable(EventHandler subscriveEvent);

        D328Record GetRecordData();
    }
}
