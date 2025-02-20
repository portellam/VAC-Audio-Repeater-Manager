using System;
using System.Linq;

namespace AudioRepeaterManager.NET4_0.Backend
{
    public class Program
    {
        #region Arguments

        private static bool doRunInConsole = false;
        public static bool DoForceColorTheme { get; private set; } = false;
        public static bool IsLightThemeEnabled { get; private set; } = false;

        /// <summary>
        /// The command line arguments.
        /// </summary>
        public static string[] Arguments { get; private set; }

        #endregion

        #region Logic

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="arguments">The command line arguments</param>
        [STAThread]
        internal static void Main(string[] arguments)
        {
            Arguments = arguments;
            ParseArguments();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (doRunInConsole)
            {
                //TODO: create console mode.
            }
            else
            {
                new GraphicsWindow();
            }
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

                switch (argument.ToLower())
                {
                    //case "/console":
                    //    doRunInConsole = true;
                    //    break;

                    case "/forcedarkmode":
                        DoForceColorTheme = true;
                        IsLightThemeEnabled = false;
                        break;

                    case "/forcelightmode":
                        DoForceColorTheme = true;
                        IsLightThemeEnabled = true;
                        break;

                    default:
                        break;
                }
            });
        }

        #endregion
    }
}