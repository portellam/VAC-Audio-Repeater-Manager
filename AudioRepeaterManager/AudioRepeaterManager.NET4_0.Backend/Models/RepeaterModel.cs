using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using AudioRepeaterManager.NET4_0.Backend.Structs;

namespace AudioRepeaterManager.NET4_0.Backend.Models
{
  public class RepeaterModel :
    INotifyPropertyChanged,
    IRepeaterModel
  {
    /*
     * NEW TODO:
     * -remove DeviceControl references, use Device model.
     *  -why? To make this class independent of ViewModel logic.
     *  -idea is to reduce parsing of bloated objects and params.
     * 
     * OLD TODO:
     * -add logic to determine lesser property value between Input and output,
     *      and set max value to that.
     * -parse the VAC manual and reference it for this app README and popups.
     * -review the manual to create recommended values and new drop downs
     *      (process priority).
     * -as normal create graph file and data file. Also, create bat script and
     * task for windows task manager. Also update said task or remove it.
     *      Educate user.
     *      Create icon to save and run task or pause and delete it.
     * -fix audio crackle, recommended settings?
     * -allow repeaters to run without GUI or warn to leave open or close repeaters
     *      after exit of GUI.
     */


    #region Default Parameters

    public byte defaultBitsPerSample = BitsPerSampleOptions[2];
    public byte defaultBufferAmount = BufferOptions[2];
    public byte defaultPrefillPercentage = PrefillOptions[2];
    public byte defaultResyncAtPercentage = ResyncAtOptions[3];
    //public string defaultPathName = Common.ExpectedExecutableFullPath; //FIXME
    public string defaultWindowName = "{0} to {1}";
    public uint defaultSampleRateKHz = SampleRateOptions[5];
    public ushort defaultBufferDurationMs = BufferMsOptions[2];

    #endregion

    #region Parameters

    private uint id;
    private uint inputDeviceId;
    private uint outputDeviceId;
    private byte bitsPerSample;
    private byte bufferAmount;
    private byte prefillPercentage;
    private byte resyncAtPercentage;
    private ChannelConfig channelConfig;
    private List<Channel> channelList;
    private string inputDeviceName;
    private string outputDeviceName;
    private string pathName;
    private string windowName;
    private uint sampleRateKHz;
    private ushort bufferDurationMs;

    /// <summary>
    /// Primary Key
    /// </summary>
    public uint Id
    {
      get
      {
        return id;
      }
      set
      {
        id = value;
        OnPropertyChanged(nameof(Id));
      }
    }

    /// <summary>
    /// Foreign key
    /// </summary>
    public uint InputDeviceId
    {
      get
      {
        return inputDeviceId;
      }
      set
      {
        inputDeviceId = value;
        OnPropertyChanged(nameof(InputDeviceId));
      }
    }

    /// <summary>
    /// Foreign key
    /// </summary>
    public uint OutputDeviceId
    {
      get
      {
        return outputDeviceId;
      }
      set
      {
        outputDeviceId = value;
        OnPropertyChanged(nameof(OutputDeviceId));
      }
    }

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
          ChannelMask = (uint)value;
        }

        channelConfig = value;

        OnPropertyChanged(nameof(ChannelConfig));
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    /// TODO: determine what this does?
    public IEnumerable<ChannelConfig> ChannelConfigEnum
    {
      get
      {
        return Enum
          .GetValues(typeof(ChannelConfig))
          .Cast<ChannelConfig>();
      }
    }

