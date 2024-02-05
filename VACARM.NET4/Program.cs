using Microsoft.Win32;
using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using VACARM.NET4.Views;

namespace VACARM.NET4
{
    public class Program
    {
        public static string[] Arguments { get; private set; }
        public static bool IsDarkModeEnabledBeforeRunTime { get; private set; }

        [STAThread]
        public static void Main(string[] arguments)
        {
            Arguments = arguments;
            ParseArguments();
            DoesSystemSupportDarkMode();
            Application.Run(new MainForm());
        }

        /// <summary>
        /// Parse arguments passed by command line.
        /// </summary>
        internal static void ParseArguments()
        {
            if (Arguments.Count() == 0)
            {
                return;
            }

            Arguments.ToList().ForEach(argument =>
            {
                if (!argument.StartsWith("/"))
                {
                    return;
                }

                switch (argument)
                {
                    case "/darkmode":
                        IsDarkModeEnabledBeforeRunTime = true;
                        break;

                    default:
                        break;
                }
            });
        }

        /// <summary>
        /// Check if system has Registry key to indicate it supports Dark Mode,
        /// and if it is enabled.
        /// </summary>
        internal static void DoesSystemSupportDarkMode()
        {
            if (IsDarkModeEnabledBeforeRunTime)
            {
                return;
            }

            const string subKey =
                @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";

            try
            {
                var registryKey = Registry.CurrentUser.OpenSubKey(subKey);
                const string registryKeyValue = "AppsUseLightTheme";
                var windowsLightThemeIsEnabled = registryKey?.GetValue
                    (registryKeyValue);

                if (windowsLightThemeIsEnabled is null)
                {
                    return;
                }

                IsDarkModeEnabledBeforeRunTime = !Convert.ToBoolean
                    (windowsLightThemeIsEnabled, CultureInfo.InvariantCulture);
            }
            catch
            {
                return;
            }
        }
    }
}
