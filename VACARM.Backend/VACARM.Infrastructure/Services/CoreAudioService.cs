using AudioSwitcher.AudioApi;
using AudioSwitcher.AudioApi.CoreAudio;
using System.Diagnostics.CodeAnalysis;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Services
{
  /// <summary>
  /// A service for the <typeparamref name="CoreAudioRepository"/>.
  /// </summary>
  public partial class CoreAudioService<TRepository, TDevice> :
    GenericService<CoreAudioRepository<TDevice>, TDevice>,
    ICoreAudioService<CoreAudioRepository<TDevice>, TDevice> where TRepository :
    CoreAudioRepository<TDevice> where TDevice :
    Device
  {
    #region Parameters

    private CoreAudioController controller { get; set; } =
      new CoreAudioController();

    private CoreAudioController Controller
    {
      get
      {
        return this.controller;
      }
      set
      {
        this.controller = value;
        base.OnPropertyChanged(nameof(Controller));
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
    [ExcludeFromCodeCoverage]
    public CoreAudioService()
    {
      base._Repository = new CoreAudioRepository<TDevice>();
      Controller = new CoreAudioController();
      var result = this.UpdateAllAsync();
    }

    #endregion
  }
}