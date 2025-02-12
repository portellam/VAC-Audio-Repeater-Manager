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
      Func<TDeviceModel, bool> func = (TDeviceModel x) => !x.IsPresent;
      return base.GetRange(func);
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
      Func<TDeviceModel, bool> func = (TDeviceModel x) => x.IsCapture;
      return base.GetRange(func);
    }

    public IEnumerable<TDeviceModel> GetAllCommunications()
    {
      Func<TDeviceModel, bool> func = (TDeviceModel x) =>
        x.Role == "Communications";

      return base.GetRange(func);
    }

    public IEnumerable<TDeviceModel> GetAllConsole()
    {
      Func<TDeviceModel, bool> func = (TDeviceModel x) =>
        x.Role == "Console";

      return base.GetRange(func);
    }

    public IEnumerable<TDeviceModel> GetAllDefault()
    {
      Func<TDeviceModel, bool> func = (TDeviceModel x) => x.IsDefault;
      return base.GetRange(func);
    }

    public IEnumerable<TDeviceModel> GetAllDisabled()
    {
      Func<TDeviceModel, bool> func = (TDeviceModel x) => !x.IsEnabled;
      return base.GetRange(func);
    }

    public IEnumerable<TDeviceModel> GetAllDuplex()
    {
      Func<TDeviceModel, bool> func = (TDeviceModel x) => x.IsDuplex;
      return base.GetRange(func);
    }

    public IEnumerable<TDeviceModel> GetAllEnabled()
    {
      Func<TDeviceModel, bool> func = (TDeviceModel x) => x.IsEnabled;
      return base.GetRange(func);
    }

    public IEnumerable<TDeviceModel> GetAllMultimedia()
    {
      Func<TDeviceModel, bool> func = (TDeviceModel x) =>
        x.Role == "Multimedia";

      return base.GetRange(func);
    }

    public IEnumerable<TDeviceModel> GetAllMuted()
    {
      Func<TDeviceModel, bool> func = (TDeviceModel x) => x.IsMuted;
      return base.GetRange(func);
    }

    public IEnumerable<TDeviceModel> GetAllNotMuted()
    {
      Func<TDeviceModel, bool> func = (TDeviceModel x) => !x.IsMuted;
      return base.GetRange(func);
    }

    public IEnumerable<TDeviceModel> GetAllPresent()
    {
      Func<TDeviceModel, bool> func = (TDeviceModel x) => x.IsPresent;
      return base.GetRange(func);
    }

    public IEnumerable<TDeviceModel> GetAllRender()
    {
      Func<TDeviceModel, bool> func = (TDeviceModel x) => x.IsRender;
      return base.GetRange(func);
    }

    #endregion
  }
}