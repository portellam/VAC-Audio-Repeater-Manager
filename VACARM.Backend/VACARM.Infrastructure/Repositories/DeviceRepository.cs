using System.Diagnostics.CodeAnalysis;
using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Repositories
{
  /// <summary>
  /// A snapshot repository of system audio devices.
  /// </summary>
  public class DeviceRepository<TDeviceModel> :
    BaseRepository<TDeviceModel>,
    IDeviceRepository<TDeviceModel> where TDeviceModel :
    DeviceModel
  {
    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    [ExcludeFromCodeCoverage]
    public DeviceRepository()
    {
      List = new List<TDeviceModel>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="enumerable">The enumerable of item(s)</param>
    [ExcludeFromCodeCoverage]
    public DeviceRepository(IEnumerable<TDeviceModel> enumerable)
    {
      List = enumerable.ToList();
    }

    public TDeviceModel? GetDefaultCommunications()
    {
      return GetAllCommunications().FirstOrDefault(x => x.IsDefault);
    }

    public TDeviceModel? GetDefaultConsole()
    {
      return GetAllConsole().FirstOrDefault(x => x.IsDefault);
    }

    public TDeviceModel? GetDefaultMultimedia()
    {
      return GetAllMultimedia().FirstOrDefault(x => x.IsDefault);
    }

    public IEnumerable<TDeviceModel> GetAllAbsent()
    {
      return base
        .GetAll()
        .Where(x => !x.IsPresent);
    }

    public IEnumerable<TDeviceModel> GetAllAlphabetical()
    {
      return base
        .GetAll()
        .OrderBy(x => x.Name);
    }

    public IEnumerable<TDeviceModel> GetAllAlphabeticalDescending()
    {
      return base
        .GetAll()
        .OrderByDescending(x => x.Name);
    }

    public IEnumerable<TDeviceModel> GetAllCapture()
    {
      return base
        .GetAll()
        .Where(x => x.IsCapture);
    }

    public IEnumerable<TDeviceModel> GetAllCommunications()
    {
      return base
        .GetAll()
        .Where(x => x.Role == "Communications");
    }

    public IEnumerable<TDeviceModel> GetAllConsole()
    {
      return base
        .GetAll()
        .Where(x => x.Role == "Console");
    }

    public IEnumerable<TDeviceModel> GetAllDefault()
    {
      return base
        .GetAll()
        .Where(x => x.IsDefault);
    }

    public IEnumerable<TDeviceModel> GetAllDisabled()
    {
      return base
        .GetAll()
        .Where(x => !x.IsEnabled);
    }

    public IEnumerable<TDeviceModel> GetAllDuplex()
    {
      return base
        .GetAll()
        .Where(x => x.IsDuplex);
    }

    public IEnumerable<TDeviceModel> GetAllEnabled()
    {
      return base
        .GetAll()
        .Where(x => x.IsEnabled);
    }

    public IEnumerable<TDeviceModel> GetAllMultimedia()
    {
      return base
        .GetAll()
        .Where(x => x.Role == "Multimedia");
    }

    public IEnumerable<TDeviceModel> GetAllMuted()
    {
      return base
        .GetAll()
        .Where(x => x.IsMuted);
    }

    public IEnumerable<TDeviceModel> GetAllNotMuted()
    {
      return base
        .GetAll()
        .Where(x => !x.IsMuted);
    }

    public IEnumerable<TDeviceModel> GetAllRender()
    {
      return base
        .GetAll()
        .Where(x => x.IsRender);
    }

    public IEnumerable<TDeviceModel> GetAllPresent()
    {
      return base
        .GetAll()
        .Where(x => x.IsPresent);
    }

    #endregion
  }
}