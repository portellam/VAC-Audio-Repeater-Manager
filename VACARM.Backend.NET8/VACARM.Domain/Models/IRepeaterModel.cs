using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VACARM.Domain.Enums;

namespace VACARM.Domain.Models
{
  public interface IRepeaterModel
  {
    #region Parameters

    /// <summary>
    /// Foreign key
    /// </summary>
    [Required]
    uint InputDeviceId { get; set; }

    /// <summary>
    /// Foreign key
    /// </summary>
    [Required]
    uint OutputDeviceId { get; set; }

    /// <summary>
    /// Foreign key
    /// </summary>
    int? ProcessId { get; set; }

    bool IsStarted { get; set; }
    byte BitsPerSample { get; set; }
    byte BufferAmount { get; set; }
    byte ChannelCount { get; }
    byte PrefillPercentage { get; set; }
    byte ResyncAtPercentage { get; set; }
    List<Channel> ChannelList { get; set; }
    string InputDeviceName { get; set; }
    string OutputDeviceName { get; set; }
    string PathName { get; set; }
    string StartArguments { get; }
    string StopArguments { get; }
    string WindowName { get; }
    uint ChannelMask { get; set; }
    uint SampleRateKHz { get; set; }
    ushort BufferDurationMs { get; set; }

    #endregion

    #region Logic

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
    /// <param name="isStarted">True/false is the repeater started</param>
    /// <param name="outputDeviceName">The output device name</param>
    /// <param name="pathName">The path name</param>
    /// <param name="prefillPercentage">The prefill percentage</param>
    /// <param name="resyncAtPercentage">The resync at percentage</param>
    /// <param name="sampleRateKHz">The sample rate in KiloHertz</param>
    /// <param name="startArguments">The start arguments</param>
    /// <param name="stopArguments">The stop arguments</param>
    /// <param name="windowName">The window name</param>
    void Deconstruct
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
    );

    string ToString();
    void Set(List<string> infoList);

    #endregion
  }
}