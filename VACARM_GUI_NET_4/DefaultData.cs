using System;
using System.IO;
using System.Linq;

namespace VACARM_GUI_NET_4
{
    public class DefaultData
    {
        private static bool doesEngineExist;
        private static string CurrentDirectory = Directory.GetCurrentDirectory();
        private const string DataPartialPath = @"\data\";
        public const string DefaultGraphValue = "\\";
        private const string DefaultRepeaterPartialFilePath = DataPartialPath + "defaultrepeater";
        private const string SavePartialPath = @"\save";
        private static readonly string DataPath = $@"{CurrentDirectory}{DataPartialPath}";
        private static readonly string DefaultRepeaterFile = $@"{CurrentDirectory}{DefaultRepeaterPartialFilePath}"; //NOTE: is it necessary for this path to exist, for VAC to work?
        private static string[] data;

        public const string ApplicationName = "VACARM";
        public const string FileExtension = ".vac";
        public static readonly string SavePath = $@"{CurrentDirectory}{SavePartialPath}";                            //NOTE: is it necessary for this path to exist, for VAC to work?

        /// <summary>
        /// Does VAC executable exist.
        /// </summary>
        public static bool DoesEngineExist
        {
            get
            {
                return doesEngineExist;
            }
            set
            {
                if (!value)
                {
                    return;
                }

                doesEngineExist = value;
            }

        }

        /// <summary>
        /// The Channel Configuration
        /// </summary>
        public static ChannelConfig ChannelConfig
        {
            get
            {
                if (!(int.TryParse(data[2], out int val) || Enum.IsDefined(typeof(ChannelConfig), val)))
                {
                    return ChannelConfig.Stereo;
                }

                return (ChannelConfig)val;
            }
            set
            {
                data[2] = ((int)value).ToString();
                SaveFile();
            }
        }

        /// <summary>
        /// The amount of bits per sample.
        /// </summary>
        public static int BitsPerSample
        {
            get
            {
                return int.Parse(data[1]);
            }
            set
            {
                data[1] = value.ToString();
                SaveFile();
            }
        }

        /// <summary>
        /// The buffer time in milliseconds.
        /// </summary>
        public static int BufferMs
        {
            get
            {
                return int.Parse(data[3]);
            }
            set
            {
                data[3] = value.ToString();
                SaveFile();
            }
        }

        /// <summary>
        /// The amount of short-term data particles.
        /// </summary>
        public static int Buffers
        {
            get
            {
                return int.Parse(data[4]);
            }
            set
            {
                data[4] = value.ToString();
                SaveFile();
            }
        }

        public static int Prefill
        {
            get
            {
                return int.Parse(data[5]);
            }
            set
            {
                data[5] = value.ToString();
                SaveFile();
            }
        }

        public static int ResyncAt
        {
            get
            {
                return int.Parse(data[6]);
            }
            set
            {
                data[6] = value.ToString();
                SaveFile();
            }
        }

        /// <summary>
        /// The sampling rate in KiloHertz.
        /// </summary>
        public static int SamplingRate
        {
            get
            {
                return int.Parse(data[0]);
            }
            set
            {
                data[0] = value.ToString();
                SaveFile();
            }
        }

        /// <summary>
        /// The default device graph.
        /// </summary>
        public static string DefaultGraph
        {
            get
            {
                if (data[9] == DefaultGraphValue)
                {
                    return null;
                }

                return data[9];
            }
            set
            {
                data[9] = value;
                SaveFile();
            }
        }

        /// <summary>
        /// The repeater path.
        /// </summary>
        public static string RepeaterPath
        {
            get
            {
                return data[8];
            }
            set
            {
                data[8] = value;
                SaveFile();
            }
        }

        /// <summary>
        /// The name of the window.
        /// </summary>
        public static string WindowName
        {
            get
            {
                return data[7];
            }
            set
            {
                data[7] = value;
                SaveFile();
            }
        }

        /// <summary>
        /// Check if engine exists.
        /// </summary>
        protected internal static void CheckEngine()
        {
            string message;

            try
            {
                doesEngineExist = File.Exists(RepeaterPath);

                if (doesEngineExist)
                {
                    return;
                }

                message = $"Could not find Virtual Audio Cable executable (\"{RepeaterPath}\").\n\nPlease install or copy Virtual Audio Cable to expected directory.";
                //LogError(iOException, message);				//TODO: add logger.
            }
            catch (IOException iOException)
            {
                doesEngineExist = false;
                iOException.Source = nameof(RepeaterPath);
                message = $"Read failed (\"{RepeaterPath}\").\n\nIf problem persists, please restart {ApplicationName}.";
                //LogError(iOException, message);				//TODO: add logger.
            }

            System.Windows.Forms.MessageBox.Show(message, ApplicationName);
        }

