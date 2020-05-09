using D328.MultiRecording.Domain;
using D328.MultiRecording.Infrastructure;
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
            serviceCollection.AddSingleton<IRecordingUseCase, RecordingUseCase>();

            serviceProvider = serviceCollection.BuildServiceProvider();
        }

        public T Resolve<T>()
        {
            return serviceProvider.GetRequiredService<T>();
        }
    }
}
