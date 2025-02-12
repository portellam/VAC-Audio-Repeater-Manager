using AudioSwitcher.AudioApi;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Controllers
{
  public partial interface ICoreAudioController<TRepository, TItem> :
    IGenericController<CoreAudioRepository<Device>, Device> where TRepository :
    CoreAudioRepository<Device> where TItem :
    Device
  {
    #region Logic

    #endregion 
  }
}