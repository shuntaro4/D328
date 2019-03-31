using D328.WPF.Views;
using Prism.Ioc;
using System.Windows;

namespace D328.WPF
{
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}