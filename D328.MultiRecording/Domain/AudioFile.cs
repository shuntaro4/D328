using System;

namespace D328.MultiRecording.Domain
{
    public class AudioFile
    {
        public string Path { get; }

        public AudioFile(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException();
            }

            Path = path;
        }
    }
}