    /// <summary>
    /// The amount of bits per sample.
    /// </summary>
    public byte BitsPerSample
    {
      get
      {
        return bitsPerSample;
      }
      set
      {
        if
        (
          value >= 8
          && value <= 32
        )
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
    public byte BufferAmount
    {
      get
      {
        return bufferAmount;
      }
      set
      {
        if
        (
          value >= 1
          && value <= byte.MaxValue
        )
        {
          bufferAmount = value;
        }
        else
        {
          bufferAmount = 8;
        }

        OnPropertyChanged(nameof(BufferAmount));
      }
    }

    /// <summary>
    /// The amount of channels.
    /// </summary>
    public byte ChannelCount
    {
      get
      {
        if (ChannelList is null)
        {
          return 0;
        }

        return (byte)ChannelList.Count;
      }
    }

    /// <summary>
    /// The queue prefill percentage of a buffer size.
    /// Specify the amount of the queue to prefill the buffer.
    /// </summary>
    public byte PrefillPercentage
    {
      get
      {
        return prefillPercentage;
      }
      set
      {
        if
        (
          value >= PrefillOptions.FirstOrDefault()
          && value <= PrefillOptions.Last()
        )
        {
          prefillPercentage = value;
        }
        else
        {
          prefillPercentage = 50;
        }

        OnPropertyChanged(nameof(PrefillPercentage));
      }
    }

    /// <summary>
    /// The resync at percentage of a buffer size.
    /// Specify the amount of the queue to resynchronize the buffer.
    /// </summary>
    public byte ResyncAtPercentage
    {
      get
      {
        return resyncAtPercentage;
      }
      set
      {
        if
        (
          value >= 0
          && value < prefillPercentage
        )
        {
          resyncAtPercentage = value;
        }
        else
        {
          resyncAtPercentage = (byte)Math
            .Round((double)(prefillPercentage / 2));
        }

        OnPropertyChanged(nameof(ResyncAtPercentage));
      }
    }

    /// <summary>
    /// The individual Channels available to the repeater, given the Channel layout.
    /// </summary>
    public List<Channel> ChannelList
    {
      get
      {
        return channelList;
      }
      set
      {
        channelList = value;
        OnPropertyChanged(nameof(ChannelList));
      }
    }

    /// <summary>
    /// The input device name.
    /// </summary>
    public string InputDeviceName
    {
      get
      {
        return inputDeviceName;
      }
      set
      {
        if (value.Length > 31)
        {
          value = value.Substring(0, 31);
        }

        inputDeviceName = value;
        OnPropertyChanged(nameof(InputDeviceName));
      }
    }

    /// <summary>
    /// The output device name.
    /// </summary>
    public string OutputDeviceName
    {
      get
      {
        return outputDeviceName;
      }
      set
      {
        if (value.Length > 31)
        {
          value = value.Substring(0, 31);
        }

        outputDeviceName = value;
        OnPropertyChanged(nameof(OutputDeviceName));
      }
    }

    /// <summary>
    /// The file pathname.
    /// </summary>
    public string PathName
    {
      get
      {
        return pathName;
      }
      set
      {
        if (!File.Exists(value))
        {
          pathName = string.Empty;
        }
        else
        {
          pathName = value;
        }

        pathName = value;
        OnPropertyChanged(nameof(PathName));
      }
    }

    /// <summary>
    /// The window name.
    /// </summary>
    public string WindowName
    {
      get
      {
        return windowName;
      }
      set
      {
        windowName = value
          .Replace
          (
            "{0}",
            InputDeviceName
          ).Replace
          (
            "{1}",
            OutputDeviceName
          );

        OnPropertyChanged(nameof(WindowName));
      }
    }

    /// <summary>
    /// The mask of the current configuration of Channels.
    /// </summary>
    public uint ChannelMask
    {
      get
      {
        uint sum = 0;

        ChannelList.ForEach(channel
            => sum += (uint)channel);

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

        uint bit = 1;
        List<Channel> newChanneList = new List<Channel>();

        while (value != 0)
        {
          uint digit = value & bit;

          if (digit > 0)
          {
            newChanneList
              .Add((Channel)digit);
          }

          value -= digit;
          bit <<= 1;
        }

        ChannelList = newChanneList;
        OnPropertyChanged(nameof(ChannelMask));
      }
    }

    /// <summary>
    /// The sample rate in KiloHertz.
    /// </summary>
    public uint SampleRateKHz
    {
      get
      {
        return sampleRateKHz;
      }
      set
      {
        if
        (
          value >= SampleRateOptions.FirstOrDefault()
          && value <= SampleRateOptions.Last()
        )
        {
          sampleRateKHz = value;
        }
        else
        {
          sampleRateKHz = 48000;
        }

        OnPropertyChanged(nameof(SampleRateKHz));
      }
    }

    /// <summary>
    /// The buffer time in milliseconds.
    /// </summary>
    public ushort BufferDurationMs
    {
      get
      {
        return bufferDurationMs;
      }
      set
      {
        if
        (
          value >= 1
          && value <= ushort.MaxValue
        )
        {
          bufferDurationMs = value;
        }
        else
        {
          bufferDurationMs = 500;
        }

        OnPropertyChanged(nameof(BufferDurationMs));
      }
    }

    /// <summary>
    /// List of relevant properties.
    /// </summary>
    public readonly List<string> PropertyList = new List<string>()
      {
        nameof(BitsPerSample),
        nameof(BufferAmount),
        nameof(BufferDurationMs),
        nameof(ChannelConfig),
        nameof(ChannelMask),
        nameof(PrefillPercentage),
        nameof(ResyncAtPercentage),
        nameof(SampleRateKHz)
      };

    /// <summary>
    /// Available choices for BitsPerSample.
    /// </summary>
    public static ReadOnlyCollection<byte> BitsPerSampleOptions =
      new ReadOnlyCollection<byte>
      (
        new byte[]
        {
          8,
          16,
          18,
          20,
          22,
          24,
          32
        }
      );

    /// <summary>
    /// Available choices for Buffer.
    /// </summary>
    public static ReadOnlyCollection<byte> BufferOptions =
      new ReadOnlyCollection<byte>
      (
        new byte[]
        {
          2,
          4,
          8,
          12,
          16,
          20,
          24,
          32
        }
      );

    /// <summary>
    /// Available choices for Prefill percentage.
    /// </summary>
    public static ReadOnlyCollection<byte> PrefillOptions =
      new ReadOnlyCollection<byte>
      (
        new byte[]
        {
          0,
          20,
          50,
          70,
          100
        }
      );

    /// <summary>
    /// Available choices for ResyncAt percentage.
    /// </summary>
    public static ReadOnlyCollection<byte> ResyncAtOptions =
      new ReadOnlyCollection<byte>(
        new byte[]
        {
          0,
          10,
          15,
          20,
          25,
          30,
          40,
          50
        }
      );

    /// <summary>
    /// Available choices for sample rate in KiloHertz.
    /// </summary>
    public static ReadOnlyCollection<uint> SampleRateOptions =
      new ReadOnlyCollection<uint>(
        new uint[]
        {
          5000,
          8000,
          11025,
          22050,
          44100,
          48000,
          96000,
          192000
        }
      );

    /// <summary>
    /// Available choices for Buffer time in milliseconds.
    /// </summary>
    public static ReadOnlyCollection<ushort> BufferMsOptions =
      new ReadOnlyCollection<ushort>
      (
        new ushort[]
        {
          20,
          50,
          100,
          200,
          400,
          800,
          1000,
          2000,
          4000,
          8000
        }
      );

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="id">The repeater ID</param>
    /// <param name="inputDeviceId">The input device ID</param>
    /// <param name="outputDeviceId">The output device ID</param>
    [ExcludeFromCodeCoverage]
    public RepeaterModel
    (
      uint id,
      uint inputDeviceId,
      uint outputDeviceId
    )
    {

      InputDeviceId = inputDeviceId;
      OutputDeviceId = outputDeviceId;
      BitsPerSample = defaultBitsPerSample;
      BufferDurationMs = defaultBufferDurationMs;
      BufferAmount = defaultBufferAmount;
      ChannelConfig = channelConfig;
      PathName = pathName;
      PrefillPercentage = defaultPrefillPercentage;
      ResyncAtPercentage = defaultResyncAtPercentage;
      SampleRateKHz = defaultSampleRateKHz;
      WindowName = defaultWindowName;
    }

    /// <summary>
    /// Deconstructor
    /// </summary>
    /// <param name="id">The repeater ID</param>
    /// <param name="inputDeviceId">The input device ID</param>
    /// <param name="outputDeviceId">The output device ID</param>
    /// <param name="bitsPerSample">The amount of bits per sample</param>
    /// <param name="bufferAmount">The buffer amount</param>
    /// <param name="bufferDurationMs">The buffer duration in milliseconds</param>
    /// <param name="channelList">The channel list</param>
    /// <param name="channelMask">The channel mask</param>
    /// <param name="pathName">The path name</param>
    /// <param name="prefillPercentage">The prefill percentage</param>
    /// <param name="propertyList">The property list</param>
    /// <param name="resyncAtPercentage">The resync at percentage</param>
    /// <param name="sampleRateKHz">The sample rate in KiloHertz</param>
    /// <param name="windowName">The window name</param>
    [ExcludeFromCodeCoverage]
    public void Deconstruct
    (
      out uint id,
      out uint inputDeviceId,
      out uint outputDeviceId,
      out byte bitsPerSample,
      out byte bufferAmount,
      out byte prefillPercentage,
      out byte resyncAtPercentage,
      out ChannelConfig channelConfig,
      out List<string> propertyList,
      out string inputDeviceName,
      out string outputDeviceName,
      out string pathName,
      out string windowName,
      out uint channelMask,
      out uint sampleRateKHz,
      out ushort bufferDurationMs
    )
    {
      id = Id;
      inputDeviceId = InputDeviceId;
      outputDeviceId = OutputDeviceId;
      bitsPerSample = BitsPerSample;
      bufferDurationMs = BufferDurationMs;
      bufferAmount = BufferAmount;
      channelConfig = ChannelConfig;
      channelMask = ChannelMask;
      inputDeviceName = InputDeviceName;
      outputDeviceName = OutputDeviceName;
      pathName = PathName;
      prefillPercentage = PrefillPercentage;
      propertyList = PropertyList;
      resyncAtPercentage = ResyncAtPercentage;
      sampleRateKHz = SampleRateKHz;
      windowName = WindowName;
    }

    //~RepeaterModel()
    //{
    //  //destruct
    //}

    /// <summary>
    /// Logs event when property has changed.
    /// </summary>
    /// <param name="propertyName">The property name</param>
    private void OnPropertyChanged(string propertyName)
    {
      PropertyChanged?.Invoke
      (
        this,
        new PropertyChangedEventArgs(propertyName)
      );

      Debug.WriteLine
      (
        string.Format
        (
          "PropertyChanged: '{1}'" +
          propertyName
        )
      );
    }

    /// <summary>
    /// Compiles a terminal command, to create and start an audio repeater.
    /// Also, use in batch script.
    /// </summary>
    /// <returns>The terminal command</returns>
    public string ToCommand()
    {
      return
        $"start " +
        $"/min \"audiorepeater\" \"{PathName}\" " +
        $"/Input:\"{InputDeviceName}\" " +
        $"/Output:\"{OutputDeviceName}\" " +
        $"/SampleRate:{SampleRateKHz} " +
        $"/BitsPerSample:{BitsPerSample} " +
        $"/Channels:{ChannelList.Count} " +
        $"/ChanCfg:custom={ChannelMask} " +
        $"/BufferMs:{BufferDurationMs} " +
        $"/Prefill:{PrefillPercentage} " +
        $"/ResyncAt:{ResyncAtPercentage} " +
        $"/WindowName:\"{WindowName}\" " +
        $"/AutoStart";
    }

    /// <summary>
    /// Compiles output to save to text file.
    /// </summary>
    /// <returns>The output</returns>
    public override string ToString()
    {
      return
        $"{SampleRateKHz}\n" +
        $"{BitsPerSample}\n" +
        $"{ChannelMask}\n" +
        $"{(int)ChannelConfig}\n" +
        $"{BufferDurationMs}\n" +
        $"{BufferAmount}\n" +
        $"{PrefillPercentage}\n" +
        $"{ResyncAtPercentage}";
    }

    /// <summary>
    /// Set properties.
    /// </summary>
    /// <param name="infoList">The info list</param>
    public void Set(List<string> infoList)
    {
      if
      (
        infoList is null
        || !byte.TryParse(infoList[5], out byte bitsPerSample)
        || !ushort.TryParse(infoList[4], out ushort bufferDurationMs)
        || !int.TryParse(infoList[3], out int channelConfig)
        || !uint.TryParse(infoList[2], out uint channelMask)
        || !byte.TryParse(infoList[6], out byte prefillPercentage)
        || !byte.TryParse(infoList[7], out byte resyncAtPercentage)
        || !uint.TryParse(infoList[0], out uint sampleRateKHz)
      )
      {
        return;
      }

      BitsPerSample = bitsPerSample;
      BufferAmount = bitsPerSample;
      BufferDurationMs = bufferDurationMs;
      ChannelConfig = (ChannelConfig)channelConfig;
      ChannelMask = channelMask;
      PrefillPercentage = prefillPercentage;
      ResyncAtPercentage = resyncAtPercentage;
      SampleRateKHz = sampleRateKHz;
    }

    #endregion
  }
}