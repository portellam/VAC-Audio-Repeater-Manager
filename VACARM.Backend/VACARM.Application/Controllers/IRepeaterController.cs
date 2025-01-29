using VACARM.Domain.Models;

namespace VACARM.Application.Controllers
{
  public interface IRepeaterController
  {
    #region Logic

    IDeviceController Get();
    List<RepeaterModel> Get(uint id);
    List<RepeaterModel> GetAll();
    List<RepeaterModel> GetByDeviceId(uint deviceId);
    List<RepeaterModel> GetRange(List<uint> idList);
    List<RepeaterModel> GetRangeByDeviceId(List<uint> deviceIdList);
    void Restart(uint id);
    void RestartAll();
    void RestartByDeviceId(uint deviceId);
    void RestartByDeviceName(string deviceName);
    void RestartRange(List<uint> idList);
    void RestartRangeByDeviceId(List<uint> deviceIdList);
    void RestartRangeByDeviceName(List<string> deviceNameList);
    void Start(uint id);
    void StartAll();
    void StartByDeviceId(uint deviceId);
    void StartByDeviceName(string deviceName);
    void StartRange(List<uint> idList);
    void StartRangeByDeviceId(List<uint> deviceIdList);
    void StartRangeByDeviceName(List<string> deviceNameList);
    void Stop(string id);
    void Stop(uint id);
    void StopAll();
    void StopByDeviceId(uint deviceId);
    void StopByDeviceName(string deviceName);
    void StopRange(List<uint> idList);
    void StopRangeByDeviceId(List<uint> deviceIdList);
    void StopRangeByDeviceName(List<string> deviceNameList);
    void Update(string id);
    void Update(uint id);
    void UpdateAll();
    void UpdateByDeviceId(uint deviceId);
    void UpdateByDeviceName(string deviceName);
    void UpdateRange(List<uint> idList);
    void UpdateRangeByDeviceId(List<uint> deviceIdList);
    void UpdateRangeByDeviceName(List<string> deviceNameList);

    #endregion
  }
}