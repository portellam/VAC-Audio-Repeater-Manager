using Microsoft.Win32;
using System;
using System.Globalization;

namespace VACARM.NET4
{
    public class DarkModeValidator
    {
        #region Parameters

        private readonly static string appUsesLightThemeRegistryKeyValue =
            "AppsUseLightTheme";

        private readonly static string darkModeRegistrySubKey =
            @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";

        private readonly static string systemUsesLightThemeRegistryKeyValue =
            "SystemUsesLightTheme";

        private static bool currentUserAppUseLightTheme
        {
            get
            {
                try
                {
                    var registryKey =
                        Registry.CurrentUser.OpenSubKey(darkModeRegistrySubKey);

                    var appUseLightThemeIsEnabled =
                        registryKey?.GetValue(appUsesLightThemeRegistryKeyValue);

                    return Convert.ToBoolean(appUseLightThemeIsEnabled,
                        CultureInfo.InvariantCulture);
                }
                catch
                {
                    return false;
                }
            }
        }

        private static bool currentUserSystemUsesLightTheme
        {
            get
            {
                try
                {
                    var registryKey =
                        Registry.CurrentUser.OpenSubKey(darkModeRegistrySubKey);

                    var appUseLightThemeIsEnabled =
                        registryKey?.GetValue(systemUsesLightThemeRegistryKeyValue);

                    return Convert.ToBoolean(appUseLightThemeIsEnabled,
                        CultureInfo.InvariantCulture);
                }
                catch
                {
                    return false;
                }
            }
        }

        private static bool localMachineAppUseLightTheme
        {
            get
            {
                try
                {
                    var registryKey =
                        Registry.LocalMachine.OpenSubKey(darkModeRegistrySubKey);

                    var appUseLightThemeIsEnabled =
                        registryKey?.GetValue(appUsesLightThemeRegistryKeyValue);

                    return Convert.ToBoolean(appUseLightThemeIsEnabled,
                        CultureInfo.InvariantCulture);
                }
                catch
                {
                    return false;
                }
            }
        }

        private static bool isLightThemeEnabled = isLightThemeEnabledAtStart;

        private readonly static bool isLightThemeEnabledAtStart =
            IsLightThemeEnabledInRegistry;

        public static bool IsLightThemeEnabled
        {
            get
            {
                if (Program.DoesArgumentForceColorTheme)
                {
                    return Program.ForcedLightTheme;
                }

                return isLightThemeEnabled;
            }
            set
            {
                isLightThemeEnabled = value;
            }
        }

        public static bool IsLightThemeEnabledInRegistry
        {
            get
            {
                return localMachineAppUseLightTheme
                    || currentUserAppUseLightTheme
                    || currentUserSystemUsesLightTheme;
            }
        }

        #endregion
    }
}