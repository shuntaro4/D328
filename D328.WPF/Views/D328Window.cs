using MahApps.Metro.Controls;
using System.Windows.Controls;

namespace D328.WPF.Views
{
    public class D328Window : MetroWindow
    {
        public D328Window()
        {
            SaveWindowPosition = true;
            WindowTransitionsEnabled = false;
            TitleCharacterCasing = CharacterCasing.Normal;
        }
    }
}
