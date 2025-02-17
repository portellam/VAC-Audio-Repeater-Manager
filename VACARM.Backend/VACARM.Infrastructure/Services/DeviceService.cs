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

    private MMDeviceService<MMDeviceRepository<MMDevice>, MMDevice>
      mMDeviceService
    { get; set; }

    /// <summary>
    /// <typeparamref name="AudioSwitcher.AudioApi"/> must declare after
    /// <typeparamref name="NAudio.CoreAudioApi"/>.
    /// Issue: <see cref="https://github.com/naudio/NAudio/issues/421"/>
    /// </summary>
    public MMDeviceService<MMDeviceRepository<MMDevice>, MMDevice> MMDeviceService
    {
      get
      {
        return this.mMDeviceService;
      }
      private set
      {
        this.mMDeviceService = value;
        base.OnPropertyChanged(nameof(MMDeviceService));
      }
    }

    private CoreAudioService<CoreAudioRepository<Device>, Device>
      coreAudioService
    { get; set; }

    /// <summary>
    /// <typeparamref name="AudioSwitcher.AudioApi"/> must declare after
    /// <typeparamref name="NAudio.CoreAudioApi"/>.
    /// Issue: <see cref="https://github.com/naudio/NAudio/issues/421"/>
    /// </summary>
    public CoreAudioService<CoreAudioRepository<Device>, Device> CoreAudioService
    {
      get
      {
        return this.coreAudioService;
      }
      private set
      {
        this.coreAudioService = value;
        base.OnPropertyChanged(nameof(CoreAudioService));
      }
    }

    protected new DeviceRepository<TDeviceModel> _Repository
    {
      get
      {
        return (TRepository)base.Repository;
      }
      set
      {
        base.Repository = value;
        base.OnPropertyChanged(nameof(_Repository));
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
      base.Repository = new DeviceRepository<TDeviceModel>();

      this.MMDeviceService =
        new MMDeviceService<MMDeviceRepository<MMDevice>, MMDevice>();

      this.CoreAudioService =
        new CoreAudioService<CoreAudioRepository<Device>, Device>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repository">The repository</param>
    /// <param name="mMDeviceService">The MMDevice service</param>
    /// <param name="coreAudioService">The Core Audio service</param>
    public DeviceService
    (
      DeviceRepository<TDeviceModel> repository,
      MMDeviceService<MMDeviceRepository<MMDevice>, MMDevice> mMDeviceService,
      CoreAudioService<CoreAudioRepository<Device>, Device> coreAudioService
    ) :
      base(repository)
    {
      base.Repository = new DeviceRepository<TDeviceModel>();
      this.MMDeviceService = mMDeviceService;
      this.CoreAudioService = coreAudioService;
    }

    /// <summary>
    /// Start an enumerable of all <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    public void Start(uint id)
    {
      this.MMDeviceService
        .Start(id);

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
        ._Repository
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
        ._Repository
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
      TDeviceModel? model = this._Repository
        .Get(id);

      this.MMDeviceService
         .Update(model.ActualId);
    }

    /// <summary>
    /// Update an enumerable of all <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    public void UpdateAll()
    {
      this.MMDeviceService
        .UpdateAll();
    }

    /// <summary>
    /// Update an enumerable of some <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    public void UpdateRange
    (
      uint startId,
      uint endId
    )
    {
      IEnumerable<TDeviceModel> enumerable = this._Repository
        .GetRange
        (
          startId,
          endId
        );

      foreach (var item in enumerable)
      {
        this.MMDeviceService
          .Update(item.ActualId);
      }
    }

    /// <summary>
    /// Update an enumerable of some <typeparamref name="TDeviceModel"/>(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    public void UpdateRange(IEnumerable<uint> idEnumerable)
    {
      IEnumerable<TDeviceModel> enumerable = this._Repository
        .GetRange(idEnumerable);

      foreach (var item in enumerable)
      {
        this.MMDeviceService
          .Update(item.ActualId);
      }
    }

    public TDeviceModel? GetByActualId(string actualId)
    {
      Func<TDeviceModel, bool> func = (TDeviceModel x) => x.ActualId == actualId;
      return this.Get(func);
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