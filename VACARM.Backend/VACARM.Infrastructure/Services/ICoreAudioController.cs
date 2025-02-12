using AudioSwitcher.AudioApi;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Services
{
  public partial interface ICoreAudioService<TRepository, TItem> :
    IGenericService<CoreAudioRepository<Device>, Device> where TRepository :
    CoreAudioRepository<Device> where TItem :
    Device
  {
    #region Logic

    #endregion 
  }
}