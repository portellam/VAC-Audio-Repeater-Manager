using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Repositories
{
  public interface IDeviceRepository : IRepository<DeviceModel>
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
