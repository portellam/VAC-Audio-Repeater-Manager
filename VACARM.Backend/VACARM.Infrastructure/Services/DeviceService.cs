#warning AudioSwitcher.AudioApi must initialize after NAudio.CoreAudioApi

using NAudio.CoreAudioApi;
using AudioSwitcher.AudioApi;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;
using System.Diagnostics.CodeAnalysis;

namespace VACARM.Application.Services
{
  /// <summary>
  /// The service of the <typeparamref name="DeviceRepository"/>.
  /// </summary>
  public partial class DeviceService<TRepository, TDeviceModel> :
    BaseService<DeviceRepository<TDeviceModel>, TDeviceModel>,
    IDeviceService<DeviceRepository<TDeviceModel>, TDeviceModel> where TRepository :
    DeviceRepository<TDeviceModel> where TDeviceModel :
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
    public DeviceService()
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
    )
    {
      base.Repository = new DeviceRepository<TDeviceModel>();
      this.MMDeviceService = mMDeviceService;
      this.CoreAudioService = coreAudioService;
    }


    /// <summary>
    /// Start an enumerable of all <typeparamref name="TDeviceModel"/> item(s).
    /// </summary>
    public void StartAll()
    {
      this
        .MMDeviceService
        .StartAll();
    }

    /// <summary>
    /// Stop an enumerable of all <typeparamref name="TDeviceModel"/> item(s).
    /// </summary>
    public void StopAll()
    {
      this
        .MMDeviceService
        .StopAll();
    }

    /// <summary>
    /// Update a <typeparamref name="TDeviceModel"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    public void Update(uint id)
    {
      TDeviceModel? model = this
        ._Repository
        .Get(id);

      this
         .MMDeviceService
         .Update(model.ActualId);
    }

    /// <summary>
    /// Update an enumerable of all <typeparamref name="TDeviceModel"/> item(s).
    /// </summary>
    public void UpdateAll()
    {
      this
        .MMDeviceService
        .UpdateAll();
    }

    /// <summary>
    /// Update an enumerable of some <typeparamref name="TDeviceModel"/> item(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    public void UpdateRange
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
        this
          .MMDeviceService
          .Update(item.ActualId);
      }
    }

    /// <summary>
    /// Update an enumerable of some <typeparamref name="TDeviceModel"/> item(s).
    /// </summary>
    /// <param name="idEnumerable">The enumerable of ID(s)</param>
    public void UpdateRange(IEnumerable<uint> idEnumerable)
    {
      IEnumerable<TDeviceModel> enumerable = this
        ._Repository
        .GetRange(idEnumerable);

      foreach (var item in enumerable)
      {
        this
          .MMDeviceService
          .Update(item.ActualId);
      }
    }

    #endregion
  }
}