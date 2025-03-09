using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using VACARM.Common;
using VACARM.Domain.Enums;
using VACARM.Domain.Structs;

namespace VACARM.Domain.Models
{
  /// <summary>
  /// A snapshot record of the relationship between a capture and render audio 
  /// device pair.
  /// </summary>
  public class RepeaterModel :
    BaseModel,
    IRepeaterModel
  {
    #region Parameters

    private uint inputDeviceId { get; set; }
    private uint outputDeviceId { get; set; }
    private int? processId { get; set; } = null;
    private bool isStarted { get; set; } = false;
    private byte bitsPerSample { get; set; } =
      DefaultRepeaterModel.BitsPerSample;

    private byte bufferAmount { get; set; } =
      DefaultRepeaterModel.BufferAmount;

    private byte prefillPercentage { get; set; } =
      DefaultRepeaterModel.PrefillPercentage;

    private byte resyncAtPercentage { get; set; } =
      DefaultRepeaterModel.ResyncAtPercentage;

    private ChannelConfig channelConfig { get; set; } =
      DefaultRepeaterModel.ChannelConfig;

    private List<Channel> channelList { get; set; } = new List<Channel>();
    private string inputDeviceName { get; set; } = string.Empty;
    private string outputDeviceName { get; set; } = string.Empty;
    private string pathName { get; set; } = string.Empty;

    private uint sampleRateKHz { get; set; } =
      DefaultRepeaterModel.SampleRateKHz;

    private ushort bufferDurationMs { get; set; } =
      DefaultRepeaterModel.BufferDurationMs;

    public uint InputDeviceId
    {
      get
      {
        return this.inputDeviceId;
      }
      set
      {
        this.inputDeviceId = value;
        base.OnPropertyChanged(nameof(this.InputDeviceId));
      }
    }

    public uint OutputDeviceId
    {
      get
      {
        return this.outputDeviceId;
      }
      set
      {
        this.outputDeviceId = value;
        base.OnPropertyChanged(nameof(this.OutputDeviceId));
      }
    }

    public int? ProcessId
    {
      get
      {
        return this.processId;
      }
      set
      {
        this.processId = value;
        base.OnPropertyChanged(nameof(this.ProcessId));
      }
    }

    public bool IsStarted
    {
      get
      {
        return this.isStarted;
      }
      set
      {
        this.isStarted = value;
        base.OnPropertyChanged(nameof(this.IsStarted));
      }
    }

    public ChannelConfig ChannelConfig
    {
      get
      {
        return this.channelConfig;
      }
      set
      {
        if (value != ChannelConfig.Custom)
        {
          this.ChannelMask = (uint)value;
        }

        this.channelConfig = value;
        base.OnPropertyChanged(nameof(this.ChannelConfig));
      }
    }

    /// <summary>
    /// The amount of bits per sample.
    /// </summary>
    public byte BitsPerSample
    {
      get
      {
        return this.bitsPerSample;
      }
      set
      {
        if
        (
          value >= 8
          && value <= 32
        )
        {
          this.bitsPerSample = value;
        }
        else
        {
          this.bitsPerSample = 16;
        }

        base.OnPropertyChanged(nameof(this.BitsPerSample));
      }
    }

    /// <summary>
    /// The amount of short-term data particles.
    /// </summary>
    public byte BufferAmount
    {
      get
      {
        return this.bufferAmount;
      }
      set
      {
        if
        (
          value >= 1
          && value <= byte.MaxValue
        )
        {
          this.bufferAmount = value;
        }
        else
        {
          this.bufferAmount = 8;
        }

        base.OnPropertyChanged(nameof(this.BufferAmount));
      }
    }

    /// <summary>
    /// The amount of channels.
    /// </summary>
    public byte ChannelCount
    {
      get
      {
        if (this.ChannelList == null)
        {
          return 0;
        }

        return (byte)this.ChannelList
          .Count;
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
        return this.prefillPercentage;
      }
      set
      {
        if
        (
          value >= DefaultRepeaterModel.PrefillPercentageOptions
            .FirstOrDefault()
          && value <= DefaultRepeaterModel.PrefillPercentageOptions
            .Last()
        )
        {
          this.prefillPercentage = value;
        }
        else
        {
          this.prefillPercentage = 50;
        }

        base.OnPropertyChanged(nameof(this.PrefillPercentage));
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
        return this.resyncAtPercentage;
      }
      set
      {
        if
        (
          value >= 0
          && value < this.prefillPercentage
        )
        {
          this.resyncAtPercentage = value;
        }
        else
        {
          this.resyncAtPercentage = (byte)Math
            .Round((double)(this.prefillPercentage / 2));
        }

        base.OnPropertyChanged(nameof(this.ResyncAtPercentage));
      }
    }

    /// <summary>
    /// The individual Channels available to the repeater, given the Channel layout.
    /// </summary>
    public List<Channel> ChannelList
    {
      get
      {
        return this.channelList;
      }
      set
      {
        if (value == null)
        {
          value = new List<Channel>();
        }

        this.channelList = value;
        base.OnPropertyChanged(nameof(this.ChannelList));
      }
    }

    /// <summary>
    /// The input device name.
    /// </summary>
    public string InputDeviceName
    {
      get
      {
        return this.inputDeviceName;
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

        this.inputDeviceName = value;
        base.OnPropertyChanged(nameof(this.InputDeviceName));
      }
    }

    /// <summary>
    /// The output device name.
    /// </summary>
    public string OutputDeviceName
    {
      get
      {
        return this.outputDeviceName;
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

        this.outputDeviceName = value;
        base.OnPropertyChanged(nameof(this.OutputDeviceName));
      }
    }

    /// <summary>
    /// The file pathname.
    /// </summary>
    public string PathName
    {
      get
      {
        return this.pathName;
      }
      set
      {
        if (value == null)
        {
          value = string.Empty;
        }

        if (!File.Exists(value))
        {
          value = string.Empty;
        }

        this.pathName = value;
        base.OnPropertyChanged(nameof(this.PathName));
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
          $"/min \"{Info.ExpectedExecutablePathName}\" \"{this.PathName}\" " +
          $"/Input:\"{this.InputDeviceName}\" " +
          $"/Output:\"{this.OutputDeviceName}\" " +
          $"/SampleRate:{this.SampleRateKHz} " +
          $"/BitsPerSample:{this.BitsPerSample} " +
          $"/Channels:{this.ChannelList.Count} " +
          $"/ChanCfg:custom={this.ChannelMask} " +
          $"/BufferMs:{this.BufferDurationMs} " +
          $"/Prefill:{this.PrefillPercentage} " +
          $"/ResyncAt:{this.ResyncAtPercentage} " +
          $"/WindowName:\"{this.WindowName}\" " +
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
          $"start \"{Info.ExpectedExecutablePathName}\" " +
          $"\"{this.PathName}\" " +
          $"/CloseInstance:\"{this.WindowName}\"";
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
            this.Id
              .ToString(),
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

        if (this.ChannelList != null)
        {
          this.ChannelList.ForEach
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

        if (this.channelConfig != ChannelConfig.Custom)
        {
          this.channelConfig = ChannelConfig.Custom;
          base.OnPropertyChanged(nameof(this.ChannelConfig));
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

        this.ChannelList = newChannelList;
        base.OnPropertyChanged(nameof(this.ChannelMask));
      }
    }

    /// <summary>
    /// The sample rate in KiloHertz.
    /// </summary>
    public uint SampleRateKHz
    {
      get
      {
        return this.sampleRateKHz;
      }
      set
      {
        if
        (
          value >= DefaultRepeaterModel.SampleRateKHzOptions
            .FirstOrDefault()
          && value <= DefaultRepeaterModel.SampleRateKHzOptions
            .Last()
        )
        {
          this.sampleRateKHz = value;
        }
        else
        {
          this.sampleRateKHz = 48000;
        }

        base.OnPropertyChanged(nameof(this.SampleRateKHz));
      }
    }

    /// <summary>
    /// The buffer time in milliseconds.
    /// </summary>
    public ushort BufferDurationMs
    {
      get
      {
        return this.bufferDurationMs;
      }
      set
      {
        if
        (
          value >= 1
          && value <= ushort.MaxValue
        )
        {
          this.bufferDurationMs = value;
        }
        else
        {
          this.bufferDurationMs = 500;
        }

        base.OnPropertyChanged(nameof(this.BufferDurationMs));
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
    ) :
      base(id)
    {
      this.Id = id;
      this.InputDeviceId = inputDeviceId;
      this.OutputDeviceId = outputDeviceId;
      this.ProcessId = processId;
      this.InputDeviceName = inputDeviceName;
      this.OutputDeviceName = outputDeviceName;
      this.ProcessId = processId;
      this.PathName = pathName;
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
      bool isStarted,
      byte bitsPerSample,
      byte bufferAmount,
      byte prefillPercentage,
      byte resyncAtPercentage,
      ChannelConfig channelConfig,
      uint sampleRateKHz,
      ushort bufferDurationMs
    ) : 
      base(id)
    {
      this.Id = id;
      this.InputDeviceId = inputDeviceId;
      this.OutputDeviceId = outputDeviceId;
      this.ProcessId = processId;
      this.BitsPerSample = bitsPerSample;
      this.BufferDurationMs = bufferDurationMs;
      this.BufferAmount = bufferAmount;
      this.ChannelConfig = channelConfig;
      this.InputDeviceName = inputDeviceName;
      this.IsStarted = isStarted;
      this.OutputDeviceName = outputDeviceName;
      this.PathName = pathName;
      this.PrefillPercentage = prefillPercentage;
      this.ResyncAtPercentage = resyncAtPercentage;
      this.SampleRateKHz = sampleRateKHz;
    }

    [ExcludeFromCodeCoverage]
    public void Deconstruct
    (
      out uint id,
      out uint inputDeviceId,
      out uint outputDeviceId,
      out int? processId,
      bool isStarted,
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
      id = this.Id;
      inputDeviceId = this.InputDeviceId;
      outputDeviceId = this.OutputDeviceId;
      processId = this.ProcessId;
      bitsPerSample = this.BitsPerSample;
      bufferDurationMs = this.BufferDurationMs;
      bufferAmount = this.BufferAmount;
      channelConfig = this.ChannelConfig;
      channelList = this.ChannelList;
      channelMask = this.ChannelMask;
      inputDeviceName = this.InputDeviceName;
      isStarted = this.IsStarted;
      outputDeviceName = this.OutputDeviceName;
      pathName = this.PathName;
      prefillPercentage = this.PrefillPercentage;
      resyncAtPercentage = this.ResyncAtPercentage;
      sampleRateKHz = this.SampleRateKHz;
      startArguments = this.StartArguments;
      stopArguments = this.StopArguments;
      windowName = this.WindowName;
    }

    /// <summary>
    /// Compiles output to save to text file.
    /// </summary>
    /// <returns>The output</returns>
    public override string ToString()
    {
      return $"{this.SampleRateKHz}\n" +
        $"{this.BitsPerSample}\n" +
        $"{this.ChannelMask}\n" +
        $"{(int)this.ChannelConfig}\n" +
        $"{this.BufferDurationMs}\n" +
        $"{this.BufferAmount}\n" +
        $"{this.PrefillPercentage}\n" +
        $"{this.ResyncAtPercentage}";
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

      this.BitsPerSample = bitsPerSample;
      this.BufferAmount = bitsPerSample;
      this.BufferDurationMs = bufferDurationMs;
      this.ChannelConfig = (ChannelConfig)channelConfig;
      this.ChannelMask = channelMask;

      this.PrefillPercentage = prefillPercentage;
      this.ResyncAtPercentage = resyncAtPercentage;
      this.SampleRateKHz = sampleRateKHz;

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