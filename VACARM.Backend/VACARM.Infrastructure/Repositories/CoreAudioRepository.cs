using AudioSwitcher.AudioApi;
using VACARM.Infrastructure.Extensions;

namespace VACARM.Infrastructure.Repositories
{
  /// <summary>
  /// A up-to-date repository of all system audio devices.
  /// Improved functionality over <typeparamref name="MMDeviceRepository".
  /// </summary>
  public class CoreAudioRepository<T> :
    GenericRepository<Device>,
    ICoreAudioRepository<Device> where T :
    Device
  {
    #region Parameters

    internal override IEnumerable<Device> Enumerable
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
      Enumerable = Array.Empty<Device>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="enumerable">The enumerable of item(s)</param>
    public CoreAudioRepository(IEnumerable<Device> enumerable)
    {
      Enumerable = enumerable;
    }

    /// <summary>
    /// Get the <typeparamref name="Guid"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The ID</returns>
    private Guid GetGuid(string id)
    {
      Guid.TryParse(id, out Guid guid);
      return guid;
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

    public Device? Get(string id)
    {
      if (IsNullOrEmpty(Enumerable))
      {
        return null;
      }

      Guid guid;

      try
      {
        guid = GetGuid(id);
      }
      catch
      {
        return null;
      }

      Func<Device, bool> func = (Device x) => x.Id == guid;
      return Get(func);
    }

    public Device? GetDefaultCommunications()
    {
      Func<Device, bool> func = (Device x) => x.IsDefaultCommunicationsDevice;
      return Get(func);
    }

    public Device? GetDefault()
    {
      Func<Device, bool> func = (Device x) => x.IsDefaultDevice;
      return Get(func);
    }

    public IEnumerable<Device> GetAllMuted()
    {
      Func<Device, bool> func = (Device x) => !x.IsMuted;
      return GetRange(func);
    }

    public IEnumerable<Device> GetAllNotMuted()
    {
      Func<Device, bool> func = (Device x) => !x.IsMuted;
      return GetRange(func);
    }

    public IEnumerable<Device> GetRange(IEnumerable<string> idEnumerable)
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
        Device? device = Get(id);

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