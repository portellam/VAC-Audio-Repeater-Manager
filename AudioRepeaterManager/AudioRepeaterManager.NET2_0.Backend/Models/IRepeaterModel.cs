using System.Collections.Generic;
using AudioRepeaterManager.NET2_0.Backend.Structs;

namespace AudioRepeaterManager.NET2_0.Backend.Models
{
  public interface IRepeaterModel
  {
    #region Parameters

    /// <summary>
    /// Primary key
    /// </summary>
    uint Id { get; set; }

    /// <summary>
    /// Foreign key
    /// </summary>
    uint InputDeviceId { get; set; }

    /// <summary>
    /// Foreign key
    /// </summary>
    uint OutputDeviceId { get; set; }

    byte BitsPerSample { get; set; }
    byte BufferAmount { get; set; }
    byte ChannelCount { get; }
    byte PrefillPercentage { get; set; }
    byte ResyncAtPercentage { get; set; }
    List<Channel> ChannelList { get; set; }
    string InputDeviceName { get; set; }
    string OutputDeviceName { get; set; }
    string PathName { get; set; }
    string WindowName { get; set; }
    uint ChannelMask { get; set; }
    uint SampleRateKHz { get; set; }
    ushort BufferDurationMs { get; set; }

    #endregion

    #region Logic

    void Deconstruct
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
    );

    string ToCommand();
    string ToString();
    void Set(List<string> infoList);

    #endregion
  }
}