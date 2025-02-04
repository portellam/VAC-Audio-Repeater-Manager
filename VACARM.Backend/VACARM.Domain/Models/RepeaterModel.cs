using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using VACARM.Common;
using VACARM.Domain.Structs;

namespace VACARM.Domain.Models
{
  public class RepeaterModel :
    BaseModel,
    IRepeaterModel,
    INotifyPropertyChanged
  {
    #region Default Parameters

    /// <summary>
    /// Enumerable of the Channel Config.
    /// </summary>
    public IEnumerable<ChannelConfig> ChannelConfigEnum
    {
      get
      {
        return Enum
          .GetValues(typeof(ChannelConfig))
          .Cast<ChannelConfig>();
      }
    }

    public static byte defaultBitsPerSample
    {
      get
      {
        return BitsPerSampleOptions[2];
      }
    }

    public static byte defaultBufferAmount
    {
      get
      {
        return BufferAmountOptions[2];
      }
    }

    public static byte defaultPrefillPercentage
    {
      get
      {
        return PrefillPercentageOptions[2];
      }
    }

    public static byte defaultResyncAtPercentage
    {
      get
      {
        return ResyncAtPercentageOptions[2];
      }
    }

    public static uint defaultSampleRateKHz
    {
      get
      {
        return SampleRateKHzOptions[2];
      }
    }

    public static ushort defaultBufferDurationMs
    {
      get
      {
        return BufferDurationMsOptions[2];
      }
    }

    public static ChannelConfig defaultChannelConfig
    {
      get
      {
        return ChannelConfig.Stereo;
      }
    }

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
    public static ReadOnlyCollection<byte> BufferAmountOptions =
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
    public static ReadOnlyCollection<byte> PrefillPercentageOptions =
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
    public static ReadOnlyCollection<byte> ResyncAtPercentageOptions =
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
    public static ReadOnlyCollection<uint> SampleRateKHzOptions =
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
    public static ReadOnlyCollection<ushort> BufferDurationMsOptions =
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

    #region Parameters

    private uint inputDeviceId { get; set; }
    private uint outputDeviceId { get; set; }
    private int? processId { get; set; } = null;
    private byte bitsPerSample { get; set; } = defaultBitsPerSample;
    private byte bufferAmount { get; set; } = defaultBufferAmount;
    private byte prefillPercentage { get; set; } = defaultPrefillPercentage;
    private byte resyncAtPercentage { get; set; } = defaultResyncAtPercentage;
    private ChannelConfig channelConfig { get; set; } = defaultChannelConfig;
    private List<Channel> channelList { get; set; } = new List<Channel>();
    private string inputDeviceName { get; set; } = string.Empty;
    private string outputDeviceName { get; set; } = string.Empty;
    private string pathName { get; set; } = string.Empty;
    private uint sampleRateKHz { get; set; } = defaultSampleRateKHz;
    private ushort bufferDurationMs { get; set; } = defaultBufferDurationMs;

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

    public int? ProcessId
    {
      get
      {
        return processId;
      }
      set
      {
        processId = value;
        OnPropertyChanged(nameof(ProcessId));
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

    public override event PropertyChangedEventHandler? PropertyChanged;

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
        if (ChannelList == null)
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
          value >= PrefillPercentageOptions.FirstOrDefault()
          && value <= PrefillPercentageOptions.Last()
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
        if (value == null)
        {
          value = new List<Channel>();
        }

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
          value = value.Substring
            (
              0,
              31
            );
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
          value = value.Substring
            (
              0,
              31
            );
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
    /// Batch command to create and start an audio repeater.
    /// </summary>
    public string StartArguments
    {
      get
      {
        return $"start " +
          $"/min \"{Info.ExpectedExecutableFullPathName}\" \"{PathName}\" " +
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
    }

    /// <summary>
    /// Batch command to stop an audio repeater.
    /// </summary>
    public string StopArguments
    {
      get
      {
        return
          $"start \"{Info.ExpectedExecutableFullPathName}\" " +
          $"\"{PathName}\" " +
          $"/CloseInstance:\"{WindowName}\"";
      }
    }

    /// <summary>
    /// The window name.
    /// </summary>
    public string WindowName
    {
      get
      {
        int startIndex = 0;
        int maxLength = 30;

        return string.Format
          (
            "Id:{0}, WaveInId:{1}, WaveOutId:{2}, '{3}' to '{4}'",
            Id.ToString(),
            inputDeviceId.ToString(),
            outputDeviceId.ToString(),

            inputDeviceName.Substring
              (
                startIndex,
                maxLength
              ),

            outputDeviceName.Substring
              (
                startIndex,
                maxLength
              )
          );
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

        if (ChannelList is not null)
        {
          ChannelList.ForEach
            (
              channel
              => sum += (uint)channel
            );
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

        uint bit = 1;
        List<Channel> newChannelList = new List<Channel>();

        while (value != 0)
        {
          uint digit = value & bit;

          if (digit > 0)
          {
            newChannelList.Add
              ((Channel)digit);
          }

          value -= digit;
          bit <<= 1;
        }

        ChannelList = newChannelList;
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
          value >= SampleRateKHzOptions.FirstOrDefault()
          && value <= SampleRateKHzOptions.Last()
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

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="id">The repeater ID</param>
    /// <param name="inputDeviceId">The input device ID</param>
    /// <param name="outputDeviceId">The output device ID</param>
    /// <param name="processId">The process ID</param>
    /// <param name="inputDeviceName">The input device name</param>
    /// <param name="outputDeviceName">The output device name</param>
    /// <param name="pathName">The path name</param>
    [ExcludeFromCodeCoverage]
    public RepeaterModel
    (
      uint id,
      uint inputDeviceId,
      uint outputDeviceId,
      int? processId,
      string inputDeviceName,
      string outputDeviceName,
      string pathName
    ) : base(id)
    {
      InputDeviceId = inputDeviceId;
      InputDeviceName = inputDeviceName;
      OutputDeviceId = outputDeviceId;
      OutputDeviceName = outputDeviceName;
      PathName = pathName;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="id">The repeater ID</param>
    /// <param name="inputDeviceId">The input device ID</param>
    /// <param name="outputDeviceId">The output device ID</param>
    /// <param name="processId">The process ID</param>
    /// <param name="inputDeviceName">The input device name</param>
    /// <param name="outputDeviceName">The output device name</param>
    /// <param name="pathName">The path name</param>
    /// <param name="bitsPerSample">The amount of bits per sample</param>
    /// <param name="bufferAmount">The buffer amount</param>
    /// <param name="bufferDurationMs">The buffer duration in milliseconds</param>
    /// <param name="channelConfig">The channel configuration</param>
    /// <param name="prefillPercentage">The prefill percentage</param>
    /// <param name="resyncAtPercentage">The resync at percentage</param>
    /// <param name="sampleRateKHz">The sample rate in KiloHertz</param>

    [ExcludeFromCodeCoverage]
    public RepeaterModel
    (
      uint id,
      uint inputDeviceId,
      uint outputDeviceId,
      int? processId,
      string inputDeviceName,
      string outputDeviceName,
      string pathName,
      byte bitsPerSample,
      byte bufferAmount,
      byte prefillPercentage,
      byte resyncAtPercentage,
      ChannelConfig channelConfig,
      uint sampleRateKHz,
      ushort bufferDurationMs
    ) : base(id)
    {
      Id = id;
      InputDeviceId = inputDeviceId;
      OutputDeviceId = outputDeviceId;
      ProcessId = processId;
      BitsPerSample = bitsPerSample;
      BufferDurationMs = bufferDurationMs;
      BufferAmount = bufferAmount;
      ChannelConfig = channelConfig;
      InputDeviceName = inputDeviceName;
      OutputDeviceName = outputDeviceName;
      PathName = pathName;
      PrefillPercentage = prefillPercentage;
      ResyncAtPercentage = resyncAtPercentage;
      SampleRateKHz = sampleRateKHz;
    }

    /// <summary>
    /// Deconstructor
    /// </summary>
    /// <param name="id">The repeater ID</param>
    /// <param name="inputDeviceId">The input device ID</param>
    /// <param name="outputDeviceId">The output device ID</param>
    /// <param name="processId">The process ID</param>
    /// <param name="bitsPerSample">The amount of bits per sample</param>
    /// <param name="bufferAmount">The buffer amount</param>
    /// <param name="bufferDurationMs">The buffer duration in milliseconds</param>
    /// <param name="channelConfig">The channel configuration</param>
    /// <param name="channelList">The channel list</param>
    /// <param name="channelMask">The channel mask</param>
    /// <param name="inputDeviceName">The input device name</param>
    /// <param name="outputDeviceName">The output device name</param>
    /// <param name="pathName">The path name</param>
    /// <param name="prefillPercentage">The prefill percentage</param>
    /// <param name="resyncAtPercentage">The resync at percentage</param>
    /// <param name="sampleRateKHz">The sample rate in KiloHertz</param>
    /// <param name="startArguments">The start arguments</param>
    /// <param name="stopArguments">The stop arguments</param>
    /// <param name="windowName">The window name</param>
    [ExcludeFromCodeCoverage]
    public void Deconstruct
    (
      out uint id,
      out uint inputDeviceId,
      out uint outputDeviceId,
      out int? processId,
      out byte bitsPerSample,
      out byte bufferAmount,
      out byte prefillPercentage,
      out byte resyncAtPercentage,
      out ChannelConfig channelConfig,
      out List<Channel> channelList,
      out string inputDeviceName,
      out string outputDeviceName,
      out string pathName,
      out string startArguments,
      out string stopArguments,
      out string windowName,
      out uint channelMask,
      out uint sampleRateKHz,
      out ushort bufferDurationMs
    )
    {
      id = Id;
      inputDeviceId = InputDeviceId;
      outputDeviceId = OutputDeviceId;
      processId = ProcessId;
      bitsPerSample = BitsPerSample;
      bufferDurationMs = BufferDurationMs;
      bufferAmount = BufferAmount;
      channelConfig = ChannelConfig;
      channelList = ChannelList;
      channelMask = ChannelMask;
      inputDeviceName = InputDeviceName;
      outputDeviceName = OutputDeviceName;
      pathName = PathName;
      prefillPercentage = PrefillPercentage;
      resyncAtPercentage = ResyncAtPercentage;
      sampleRateKHz = SampleRateKHz;
      startArguments = StartArguments;
      stopArguments = StopArguments;
      windowName = WindowName;
    }

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
          "PropertyChanged: {0}",
          propertyName
        )
      );
    }

    /// <summary>
    /// Compiles output to save to text file.
    /// </summary>
    /// <returns>The output</returns>
    public override string ToString()
    {
      return $"{SampleRateKHz}\n" +
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
        || !byte.TryParse
            (
              infoList[5],
              out byte bitsPerSample
            )
        || !ushort.TryParse
            (
              infoList[4],
              out ushort bufferDurationMs
            )
        || !int.TryParse
            (
              infoList[3],
              out int channelConfig
            )
        || !uint.TryParse
            (
              infoList[2],
              out uint channelMask
            )
        || !byte.TryParse
            (
              infoList[6],
              out byte prefillPercentage
            )
        || !byte.TryParse
            (
              infoList[7],
              out byte resyncAtPercentage
            )
        || !uint.TryParse
            (
              infoList[0],
              out uint sampleRateKHz
            )
      )
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Failed to update the repeater\t=> Info List: {0}",
            infoList
          )
        );

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

      Debug.WriteLine
      (
        string.Format
        (
          "Updated the repeater\t=> Info List: {0}",
          infoList
        )
      );
    }

    #endregion
  }
}