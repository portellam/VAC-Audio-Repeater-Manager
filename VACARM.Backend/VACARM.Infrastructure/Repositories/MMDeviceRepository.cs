using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using NAudio.CoreAudioApi;

namespace VACARM.Infrastructure.Repositories
{
  /// <summary>
  /// A up-to-date repository of all system audio devices.
  /// </summary>
  public class MMDeviceRepository<T> :
    GenericRepository<T>,
    IMMDeviceRepository<T> where T :
    MMDevice
  {
    #region Parameters

    private DeviceState DeviceStatePresent
    {
      get
      {
        return DeviceState.Active
          | DeviceState.Disabled
          | DeviceState.Unplugged;
      }
    }

    private Dictionary<Role, MMDevice> defaultDictionary { get; set; }

    private Dictionary<Role, MMDevice> DefaultDictionary
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

    private IEnumerable<MMDevice> enumerable { get; set; }

    /// <summary>
    /// The enumerable of all <typeparamref name="MMDevice"/> item(s).
    /// </summary>
    public override IEnumerable<T> Enumerable
    {
      get
      {
        return (IEnumerable<T>)enumerable;
      }
      set
      {
        enumerable = value;
        OnPropertyChanged(nameof(Enumerable));
      }
    }

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

    public new event PropertyChangedEventHandler PropertyChanged;

    #endregion

    #region Logic

    /// <summary>
    /// Get the default <typeparamref name="MMDevice"/> item.
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
    /// Logs event when property has changed.
    /// </summary>
    /// <param name="propertyName">The property name</param>
    private void OnPropertyChanged(string propertyName)
    {
      PropertyChanged?.Invoke
      (
        this,
        new PropertyChangedEventArgs(propertyName)
      );

      Debug.WriteLine
      (
        string.Format
        (
          "PropertyChanged: {0}",
          propertyName
        )
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

    public MMDevice? Get(string id)
    {
      Func<MMDevice, bool> func = (MMDevice x) => x.ID == id;
      return Get(func);
    }

    public MMDevice? GetDefault
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

    public MMDevice? GetDefaultCommunications(DataFlow dataFlow)
    {
      return GetDefault
        (
          dataFlow,
          Role.Communications
        );
    }

    public MMDevice? GetDefaultConsole(DataFlow dataFlow)
    {
      return GetDefault
        (
          dataFlow,
          Role.Console
        );
    }

    public MMDevice? GetDefaultMultimedia(DataFlow dataFlow)
    {
      return GetDefault
        (
          dataFlow,
          Role.Multimedia
        );
    }

    public IEnumerable<MMDevice> GetAllAbsent()
    {
      Func<MMDevice, bool> func = (MMDevice x) => x.State != DeviceStatePresent;
      return GetRange(func);
    }

    public IEnumerable<MMDevice> GetAllCapture()
    {
      Func<MMDevice, bool> func = (MMDevice x) => x.DataFlow == DataFlow.Capture;
      return GetRange(func);
    }

    public IEnumerable<MMDevice> GetAllDisabled()
    {
      Func<MMDevice, bool> func = (MMDevice x) => x.State == DeviceState.Disabled;
      return GetRange(func);
    }

    public IEnumerable<MMDevice> GetAllDuplex()
    {
      Func<MMDevice, bool> func = (MMDevice x) => x.DataFlow == DataFlow.All;
      return GetRange(func);
    }

    public IEnumerable<MMDevice> GetAllEnabled()
    {
      Func<MMDevice, bool> func = (MMDevice x) => x.State != DeviceState.Disabled;
      return GetRange(func);
    }

    public IEnumerable<MMDevice> GetAllPresent()
    {
      Func<MMDevice, bool> func = (MMDevice x) => x.State == DeviceStatePresent;
      return GetRange(func);
    }

    public IEnumerable<MMDevice> GetAllRender()
    {
      Func<MMDevice, bool> func = (MMDevice x) => x.DataFlow == DataFlow.Render;
      return GetRange(func);
    }

    public IEnumerable<MMDevice> GetRange(IEnumerable<string> idEnumerable)
    {
      Func<MMDevice, bool> func = (MMDevice x) => idEnumerable.Contains(x.ID);
      return GetRange(func);
    }

    public void UpdateAll()
    {
      UpdateAllDefaults();

      if (Enumerator == null)
      {
        Enumerable = Array.Empty<T>();
        return;
      }

      Enumerable = (IEnumerable<T>)Enumerator.EnumerateAudioEndPoints
        (
          DataFlow.All,
          DeviceState.All
        );
    }

    public void UpdateAllDefaults()
    {
      if (Enumerator == null)
      {
        return;
      }

      DefaultDictionary.Clear();

      foreach (DataFlow dataFlow in Enum.GetValues(typeof(DataFlow)))
      {
        DefaultDictionary.TryAdd
        (
          Role.Multimedia, 
          getDefaultCommunications(dataFlow)
        );

        DefaultDictionary.TryAdd
        (
          Role.Multimedia,
          getDefaultConsole(dataFlow)
        );

        DefaultDictionary.TryAdd
        (
          Role.Multimedia,
          getDefaultMultimedia(dataFlow)          
        );
      }
    }

    #endregion
  }
}
}