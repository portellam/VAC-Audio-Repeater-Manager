using System.Diagnostics.CodeAnalysis;
using VACARM.Domain.Enums;
using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Functions
{
  public static class RepeaterFunctions<TRepeaterModel>
    where TRepeaterModel :
    RepeaterModel
  {
    #region Parameters

    public static Func<TRepeaterModel, bool> IsStarted =
      (TRepeaterModel x) => x.IsStarted;

    public static Func<TRepeaterModel, bool> IsStopped =
      (TRepeaterModel x) => !x.IsStarted;

    #endregion

    #region Logic

    /// <summary>
    /// Get a <typeparamref name="TRepeaterModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <param name="inputDeviceModel">The device model</param>
    /// <param name="outputDeviceModel">The device model</param>
    /// <param name="processId">The process ID</param>
    /// <param name="pathName">The path name</param>
    /// <returns>The repeater model.</returns>
    public static TRepeaterModel Get
    (
      uint id,
      DeviceModel inputDeviceModel,
      DeviceModel outputDeviceModel,
      int? processId,
      string? pathName
    )
    {
      RepeaterModel model = new RepeaterModel
        (
          id,
          inputDeviceModel.Id,
          outputDeviceModel.Id,
          processId,
          inputDeviceModel.Name,
          outputDeviceModel.Name,
          pathName
        );

      return (TRepeaterModel)model;
    }

    /// <summary>
    /// Get a <typeparamref name="TRepeaterModel"/>.
    /// </summary>
    /// <param name="id">The repeater ID</param>
    /// <param name="inputDeviceId">The input device ID</param>
    /// <param name="outputDeviceId">The output device ID</param>
    /// <param name="processId">The process ID</param>
    /// <param name="inputDeviceName">The input device name</param>
    /// <param name="outputDeviceName">The output device name</param>
    /// <param name="pathName">The path name</param>
    public static RepeaterModel Get
    (
      uint id,
      uint inputDeviceId,
      uint outputDeviceId,
      int? processId,
      string inputDeviceName,
      string outputDeviceName,
      string? pathName
    )
    {
      RepeaterModel model = new RepeaterModel
        (
          id,
          inputDeviceId,
          outputDeviceId,
          processId,
          inputDeviceName,
          outputDeviceName,
          pathName
        );

      return (TRepeaterModel)model;
    }

    /// <summary>
    /// Get a <typeparamref name="TRepeaterModel"/>.
    /// </summary>
    /// <param name="id">The repeater ID</param>
    /// <param name="inputDeviceModel">The device model</param>
    /// <param name="outputDeviceModel">The device model</param>
    /// <param name="processId">The process ID</param>
    /// <param name="pathName">The path name</param>
    /// <param name="bitsPerSample">The amount of bits per sample</param>
    /// <param name="bufferAmount">The buffer amount</param>
    /// <param name="bufferDurationMs">The buffer duration in milliseconds</param>
    /// <param name="channelConfig">The channel configuration</param>
    /// <param name="prefillPercentage">The prefill percentage</param>
    /// <param name="resyncAtPercentage">The resync at percentage</param>
    /// <param name="sampleRateKHz">The sample rate in KiloHertz</param>
    /// <returns>The repeater model.</returns>
    public static TRepeaterModel Get
    (
      uint id,
      DeviceModel inputDeviceModel,
      DeviceModel outputDeviceModel,
      int? processId,
      string? pathName,
      bool isStarted,
      byte bitsPerSample,
      byte bufferAmount,
      byte prefillPercentage,
      byte resyncAtPercentage,
      ChannelConfig channelConfig,
      uint sampleRateKHz,
      ushort bufferDurationMs
    )
    {
      RepeaterModel model = new RepeaterModel
        (
          id,
          inputDeviceModel.Id,
          outputDeviceModel.Id,
          processId,
          inputDeviceModel.Name,
          outputDeviceModel.Name,
          pathName
        )
        {
          IsStarted = isStarted,
          BitsPerSample = bitsPerSample,
          BufferAmount = bufferAmount,
          BufferDurationMs = bufferDurationMs,
          ChannelConfig = channelConfig,
          PrefillPercentage = prefillPercentage,
          ResyncAtPercentage = resyncAtPercentage,
          SampleRateKHz = sampleRateKHz,
        };

      return (TRepeaterModel)model;
    }


    /// <summary>
    /// Get a <typeparamref name="TRepeaterModel"/>.
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
    public static RepeaterModel Get
    (
      uint id,
      uint inputDeviceId,
      uint outputDeviceId,
      int? processId,
      string inputDeviceName,
      string outputDeviceName,
      string? pathName,
      bool isStarted,
      byte bitsPerSample,
      byte bufferAmount,
      byte prefillPercentage,
      byte resyncAtPercentage,
      ChannelConfig channelConfig,
      uint sampleRateKHz,
      ushort bufferDurationMs
    )
    {
      RepeaterModel model = new RepeaterModel
       (
         id,
         inputDeviceId,
         outputDeviceId,
         processId,
         inputDeviceName,
         outputDeviceName,
         pathName
       )
      {
        IsStarted = isStarted,
        BitsPerSample = bitsPerSample,
        BufferAmount = bufferAmount,
        BufferDurationMs = bufferDurationMs,
        ChannelConfig = channelConfig,
        PrefillPercentage = prefillPercentage,
        ResyncAtPercentage = resyncAtPercentage,
        SampleRateKHz = sampleRateKHz,
      };

      return (TRepeaterModel)model;
    }

    /// <summary>
    /// Match a <typeparamref name="TRepeaterModel"/> device ID.
    /// </summary>
    /// <param name="deviceId">The device ID</param>
    /// <returns>The function</returns>
    public static Func<TRepeaterModel, bool> ContainsDeviceId(uint deviceId)
    {
      return (TRepeaterModel x) => 
        x.InputDeviceId == deviceId
        || x.OutputDeviceId == deviceId;
    }

    /// <summary>
    /// Match a <typeparamref name="TRepeaterModel"/> device name.
    /// </summary>
    /// <param name="deviceName">The device name</param>
    /// <returns>The function</returns>
    public static Func<TRepeaterModel, bool> ContainsDeviceName(string deviceName)
    {
      return (TRepeaterModel x) =>
        x.InputDeviceName == deviceName
        || x.OutputDeviceName == deviceName;
    }

    /// <summary>
    /// Match a <typeparamref name="TRepeaterModel"/> window name.
    /// </summary>
    /// <param name="windowName">The window name</param>
    /// <returns>The function</returns>
    public static Func<TRepeaterModel, bool> ContainsWindowName(string windowName)
    {
      return (TRepeaterModel x) =>
        x.WindowName
          .ToLower()
          .Contains(windowName);
    }

    #endregion
  }
}