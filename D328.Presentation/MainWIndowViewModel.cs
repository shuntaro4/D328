using D328.DI;
using D328.MultiRecording.Domain;
using D328.MultiRecording.UseCase;
using D328.Presentation.Framework;
using Reactive.Bindings;
using System.Linq;

namespace D328.Presentation
{
    public class MainWIndowViewModel : ViewModelBase
    {
        public ReactiveCollection<AudioDevice> InputAudioDevices { get; private set; } = new ReactiveCollection<AudioDevice>();
        public ReactiveProperty<AudioDevice> InputAudioDevice { get; private set; } = new ReactiveProperty<AudioDevice>();
        public ReactiveCollection<AudioDevice> OutputAudioDevices { get; private set; } = new ReactiveCollection<AudioDevice>();
        public ReactiveProperty<AudioDevice> OutputAudioDevice { get; private set; } = new ReactiveProperty<AudioDevice>();
        public ReactiveProperty<bool> IsRecording { get; private set; } = new ReactiveProperty<bool>(false);

        public ReactiveCommand ContentRenderedCommand { get; } = new ReactiveCommand();

        private readonly IAudioDeviceUseCase audioDeviceUseCase = DIContainer.Instance.Resolve<IAudioDeviceUseCase>();

        public MainWIndowViewModel()
        {
            ContentRenderedCommand.Subscribe(ContentRenderedAction);
        }

        private async void ContentRenderedAction()
        {
            var inputCollection = await audioDeviceUseCase.GetInputAudioDevicesAsync();
            InputAudioDevices.AddRangeOnScheduler(inputCollection);
            InputAudioDevice.Value = inputCollection.Where(x => x.IsDefault).FirstOrDefault();

            var outputCollection = await audioDeviceUseCase.GetOutputAudioDevicesAsync();
            OutputAudioDevices.AddRangeOnScheduler(outputCollection);
            OutputAudioDevice.Value = outputCollection.Where(x => x.IsDefault).FirstOrDefault();
        }
    }
}
