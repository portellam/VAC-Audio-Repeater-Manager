using AudioSwitcher.AudioApi;
using AudioSwitcher.AudioApi.CoreAudio;
using VACARM.Application.Commands;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Controllers
{
  /// <summary>
  /// A controller for the <typeparamref name="CoreAudioRepository"/>.
  /// </summary>
  /// <typeparam name="T1">The repository</typeparam>
  /// <typeparam name="T2">The item</typeparam>
  public partial class CoreAudioController<T1, T2> :
    GenericController<CoreAudioRepository<Device>, Device>,
    ICoreAudioController<CoreAudioRepository<Device>, Device> where T1 :
    CoreAudioRepository<Device> where T2 :
    Device
  {
    #region Parameters

    private CoreAudioController Controller { get; set; }

    internal override GenericRepository<Device> Repository
    {
      get
      {
        return (CoreAudioRepository<Device>)base.Repository;
      }
      set
      {
        base.Repository = value;
        OnPropertyChanged(nameof(Repository));
      }
    }

    #endregion

    #region Logic

    /// <summary>
    /// Get the device type.
    /// </summary>
    /// <param name="isInput">True/false is an input</param>
    /// <param name="isOutput">True/false is an output</param>
    /// <returns>The device type.</returns>
    private static DeviceType GetDeviceType
    (
      bool isInput,
      bool isOutput
    )
    {
      if (isInput == isOutput)
      {
        return DeviceType.All;
      }

      if (isInput)
      {
        return DeviceType.Capture;
      }

      return DeviceType.Playback;
    }

    /// <summary>
    /// Convert an ID from a <typeparamref name="string"/> to a 
    /// <typeparamref name="GUID"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The GUID</returns>
    private static Guid ToGuid(string id)
    {
      if (string.IsNullOrWhiteSpace(id))
      {
        id = string.Empty;
      }

      return new Guid(id);
    }

    /// <summary>
    /// Constructor
    /// </summary>
    public CoreAudioController()
    {
      Controller = new CoreAudioController();
      var result = UpdateAllAsync();
    }

    #endregion
  }
}