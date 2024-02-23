using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using VACARM.NET4.Extensions;

namespace VACARM.NET4.ViewModels
{
    public class LightThemeValidator
    {
        #region Parameters

        private Dictionary<RegistryKey, List<string>>
            registryKeyAndSubKeyListDictionary;

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
                return currentUserAppUseLightTheme
                    || currentUserSystemUsesLightTheme;
            }
        }

        public static WMIRegistryEventListener WMIRegistryEventListener;

        #endregion

        #region Logic

        /// <summary>
        /// Constructor
        /// </summary>
        public LightThemeValidator()
        {
            if (Program.DoesArgumentForceColorTheme)
            {
                return;
            }

            RegistryKey registryKey =
                Registry.CurrentUser.OpenSubKey(darkModeRegistrySubKey);

            List<string> registryKeyList = new List<string>()
            {
                appUsesLightThemeRegistryKeyValue, systemUsesLightThemeRegistryKeyValue
            };

            registryKeyAndSubKeyListDictionary
                = new Dictionary<RegistryKey, List<string>>
                {
                    {
                        registryKey, registryKeyList
                    },
                };

            WMIRegistryEventListener =
                new WMIRegistryEventListener(registryKeyAndSubKeyListDictionary);
        }


        //      public async void WatchAndSetLightThemeEnabled()
        //      {
        //          if (WMIRegistryEventListener is null)
        //          {
        //              return;
        //          }

        //          //TODO: add task here
        //}

        #endregion
    }
}