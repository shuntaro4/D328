using System;
using System.IO;

namespace D328.Application.Services
{
    public class AudioFileService : IAudioFileService
    {
        public void Delete(string audioPath)
        {
            try
            {
                if (File.Exists(audioPath))
                {
                    File.Delete(audioPath);
                }
            }
            catch (Exception ex)
            {
                // todo log
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
