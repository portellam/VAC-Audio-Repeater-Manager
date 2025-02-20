using System.Collections.Generic;
using System.ComponentModel;
using AudioRepeaterManager.NET4_0.Backend.Models;

namespace AudioRepeaterManager.NET4_0.GUI.Controllers
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
    List<DeviceModel> GetAllDuplex();
    List<DeviceModel> GetAllEnabled();
    List<DeviceModel> GetAllInput();
    List<DeviceModel> GetAllOutput();
    List<DeviceModel> GetAllPresent();
    List<DeviceModel> GetRange(List<string> actualIdList);
    List<DeviceModel> GetRange(List<uint?> idList);

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

    #endregion
  }
}