#warning AudioSwitcher.AudioApi must initialize after NAudio.CoreAudioApi

using NAudio.CoreAudioApi;
using AudioSwitcher.AudioApi;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;
using System.Diagnostics.CodeAnalysis;
using VACARM.Infrastructure.Functions;

namespace VACARM.Application.Services
{
  /// <summary>
  /// The service of the <typeparamref name="DeviceRepository"/>.
  /// </summary>
  public partial class DeviceService<TRepository, TDeviceModel> :
    BaseService<BaseRepository<TDeviceModel>, TDeviceModel>,
    IDeviceService<BaseRepository<TDeviceModel>, TDeviceModel> where TRepository :
    BaseRepository<TDeviceModel> where TDeviceModel :
    DeviceModel
  {
    #region Parameters

    private CoreAudioService<ReadonlyRepository<Device>, Device> coreAudioService
    { get; set; }

    private MMDeviceService<ReadonlyRepository<MMDevice>, MMDevice> mMDeviceService
    { get; set; }

    /// <summary>
    /// <typeparamref name="AudioSwitcher.AudioApi"/> must declare after
    /// <typeparamref name="NAudio.CoreAudioApi"/>.
    /// Issue: <see cref="https://github.com/naudio/NAudio/issues/421"/>
    /// </summary>
    public CoreAudioService<ReadonlyRepository<Device>, Device> CoreAudioService
    {
      get
      {
        return this.coreAudioService;
      }
      private set
      {
        this.coreAudioService = value;
        this.OnPropertyChanged(nameof(CoreAudioService));
      }
    }

    /// <summary>
    /// <typeparamref name="AudioSwitcher.AudioApi"/> must declare after
    /// <typeparamref name="NAudio.CoreAudioApi"/>.
    /// Issue: <see cref="https://github.com/naudio/NAudio/issues/421"/>
    /// </summary>
    public MMDeviceService<ReadonlyRepository<MMDevice>, MMDevice> MMDeviceService
    {
      get
      {
        return this.mMDeviceService;
      }
      private set
      {
        this.mMDeviceService = value;
        this.OnPropertyChanged(nameof(MMDeviceService));
      }
    }

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    [ExcludeFromCodeCoverage]
    public DeviceService() :
      base()
    {
      this.Repository = new BaseRepository<TDeviceModel>();

      this.MMDeviceService =
        new MMDeviceService<ReadonlyRepository<MMDevice>, MMDevice>();

      this.CoreAudioService =
        new CoreAudioService<ReadonlyRepository<Device>, Device>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repository">The repository</param>
    /// <param name="mMDeviceService">The MMDevice service</param>
    /// <param name="coreAudioService">The Core Audio service</param>
    public DeviceService
    (
      BaseRepository<TDeviceModel> repository,
      MMDeviceService<ReadonlyRepository<MMDevice>, MMDevice> mMDeviceService,
      CoreAudioService<ReadonlyRepository<Device>, Device> coreAudioService
    ) :
      base(repository)
    {
      this.Repository = new BaseRepository<TDeviceModel>();
      this.MMDeviceService = mMDeviceService;
      this.CoreAudioService = coreAudioService;
    }

    public TDeviceModel? GetByActualId(string actualId)
    {
      var func = DeviceFunctions<TDeviceModel>.ContainsActualId(actualId);

      return this.Repository
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

      return this.Repository
        .GetRange(func);
    }

    public IEnumerable<TDeviceModel> GetAllAlphabetical()
    {
      return this.Repository
        .GetAll()
        .OrderBy(x => x.Name);
    }

    public IEnumerable<TDeviceModel> GetAllAlphabeticalDescending()
    {
      return this.Repository
        .GetAll()
        .OrderByDescending(x => x.Name);
    }

    public IEnumerable<TDeviceModel> GetAllCapture()
    {
      var func = DeviceFunctions<TDeviceModel>.IsCapture;

      return this.Repository
        .GetRange(func);
    }

    public IEnumerable<TDeviceModel> GetAllCommunications()
    {
      var func = DeviceFunctions<TDeviceModel>.IsCommunications;

      return this.Repository
        .GetRange(func);
    }

    public IEnumerable<TDeviceModel> GetAllConsole()
    {
      var func = DeviceFunctions<TDeviceModel>.IsConsole;

      return this.Repository
        .GetRange(func);
    }

    public IEnumerable<TDeviceModel> GetAllDefault()
    {
      var func = DeviceFunctions<TDeviceModel>.IsDefault;

      return this.Repository
        .GetRange(func);
    }

    public IEnumerable<TDeviceModel> GetAllDisabled()
    {
      var func = DeviceFunctions<TDeviceModel>.IsDisabled;

      return this.Repository
        .GetRange(func);
    }

    public IEnumerable<TDeviceModel> GetAllDuplex()
    {
      var func = DeviceFunctions<TDeviceModel>.IsDuplex;

      return this.Repository
        .GetRange(func);
    }

    public IEnumerable<TDeviceModel> GetAllEnabled()
    {
      var func = DeviceFunctions<TDeviceModel>.IsEnabled;

      return this.Repository
        .GetRange(func);
    }

    public IEnumerable<TDeviceModel> GetAllMultimedia()
    {
      var func = DeviceFunctions<TDeviceModel>.IsMultimedia;

      return this.Repository
        .GetRange(func);
    }

    public IEnumerable<TDeviceModel> GetAllMuted()
    {
      var func = DeviceFunctions<TDeviceModel>.IsMuted;

      return this.Repository
        .GetRange(func);
    }

    public IEnumerable<TDeviceModel> GetAllPresent()
    {
      var func = DeviceFunctions<TDeviceModel>.IsPresent;

      return this.Repository
        .GetRange(func);
    }

    public IEnumerable<TDeviceModel> GetAllRender()
    {
      var func = DeviceFunctions<TDeviceModel>.IsRender;

      return this.Repository
        .GetRange(func);
    }

    public IEnumerable<TDeviceModel> GetAllUnmuted()
    {
      var func = DeviceFunctions<TDeviceModel>.IsUnmuted;

      return this.Repository
        .GetRange(func);
    }

    /// <summary>
    /// Start an enumerable of all <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    public void Start(uint id)
    {
      var func = BaseFunctions<DeviceModel>.ContainsId(id);

      var item = this.Repository
        .Get(func);

      this.MMDeviceService
        .Start(item.ActualId);

      this.Update(id);
    }

    /// <summary>
    /// Start an enumerable of all <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    public void StartAll()
    {
      this.MMDeviceService
        .StartAll();

      this.UpdateAll();
    }

    /// <summary>
    /// Start an enumerable of some <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    public void StartRange
    (
      uint startId,
      uint endId
    )
    {
      var func = BaseFunctions<TDeviceModel>.ContainsIdRange
        (
          startId,
          endId
        );

      var enumerable = this.Repository
        .GetRange(func);

      foreach (var item in enumerable)
      {
        this.Start(item.Id);
      }
    }

    /// <summary>
    /// Start an enumerable of some <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    public void StartRange(IEnumerable<uint> idEnumerable)
    {
      var func = BaseFunctions<TDeviceModel>.ContainsIdEnumerable(idEnumerable);

      var enumerable = this.Repository
        .GetRange(func);

      foreach (var item in enumerable)
      {
        this.Start(item.Id);
      }
    }

    /// <summary>
    /// Stop an enumerable of all <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    public void StopAll()
    {
      this.MMDeviceService
        .StopAll();
    }

    /// <summary>
    /// Stop an enumerable of some <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    public void StopRange
    (
      uint startId,
      uint endId
    )
    {
      IEnumerable<TDeviceModel> enumerable = this
        .Repository
        .GetRange
        (
          startId,
          endId
        );

      foreach (var item in enumerable)
      {
        this.MMDeviceService
          .Stop(item.ActualId);
      }
    }

    /// <summary>
    /// Stop an enumerable of some <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    public void StopRange
        (IEnumerable<uint> idEnumerable)
    {
      IEnumerable<TDeviceModel> enumerable = this
        .Repository
        .GetRange(idEnumerable);

      foreach (var item in enumerable)
      {
        this.MMDeviceService
          .Stop(item.ActualId);
      }
    }

    /// <summary>
    /// Update a <typeparamref name="TDeviceModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    public void Update(uint id)
    {
      var model = this.Repository
        .Get(id);

      this.MMDeviceService
         .Update(model.ActualId);
    }

    public void UpdateAll()
    {
      this.MMDeviceService
        .UpdateAll();
    }

    public void UpdateRange(Func<TDeviceModel, bool> func)
    {
      if (func == null)
      {
        return;
      }

      var enumerable = this.Repository
        .GetRange(func)
        .Select(x => x.ActualId);

      this.MMDeviceService
        .UpdateRange(enumerable);
    }

    public void UpdateRange(IEnumerable<uint> idEnumerable)
    {
      var func = BaseFunctions<TDeviceModel>.ContainsIdEnumerable(idEnumerable);
      this.UpdateRange(func);
    }

    public void UpdateRange
    (
      uint startId,
      uint endId
    )
    {
      var func = BaseFunctions<TDeviceModel>.ContainsIdRange
        (
          startId,
          endId
        );

      this.UpdateRange(func);
    }


    #endregion
  }
}