using MahApps.Metro.Controls;
using System;
using System.Diagnostics;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WPFThemeAware
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private bool _isMonitoring = false;
        private string _themeChoice;

        public MainWindow()
        {
            InitializeComponent();

            ThemeHelper.ObserveThemeChange().ObserveOn(DispatcherScheduler.Current).Subscribe(unit => {
                Debug.WriteLine("ObserveThemeChange");
                if (_isMonitoring && WatchForChanges.IsChecked == true)
                {
                    _themeChoice = "Use System";
                    SetChosenTheme();
                }
                    
            });

            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            UseSystemRadioButton.IsChecked = true;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            _themeChoice = (sender as RadioButton).Content as string;
            SetChosenTheme();
        }

        private void WatchForChanges_Click(object sender, RoutedEventArgs e)
        {
            _isMonitoring = !_isMonitoring;
            Debug.WriteLine($"Monitoring: {_isMonitoring}");
        }

        private void SetChosenTheme()
        {
            if (_themeChoice == "Use System")
            {
                WatchForChanges.IsEnabled = true;
                var theme = ThemeHelper.GetCurrentTheme();

                if (theme == SystemTheme.Unknown)
                    return;
                else
                    _themeChoice = theme.ToString();
            }
            else
            {
                WatchForChanges.IsEnabled = false;
            }

            if (_themeChoice == "Dark")
                ThemeHelper.SetDarkTheme();
            else
                ThemeHelper.SetLightTheme();
        }
    }
}