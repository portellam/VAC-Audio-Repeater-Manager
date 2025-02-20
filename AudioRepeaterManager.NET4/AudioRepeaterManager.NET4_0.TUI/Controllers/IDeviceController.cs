using NAudio.CoreAudioApi;
using System.Collections.Generic;
using System.ComponentModel;
using AudioRepeaterManager.NET4_0.Backend.Models;

namespace AudioRepeaterManager.NET4_0.Backend.Controllers
{
  public interface IDeviceController
  {
    #region Parameters

    event PropertyChangedEventHandler PropertyChanged;

    #endregion

    #region Logic

    DeviceModel Get(string actualId);
    DeviceModel Get(uint? id);
    List<DeviceModel> GetAll();
    List<DeviceModel> GetAllAbsent();
    List<DeviceModel> GetAllDisabled();
    List<DeviceModel> GetAllEnabled();
    List<DeviceModel> GetAllDuplex();
    List<DeviceModel> GetAllInput();
    List<DeviceModel> GetAllOutput();
    List<DeviceModel> GetAllPresent();
    List<DeviceModel> GetRange(List<string> actualIdList);
    List<DeviceModel> GetRange(List<uint?> idList);
    void DisableActual(string actualId);
    void EnableActual(string actualId);
    void Insert(MMDevice mMDevice);

    void Insert
    (
      string actualId,
      string name,
      bool? isInput,
      bool? isOutput,
      bool? isPresent
    );

    void Remove(uint? id);

    void Remove(string actualId);

    void SetAsDefault(string actualId);

    void Update
    (
      uint? id,
      MMDevice mMDevice
    );

    #endregion
  }
}