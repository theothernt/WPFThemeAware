using Dapplo.Windows.Advapi32;
using MahApps.Metro;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Reactive;
using System.Windows;

namespace WPFThemeAware
{
    public static class ThemeHelper
    {
        const string PersonalizeKeys = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
        const string AppThemeKey = "AppsUseLightTheme";
        //const string SystemThemeKey = "SystemUsesLightTheme";
        
        public static SystemTheme GetCurrentTheme()
        {
            Debug.WriteLine("GetCurrentTheme");
            using (RegistryKey keys = Registry.CurrentUser.OpenSubKey(PersonalizeKeys, false))
            {
                if (keys == null)
                    return SystemTheme.Unknown;

                var appTheme = keys.GetValue(AppThemeKey) as int?;
                if (appTheme == null)
                    return SystemTheme.Unknown;
                
                if (appTheme == 1)
                    return SystemTheme.Light;
                else
                    return SystemTheme.Dark;
            }
        }

        public static IObservable<Unit> ObserveThemeChange()
        {
            Debug.WriteLine("ObserveThemeChange");
            return RegistryMonitor.ObserveChanges(RegistryHive.CurrentUser, PersonalizeKeys);
        }

        public static void SetDarkTheme()
        {
            ThemeManager.ChangeAppStyle(Application.Current,
                                        ThemeManager.GetAccent("Orange"),
                                        ThemeManager.GetAppTheme("BaseDark"));
        }

        public static void SetLightTheme()
        {
            ThemeManager.ChangeAppStyle(Application.Current,
                                          ThemeManager.GetAccent("Steel"),
                                          ThemeManager.GetAppTheme("BaseLight"));
        }
    }

    public enum SystemTheme{
        Dark,
        Light,
        Unknown,
    }
}
