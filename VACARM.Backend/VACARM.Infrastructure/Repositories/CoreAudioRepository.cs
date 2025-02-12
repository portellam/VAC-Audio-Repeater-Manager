using AudioSwitcher.AudioApi;
using VACARM.Infrastructure.Extensions;

namespace VACARM.Infrastructure.Repositories
{
  /// <summary>
  /// A up-to-date repository of all system audio devices.
  /// Extended functionality over <typeparamref name="MMDeviceRepository".
  /// </summary>
  public class CoreAudioRepository<TDevice> :
    GenericRepository<TDevice>,
    ICoreAudioRepository<TDevice> where TDevice :
    Device
  {
    #region Parameters

    /// <summary>
    /// The <typeparamref name="Enumerable"/> of all
    /// <typeparamref name="TDevice"/> item(s).
    /// </summary>
    internal override IEnumerable<TDevice> Enumerable
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

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    public CoreAudioRepository()
    {
      Enumerable = Array.Empty<TDevice>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="enumerable">The enumerable of item(s)</param>
    public CoreAudioRepository(IEnumerable<TDevice> enumerable)
    {
      Enumerable = enumerable;
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

    public bool IsDefault(string id)
    {
      Device? device = Get(id);

      if (device == null)
      {
        return false;
      }

      return device.IsDefaultDevice;
    }

    public bool IsDefaultCommunications(string id)
    {
      Device? device = Get(id);

      if (device == null)
      {
        return false;
      }

      return device.IsDefaultCommunicationsDevice;
    }

    public bool IsMuted(string id)
    {
      Device? device = Get(id);

      if (device == null)
      {
        return false;
      }

      return device.IsMuted;
    }

    public double GetVolume(string id)
    {
      Device? device = Get(id);

      if (device == null)
      {
        return double.NaN;
      }

      return device.Volume;
    }

    public TDevice? Get(string id)
    {
      if (IsNullOrEmpty(Enumerable))
      {
        return null;
      }

      Guid guid;

      try
      {
        guid = ToGuid(id);
      }
      catch
      {
        return null;
      }

      Func<Device, bool> func = (Device x) => x.Id == guid;
      return Get(func);
    }

    public TDevice? GetDefaultCommunications()
    {
      Func<Device, bool> func = (Device x) => x.IsDefaultCommunicationsDevice;
      return Get(func);
    }

    public TDevice? GetDefault()
    {
      Func<Device, bool> func = (Device x) => x.IsDefaultDevice;
      return Get(func);
    }

    public IEnumerable<TDevice> GetAllMuted()
    {
      Func<Device, bool> func = (Device x) => !x.IsMuted;
      return GetRange(func);
    }

    public IEnumerable<TDevice> GetAllNotMuted()
    {
      Func<Device, bool> func = (Device x) => !x.IsMuted;
      return GetRange(func);
    }

    public IEnumerable<TDevice> GetRange(IEnumerable<string> idEnumerable)
    {
      if (IsNullOrEmpty(Enumerable))
      {
        yield break;
      }

      if (IEnumerableExtension<string>.IsNullOrEmpty(idEnumerable))
      {
        yield break;
      }

      foreach (var id in idEnumerable)
      {
        TDevice? device = Get(id);

        if (device == null)
        {
          continue;
        }

        yield return device;
      }
    }

    #endregion
  }
}