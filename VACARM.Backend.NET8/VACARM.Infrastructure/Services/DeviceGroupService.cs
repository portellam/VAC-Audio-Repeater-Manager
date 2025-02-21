#warning AudioSwitcher.AudioApi must initialize after NAudio.CoreAudioApi

using AudioSwitcher.AudioApi;
using NAudio.CoreAudioApi;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Functions;
using VACARM.Infrastructure.Repositories;
using VACARM.Infrastructure.Services;

namespace VACARM.Application.Services
{
  /// <summary>
  /// The service to manage multiple configurations of system audio device(s). 
  /// Configurations may be from a foreign system or a previous state of the 
  /// current system.
  /// Manages <typeparamref name="CoreAudioService"/>
  ///  and <typeparamref name="MMDeviceService"/>.
  /// </summary>
  public partial class DeviceGroupService
    <
      TGroupReadonlyRepository,
      TBaseService,
      TBaseRepository,
      TDeviceModel
    > :
    BaseGroupService
    <
      ReadonlyRepository
      <
        BaseService
        <
          BaseRepository<TDeviceModel>,
          TDeviceModel
        >
      >,
      BaseService
      <
        BaseRepository<TDeviceModel>,
        TDeviceModel
      >,
      BaseRepository<TDeviceModel>,
      TDeviceModel
    >,
    IDeviceGroupService
    <
      ReadonlyRepository
      <
        BaseService
        <
          BaseRepository<TDeviceModel>,
          TDeviceModel
        >
      >,
      BaseService
      <
        BaseRepository<TDeviceModel>,
        TDeviceModel
      >,
      BaseRepository<TDeviceModel>,
      TDeviceModel
    >
    where TGroupReadonlyRepository :
    ReadonlyRepository
    <
      BaseService
      <
        BaseRepository<TDeviceModel>,
        TDeviceModel
      >
    >
    where TBaseService :
    BaseService
    <
      BaseRepository<TDeviceModel>,
      TDeviceModel
    >
    where TBaseRepository :
    BaseRepository<TDeviceModel>
    where TDeviceModel :
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
    public DeviceGroupService() :
      base()
    {
      this.List =
        new List<BaseService<BaseRepository<TDeviceModel>, TDeviceModel>>();

      this.MMDeviceService =
          new MMDeviceService<ReadonlyRepository<MMDevice>, MMDevice>();

      this.CoreAudioService =
          new CoreAudioService<ReadonlyRepository<Device>, Device>();

      this.UpdateSelectedService();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="list">The list of service(s)</param>
    /// <param name="maxCount">The maximum count of service(s)</param>
    /// <param name="mMDeviceService">The MMDevice service</param>
    /// <param name="coreAudioService">The Core Audio service</param>
    public DeviceGroupService
    (
      List<BaseService<BaseRepository<TDeviceModel>, TDeviceModel>> list,
      int maxCount,
      MMDeviceService<ReadonlyRepository<MMDevice>, MMDevice> mMDeviceService,
      CoreAudioService<ReadonlyRepository<Device>, Device> coreAudioService
    ) :
      base
      (
        list,
        maxCount
      )
    {
      this.List = list;
      this.MMDeviceService = mMDeviceService;
      this.CoreAudioService = coreAudioService;

      if (this.SelectedService == null)
      {
        this.UpdateSelectedService();
      }
    }

    /// <summary>
    /// Restart a <typeparamref name="TDeviceModel"/>.
    /// </summary>
    /// <param name="model">The model</param>
    private void Restart(TDeviceModel model)
    {
      if (model == null)
      {
        return;
      }

      if (this.MMDeviceService == null)
      {
        return;
      }

      this.MMDeviceService
        .Reset(model.ActualId);
    }


    /// <summary>
    /// Start a <typeparamref name="TDeviceModel"/>.
    /// </summary>
    /// <param name="model">The model</param>
    private void Start(TDeviceModel model)
    {
      if (model == null)
      {
        return;
      }

      if (this.MMDeviceService == null)
      {
        return;
      }

      this.MMDeviceService
        .Start(model.ActualId);
    }


    /// <summary>
    /// Stop a <typeparamref name="TDeviceModel"/>.
    /// </summary>
    /// <param name="model">The model</param>
    private void Stop(TDeviceModel model)
    {
      if (model == null)
      {
        return;
      }

      if (this.MMDeviceService == null)
      {
        return;
      }

      this.MMDeviceService
        .Start(model.ActualId);
    }


    /// <summary>
    /// Update a <typeparamref name="TDeviceModel"/>.
    /// </summary>
    /// <param name="model">The model</param>
    private void Update(TDeviceModel model)
    {
      if (model == null)
      {
        return;
      }

      if (this.MMDeviceService == null)
      {
        return;
      }

      this.MMDeviceService
        .Update(model.ActualId);
    }

    protected override void Dispose(bool isDisposed)
    {
      if (this.HasDisposed)
      {
        return;
      }

      if (isDisposed)
      {
        this.Dispose();

        this.CoreAudioService
          .Dispose();

        this.MMDeviceService
          .Dispose();
      }

      this.HasDisposed = true;
    }

    public TDeviceModel? GetByActualId(string actualId)
    {
      var func = DeviceModelFunctions<TDeviceModel>.ContainsActualId(actualId);

      return this.SelectedRepository
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
      var func = DeviceModelFunctions<TDeviceModel>.IsAbsent;

      return this.SelectedRepository
        .GetRange(func);
    }

    public IEnumerable<TDeviceModel> GetAllAlphabetical()
    {
      return this.SelectedRepository
        .GetAll()
        .OrderBy(x => x.Name);
    }

    public IEnumerable<TDeviceModel> GetAllAlphabeticalDescending()
    {
      return this.SelectedRepository
        .GetAll()
        .OrderByDescending(x => x.Name);
    }

    public IEnumerable<TDeviceModel> GetAllCapture()
    {
      var func = DeviceModelFunctions<TDeviceModel>.IsCapture;

      return this.SelectedRepository
        .GetRange(func);
    }

    public IEnumerable<TDeviceModel> GetAllCommunications()
    {
      var func = DeviceModelFunctions<TDeviceModel>.IsCommunications;

      return this.SelectedRepository
        .GetRange(func);
    }

    public IEnumerable<TDeviceModel> GetAllConsole()
    {
      var func = DeviceModelFunctions<TDeviceModel>.IsConsole;

      return this.SelectedRepository
        .GetRange(func);
    }

    public IEnumerable<TDeviceModel> GetAllDefault()
    {
      var func = DeviceModelFunctions<TDeviceModel>.IsDefault;

      return this.SelectedRepository
        .GetRange(func);
    }

    public IEnumerable<TDeviceModel> GetAllDisabled()
    {
      var func = DeviceModelFunctions<TDeviceModel>.IsDisabled;

      return this.SelectedRepository
        .GetRange(func);
    }

    public IEnumerable<TDeviceModel> GetAllDuplex()
    {
      var func = DeviceModelFunctions<TDeviceModel>.IsDuplex;

      return this.SelectedRepository
        .GetRange(func);
    }

    public IEnumerable<TDeviceModel> GetAllEnabled()
    {
      var func = DeviceModelFunctions<TDeviceModel>.IsEnabled;

      return this.SelectedRepository
        .GetRange(func);
    }

    public IEnumerable<TDeviceModel> GetAllMultimedia()
    {
      var func = DeviceModelFunctions<TDeviceModel>.IsMultimedia;

      return this.SelectedRepository
        .GetRange(func);
    }

    public IEnumerable<TDeviceModel> GetAllMuted()
    {
      var func = DeviceModelFunctions<TDeviceModel>.IsMuted;

      return this.SelectedRepository
        .GetRange(func);
    }

    public IEnumerable<TDeviceModel> GetAllPresent()
    {
      var func = DeviceModelFunctions<TDeviceModel>.IsPresent;

      return this.SelectedRepository
        .GetRange(func);
    }

    public IEnumerable<TDeviceModel> GetAllRender()
    {
      var func = DeviceModelFunctions<TDeviceModel>.IsRender;

      return this.SelectedRepository
        .GetRange(func);
    }

    public IEnumerable<TDeviceModel> GetAllUnmuted()
    {
      var func = DeviceModelFunctions<TDeviceModel>.IsUnmuted;

      return this.SelectedRepository
        .GetRange(func);
    }

    public void Restart(uint id)
    {
      var func = BaseFunctions<TDeviceModel>.ContainsId(id);

      this.SelectedService
        .DoWork
        (
          this.Restart,
          func
        );
    }

    public void RestartAll()
    {
      this.MMDeviceService
        .ResetAll();
    }

    public void RestartRange(IEnumerable<uint> idEnumerable)
    {
      var func = BaseFunctions<TDeviceModel>.ContainsIdEnumerable(idEnumerable);

      this.SelectedService
      .DoWorkRange
      (
        this.Restart,
        func
      );
    }

    public void RestartRange
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

      this.SelectedService
        .DoWorkRange
        (
          this.Restart,
          func
        );
    }

