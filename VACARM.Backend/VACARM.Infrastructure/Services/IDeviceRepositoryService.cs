using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Services
{
  public partial interface IDeviceRepositoryService
    <TService, TRepository, TDeviceModel> where TService :
    BaseService<TRepository, TDeviceModel> where TRepository :
    BaseRepository<TDeviceModel> where TDeviceModel :
    DeviceModel
  {
  }
}