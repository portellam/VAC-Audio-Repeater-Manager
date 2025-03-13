#warning Differs from projects of later NET revisions (above Framework 4.0).

using System.Collections.Generic;
using System.Threading.Tasks;

namespace VACARM.Infrastructure.Services
{
  public partial interface IDeviceGroupService
    <
      TGroupReadonlyRepository,
      TBaseService,
      TBaseRepository,
      TDeviceModel
    >
  {
    #region Logic

    /// <summary>
    /// Update the service.
    /// </summary>
    Task UpdateServiceAsync();

    #endregion
  }
}