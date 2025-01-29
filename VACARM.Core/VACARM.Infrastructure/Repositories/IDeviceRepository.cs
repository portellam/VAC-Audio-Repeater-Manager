using VACARM.Core.Models;

namespace VACARM.Core.Repositories
{
  public interface IDeviceRepository
  {
    #region Logic

    List<DeviceModel> GetAllAbsent();
    List<DeviceModel> GetAllInAlphabeticalOrder();
    List<DeviceModel> GetAllInput();
    List<DeviceModel> GetAllOutput();
    List<DeviceModel> GetAllPresent();

    #endregion
  }
}
