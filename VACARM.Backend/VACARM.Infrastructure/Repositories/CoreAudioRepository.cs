using AudioSwitcher.AudioApi;

namespace VACARM.Infrastructure.Repositories
{
  /// <summary>
  /// A up-to-date repository of all system audio devices.
  /// Improved functionality over <typeparamref name="MMDeviceRepository".
  /// </summary>
  public class CoreAudioRepository<T> :
    GenericRepository<T>,
    ICoreAudioRepository<T> where T :
    Device
  {
    #region Parameters

    #endregion

    #region Logic

    public CoreAudioRepository()
    {
    }

    #endregion
  }
}
