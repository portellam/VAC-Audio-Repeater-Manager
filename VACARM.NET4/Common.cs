using System;
using System.Diagnostics;
using System.IO;
using VACARM.NET4.Extensions;

namespace VACARM.NET4
{
    public class Common
    {
        #region Application name logic

        public readonly static string ApplicationNameAsAbbreviation = "VACARM";

        public readonly static string FileExtension = "." +
            ApplicationNameAsAbbreviation.ToLower();

        public readonly static string ReferencedApplicationName = "Virtual Audio Cable";
        public readonly static string ReferencedFileExtension = ".vac";

        #endregion

        #region Application support logic

        /*
        * NOTES:
        * 
        * VAC Control Panel v4.70 documentation:
        * 
        * [1] https://vac.muzychenko.net/en/manual/features.htm
        * Up to 256 virtual cable devices (some systems limit number of MME devices).
        * 
        * [2] https://vac.muzychenko.net/en/manual/ctlpan.htm#Limitations
        * Windows XP/2003 limit number of MME devices to 32. So MME applications that
        * use waveIn/waveOut functions won't see more than 32 cable endpoints in
        * Windows XP/2003. To use more cables you need either to use DirectSound or
        * WDM/KS interfaces or switch to VAC 3 because it is a legacy MME driver.
        * 
        * Windows 6.x+ systems don't limit number of MME/DirectSound endpoints.
        * 
        * Moreover, creating too many (100 and more) virtual cables may even cause
        * other audio endpoints to disappear. Use this feature with care. 
        * 
        * Assumptions:
        * 
        * One (1) Audio Repeater has one Input and one Output device.
        * 
        * Given [1] a mamximum value of audio devices exist on Windows XP/2003, we will
        * limit the total Audio Repeaters to 32.
        * (32 devices / 2 types) * (2 types / 1 repeater) == 32
        * 
        * Given that a maximum of 256 Virtual Cables may exist, and that a Virtual
        * cable has one input and output, same as an Audio Repeater, we will limit
        * the total number of Audio Repeaters to 256.
        * 
        * Given that 100 or more Virtual Cables can cause slowdown, and that no
        * provided citations regard a limitation of actual audio cables, we will
        * warn the user if 100 or more Audio Repeaters exist.
        * 
        */

        public const byte AudioRepeaterMaxCountForWindowsNT5 = 32;
        public const byte AudioRepeaterMaxCountForWindowsNT6AndAbove = byte.MaxValue;
        public const ushort genericUIElementMaxCount = ushort.MaxValue;

        public static bool IsWindowsNT5
        {
            get
            {
                return IsWindowsNT && Environment.OSVersion.Version.Major == 5;
            }
        }

        public static bool IsWindowsNT6OrAbove
        {
            get
            {
                return IsWindowsNT && Environment.OSVersion.Version.Major >= 6;
            }
        }

        public static bool IsWindowsNT
        {
            get
            {
                return Environment.OSVersion.Platform == PlatformID.Win32NT;
            }
        }

        #endregion Application support logic

        #region Executable path logic

        private readonly static string executableName = "audiorepeater.exe";

        private readonly static string firstParentAndBasePathName =
            $"{ReferencedApplicationName}\\{executableName}";                           //NOTE: "Virtual Audio Cable\audiorepeater.exe"

        private readonly static string programFilesPathName = "Program Files\\";

        private readonly static string x86ProgramFilesOnX64SystemPathName =
            "Program Files (x86)\\";

        private static string possibleExecutableFullPath1 = $"{systemRootPathName}" +   //NOTE: "C:\Program Files\Virtual Audio Cable\audiorepeater.exe"
            $"{programFilesPathName}{firstParentAndBasePathName}";

        private static string possibleExecutableFullPath2 = $"{systemRootPathName}" +   //NOTE: "C:\Program Files (x86)\Virtual Audio Cable\audiorepeater.exe"
            $"{x86ProgramFilesOnX64SystemPathName}{firstParentAndBasePathName}";

        private static string systemRootPathName = Path.GetPathRoot                     //NOTE: expect "C:\"
            (Environment.GetFolderPath(Environment.SpecialFolder.System));

        public static string ExpectedExecutableFullPath
        {
            get
            {
                if (Environment.Is64BitProcess == Environment.Is64BitOperatingSystem)   //NOTE: ARM, x86, and x64 Windows, the default is "Program Files".
                {
                    return possibleExecutableFullPath1;
                }

                return possibleExecutableFullPath2;                                     //NOTE: x86 executable on x64 Windows.
            }
        }

        public static bool DoesExecutableExist
        {
            get
            {
                if (systemRootPathName is null)
                {
                    return false;
                }

                return File.Exists(ExpectedExecutableFullPath);
            }
        }

        #endregion Executable path logic

        #region File logic

        //TODO: add settings.ini ?
        private static byte applicationProblemEventCounter = 0;

        private static string currentDirectory
        {
            get
            {
                return Directory.GetCurrentDirectory();                                 //NOTE: expected "C:\Program Files\VACARM"
            }
        }

        private static readonly string SavePartialPath = @"\graphs";

        public readonly string DefaultGraphEmptyValue = "\\";

        public static readonly string CatchAllErrorMessage =
            $"If problem persists, please restart\"{ApplicationNameAsAbbreviation}\".";

        public static readonly string SavePath =
            $"{currentDirectory}{SavePartialPath}\\";                                   //NOTE: "C:\Program Files\VACARM\save"

        #endregion File logic

        #region Problem event counter logic

        private const byte maxProblemEventCount = 3;

        /// <summary>
        /// Check if application problem event counter has exceeded set value.
        /// If not, return immediately.
        /// If yes, create a messageText box to warn the user, and offer to either reset
        /// counter, or save file and exit application.
        /// </summary>
        internal static void WarnAndAskUserToExitIfProblemCounterExceededMaxValue()
        {
            if (applicationProblemEventCounter < maxProblemEventCount)
            {
                return;
            }

            string messageText = $"Unexpected behavior detected." +
                $"Do you wish to exit {ApplicationNameAsAbbreviation}?" +
                "\n\nTo prevent data loss," +
                " you will be prompted to save your latest changes.";

            bool doExitApplication = MessageBoxWrapper.ShowYesNoAndReturnTrueFalse
                (messageText, ApplicationNameAsAbbreviation);

            if (!doExitApplication)
            {
                ResetProblemEventCounter();
                return;
            }

            //TODO: add logic here to save changes to file.

            Process.GetCurrentProcess().Kill();
        }

        /// <summary>
        /// Increment problem counter.
        /// </summary>
        internal static void IncrementProblemEventCounter()
        {
            //TODO: add logger here.

            if (applicationProblemEventCounter <= byte.MaxValue)
            {
                return;
            }

            applicationProblemEventCounter++;
        }

        /// <summary>
        /// Reset problem counter to zero.
        /// </summary>
        internal static void ResetProblemEventCounter()
        {
            applicationProblemEventCounter = 0;
            //TODO: add logger here.
        }

        /// <summary>
        /// Increments problem event counter, logs counter and stack trace,
        /// and checks if counter has exceeded max value. If yes, warn user.
        /// </summary>
        /// <param name="stackTrace"></param>
        public static void HasHadProblem(StackTrace stackTrace)
        {
            IncrementProblemEventCounter();
            //TODO: add logger here for increment counter, and stack trace.
            WarnAndAskUserToExitIfProblemCounterExceededMaxValue();
        }

        #endregion Problem event counter logic
    }
}