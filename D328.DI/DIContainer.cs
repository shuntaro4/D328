using D328.MultiRecording.Domain;
using D328.MultiRecording.Infrastructure;
using D328.MultiRecording.Infrastructure.Repository;
using D328.MultiRecording.UseCase;
using Microsoft.Extensions.DependencyInjection;

namespace D328.DI
{
    public class DIContainer
    {
        public static DIContainer Instance { get; } = new DIContainer();

        private ServiceProvider serviceProvider;

        public DIContainer()
        {
            var serviceCollection = new ServiceCollection();

            // MultiRecording
            serviceCollection.AddSingleton<IFileCreator, FileCreator>();
            serviceCollection.AddSingleton<IRecorder, Recorder>();
            serviceCollection.AddSingleton<IUserRepository, UserRepository>();
            serviceCollection.AddSingleton<IRecordingUseCase, RecordingUseCase>();
            serviceCollection.AddSingleton<IAudioDeviceUseCase, AudioDeviceUseCase>();

            serviceProvider = serviceCollection.BuildServiceProvider();
        }

        public T Resolve<T>()
        {
            return serviceProvider.GetRequiredService<T>();
        }
    }
}
