using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Services
{
  public partial interface IRepeaterService<TRepository, TRepeaterModel> where
    TRepository :
    RepeaterRepository<TRepeaterModel> where TRepeaterModel :
    RepeaterModel
  {
    #region Parameters

    bool PreferLegacyExecutable { get; set; }
    DeviceService<DeviceRepository<DeviceModel>, DeviceModel> DeviceService { get; }
    string CustomExecutablePathName { get; set; }
    string ExecutableFullPathName { get; }

    #endregion
  }
}