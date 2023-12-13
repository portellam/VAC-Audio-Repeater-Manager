using System;
using System.IO;
using System.Linq;

namespace VACARM
{
	class DefaultData
	{
		/// <summary>
		/// File and pathnames
		/// </summary>
		private const string DefaultRepeaterPartialPath = @"\data\defaultrepeater";
		public const string FileExtension = ".vac";
		public const string SavePartialPath = @"\save";
		private static string[] data;
		public static readonly string Path = $@"{Directory.GetCurrentDirectory()}{DefaultRepeaterPartialPath}";
		public static readonly string SavePath = $@"{Directory.GetCurrentDirectory()}{SavePartialPath}";

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
				Save();
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
				Save();
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
				Save();
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
				Save();
			}
		}

		/// <summary>
		/// The default device graph.
		/// </summary>
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
				Save();
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
				Save();
			}
		}

		/// <summary>
		/// Check file for existing Repeater configuration.
		/// </summary>
		public static void CheckFile()
		{
			if (!File.Exists(Path))
			{
				string defaultRepeaterAndPathName = "48000\r\n16\r\n3\r\n500\r\n12\r\n50\r\n20\r\n{0} to {1}\r\nC:\\Program Files\\Virtual Audio Cable\\audiorepeater.exe\r\n\\";
				File.WriteAllText(Path, defaultRepeaterAndPathName);
			}

			data = File.ReadAllLines(Path);
			Directory.CreateDirectory(SavePath);
			string[] networks = Directory.GetFiles(SavePath).Where(x => x.EndsWith(".vac")).ToArray();

			if (networks.Length == 1)
			{
				DefaultGraph = networks[0].Replace($@"{SavePath}\", "");
			}

			if ((networks.Length == 0 ||
				(DefaultGraph != null && !File.Exists($@"{SavePath}\{DefaultGraph}"))))
			{
				DefaultGraph = "\\";
			}
		}

		/// <summary>
		/// Refresh graph data from file.
		/// </summary>
		public static void Refresh()
		{
			CheckFile();
			data = File.ReadAllLines(Path);
		}

		/// <summary>
		/// Save graph data to file.
		/// </summary>
		private static void Save()
		{
			File.WriteAllLines(Path, data);
		}
	}
}