using System.Threading.Tasks;
using Windows.Storage;

namespace D328.MultiRecording.Domain
{
    public interface IFileCreator
    {
        Task<StorageFile> CreateAudioFileAsync();
    }
}
