using VACARM.Domain.Structs;

namespace VACARM.Domain.Models
{
  public interface IRepeaterModel : IModel
  {
    #region Parameters

    /// <summary>
    /// Foreign key
    /// </summary>
    uint InputDeviceId { get; set; }

    /// <summary>
    /// Foreign key
    /// </summary>
    uint OutputDeviceId { get; set; }

    /// <summary>
    /// Foreign key
    /// </summary>
    int? ProcessId { get; set; }

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

    void Deconstruct
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
      out List<string> propertyList,
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