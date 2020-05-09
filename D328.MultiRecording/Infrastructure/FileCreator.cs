using D328.MultiRecording.Domain;
using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;

namespace D328.MultiRecording.Infrastructure
{
    public class FileCreator : IFileCreator
    {
        public async Task<StorageFile> CreateAudioFileAsync()
        {
            var folderPath = @$"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\D328\AudioFiles";
            Directory.CreateDirectory(folderPath);
            var folder = await StorageFolder.GetFolderFromPathAsync(folderPath);
            var fileName = $"{DateTime.Now:yyyy-MM-dd-HHmmssfffffff}.mp3";
            return await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
        }
    }
}
