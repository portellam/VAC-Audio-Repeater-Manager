using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using VACARM.NET4.Extensions;

namespace VACARM.NET4.ViewModels
{
    public class LightThemeValidator
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

        public static bool DoesLightThemeDifferFromRegistry
        {
            get
            {
                return IsLightThemeEnabled != IsLightThemeEnabledInRegistry;
            }
        }

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

        public static WmiRegistryEventListener WmiRegistryEventListener;

        #endregion

        #region

        /// <summary>
        /// Constructor
        /// </summary>
        public LightThemeValidator()
        {
            RegistryKey registryKey =
                Registry.CurrentUser.OpenSubKey(darkModeRegistrySubKey);

            List<string> registryKeyValueList = new List<string>()
            {
                string.Concat
                    (darkModeRegistrySubKey, "\\" , appUsesLightThemeRegistryKeyValue),

                string.Concat
                    (darkModeRegistrySubKey, "\\", systemUsesLightThemeRegistryKeyValue)
            };

            Dictionary<RegistryKey, List<string>> registryKeyAndSubKeyPathListDictionary
                = new Dictionary<RegistryKey, List<string>>
                {
                    {
                        registryKey, registryKeyValueList

                    },
                };

            WmiRegistryEventListener =
                new WmiRegistryEventListener(registryKeyAndSubKeyPathListDictionary);
        }

        #endregion
    }
}