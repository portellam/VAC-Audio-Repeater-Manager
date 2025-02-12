using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Services
{
  /// <summary>
  /// The service of the <typeparamref name="DeviceRepository"/>.
  /// </summary>
  public class DeviceService<TRepository, TItem> :
    BaseService<DeviceRepository<DeviceModel>, DeviceModel>,
    IDeviceService<DeviceRepository<DeviceModel>, DeviceModel> where TRepository :
    DeviceRepository<DeviceModel> where TItem :
    DeviceModel
  {
    #region Parameters

    #endregion

    #region Logic

    #endregion
  }
}