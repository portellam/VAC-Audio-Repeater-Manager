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
        #region Arguments

        /// <summary>
        /// The command line arguments.
        /// </summary>
        public static string[] Arguments { get; private set; }

        #endregion

        #region Execution flags

        private static bool firstTimeToSetIsDarkModeIsEnabled = true;

        private readonly static bool isDarkModeEnabledBySystemBeforeRunTime =
            DoesSystemSupportDarkMode();

        private static bool IsDarkModeEnabledDuringRunTime
        {
            get
            {
                return DoesSystemSupportDarkMode();
            }
        }

        private static bool isDarkModeEnabled { get; set; }

        public static bool IsDarkModeEnabled
        {
            get
            {
                return isDarkModeEnabled;
            }
            set
            {
                if (!firstTimeToSetIsDarkModeIsEnabled)
                {
                    isDarkModeEnabled = value;
                }

                firstTimeToSetIsDarkModeIsEnabled = !firstTimeToSetIsDarkModeIsEnabled;

                if (IsDarkModeEnabledByArgument is null)
                {
                    isDarkModeEnabled = isDarkModeEnabledBySystemBeforeRunTime;
                }
                else
                {
                    isDarkModeEnabled = IsDarkModeEnabledByArgument.Value;
                }
            }
        }

        public static bool? IsDarkModeEnabledByArgument { get; private set; }

        #endregion

        #region Logic

        /// <summary>
        /// The main code block, to be executed at run time.
        /// </summary>
        /// <param name="arguments">The command line arguments</param>
        [STAThread]
        public static void Main(string[] arguments)
        {
            Arguments = arguments;
            ParseArguments();
            IsDarkModeEnabled = true;
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
                        IsDarkModeEnabledByArgument = true;
                        break;

                    default:
                        break;
                }
            });
        }

        /// <summary>
        /// Check if system has Registry key to indicate it supports Dark Mode,
        /// and if it is enabled.
        /// Easy way to check if system is Windows 10 or later.
        /// </summary>
        /// <returns>True/False</returns>
        internal static bool DoesSystemSupportDarkMode()
        {
            const string registrySubKey =
                @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";

            try
            {
                var registryKey = Registry.LocalMachine.OpenSubKey(registrySubKey);
                const string registryKeyValue = "AppsUseLightTheme";

                var windowsLightThemeIsEnabled = registryKey?.GetValue
                    (registryKeyValue);

                return !Convert.ToBoolean
                    (windowsLightThemeIsEnabled, CultureInfo.InvariantCulture);
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}