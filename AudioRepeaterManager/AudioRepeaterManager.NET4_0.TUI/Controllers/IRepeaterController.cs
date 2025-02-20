using NAudio.CoreAudioApi;
using System.Collections.Generic;
using System.ComponentModel;
using AudioRepeaterManager.NET4_0.Backend.Models;
using AudioRepeaterManager.NET4_0.Backend.Structs;

namespace AudioRepeaterManager.NET4_0.Backend.Controllers
{
  public interface IRepeaterController
  {
    #region Parameters

    event PropertyChangedEventHandler PropertyChanged;

    #endregion

    #region Logic

    DeviceModel Get(string actualId);
    DeviceModel Get(uint? id);
    List<DeviceModel> GetAllAbsent();
    List<DeviceModel> GetAll();
    List<DeviceModel> GetAllInput();
    List<DeviceModel> GetAllOutput();
    List<DeviceModel> GetAllPresent();
    void Insert(MMDevice mMDevice);

    void Insert
    (
      string actualId,
      string name,
      bool? isInput,
      bool? isOutput,
      bool? isPresent
    );

    void Remove
    (
      uint? id
    );

    void Update
    (
      uint? id,
      uint? inputDeviceId,
      uint? outputDeviceId
    );

    void Update
    (
      uint? id,
      uint? inputDeviceId,
      uint? outputDeviceId,
      byte bitsPerSample,
      byte bufferAmount,
      byte channelCount,
      byte prefillPercentage,
      byte resyncAtPercentage,
      List<Channel> channelList,
      string pathName,
      string windowName,
      uint sampleRateKHz,
      ushort bufferDurationMs
    );

    #endregion
  }
}
