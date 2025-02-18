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

      this.ReadonlyRepository = new ReadonlyRepository<TDevice>
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

        this.ReadonlyRepository
          .Dispose();
      }

      this.HasDisposed = true;
    }

    public bool IsAbsent(string id)
    {
      var item = this.Get(id);
      return CoreAudioDeviceFunctions<TDevice>.IsAbsent(item);
    }

    public bool IsCapture(string id)
    {
      var item = this.Get(id);
      return CoreAudioDeviceFunctions<TDevice>.IsCapture(item);
    }

    public bool IsDefault(string id)
    {
      var item = this.Get(id);
      return CoreAudioDeviceFunctions<TDevice>.IsDefault(item);
    }

    public bool IsDefaultCommunications(string id)
    {
      var item = this.Get(id);
      return CoreAudioDeviceFunctions<TDevice>.IsDefaultCommunications(item);
    }

    public bool IsDisabled(string id)
    {
      var item = this.Get(id);
      return CoreAudioDeviceFunctions<TDevice>.IsDisabled(item);
    }

    public bool IsDuplex(string id)
    {
      var item = this.Get(id);
      return CoreAudioDeviceFunctions<TDevice>.IsDuplex(item);
    }

    public bool IsEnabled(string id)
    {
      var item = this.Get(id);
      return CoreAudioDeviceFunctions<TDevice>.IsEnabled(item);
    }

    public bool IsMuted(string id)
    {
      var item = this.Get(id);
      return CoreAudioDeviceFunctions<TDevice>.IsMuted(item);
    }

    public bool IsPlayback(string id)
    {
      var item = this.Get(id);
      return CoreAudioDeviceFunctions<TDevice>.IsPlayback(item);
    }

    public bool IsPresent(string id)
    {
      var item = this.Get(id);
      return CoreAudioDeviceFunctions<TDevice>.IsPresent(item);
    }

    public bool IsUnmuted(string id)
    {
      var item = this.Get(id);
      return CoreAudioDeviceFunctions<TDevice>.IsUnmuted(item);
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
      var func = CoreAudioDeviceFunctions<TDevice>.ContainsId(id);

      return this.ReadonlyRepository
        .Get(func);
    }

    public TDevice? GetDefault()
    {
      var func = CoreAudioDeviceFunctions<TDevice>.IsDefault;

      return this.ReadonlyRepository
        .Get(func);
    }

    public TDevice? GetDefaultCommunications()
    {
      var func = CoreAudioDeviceFunctions<TDevice>.IsDefaultCommunications;

      return this.ReadonlyRepository
        .Get(func);
    }

    public IEnumerable<TDevice> GetAllAbsent()
    {
      var func = CoreAudioDeviceFunctions<TDevice>.IsAbsent;

      return this.ReadonlyRepository
        .GetRange(func);
    }

    public IEnumerable<TDevice> GetAllCapture()
    {
      var func = CoreAudioDeviceFunctions<TDevice>.IsCapture;

      return this.ReadonlyRepository
        .GetRange(func);
    }

    public IEnumerable<TDevice> GetAllDisabled()
    {
      var func = CoreAudioDeviceFunctions<TDevice>.IsDisabled;

      return this.ReadonlyRepository
        .GetRange(func);
    }

    public IEnumerable<TDevice> GetAllEnabled()
    {
      var func = CoreAudioDeviceFunctions<TDevice>.IsEnabled;

      return this.ReadonlyRepository
        .GetRange(func);
    }

    public IEnumerable<TDevice> GetAllMuted()
    {
      var func = CoreAudioDeviceFunctions<TDevice>.IsMuted;

      return this.ReadonlyRepository
        .GetRange(func);
    }

    public IEnumerable<TDevice> GetAllPlayback()
    {
      var func = CoreAudioDeviceFunctions<TDevice>.IsPlayback;

      return this.ReadonlyRepository
        .GetRange(func);
    }

    public IEnumerable<TDevice> GetAllPresent()
    {
      var func = CoreAudioDeviceFunctions<TDevice>.IsPresent;

      return this.ReadonlyRepository
        .GetRange(func);
    }

    public IEnumerable<TDevice> GetAllUnmuted()
    {
      var func = CoreAudioDeviceFunctions<TDevice>.IsUnmuted;

      return this.ReadonlyRepository
        .GetRange(func);
    }

    public IEnumerable<TDevice> GetRange(IEnumerable<string> idEnumerable)
    {
      var func = CoreAudioDeviceFunctions<TDevice>
        .ContainsIdEnumerable(idEnumerable);

      return this.ReadonlyRepository
        .GetRange(func);
    }

    #endregion
  }
}