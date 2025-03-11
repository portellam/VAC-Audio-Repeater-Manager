using NAudio.CoreAudioApi;
using System.Collections.Generic;
using System.Threading.Tasks;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Infrastructure.Services
{
  public partial class DeviceGroupService
    <
      TGroupReadonlyRepository,
      TBaseService,
      TBaseRepository,
      TDeviceModel
    >
  {
    #region Logic

    public async Task UpdateServiceAsync()
    {
      if (this.MMDeviceService == null)
      {
        this.MMDeviceService =
          new MMDeviceService<ReadonlyRepository<MMDevice>, MMDevice>();
      }

      else
      {
        this.MMDeviceService
          .UpdateAll();
      }
    }

    #endregion
  }
}