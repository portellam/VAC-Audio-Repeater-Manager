using VACARM.Domain.Models;

namespace VACARM.Application.Controllers
{
  public interface IDeviceController
  {
    #region Logic

    void SetAsDefault(string actualId);
    void SetAsDefault(uint id);
    DeviceModel Get(string actualId);
    DeviceModel Get(uint id);
    List<DeviceModel> GetRange(List<string> actualIdList);
    List<DeviceModel> GetRange(List<uint> idList);
    void Restart(string id);
    void Restart(uint id);
    void RestartRange(List<string> actualIdList);
    void RestartRange(List<uint> idList);
    void Start(string id);
    void Start(uint id);
    void StartRange(List<string> actualIdList);
    void StartRange(List<uint> idList);
    void Stop(string id);
    void Stop(uint id);
    void StopRange(List<string> actualIdList);
    void StopRange(List<uint> idList);
    void Update(string id);
    void Update(uint id);
    void UpdateRange(List<string> actualIdList);
    void UpdateRange(List<uint> idList);

    #endregion
  }
}