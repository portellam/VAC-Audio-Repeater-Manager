using System;
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

        public static bool DoesArgumentForceColorTheme
        {
            get
            {
                return doForceDarkThemeAtStart.HasValue
                    || doForceLightThemeAtStart.HasValue;
            }
        }

        public static bool ForcedLightTheme
        {
            get
            {
                if (doForceLightThemeAtStart.HasValue)
                {
                    return doForceLightThemeAtStart.Value;
                }

                return false;
            }
        }

        private static bool? doForceDarkThemeAtStart;
        private static bool? doForceLightThemeAtStart;

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
                    case "/forcedarkmode":
                        doForceDarkThemeAtStart = true;
                        break;

                    case "/forcelightmode":
                        doForceLightThemeAtStart = true;
                        break;

                    default:
                        break;
                }
            });
        }

        #endregion
    }
}