using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace VACARM
{
	public class RepeaterInfo : INotifyPropertyChanged
	{
		private BipartiteDeviceGraph graph;
		private ChannelConfig channelConfig;
		private int bitsPerSample;
		private int bufferMs;
		private int buffers;
		private int prefill;
		private int resyncAt;
		private int samplingRate;
		private string path;
		private string windowName;

		public ChannelConfig ChannelConfig
		{
			get
			{
				return channelConfig;
			}
			set
			{
				if (value != ChannelConfig.Custom)
				{
					ChannelMask = (int)value;
				}

				channelConfig = value;
				OnPropertyChanged(nameof(ChannelConfig));
			}
		}

		public IEnumerable<ChannelConfig> ChannelConfigs
		{
			get
			{
				return Enum.GetValues(typeof(ChannelConfig)).Cast<ChannelConfig>();
			}
		}

		public DeviceControl Capture { get; }
		public DeviceControl Render { get; }
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// The amount of bits per sample.
		/// </summary>
		public int BitsPerSample
		{
			get
			{
				return bitsPerSample;
			}
			set
			{
				if (value >= 8 && value <= 32)
				{
					bitsPerSample = value;
				}
				else
				{
					bitsPerSample = 16;
				}

				OnPropertyChanged(nameof(BitsPerSample));
			}
		}
	
		/// <summary>
		/// The amount of short-term data particles.
		/// </summary>
		public int Buffers
		{
			get
			{
				return buffers;
			}
			set
			{
				if (value >= 1 && value <= 256)
				{
					buffers = value;
				}
				else
				{
					buffers = 8;
				}

				OnPropertyChanged(nameof(Buffers));
			}
		}

		/// <summary>
		/// The buffer time in milliseconds.
		/// </summary>
		public int BufferMs
		{
			get
			{
				return bufferMs;
			}
			set
			{
				if (value >= 1 && value <= 300000)
				{
					bufferMs = value;
				}
				else
				{
					bufferMs = 500;
				}

				OnPropertyChanged(nameof(BufferMs));
			}
		}

		/// <summary>
		/// The mask of the current configuration of channels.
		/// </summary>
		public int ChannelMask
		{
			get
			{
				int sum = 0;

				foreach (Channel c in Channels)
				{
					sum += (int)c;
				}

				return sum;
			}
			set
			{
				if (value < 0)
				{
					value = 0;
				}

				value &= 0x7FF;

				if (channelConfig != ChannelConfig.Custom)
				{
					channelConfig = ChannelConfig.Custom;
					OnPropertyChanged(nameof(ChannelConfig));
				}

				int bit = 1;
				List<Channel> newChannels = new List<Channel>();

				while (value != 0)
				{
					int digit = value & bit;

					if (digit > 0)
					{
						newChannels.Add((Channel)digit);
					}

					value -= digit;
					bit <<= 1;
				}

				Channels = newChannels;
				OnPropertyChanged(nameof(ChannelMask));
			}
		}

		public int Prefill	//TODO: understand and explain this parameter.
		{
			get
			{
				return prefill;
			}
			set
			{
				if (value >= 0 && value <= 100)
				{
					prefill = value;
				}
				else
				{
					prefill = 50;
				}

				OnPropertyChanged(nameof(Prefill));
			}
		}

		public int ResyncAt //TODO: understand and explain this parameter.
		{
			get
			{
				return resyncAt;
			}
			set
			{
				if (value >= 0 && value < prefill)
				{
					resyncAt = value;
				}
				else
				{
					resyncAt = prefill / 2;
				}

				OnPropertyChanged(nameof(ResyncAt));
			}
		}

		/// <summary>
		/// The sampling rate in KiloHertz.
		/// </summary>
		public int SamplingRate
		{
			get
			{
				return samplingRate;
			}
			set
			{
				if (value >= 1000 && value <= 384000)
				{
					samplingRate = value;
				}
				else
				{
					samplingRate = 48000;
				}

				OnPropertyChanged(nameof(SamplingRate));
			}
		}

		public Line Link { get; }
		public List<Channel> Channels;

		public readonly List<string> repeaterInfoPropertyList = new List<string>()
		{
			nameof(BitsPerSample),
			nameof(Buffers),
			nameof(BufferMs),
			nameof(ChannelConfig),
			nameof(ChannelMask),
			nameof(Prefill),
			nameof(ResyncAt),
			nameof(SamplingRate)
		};
		
		public MMDevice InputDevice
		{
			get
			{
				return Capture.mMDevice;
			}
		}

		public MMDevice OutputDevice
		{
			get
			{
				return Render.mMDevice;
			}
		}

		MenuItem captureContext;
		MenuItem renderContext;

		/// <summary>
		/// Available choices for BitsPerSample.
		/// </summary>
		public static ReadOnlyCollection<int> BitsPerSampleOptions = new ReadOnlyCollection<int>(new int[] { 8, 16, 18, 20, 22, 24, 32 });

		/// <summary>
		/// Available choices for Buffer time in milliseconds.
		/// </summary>
		public static ReadOnlyCollection<int> BufferMsOptions = new ReadOnlyCollection<int>(new int[] { 20, 50, 100, 200, 400, 800, 1000, 2000, 4000, 8000 });

		/// <summary>
		/// Available choices for Prefill.
		/// </summary>
		public static ReadOnlyCollection<int> PrefillOptions = new ReadOnlyCollection<int>(new int[] { 0, 20, 50, 70, 100 });

		/// <summary>
		/// Available choices for ResyncAt.
		/// </summary>
		public static ReadOnlyCollection<int> ResyncAtOptions = new ReadOnlyCollection<int>(new int[] { 0, 10, 15, 20, 25, 30, 40, 50 });

		/// <summary>
		/// Available choices for Sampling rate in KiloHertz.
		/// </summary>
		public static ReadOnlyCollection<int> SamplingRateOptions = new ReadOnlyCollection<int>(new int[] { 5000, 8000, 11025, 22050, 44100, 48000, 96000, 192000 });

		/// <summary>
		/// The input devices's display name.
		/// </summary>
		public string Input
		{
			get
			{
				if (InputDevice.FriendlyName.Length > 31)
				{
					return InputDevice.FriendlyName.Substring(0, 31);
				}

				return InputDevice.FriendlyName;
			}
		}

		/// <summary>
		/// The output device's display name.
		/// </summary>
		public string Output
		{
			get
			{
				if (OutputDevice.FriendlyName.Length > 31)
				{
					return OutputDevice.FriendlyName.Substring(0, 31);
				}

				return OutputDevice.FriendlyName;
			}
		}

		/// <summary>
		/// The file pathname.
		/// </summary>
		public string Path
		{
			get
			{
				return path;
			}
			set
			{
				if (File.Exists(value))
				{
					path = value;
				}
			}
		}

		/// <summary>
		/// The window name
		/// </summary>
		public string WindowName
		{
			get
			{
				return windowName;
			}
			set
			{
				windowName = value.Replace("{0}", Input).Replace("{1}", Output);
			}
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="captureDeviceControl">The capture device</param>
		/// <param name="renderDeviceControl">The render device</param>
		/// <param name="bipartiteDeviceGraph">The graph</param>
		public RepeaterInfo(DeviceControl captureDeviceControl, DeviceControl renderDeviceControl, BipartiteDeviceGraph bipartiteDeviceGraph)
		{
			captureContext = new MenuItem();
			captureContext.Header = renderDeviceControl.DeviceName;
			captureContext.Click += ContextClick;
			captureDeviceControl.ContextMenu.Items.Add(captureContext);

			renderContext = new MenuItem();
			renderContext.Header = captureDeviceControl.DeviceName;
			renderContext.Click += ContextClick;
			renderDeviceControl.ContextMenu.Items.Add(renderContext);

			Capture = captureDeviceControl;
			Render = renderDeviceControl;
			Link = new Line
			{
				Stroke = Brushes.White,
				StrokeThickness = 2,
			};

			Binding bx1 = new Binding("X")
			{
				UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
				Source = captureDeviceControl
			};
			Link.SetBinding(Line.X1Property, bx1);

			Binding by1 = new Binding("Y")
			{
				UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
				Source = captureDeviceControl
			};
			Link.SetBinding(Line.Y1Property, by1);

			Binding bx2 = new Binding("X")
			{
				UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
				Source = renderDeviceControl
			};
			Link.SetBinding(Line.X2Property, bx2);

			Binding by2 = new Binding("Y")
			{
				UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
				Source = renderDeviceControl
			};
			Link.SetBinding(Line.Y2Property, by2);

			SamplingRate = DefaultData.SamplingRate;
			BitsPerSample = DefaultData.BitsPerSample;
			ChannelConfig = DefaultData.ChannelConfig;
			BufferMs = DefaultData.BufferMs;
			Buffers = DefaultData.Buffers;
			Prefill = DefaultData.Prefill;
			ResyncAt = DefaultData.ResyncAt;
			WindowName = DefaultData.WindowName;
			Path = DefaultData.RepeaterPath;

			this.graph = bipartiteDeviceGraph;
		}

		/// <summary>
		/// Show dialog for click on repeater.
		/// </summary>
		/// <param name="sender">The sender</param>
		/// <param name="routedEventArgs">The event</param>
		private void ContextClick(object sender, System.Windows.RoutedEventArgs routedEventArgs)
		{
			RepeaterMenu repeaterMenu = new RepeaterMenu(this, graph);
			repeaterMenu.Owner = MainWindow.GraphMap.Parent as Window;
			repeaterMenu.ShowDialog();
		}

		/// <summary>
		/// Logs event when RepeaterInfo property has changed.
		/// </summary>
		/// <param name="propertyName">The property name</param>
		public void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		/// <summary>
		/// Sets the repeater info data.
		/// </summary>
		/// <param name="info"></param>
		public void SetData(List<string> info)
		{
			SamplingRate = int.Parse(info[0]);
			BitsPerSample = int.Parse(info[1]);
			ChannelMask = int.Parse(info[2]);
			ChannelConfig = (ChannelConfig)int.Parse(info[3]);
			BufferMs = int.Parse(info[4]);
			Buffers = int.Parse(info[5]);
			Prefill = int.Parse(info[6]);
			ResyncAt = int.Parse(info[7]);
		}

		/// <summary>
		/// Compiles a terminal command to create and start a repeater given the repeater info.
		/// </summary>
		/// <returns>The terminal command</returns>
		public string ToCommand()
		{
			return $"start " +
				$"/min \"audiorepeater\" \"{Path}\" " +
				$"/Input:\"{Input}\" " +
				$"/Output:\"{Output}\" " +
				$"/SamplingRate:{SamplingRate} " +
				$"/BitsPerSample:{BitsPerSample} " +
				$"/Channels:{Channels.Count} " +
				$"/ChanCfg:custom={ChannelMask} " +
				$"/BufferMs:{BufferMs} " +
				$"/Prefill:{Prefill} " +
				$"/ResyncAt:{ResyncAt} " +
				$"/WindowName:\"{WindowName}\" " +
				$"/AutoStart";
		}

		/// <summary>
		/// Returns the repeater info as a string.
		/// </summary>
		/// <returns>The repeater info</returns>
		public string ToSaveData()
		{
			return
				$"{SamplingRate}\n" +
				$"{BitsPerSample}\n" +
				$"{ChannelMask}\n" +
				$"{(int)ChannelConfig}\n" +
				$"{BufferMs}\n" +
				$"{Buffers}\n" +
				$"{Prefill}\n" +
				$"{ResyncAt}";
		}
	}

	/// <summary>
	/// The masks of individual speakers/channels.
	/// </summary>
	public enum Channel
	{
		FL = 0x1,
		FR = 0x2,
		FC = 0x4,
		LF = 0x8,
		BL = 0x10,
		BR = 0x20,
		FLC = 0x40,
		FRC = 0x80,
		BC = 0x100,
		SL = 0x200,
		SR = 0x400
	}

	/// <summary>
	/// The masks of speaker layout/channel amounts.
	/// </summary>
	public enum ChannelConfig
	{
		Custom = -1,
		Mono = 0x4,
		Stereo = 0x3,
		Quadraphonic = 0x33,
		Surround = 0x107,
		Back51 = 0x3F,
		Surround51 = 0x60F,
		Wide71 = 0xFF,
		Surround71 = 0x63F
	}
}