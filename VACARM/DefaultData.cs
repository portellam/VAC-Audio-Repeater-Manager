using System;
using System.IO;
using System.Linq;

namespace VACARM
{
	class DefaultData
	{
		private const string DefaultRepeaterPartialFilePath = @"\data\defaultrepeater";	//NOTE: is it necessary for this path to exist, for VAC to work?
		public const string FileExtension = ".vac";
		public const string SavePartialPath = @"\save";     //NOTE: is it necessary for this path to exist, for VAC to work?
		private static string[] data;
		public static readonly string DefaultRepeaterFile = $@"{Directory.GetCurrentDirectory()}{DefaultRepeaterPartialFilePath}";	//NOTE: must this be public?
		public static readonly string SavePath = $@"{Directory.GetCurrentDirectory()}{SavePartialPath}"; //NOTE: must this be public?

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
			if (!File.Exists(DefaultRepeaterFile))
			{
				string defaultRepeaterAndPathName = "48000\r\n16\r\n3\r\n500\r\n12\r\n50\r\n20\r\n{0} to {1}\r\nC:\\Program Files\\Virtual Audio Cable\\audiorepeater.exe\r\n\\";

				//NOTE: assuming default repeater path is necessary to program function.
				try
				{
					File.WriteAllText(DefaultRepeaterFile, defaultRepeaterAndPathName);
				}
				catch (IOException iOException)
				{
					//TODO: add logger, then throw.
					iOException.Source = nameof(defaultRepeaterAndPathName);
					//LogError(iOException, $"Write failed for default repeater.");
					throw;
				}
			}

			try
			{
				data = File.ReadAllLines(DefaultRepeaterFile);
			}
			catch (IOException iOException)
			{
				//TODO: add logger, then throw.
				iOException.Source = nameof(data);
				//LogError(iOException, $"Read failed for default repeater file.");
				throw;
			}

			//NOTE: assuming save path is not necessary to program function.
			if (!DoesSavePathExist())
			{
				return;
			}

			string[] networks = Directory.GetFiles(SavePath).Where(x => x.EndsWith(FileExtension)).ToArray();

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
		/// Check if default repeater path exists. If not, try to create path. If the path does not exist, write to logger then fail with exception.
		/// </summary>
		/// <exception cref="ArgumentException"></exception>
		public static void CheckDefaultSavePath()
		{
			if (Directory.Exists(DefaultRepeaterFile))
			{
				return;
			}

			try
			{
				Directory.CreateDirectory(DefaultRepeaterFile);
			}
			catch (IOException iOException)
			{
				//TODO: add logger, then throw.
				//LogError(iOException, $"Failed to create folder at path '{DefaultRepeaterFile}'.");
				throw;
			}
		}

		/// <summary>
		/// Create save path if not found. If failed, output a form warning the user and return false. If successful, return true.
		/// </summary>
		/// <returns>True/false</returns>
		public static bool DoesSavePathExist()
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
				string message = $"Save failed ({SavePath}). Continuing without saving.";
				//TODO: add logger.
				//LogError(iOException, message);
				System.Windows.Forms.MessageBox.Show($"Save failed ({SavePath}). Continuing without saving.");
				return false;
			}

			return true;
		}

		/// <summary>
		/// Refresh graph data from file.
		/// </summary>
		public static void Refresh()
		{
			CheckFile();
			data = File.ReadAllLines(DefaultRepeaterFile);
		}

		/// <summary>
		/// Save graph data to file.
		/// </summary>
		private static void Save()
		{
			File.WriteAllLines(DefaultRepeaterFile, data);
		}
	}
}