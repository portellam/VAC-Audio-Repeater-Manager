using AudioSwitcher.AudioApi;

namespace VACARM.Infrastructure.Repositories
{
  public partial class CoreAudioRepository<T> :
    GenericRepository<Device>,
    ICoreAudioRepository<Device> where T :
    Device
  {
    #region Logic

    #endregion
  }
}
