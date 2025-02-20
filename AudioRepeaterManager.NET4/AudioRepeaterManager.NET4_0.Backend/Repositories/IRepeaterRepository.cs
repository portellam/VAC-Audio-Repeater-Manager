using System.Collections.Generic;
using System.ComponentModel;
using AudioRepeaterManager.NET4_0.Backend.Models;

namespace AudioRepeaterManager.NET4_0.Backend.Repositories
{
  public interface IRepeaterRepository
  {
    #region Parameters

    event PropertyChangedEventHandler PropertyChanged;

    #endregion

    #region Logic

    RepeaterModel Get(uint? id);

    RepeaterModel Get
    (
      uint? firstDeviceId,
      uint? secondDeviceId
    );

    List<RepeaterModel> GetAll();

    List<RepeaterModel> GetRange
    (
      string deviceName,
      bool isInputDevice,
      bool isOutputDevice
    );

    List<RepeaterModel> GetRange(List<uint?> idList);
    void Insert(RepeaterModel repeaterModel);

    void Insert
    (
      uint id,
      uint inputDeviceId,
      uint outputDeviceId,
      byte bitsPerSample,
      byte bufferAmount,
      byte prefillPercentage,
      byte resyncAtPercentage,
      string inputDeviceName,
      string outputDeviceName,
      string pathName,
      string windowName,
      uint channelMask,
      uint sampleRateKHz,
      ushort bufferDurationMs
    );

    void Remove
    (
      uint? id
    );

    void Remove
    (
      uint? firstDeviceId,
      uint? secondDeviceId
    );

    void RemoveRange
    (
      string deviceName
    );

    void Update(RepeaterModel repeaterModel);

    void Update
    (
      uint id,
      uint inputDeviceId,
      uint outputDeviceId,
      byte bitsPerSample,
      byte bufferAmount,
      byte prefillPercentage,
      byte resyncAtPercentage,
      string inputDeviceName,
      string outputDeviceName,
      string pathName,
      string windowName,
      uint channelMask,
      uint sampleRateKHz,
      ushort bufferDurationMs
    );

    #endregion
  }
}
