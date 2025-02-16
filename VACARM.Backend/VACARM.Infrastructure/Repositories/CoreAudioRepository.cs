using AudioSwitcher.AudioApi;
using System.Collections.ObjectModel;
using VACARM.Infrastructure.Extensions;

namespace VACARM.Infrastructure.Repositories
{
  /// <summary>
  /// A up-to-date repository of all system audio devices.
  /// Extended functionality over <typeparamref name="MMDeviceRepository".
  /// </summary>
  public class CoreAudioRepository<TDevice> :
    ReadonlyRepository<TDevice>,
    ICoreAudioRepository<TDevice> where TDevice :
    Device
  {
    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="enumerable">The enumerable of item(s)</param>
    public CoreAudioRepository(ObservableCollection<TDevice> enumerable) :
      base(enumerable)
    {
      this.Enumerable = enumerable;
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

    public TDevice? Get(string id)
    {
      if (this.IsNullOrEmpty(Enumerable))
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
      return this.Get(func);
    }

    public IEnumerable<TDevice> GetRange(IEnumerable<string> idEnumerable)
    {
      if (this.IsNullOrEmpty(this.Enumerable))
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