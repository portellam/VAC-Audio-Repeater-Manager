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
  /// The service to manage multiple configurations of system audio device(s). 
  /// Configurations may be from a foreign system or a previous state of the 
  /// current system.
  /// Manages <typeparamref name="CoreAudioService"/>
  /// and <typeparamref name="MMDeviceService"/>.
  /// </summary>
  public partial class DeviceRepositoryService<TRepository, TDeviceModel> :
    BaseService<BaseRepository<TDeviceModel>, TDeviceModel>,
    IDisposable,
    IDeviceService<BaseRepository<TDeviceModel>, TDeviceModel> where
    TRepository :
    BaseRepository<DeviceModel> where TDeviceModel :
    DeviceModel
  {
    #region Parameters

    internal readonly int SafeMaxRepositoryCount = 3;                                   // TODO: test, and change me!
    internal uint SelectedId { get; set; } = uint.MinValue;

    private ReadonlyRepository<TDeviceModel> repositoryRepository
    { get; set; }

    private CoreAudioService<ReadonlyRepository<Device>, Device> coreAudioService
    { get; set; }

    private MMDeviceService<ReadonlyRepository<MMDevice>, MMDevice> mMDeviceService
    { get; set; }

    private Func<BaseRepository<DeviceModel>, bool> IsSelectedId
    {
      get
      {
        return (BaseRepository<DeviceModel> x) => x. == this.SelectedId;
      }
    }

    protected ReadonlyRepository<TDeviceModel> RepositoryRepository
    {
      get
      {
        return base.Repository;
      }
      private set
      {
        base.Repository = value;
        this.OnPropertyChanged(nameof(RepositoryRepository));
      }
    }

    protected new BaseRepository<DeviceModel> SelectedRepository
    {
      get
      {
        return this.RepositoryRepository
          .Get(IsSelectedId);
      }
    }

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
    public DeviceRepositoryService() :
      base()
    {
      Dictionary<int, BaseRepository<DeviceModel>> Dictionary =
        new Dictionary<int, BaseRepository<DeviceModel>>();

      Dictionary.Add(0, new BaseRepository<DeviceModel>());

      this.RepositoryRepository = new ReadonlyRepository<TDeviceModel>
        (
          Dictionary
        );

      this.MMDeviceService =
        new MMDeviceService<ReadonlyRepository<MMDevice>, MMDevice>();

      this.CoreAudioService =
        new CoreAudioService<ReadonlyRepository<Device>, Device>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repositoryRepository">The repository of repositories</param>
    /// <param name="mMDeviceService">The MMDevice service</param>
    /// <param name="coreAudioService">The Core Audio service</param>
    public DeviceRepositoryService
    (
      BaseRepository<TDeviceModel> repositoryRepository,
      MMDeviceService<ReadonlyRepository<MMDevice>, MMDevice> mMDeviceService,
      CoreAudioService<ReadonlyRepository<Device>, Device> coreAudioService
    ) :
      base(repositoryRepository)
    {
      this.Repository = repositoryRepository;
      this.MMDeviceService = mMDeviceService;
      this.CoreAudioService = coreAudioService;
    }

    protected override void Dispose(bool isDisposed)
    {
      if (this.HasDisposed)
      {
        return;
      }

      if (isDisposed)
      {
        this.Repository
          .Dispose();

        this.CoreAudioService
          .Dispose();

        this.MMDeviceService
          .Dispose();
      }

      this.HasDisposed = true;
    }

    public DeviceModel? GetByActualId(string actualId)
    {
      var func = DeviceFunctions<DeviceModel>.ContainsActualId(actualId);

      return this.Repository
        .Get(func);
    }

    public DeviceModel? GetDefaultCommunications()
    {
      return this
        .GetAllCommunications()
        .FirstOrDefault(x => x.IsDefault);
    }

    public DeviceModel? GetDefaultConsole()
    {
      return this
        .GetAllConsole()
        .FirstOrDefault(x => x.IsDefault);
    }

    public DeviceModel? GetDefaultMultimedia()
    {
      return this
        .GetAllMultimedia()
        .FirstOrDefault(x => x.IsDefault);
    }

    public IEnumerable<DeviceModel> GetAllAbsent()
    {
      var func = DeviceFunctions<DeviceModel>.IsAbsent;

      return this.Repository
        .GetRange(func);
    }

    public IEnumerable<DeviceModel> GetAllAlphabetical()
    {
      return this.Repository
        .GetAll()
        .OrderBy(x => x.Name);
    }

    public IEnumerable<DeviceModel> GetAllAlphabeticalDescending()
    {
      return this.Repository
        .GetAll()
        .OrderByDescending(x => x.Name);
    }

    public IEnumerable<DeviceModel> GetAllCapture()
    {
      var func = DeviceFunctions<DeviceModel>.IsCapture;

      return this.Repository
        .GetRange(func);
    }

    public IEnumerable<DeviceModel> GetAllCommunications()
    {
      var func = DeviceFunctions<DeviceModel>.IsCommunications;

      return this.Repository
        .GetRange(func);
    }

    public IEnumerable<DeviceModel> GetAllConsole()
    {
      var func = DeviceFunctions<DeviceModel>.IsConsole;

      return this.Repository
        .GetRange(func);
    }

    public IEnumerable<DeviceModel> GetAllDefault()
    {
      var func = DeviceFunctions<DeviceModel>.IsDefault;

      return this.Repository
        .GetRange(func);
    }

    public IEnumerable<DeviceModel> GetAllDisabled()
    {
      var func = DeviceFunctions<DeviceModel>.IsDisabled;

      return this.Repository
        .GetRange(func);
    }

    public IEnumerable<DeviceModel> GetAllDuplex()
    {
      var func = DeviceFunctions<DeviceModel>.IsDuplex;

      return this.Repository
        .GetRange(func);
    }

    public IEnumerable<DeviceModel> GetAllEnabled()
    {
      var func = DeviceFunctions<DeviceModel>.IsEnabled;

      return this.Repository
        .GetRange(func);
    }

    public IEnumerable<DeviceModel> GetAllMultimedia()
    {
      var func = DeviceFunctions<DeviceModel>.IsMultimedia;

      return this.Repository
        .GetRange(func);
    }

    public IEnumerable<DeviceModel> GetAllMuted()
    {
      var func = DeviceFunctions<DeviceModel>.IsMuted;

      return this.Repository
        .GetRange(func);
    }

    public IEnumerable<DeviceModel> GetAllPresent()
    {
      var func = DeviceFunctions<DeviceModel>.IsPresent;

      return this.Repository
        .GetRange(func);
    }

    public IEnumerable<DeviceModel> GetAllRender()
    {
      var func = DeviceFunctions<DeviceModel>.IsRender;

      return this.Repository
        .GetRange(func);
    }

    public IEnumerable<DeviceModel> GetAllUnmuted()
    {
      var func = DeviceFunctions<DeviceModel>.IsUnmuted;

      return this.Repository
        .GetRange(func);
    }

    /// <summary>
    /// Start an enumerable of all <typeparamref name="DeviceModel"/>(s).
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
    /// Start an enumerable of all <typeparamref name="DeviceModel"/>(s).
    /// </summary>
    public void StartAll()
    {
      this.MMDeviceService
        .StartAll();

      this.UpdateAll();
    }

    /// <summary>
    /// Start an enumerable of some <typeparamref name="DeviceModel"/>(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    public void StartRange
    (
      uint startId,
      uint endId
    )
    {
      var func = BaseFunctions<DeviceModel>.ContainsIdRange
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
    /// Start an enumerable of some <typeparamref name="DeviceModel"/>(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    public void StartRange(IEnumerable<uint> idEnumerable)
    {
      var func = BaseFunctions<DeviceModel>.ContainsIdEnumerable(idEnumerable);

      var enumerable = this.Repository
        .GetRange(func);

      foreach (var item in enumerable)
      {
        this.Start(item.Id);
      }
    }

    /// <summary>
    /// Stop an enumerable of all <typeparamref name="DeviceModel"/>(s).
    /// </summary>
    public void StopAll()
    {
      this.MMDeviceService
        .StopAll();
    }

    /// <summary>
    /// Stop an enumerable of some <typeparamref name="DeviceModel"/>(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    public void StopRange
    (
      uint startId,
      uint endId
    )
    {
      IEnumerable<DeviceModel> enumerable = this
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
    /// Stop an enumerable of some <typeparamref name="DeviceModel"/>(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    public void StopRange(IEnumerable<uint> idEnumerable)
    {
      var enumerable = this
        .Repository
        .GetRange(idEnumerable);

      foreach (var item in enumerable)
      {
        this.MMDeviceService
          .Stop(item.ActualId);
      }
    }

    /// <summary>
    /// Update a <typeparamref name="DeviceModel"/>.
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

    public void UpdateRange(Func<DeviceModel, bool> func)
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
      var func = BaseFunctions<DeviceModel>.ContainsIdEnumerable(idEnumerable);
      this.UpdateRange(func);
    }

    public void UpdateRange
    (
      uint startId,
      uint endId
    )
    {
      var func = BaseFunctions<DeviceModel>.ContainsIdRange
        (
          startId,
          endId
        );

      this.UpdateRange(func);
    }

    #endregion
  }
}