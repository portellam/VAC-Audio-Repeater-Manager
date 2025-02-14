using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Services
{
  public partial interface IDeviceService<TRepository, TDeviceModel> where
    TRepository :
    DeviceRepository<TDeviceModel> where TDeviceModel :
    DeviceModel
  {
  }
}