        /// <summary>
        /// Check file for existing Repeater configuration.
        /// </summary>
        public static void CheckFile()
        {
            DoesDataPathExist();
            DoesFileExist();
            ReadFile();

            if (!DoesSavePathExist())		//NOTE: assuming save path is not necessary to program function.
            {
                return;
            }

            SetDefaultGraph();
        }

        /// <summary>
        /// Check if data path exists. If not, try to create path. If the path does not exist, throw exception.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        protected internal static void DoesDataPathExist()
        {
            if (Directory.Exists(DataPath))
            {
                return;
            }

            try
            {
                Directory.CreateDirectory(DataPath);
            }
            catch (IOException iOException)
            {
                iOException.Source = nameof(DataPath);
                string message = $"Folder creation failed (\"{DataPath}\"). Please try creating folder, then restart {ApplicationName}.";
                //LogError(iOException, message);				//TODO: add logger.
                System.Windows.Forms.MessageBox.Show(message);
                throw;
            }
        }

        /// <summary>
        /// If file exists, exit. If file does not exist, try to write to file the default repeater information. If write fails, throw exception.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        protected internal static void DoesFileExist()      //NOTE: assuming default repeater path is necessary to program function.
        {
            if (File.Exists(DefaultRepeaterFile))
            {
                return;
            }

            string defaultRepeaterAndPathName = "48000\r\n16\r\n3\r\n500\r\n12\r\n50\r\n20\r\n{0} to {1}\r\nC:\\Program Files\\Virtual Audio Cable\\audiorepeater.exe\r\n\\";

            try
            {
                File.WriteAllText(DefaultRepeaterFile, defaultRepeaterAndPathName);
            }
            catch (IOException iOException)
            {
                iOException.Source = nameof(defaultRepeaterAndPathName);
                string message = $"Write failed (\"{DefaultRepeaterFile}\"). Exiting.";
                //LogError(iOException, message);				//TODO: add logger.
                System.Windows.Forms.MessageBox.Show(message);
                throw;
            }
        }

        /// <summary>
        /// Create save path if not found. If failed, output a form warning the user and return false. If successful, return true.
        /// </summary>
        /// <returns>True/false</returns>
        protected internal static bool DoesSavePathExist()
        {
            if (Directory.Exists(SavePath))
            {
                return true;
            }

            try
            {
                Directory.CreateDirectory(SavePath);
            }
            catch (IOException iOException)
            {
                string message = $"Save failed (\"{SavePath}\"). Continuing without saving. Please try creating save folder, then restart {ApplicationName}.";
                //LogError(iOException, message);				//TODO: add logger.
                System.Windows.Forms.MessageBox.Show(message);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Read graph data from file.
        /// </summary>
        protected internal static void ReadFile()
        {
            string[] dataCopy = data;

            try
            {
                data = File.ReadAllLines(DefaultRepeaterFile);
            }
            catch (IOException iOException)
            {
                data = dataCopy;
                iOException.Source = nameof(DefaultRepeaterFile);
                string message = $"Read failed (\"{DefaultRepeaterFile}\"). If problem persists, please restart {ApplicationName}.";
                //LogError(iOException, message);				//TODO: add logger.
                System.Windows.Forms.MessageBox.Show(message);
            }
        }

        /// <summary>
        /// Save graph data to file.
        /// </summary>
        protected internal static void SaveFile()
        {
            try
            {
                File.WriteAllLines(DefaultRepeaterFile, data);
            }
            catch (IOException iOException)
            {
                iOException.Source = nameof(DefaultRepeaterFile);
                string message = $"Save failed \"({DefaultRepeaterFile}\"). Continuing without saving. If problem persists, please restart {ApplicationName}.";
                //LogError(iOException, message);				//TODO: add logger.
                System.Windows.Forms.MessageBox.Show(message);
            }
        }

        /// <summary>
        /// Update default graph with first found graph. If none are found or graph is null, update with default graph.
        /// </summary>
        protected internal static void SetDefaultGraph()
        {
            string[] graphArray = Directory.GetFiles(SavePath).Where(x => x.EndsWith(FileExtension)).ToArray();

            if (graphArray.Length == 1)
            {
                DefaultGraph = graphArray[0].Replace($@"{SavePath}\", "");
            }

            if ((graphArray.Length == 0 ||
                (DefaultGraph != null && !File.Exists($@"{SavePath}\{DefaultGraph}"))))
            {
                DefaultGraph = DefaultGraphValue;
            }
        }
    }
}