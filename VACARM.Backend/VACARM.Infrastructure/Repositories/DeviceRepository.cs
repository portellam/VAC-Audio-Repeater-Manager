using System.Diagnostics.CodeAnalysis;
using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Repositories
{
  /// <summary>
  /// A snapshot repository of system audio devices.
  /// </summary>
  public class DeviceRepository<T> :
    BaseRepository<DeviceModel>,
    IDeviceRepository<DeviceModel>
  {
    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    [ExcludeFromCodeCoverage]
    public DeviceRepository()
    {
      List = new List<DeviceModel>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="enumerable">The enumerable of item(s)</param>
    [ExcludeFromCodeCoverage]
    public DeviceRepository(IEnumerable<DeviceModel> enumerable)
    {
      List = enumerable.ToList();
    }

    public DeviceModel? GetDefaultCommunications()
    {
      return GetAllCommunications()
        .FirstOrDefault(x => x.IsDefault);
    }

    public DeviceModel? GetDefaultConsole()
    {
      return GetAllConsole()
        .FirstOrDefault(x => x.IsDefault);
    }

    public DeviceModel? GetDefaultMultimedia()
    {
      return GetAllMultimedia()
        .FirstOrDefault(x => x.IsDefault);
    }

    public IEnumerable<DeviceModel> GetAllAbsent()
    {
      return GetAll()
        .Where(x => !x.IsPresent);
    }

    public IEnumerable<DeviceModel> GetAllAlphabeticalOrder()
    {
      return GetAll()
        .OrderBy(x => x.Name);
    }

    public IEnumerable<DeviceModel> GetAllCapture()
    {
      return GetAll()
        .Where(x => x.IsCapture);
    }

    public IEnumerable<DeviceModel> GetAllCommunications()
    {
      return GetAll()
        .Where(x => x.Role == "Communications");
    }

    public IEnumerable<DeviceModel> GetAllConsole()
    {
      return GetAll()
        .Where(x => x.Role == "Console");
    }

    public IEnumerable<DeviceModel> GetAllDefault()
    {
      return GetAll()
        .Where(x => x.IsDefault);
    }

    public IEnumerable<DeviceModel> GetAllDisabled()
    {
      return GetAll()
        .Where(x => !x.IsEnabled);
    }

    public IEnumerable<DeviceModel> GetAllDuplex()
    {
      return GetAll()
        .Where(x => x.IsDuplex);
    }

    public IEnumerable<DeviceModel> GetAllEnabled()
    {
      return GetAll()
        .Where(x => x.IsEnabled);
    }

    public IEnumerable<DeviceModel> GetAllMultimedia()
    {
      return GetAll()
        .Where(x => x.Role == "Multimedia");
    }

    public IEnumerable<DeviceModel> GetAllMuted()
    {
      return GetAll()
        .Where(x => x.IsMuted);
    }

    public IEnumerable<DeviceModel> GetAllNotMuted()
    {
      return GetAll()
        .Where(x => !x.IsMuted);
    }

    public IEnumerable<DeviceModel> GetAllRender()
    {
      return GetAll()
        .Where(x => x.IsRender);
    }

    public IEnumerable<DeviceModel> GetAllReverseAlphabeticalOrder()
    {
      return GetAll()
        .OrderByDescending(x => x.Name);
    }

    public IEnumerable<DeviceModel> GetAllPresent()
    {
      return GetAll()
        .Where(x => x.IsPresent);
    }

    #endregion
  }
}