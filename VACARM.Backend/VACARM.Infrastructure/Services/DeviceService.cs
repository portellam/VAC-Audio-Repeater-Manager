using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;
using System.Diagnostics.CodeAnalysis;
using VACARM.Infrastructure.Functions;

namespace VACARM.Application.Services
{
  /// <summary>
  /// The service to one (1) configuration of system audio device(s). 
  /// </summary>
  public partial class DeviceService
    <
      TRepository,
      TDeviceModel
    > :
    BaseService
    <
      BaseRepository<TDeviceModel>,
      TDeviceModel
    >,
    IDisposable,
    IDeviceService
    <
      BaseRepository<TDeviceModel>,
      TDeviceModel
    > where TRepository :
    BaseRepository<TDeviceModel>
    where TDeviceModel :
    DeviceModel
  {
    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    [ExcludeFromCodeCoverage]
    public DeviceService() :
      base()
    {
      this.BaseRepository = new BaseRepository<TDeviceModel>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repository">The repository</param>
    public DeviceService(BaseRepository<TDeviceModel> repository) :
      base(repository)
    {
      this.BaseRepository = repository;
    }

    protected override void Dispose(bool isDisposed)
    {
      if (this.HasDisposed)
      {
        return;
      }

      if (isDisposed)
      {
        this.BaseRepository
          .Dispose();
      }

      this.HasDisposed = true;
    }

    public TDeviceModel? GetByActualId(string actualId)
    {
      var func = DeviceFunctions<TDeviceModel>.ContainsActualId(actualId);

      return this.BaseRepository
        .Get(func);
    }

    public TDeviceModel? GetDefaultCommunications()
    {
      return this
        .GetAllCommunications()
        .FirstOrDefault(x => x.IsDefault);
    }

    public TDeviceModel? GetDefaultConsole()
    {
      return this
        .GetAllConsole()
        .FirstOrDefault(x => x.IsDefault);
    }

    public TDeviceModel? GetDefaultMultimedia()
    {
      return this
        .GetAllMultimedia()
        .FirstOrDefault(x => x.IsDefault);
    }

    public IEnumerable<TDeviceModel> GetAllAbsent()
    {
      var func = DeviceFunctions<TDeviceModel>.IsAbsent;

      return this.BaseRepository
        .GetRange(func);
    }

    public IEnumerable<TDeviceModel> GetAllAlphabetical()
    {
      return this.BaseRepository
        .GetAll()
        .OrderBy(x => x.Name);
    }

    public IEnumerable<TDeviceModel> GetAllAlphabeticalDescending()
    {
      return this.BaseRepository
        .GetAll()
        .OrderByDescending(x => x.Name);
    }

    public IEnumerable<TDeviceModel> GetAllCapture()
    {
      var func = DeviceFunctions<TDeviceModel>.IsCapture;

      return this.BaseRepository
        .GetRange(func);
    }

    public IEnumerable<TDeviceModel> GetAllCommunications()
    {
      var func = DeviceFunctions<TDeviceModel>.IsCommunications;

      return this.BaseRepository
        .GetRange(func);
    }

    public IEnumerable<TDeviceModel> GetAllConsole()
    {
      var func = DeviceFunctions<TDeviceModel>.IsConsole;

      return this.BaseRepository
        .GetRange(func);
    }

    public IEnumerable<TDeviceModel> GetAllDefault()
    {
      var func = DeviceFunctions<TDeviceModel>.IsDefault;

      return this.BaseRepository
        .GetRange(func);
    }

    public IEnumerable<TDeviceModel> GetAllDisabled()
    {
      var func = DeviceFunctions<TDeviceModel>.IsDisabled;

      return this.BaseRepository
        .GetRange(func);
    }

    public IEnumerable<TDeviceModel> GetAllDuplex()
    {
      var func = DeviceFunctions<TDeviceModel>.IsDuplex;

      return this.BaseRepository
        .GetRange(func);
    }

    public IEnumerable<TDeviceModel> GetAllEnabled()
    {
      var func = DeviceFunctions<TDeviceModel>.IsEnabled;

      return this.BaseRepository
        .GetRange(func);
    }

    public IEnumerable<TDeviceModel> GetAllMultimedia()
    {
      var func = DeviceFunctions<TDeviceModel>.IsMultimedia;

      return this.BaseRepository
        .GetRange(func);
    }

    public IEnumerable<TDeviceModel> GetAllMuted()
    {
      var func = DeviceFunctions<TDeviceModel>.IsMuted;

      return this.BaseRepository
        .GetRange(func);
    }

    public IEnumerable<TDeviceModel> GetAllPresent()
    {
      var func = DeviceFunctions<TDeviceModel>.IsPresent;

      return this.BaseRepository
        .GetRange(func);
    }

    public IEnumerable<TDeviceModel> GetAllRender()
    {
      var func = DeviceFunctions<TDeviceModel>.IsRender;

      return this.BaseRepository
        .GetRange(func);
    }

    public IEnumerable<TDeviceModel> GetAllUnmuted()
    {
      var func = DeviceFunctions<TDeviceModel>.IsUnmuted;

      return this.BaseRepository
        .GetRange(func);
    }

    #endregion
  }
}