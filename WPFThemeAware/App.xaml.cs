using System.Windows;

namespace WPFThemeAware
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            ThemeHelper.SetLightTheme();

            base.OnStartup(e);
        }
    }
}
