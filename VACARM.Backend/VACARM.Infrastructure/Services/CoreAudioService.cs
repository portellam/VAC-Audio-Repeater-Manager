using AudioSwitcher.AudioApi;
using AudioSwitcher.AudioApi.CoreAudio;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using VACARM.Infrastructure.Functions;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Services
{
  /// <summary>
  /// A service for the <typeparamref name="CoreAudioRepository"/>.
  /// </summary>
  public partial class CoreAudioService<TRepository, TDevice> :
    Service<CoreAudioRepository<TDevice>, TDevice>,
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
    public CoreAudioService() : 
      base()
    {
      Controller = new CoreAudioController();

      this.Repository = new CoreAudioRepository<TDevice>
        (new ObservableCollection<TDevice>()) as Repository<TDevice>;

      var result = this.UpdateAllAsync();
    }

    public bool IsDefault(string id)
    {
      Device? device = this.Repository
        .Get(id);

      if (device == null)
      {
        return false;
      }

      return device.IsDefaultDevice;
    }

    public bool IsDefaultCommunications(string id)
    {
      Device? device = this.Repository
        .Get(id);

      if (device == null)
      {
        return false;
      }

      return device.IsDefaultCommunicationsDevice;
    }

    public bool IsMuted(string id)
    {
      Device? device = this.Repository
        .Get(id);

      if (device == null)
      {
        return false;
      }

      return device.IsMuted;
    }

    public double GetVolume(string id)
    {
      TDevice? device = this.Repository
        .Get(id);

      if (device == null)
      {
        return double.NaN;
      }

      return device.Volume;
    }

    public TDevice? GetDefault()
    {
      var func = CoreAudioDeviceFunctions<TDevice>.IsDefault;
      return this.Repository.Get(func);
    }

    public TDevice? GetDefaultCommunications()
    {
      var func = CoreAudioDeviceFunctions<TDevice>.IsDefaultCommunications;
      return this.Repository.Get(func);
    }

    public IEnumerable<TDevice> GetAllMuted()
    {
      var func = CoreAudioDeviceFunctions<TDevice>.IsMuted;
      return this.Repository.GetRange(func);
    }

    public IEnumerable<TDevice> GetAllUnmuted()
    {
      var func = CoreAudioDeviceFunctions<TDevice>.IsUnmuted;
      return this.Repository.GetRange(func);
    }

    #endregion
  }
}