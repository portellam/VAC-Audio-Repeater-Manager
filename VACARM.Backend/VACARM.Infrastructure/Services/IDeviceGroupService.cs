using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Services
{
  public partial interface IDeviceGroupService
    <
      TParentRepository,
      TRepository,
      TDeviceModel
    >
    where TParentRepository :
    GroupService
    <
      DeviceService
      <
        BaseRepository<TDeviceModel>,
        TDeviceModel
      >,
      BaseRepository<TDeviceModel>,
      TDeviceModel
    >
    where TRepository :
    DeviceService
    <
      BaseRepository<TDeviceModel>,
      TDeviceModel
    >
    where TDeviceModel :
    DeviceModel
  {
  }
}