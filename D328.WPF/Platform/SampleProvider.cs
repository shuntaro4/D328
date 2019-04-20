using NAudio.Wave;

namespace D328.WPF.Platform
{
    public class SampleProvider : ISampleProvider
    {
        private ISampleProvider _source;

        public SampleProvider(ISampleProvider source)
        {
            _source = source;
        }

        public WaveFormat WaveFormat => _source.WaveFormat;

        public int Read(float[] buffer, int offset, int count)
        {
            var samplesRead = _source.Read(buffer, offset, count);
            return samplesRead;
        }
    }
}
