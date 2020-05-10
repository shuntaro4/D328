using D328.DI;
using D328.MultiRecording.UseCase;
using System.Windows;

namespace D328.Presentation
{
    public partial class MainWindow : Window
    {
        private MainWIndowViewModel viewModel;

        private readonly IRecordingUseCase recordingUseCase = DIContainer.Instance.Resolve<IRecordingUseCase>();

        public MainWindow()
        {
            InitializeComponent();

            viewModel = new MainWIndowViewModel();
            DataContext = viewModel;
        }

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            await recordingUseCase.StartAsync();
        }

        private async void StopButton_Click(object sender, RoutedEventArgs e)
        {
            var recording = await recordingUseCase.StopAsync();
        }
    }
}
