using System.Diagnostics.CodeAnalysis;
using NAudio.CoreAudioApi;

namespace VACARM.Infrastructure.Repositories
{
  /// <summary>
  /// An up-to-date repository of all system audio devices.
  /// </summary>
  public class MMDeviceRepository<TMMDevice> :
    Repository<TMMDevice>,
    IMMDeviceRepository<TMMDevice> where TMMDevice :
    MMDevice
  {
    #region Parameters

    /// <summary>
    /// The <typeparamref name="Enumerable"/> of all
    /// <typeparamref name="TMMDevice"/> item(s).
    /// </summary>
    protected override IEnumerable<TMMDevice> Enumerable
    {
      get
      {
        return base.Enumerable;
      }
      set
      {
        base.Enumerable = value;
        OnPropertyChanged(nameof(Enumerable));
      }
    }

    private DeviceState DeviceStatePresent
    {
      get
      {
        return DeviceState.Active
          | DeviceState.Disabled
          | DeviceState.Unplugged;
      }
    }

    private Dictionary<Role, TMMDevice> defaultDictionary { get; set; }

    private Dictionary<Role, TMMDevice> DefaultDictionary
    {
      get
      {
        return this.defaultDictionary;
      }
      set
      {
        this.defaultDictionary = value;
        base.OnPropertyChanged(nameof(DefaultDictionary));
      }
    }

    private IEnumerable<TMMDevice> enumerable { get; set; }

    private MMDeviceEnumerator enumerator { get; set; } = new MMDeviceEnumerator();

    /// <summary>
    /// The enumerator of audio devices.
    /// </summary>
    private MMDeviceEnumerator Enumerator
    {
      get
      {
        return this.enumerator;
      }
      set
      {
        this.enumerator = value;
        this.UpdateAll();
        base.OnPropertyChanged(nameof(Enumerator));
      }
    }

    #endregion

    #region Logic

    /// <summary>
    /// Get the default <typeparamref name="TMMDevice"/> item.
    /// </summary>
    /// <param name="dataFlow">The data flow</param>
    /// <param name="role">The role</param>
    /// <returns>The item.</returns>
    private MMDevice? getDefault
    (
      DataFlow dataFlow,
      Role role
    )
    {
      if (this.Enumerator == null)
      {
        return null;
      }

      return this.Enumerator.GetDefaultAudioEndpoint
        (
          dataFlow,
          role
        );
    }

    /// <summary>
    /// Get the default communications <typeparamref name="MMDevice"/> item.
    /// </summary>
    /// <param name="dataFlow">The data flow</param>
    /// <returns>The item.</returns>
    private MMDevice? getDefaultCommunications(DataFlow dataFlow)
    {
      return this.getDefault
        (
          dataFlow,
          Role.Communications
        );
    }

    /// <summary>
    /// Get the default console <typeparamref name="MMDevice"/> item.
    /// </summary>
    /// <param name="dataFlow">The data flow</param>
    /// <returns>The item.</returns>
    private MMDevice? getDefaultConsole(DataFlow dataFlow)
    {
      return this.getDefault
        (
          dataFlow,
          Role.Console
        );
    }

    /// <summary>
    /// Get the default multimedia <typeparamref name="MMDevice"/> item.
    /// </summary>
    /// <param name="dataFlow">The data flow</param>
    /// <returns>The item.</returns>
    private MMDevice? getDefaultMultimedia(DataFlow dataFlow)
    {
      return this.getDefault
        (
          dataFlow,
          Role.Multimedia
        );
    }

    /// <summary>
    /// Constructor
    /// </summary>
    [ExcludeFromCodeCoverage]
    public MMDeviceRepository()
    {
      this.Enumerator = new MMDeviceEnumerator();
    }

    public TMMDevice? Get(string id)
    {
      Func<TMMDevice, bool> func = (TMMDevice x) => x.ID == id;
      return Get(func);
    }

    public TMMDevice? GetDefault
    (
      DataFlow dataFlow,
      Role role
    )
    {
      return this.DefaultDictionary
        .FirstOrDefault
        (
          x =>
          {
            return x.Key == role
              && x.Value.DataFlow == dataFlow;
          }
        ).Value;
    }

    public TMMDevice? GetDefaultCommunications(DataFlow dataFlow)
    {
      return this.GetDefault
        (
          dataFlow,
          Role.Communications
        );
    }

    public TMMDevice? GetDefaultConsole(DataFlow dataFlow)
    {
      return this.GetDefault
        (
          dataFlow,
          Role.Console
        );
    }

    public TMMDevice? GetDefaultMultimedia(DataFlow dataFlow)
    {
      return this.GetDefault
        (
          dataFlow,
          Role.Multimedia
        );
    }

    public IEnumerable<TMMDevice> GetAllAbsent()
    {
      Func<TMMDevice, bool> func = (TMMDevice x) => x.State != DeviceStatePresent;
      return this.GetRange(func);
    }

    public IEnumerable<TMMDevice> GetAllCapture()
    {
      Func<TMMDevice, bool> func = (TMMDevice x) => x.DataFlow == DataFlow.Capture;
      return this.GetRange(func);
    }

    public IEnumerable<TMMDevice> GetAllDisabled()
    {
      Func<TMMDevice, bool> func = (TMMDevice x) => x.State == DeviceState.Disabled;
      return this.GetRange(func);
    }

    public IEnumerable<TMMDevice> GetAllDuplex()
    {
      Func<TMMDevice, bool> func = (TMMDevice x) => x.DataFlow == DataFlow.All;
      return this.GetRange(func);
    }

    public IEnumerable<TMMDevice> GetAllEnabled()
    {
      Func<TMMDevice, bool> func = (TMMDevice x) => x.State != DeviceState.Disabled;
      return this.GetRange(func);
    }

    public IEnumerable<TMMDevice> GetAllPresent()
    {
      Func<TMMDevice, bool> func = (TMMDevice x) => 
        x.State == this.DeviceStatePresent;

      return this.GetRange(func);
    }

    public IEnumerable<TMMDevice> GetAllRender()
    {
      Func<TMMDevice, bool> func = (TMMDevice x) => x.DataFlow == DataFlow.Render;
      return this.GetRange(func);
    }

    public IEnumerable<TMMDevice> GetRange(IEnumerable<string> idEnumerable)
    {
      Func<TMMDevice, bool> func = (TMMDevice x) => idEnumerable.Contains(x.ID);
      return this.GetRange(func);
    }

    public void UpdateAll()
    {
      this.UpdateAllDefaults();

      if (this.Enumerator == null)
      {
        this.Enumerable = Array.Empty<TMMDevice>();
        return;
      }

      MMDeviceCollection collection = this.Enumerator.EnumerateAudioEndPoints
        (
          DataFlow.All,
          DeviceState.All
        );

      this.Enumerable = (IEnumerable<TMMDevice>)collection.AsEnumerable();
    }

    public void UpdateAllDefaults()
    {
      if (this.Enumerator == null)
      {
        return;
      }

      this.DefaultDictionary.Clear();
      Array array = Enum.GetValues(typeof(DataFlow));

      if (array == null)
      {
        return;
      }

      if (array.Length == 0)
      {
        return;
      }

      foreach (DataFlow dataFlow in array)
      {
        this.DefaultDictionary.TryAdd
        (
          Role.Multimedia,
          (TMMDevice?)this.getDefaultCommunications(dataFlow)
        );

        this.DefaultDictionary.TryAdd
        (
          Role.Multimedia,
          (TMMDevice?)this.getDefaultConsole(dataFlow)
        );

        this.DefaultDictionary.TryAdd
        (
          Role.Multimedia,
          (TMMDevice?)this.getDefaultMultimedia(dataFlow)
        );
      }
    }

    #endregion
  }
}