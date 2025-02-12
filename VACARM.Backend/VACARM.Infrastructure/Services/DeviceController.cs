using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Controllers
{
  /// <summary>
  /// The controller of the <typeparamref name="DeviceRepository"/>.
  /// </summary>
  public class DeviceController<T1, T2> :
    BaseController<DeviceRepository<DeviceModel>, DeviceModel>,
    IDeviceController<DeviceRepository<DeviceModel>, DeviceModel> where T1 :
    DeviceRepository<DeviceModel> where T2 :
    DeviceModel
  {
    #region Parameters

    #endregion

    #region Logic

    #endregion
  }
}