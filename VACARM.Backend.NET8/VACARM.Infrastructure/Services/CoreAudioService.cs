using AudioSwitcher.AudioApi;
using AudioSwitcher.AudioApi.CoreAudio;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using VACARM.Infrastructure.Functions;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Services
{
  /// <summary>
  /// The service to update system audio device(s).
  /// Extended functionality over <typeparamref name="MMDeviceService"/>.
  /// </summary>
  public partial class CoreAudioService
    <
      TRepository,
      TDevice
    > :
    ReadonlyService
    <
      ReadonlyRepository<TDevice>,
      TDevice
    >,
    IDisposable,
    ICoreAudioService
    <
      ReadonlyRepository<TDevice>,
      TDevice
    >
    where TRepository :
    ReadonlyRepository<TDevice>
    where TDevice :
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
        this.OnPropertyChanged(nameof(Controller));
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

      this.Repository = new ReadonlyRepository<TDevice>
        (new ObservableCollection<TDevice>()) as ReadonlyRepository<TDevice>;

      var result = this.UpdateServiceAsync();
    }

    protected override void Dispose(bool isDisposed)
    {
      if (this.HasDisposed)
      {
        return;
      }

      if (isDisposed)
      {
        this.Controller
          .Dispose();

        this.Repository
          .Dispose();
      }

      this.HasDisposed = true;
    }

    public bool IsAbsent(string id)
    {
      var item = this.Get(id);
      return DeviceFunctions<TDevice>.IsAbsent(item);
    }

    public bool IsCapture(string id)
    {
      var item = this.Get(id);
      return DeviceFunctions<TDevice>.IsCapture(item);
    }

    public bool IsDefault(string id)
    {
      var item = this.Get(id);
      return DeviceFunctions<TDevice>.IsDefault(item);
    }

    public bool IsDefaultCommunications(string id)
    {
      var item = this.Get(id);
      return DeviceFunctions<TDevice>.IsDefaultCommunications(item);
    }

    public bool IsDisabled(string id)
    {
      var item = this.Get(id);
      return DeviceFunctions<TDevice>.IsDisabled(item);
    }

    public bool IsDuplex(string id)
    {
      var item = this.Get(id);
      return DeviceFunctions<TDevice>.IsDuplex(item);
    }

    public bool IsEnabled(string id)
    {
      var item = this.Get(id);
      return DeviceFunctions<TDevice>.IsEnabled(item);
    }

    public bool IsMuted(string id)
    {
      var item = this.Get(id);
      return DeviceFunctions<TDevice>.IsMuted(item);
    }

    public bool IsPlayback(string id)
    {
      var item = this.Get(id);
      return DeviceFunctions<TDevice>.IsPlayback(item);
    }

    public bool IsPresent(string id)
    {
      var item = this.Get(id);
      return DeviceFunctions<TDevice>.IsPresent(item);
    }

    public bool IsUnmuted(string id)
    {
      var item = this.Get(id);
      return DeviceFunctions<TDevice>.IsUnmuted(item);
    }

    public double GetVolume(string id)
    {
      var item = this.Get(id);

      if (item == null)
      {
        return double.NaN;
      }

      return item.Volume;
    }

    public TDevice? Get(string id)
    {
      var func = DeviceFunctions<TDevice>.ContainsId(id);

      return this.Repository
        .Get(func);
    }

    public TDevice? GetDefault()
    {
      var func = DeviceFunctions<TDevice>.IsDefault;

      return this.Repository
        .Get(func);
    }

    public TDevice? GetDefaultCommunications()
    {
      var func = DeviceFunctions<TDevice>.IsDefaultCommunications;

      return this.Repository
        .Get(func);
    }

    public IEnumerable<TDevice> GetAllAbsent()
    {
      var func = DeviceFunctions<TDevice>.IsAbsent;

      return this.Repository
        .GetRange(func);
    }

    public IEnumerable<TDevice> GetAllCapture()
    {
      var func = DeviceFunctions<TDevice>.IsCapture;

      return this.Repository
        .GetRange(func);
    }

    public IEnumerable<TDevice> GetAllDisabled()
    {
      var func = DeviceFunctions<TDevice>.IsDisabled;

      return this.Repository
        .GetRange(func);
    }

    public IEnumerable<TDevice> GetAllEnabled()
    {
      var func = DeviceFunctions<TDevice>.IsEnabled;

      return this.Repository
        .GetRange(func);
    }

    public IEnumerable<TDevice> GetAllMuted()
    {
      var func = DeviceFunctions<TDevice>.IsMuted;

      return this.Repository
        .GetRange(func);
    }

    public IEnumerable<TDevice> GetAllPlayback()
    {
      var func = DeviceFunctions<TDevice>.IsPlayback;

      return this.Repository
        .GetRange(func);
    }

    public IEnumerable<TDevice> GetAllPresent()
    {
      var func = DeviceFunctions<TDevice>.IsPresent;

      return this.Repository
        .GetRange(func);
    }

    public IEnumerable<TDevice> GetAllUnmuted()
    {
      var func = DeviceFunctions<TDevice>.IsUnmuted;

      return this.Repository
        .GetRange(func);
    }

    public IEnumerable<TDevice> GetRange(IEnumerable<string> idEnumerable)
    {
      var func = DeviceFunctions<TDevice>
        .ContainsIdEnumerable(idEnumerable);

      return this.Repository
        .GetRange(func);
    }

    #endregion
  }
}