using D328.DI;
using D328.MultiRecording.Domain;
using D328.MultiRecording.UseCase;
using D328.Presentation.Framework;
using Reactive.Bindings;

namespace D328.Presentation
{
    public class MainWIndowViewModel : ViewModelBase
    {
        public ReactiveCollection<AudioDevice> InputAudioDevices { get; private set; } = new ReactiveCollection<AudioDevice>();

        public ReactiveCollection<AudioDevice> OutputAudioDevices { get; private set; } = new ReactiveCollection<AudioDevice>();

        public ReactiveCommand ContentRenderedCommand { get; } = new ReactiveCommand();

        private readonly IAudioDeviceUseCase audioDeviceUseCase = DIContainer.Instance.Resolve<IAudioDeviceUseCase>();

        public MainWIndowViewModel()
        {
            ContentRenderedCommand.Subscribe(ContentRenderedAction);
        }

        private async void ContentRenderedAction()
        {
            InputAudioDevices.AddRangeOnScheduler(await audioDeviceUseCase.GetInputAudioDevicesAsync());
            OutputAudioDevices.AddRangeOnScheduler(await audioDeviceUseCase.GetOutputAudioDevicesAsync());
        }
    }
}
