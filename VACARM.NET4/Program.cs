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

		private static bool isDarkModeEnabledDuringRunTime { get; set; }

		public static bool IsDarkModeEnabledBeforeRunTime { get; private set; }

		public static bool IsDarkModeEnabledDuringRunTime
		{
			get
			{
				DoesSystemSupportDarkMode();
				return isDarkModeEnabledDuringRunTime;
			}
			set
			{
				if (value != IsDarkModeEnabledBeforeRunTime)
				{
					isDarkModeEnabledDuringRunTime = value;
					return;
				}

				isDarkModeEnabledDuringRunTime = IsDarkModeEnabledBeforeRunTime;
			}
		}

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
		/// Easy way to check if system is Windows 10 or later.
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

		#endregion
	}
}