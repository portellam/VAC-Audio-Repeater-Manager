using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using NAudio.CoreAudioApi;
using VACARM.Infrastructure.Functions;

namespace VACARM.Infrastructure.Repositories
{
  /// <summary>
  /// An up-to-date repository of all system audio devices.
  /// </summary>
  public class MMDeviceRepository<TMMDevice> :
    ReadonlyRepository<TMMDevice>,
    IMMDeviceRepository<TMMDevice> where TMMDevice :
    MMDevice
  {
    #region Parameters

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
      this.Enumerable = new ObservableCollection<TMMDevice>();
      this.Enumerator = new MMDeviceEnumerator();
    }

    public TMMDevice? Get(string id)
    {
      var func = MMDeviceFunctions<TMMDevice>.ContainsId(id);
      return this.Get(func);
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

    public IEnumerable<TMMDevice> GetRange(IEnumerable<string> idEnumerable)
    {
      var func = MMDeviceFunctions<TMMDevice>
        .ContainsIdEnumerable(idEnumerable);
      
      return this.GetRange(func);
    }

    public void UpdateAll()
    {
      this.UpdateAllDefaults();

      if (this.Enumerator == null)
      {
        this.Enumerator = new MMDeviceEnumerator();
        return;
      }

      MMDeviceCollection collection = this.Enumerator
        .EnumerateAudioEndPoints
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

      this.DefaultDictionary
        .Clear();

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
        this.DefaultDictionary
          .TryAdd
          (
            Role.Multimedia,
            (TMMDevice?)this.getDefaultCommunications(dataFlow)
          );

        this.DefaultDictionary
          .TryAdd
          (
            Role.Multimedia,
            (TMMDevice?)this.getDefaultConsole(dataFlow)
          );

        this.DefaultDictionary
          .TryAdd
          (
            Role.Multimedia,
            (TMMDevice?)this.getDefaultMultimedia(dataFlow)
          );
      }
    }

    #endregion
  }
}