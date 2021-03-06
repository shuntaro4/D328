﻿using System;

namespace D328.Application.Services
{
    public interface IAudioRecorderService : IDisposable
    {
        void Ready();

        void Start();

        void Stop();

        float GetPeak();

        void SubscriveEventOnDataAvailable(EventHandler subscriveEvent);

        string GetAudioPath();
    }
}