    public void Start(uint id)
    {
      var func = BaseFunctions<TDeviceModel>.ContainsId(id);

      this.SelectedService
        .DoWork
        (
          this.Start,
          func
        );
    }

    public void StartAll()
    {
      this.MMDeviceService
        .StartAll();
    }

    public void StartRange(IEnumerable<uint> idEnumerable)
    {
      var func = BaseFunctions<TDeviceModel>.ContainsIdEnumerable(idEnumerable);

      this.SelectedService
        .DoWorkRange
        (
          this.Start,
          func
        );
    }

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

      this.SelectedService
        .DoWorkRange
        (
          this.Start,
          func
        );
    }

    public void Stop(uint id)
    {
      var func = BaseFunctions<TDeviceModel>.ContainsId(id);

      this.SelectedService
        .DoWork
        (
          this.Stop,
          func
        );
    }

    public void StopAll()
    {
      this.MMDeviceService
        .StopAll();
    }

    public void StopRange(IEnumerable<uint> idEnumerable)
    {
      var func = BaseFunctions<TDeviceModel>.ContainsIdEnumerable(idEnumerable);

      this.SelectedService
        .DoWorkRange
        (
          this.Stop,
          func
        );
    }

    public void StopRange
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

      this.SelectedService
        .DoWorkRange
        (
          this.Stop,
          func
        );
    }

    public void Update(uint id)
    {
      var func = BaseFunctions<TDeviceModel>.ContainsId(id);

      this.SelectedService
        .DoWork
        (
          this.Update,
          func
        );
    }

    public void UpdateAll()
    {
      this.MMDeviceService
        .UpdateAll();
    }

    public void UpdateRange(IEnumerable<uint> idEnumerable)
    {
      var func = BaseFunctions<TDeviceModel>.ContainsIdEnumerable(idEnumerable);

      this.SelectedService
        .DoWorkRange
        (
          this.Update,
          func
        );
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

      this.SelectedService
        .DoWorkRange
        (
          this.Update,
          func
        );
    }

    public void UpdateSelectedService()
    {
      var enumerable = this.MMDeviceService
        .GetAll();

      foreach (MMDevice mMDevice in enumerable)
      {
        var device = this.CoreAudioService
          .Get(mMDevice.ID);

        uint id = this.SelectedRepository
          .NextId;

        var deviceModel = DeviceModelFunctions<TDeviceModel>.GetDeviceModel
          (
            id,
            mMDevice,
            device,
            null
          );
      }
    }

    #endregion
  }
}