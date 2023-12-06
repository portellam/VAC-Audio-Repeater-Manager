using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VACARM
{
    class DefaultData
    {
        private const string DefaultRepeaterPartialPath = @"\data\defaultrepeater";
        private const string SavePartialPath = @"\save";
        public static readonly string Path = $@"{Directory.GetCurrentDirectory()}{DefaultRepeaterPartialPath}";
        public static readonly string SavePath = $@"{Directory.GetCurrentDirectory()}{SavePartialPath}";
        private static string[] data;

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
                Save();
            }
        }

        public static int BitsPerSample
        {
            get
            {
                return int.Parse(data[1]);
            }
            set
            {
                data[1] = value.ToString();
                Save();
            }
        }

        public static int BufferMs
        {
            get
            {
                return int.Parse(data[3]);
            }
            set
            {
                data[3] = value.ToString();
                Save();
            }
        }

        public static int Buffers
        {
            get
            {
                return int.Parse(data[4]);
            }
            set
            {
                data[4] = value.ToString();
                Save();
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
                Save();
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
                Save();
            }
        }

        public static int SamplingRate
        {
            get
            {
                return int.Parse(data[0]);
            }
            set
            {
                data[0] = value.ToString();
                Save();
            }
        }

        public static string DefaultGraph
        {
            get
            {
                if (data[9] == "\\")
                {
                    return null;
                }

                return data[9];
            }
            set
            {
                data[9] = value;
                Save();
            }
        }

        public static string RepeaterPath
        {
            get
            {
                return data[8];
            }
            set
            {
                data[8] = value;
                Save();
            }
        }

        public static string WindowName
        {
            get
            {
                return data[7];
            }
            set
            {
                data[7] = value;
                Save();
            }
        }

        public static void CheckFile()
        {
            if (!File.Exists(Path))
            {
                File.WriteAllText(Path, "48000\r\n16\r\n3\r\n500\r\n12\r\n50\r\n20\r\n{0} to {1}\r\nC:\\Program Files\\Virtual Audio Cable\\audiorepeater.exe\r\n\\");
            }

            data = File.ReadAllLines(Path);
            Directory.CreateDirectory(SavePath);
            string[] networks = Directory.GetFiles(SavePath).Where(x => x.EndsWith(".vac")).ToArray();

            if (networks.Length == 0)
            {
                DefaultGraph = "\\";
            }

            if (networks.Length == 1)
            {
                DefaultGraph = networks[0].Replace($@"{SavePath}\", "");
            }

            if (DefaultGraph != null && !File.Exists($@"{SavePath}\{DefaultGraph}"))
            {
                DefaultGraph = "\\";
            }
        }

        public static void Refresh()
        {
            CheckFile();
            data = File.ReadAllLines(Path);
        }

        private static void Save()
        {
            File.WriteAllLines(Path, data);
        }
    }
}