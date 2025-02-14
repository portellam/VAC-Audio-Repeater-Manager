using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Services
{
  public partial interface IDeviceService<TRepository, TDeviceModel> where 
    TRepository :
    IDeviceRepository<TDeviceModel> where TDeviceModel :
    DeviceModel
  {
  }
}