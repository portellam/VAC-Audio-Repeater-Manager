using AudioSwitcher.AudioApi;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Controllers
{
  public partial interface ICoreAudioController<T1, T2> :
    IGenericController<CoreAudioRepository<Device>, Device> where T1 :
    CoreAudioRepository<Device> where T2 :
    Device
  {
    #region Logic

    #endregion 
  }
}