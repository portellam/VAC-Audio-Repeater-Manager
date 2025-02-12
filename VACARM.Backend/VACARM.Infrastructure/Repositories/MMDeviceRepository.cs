using System.Diagnostics.CodeAnalysis;
using NAudio.CoreAudioApi;

namespace VACARM.Infrastructure.Repositories
{
  /// <summary>
  /// An up-to-date repository of all system audio devices.
  /// </summary>
  public class MMDeviceRepository<TMMDevice> :
    GenericRepository<TMMDevice>,
    IMMDeviceRepository<TMMDevice> where TMMDevice :
    MMDevice
  {
    #region Parameters

    /// <summary>
    /// The <typeparamref name="Enumerable"/> of all
    /// <typeparamref name="TMMDevice"/> item(s).
    /// </summary>
    internal override IEnumerable<TMMDevice> Enumerable
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
        return defaultDictionary;
      }
      set
      {
        defaultDictionary = value;
        OnPropertyChanged(nameof(DefaultDictionary));
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
        return enumerator;
      }
      set
      {
        enumerator = value;
        UpdateAll();
        OnPropertyChanged(nameof(Enumerator));
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
      if (Enumerator == null)
      {
        return null;
      }

      return Enumerator.GetDefaultAudioEndpoint
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
      return getDefault
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
      return getDefault
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
      return getDefault
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
      Enumerator = new MMDeviceEnumerator();
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
      return DefaultDictionary
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
      return GetDefault
        (
          dataFlow,
          Role.Communications
        );
    }

    public TMMDevice? GetDefaultConsole(DataFlow dataFlow)
    {
      return GetDefault
        (
          dataFlow,
          Role.Console
        );
    }

    public TMMDevice? GetDefaultMultimedia(DataFlow dataFlow)
    {
      return GetDefault
        (
          dataFlow,
          Role.Multimedia
        );
    }

    public IEnumerable<TMMDevice> GetAllAbsent()
    {
      Func<TMMDevice, bool> func = (TMMDevice x) => x.State != DeviceStatePresent;
      return GetRange(func);
    }

    public IEnumerable<TMMDevice> GetAllCapture()
    {
      Func<TMMDevice, bool> func = (TMMDevice x) => x.DataFlow == DataFlow.Capture;
      return GetRange(func);
    }

    public IEnumerable<TMMDevice> GetAllDisabled()
    {
      Func<TMMDevice, bool> func = (TMMDevice x) => x.State == DeviceState.Disabled;
      return GetRange(func);
    }

    public IEnumerable<TMMDevice> GetAllDuplex()
    {
      Func<TMMDevice, bool> func = (TMMDevice x) => x.DataFlow == DataFlow.All;
      return GetRange(func);
    }

    public IEnumerable<TMMDevice> GetAllEnabled()
    {
      Func<TMMDevice, bool> func = (TMMDevice x) => x.State != DeviceState.Disabled;
      return GetRange(func);
    }

    public IEnumerable<TMMDevice> GetAllPresent()
    {
      Func<TMMDevice, bool> func = (TMMDevice x) => x.State == DeviceStatePresent;
      return GetRange(func);
    }

    public IEnumerable<TMMDevice> GetAllRender()
    {
      Func<TMMDevice, bool> func = (TMMDevice x) => x.DataFlow == DataFlow.Render;
      return GetRange(func);
    }

    public IEnumerable<TMMDevice> GetRange(IEnumerable<string> idEnumerable)
    {
      Func<TMMDevice, bool> func = (TMMDevice x) => idEnumerable.Contains(x.ID);
      return GetRange(func);
    }

    public void UpdateAll()
    {
      UpdateAllDefaults();

      if (Enumerator == null)
      {
        Enumerable = Array.Empty<TMMDevice>();
        return;
      }

      MMDeviceCollection collection = Enumerator.EnumerateAudioEndPoints
        (
          DataFlow.All,
          DeviceState.All
        );

      Enumerable = (IEnumerable<TMMDevice>)collection.AsEnumerable();
    }

    public void UpdateAllDefaults()
    {
      if (Enumerator == null)
      {
        return;
      }

      DefaultDictionary.Clear();
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
        DefaultDictionary.TryAdd
        (
          Role.Multimedia,
          (TMMDevice?)getDefaultCommunications(dataFlow)
        );

        DefaultDictionary.TryAdd
        (
          Role.Multimedia,
          (TMMDevice?)getDefaultConsole(dataFlow)
        );

        DefaultDictionary.TryAdd
        (
          Role.Multimedia,
          (TMMDevice?)getDefaultMultimedia(dataFlow)
        );
      }
    }

    #endregion
  }
}