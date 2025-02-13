using AudioSwitcher.AudioApi;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Services
{
  public partial interface ICoreAudioService<TRepository, TDevice> :
    IService<CoreAudioRepository<TDevice>, TDevice> where TRepository :
    CoreAudioRepository<TDevice> where TDevice :
    Device
  {
  }
}