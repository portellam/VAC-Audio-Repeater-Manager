using AudioSwitcher.AudioApi;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Services
{
  public partial interface ICoreAudioService<TRepository, TDevice> where
    TRepository :
    CoreAudioRepository<TDevice> where TDevice :
    Device
  {
  }
}