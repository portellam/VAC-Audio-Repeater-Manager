#warning Differs from projects of earlier NET revisions (below Framework 4.6).
#warning AudioSwitcher.AudioApi must initialize after NAudio.CoreAudioApi

using AudioSwitcher.AudioApi;
using NAudio.CoreAudioApi;
using System.Diagnostics.CodeAnalysis;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Functions;

namespace VACARM.Infrastructure.Services
{
  /// <summary>
  /// The service to manage multiple configurations of system audio device(s). 
  /// Configurations may be from a foreign system or a previous state of the 
  /// current system.
  /// Manages <typeparamref name="CoreAudioService"/>
  ///  and <typeparamref name="MMDeviceService"/>.
  /// </summary>
  public partial class DeviceGroupService :
    BaseGroupService
    <
      DeviceModel
    >,
    IDeviceGroupService
  {
    #region Parameters

    private CoreAudioService<Device> coreAudioService
    { get; set; }

    private MMDeviceService<MMDevice> mMDeviceService
    { get; set; }

    /// <summary>
    /// <typeparamref name="AudioSwitcher.AudioApi"/> must declare after
    /// <typeparamref name="NAudio.CoreAudioApi"/>.
    /// Issue: <see cref="https://github.com/naudio/NAudio/issues/421"/>
    /// </summary>
    public CoreAudioService<Device> CoreAudioService
    {
      get
      {
        return this.coreAudioService;
      }
      private set
      {
        this.coreAudioService = value;
        base.OnPropertyChanged(nameof(this.CoreAudioService));
      }
    }

    /// <summary>
    /// <typeparamref name="AudioSwitcher.AudioApi"/> must declare after
    /// <typeparamref name="NAudio.CoreAudioApi"/>.
    /// Issue: <see cref="https://github.com/naudio/NAudio/issues/421"/>
    /// </summary>
    public MMDeviceService<MMDevice> MMDeviceService
    {
      get
      {
        return this.mMDeviceService;
      }
      private set
      {
        this.mMDeviceService = value;
        base.OnPropertyChanged(nameof(this.MMDeviceService));
      }
    }

    #endregion

    #region Logic

    // TODO: specify a default file name value? Or generate one given index?

    /// <summary>
    /// Constructor
    /// </summary>
    [ExcludeFromCodeCoverage]
    public DeviceGroupService() :
      base()
    {
      this.MMDeviceService = new MMDeviceService<MMDevice>();
      this.CoreAudioService = new CoreAudioService<Device>();
      this.UpdateSelectedService();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="maxCount">The maximum count of service(s)</param>
    public DeviceGroupService(int maxCount) :
      base()
    {
      this.MaxCount = maxCount;
      this.MMDeviceService = new MMDeviceService<MMDevice>();
      this.CoreAudioService = new CoreAudioService<Device>();

      if (this.SelectedService == null)
      {
        this.UpdateSelectedService();
      }
    }

    /// <summary>
    /// Restart a <typeparamref name="DeviceModel"/>.
    /// </summary>
    /// <param name="model">The model</param>
    private void Restart(DeviceModel model)
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
    /// Start a <typeparamref name="DeviceModel"/>.
    /// </summary>
    /// <param name="model">The model</param>
    private void Start(DeviceModel model)
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
    /// Stop a <typeparamref name="DeviceModel"/>.
    /// </summary>
    /// <param name="model">The model</param>
    private void Stop(DeviceModel model)
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
    /// Update a <typeparamref name="DeviceModel"/>.
    /// </summary>
    /// <param name="model">The model</param>
    private void Update(DeviceModel model)
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

    public DeviceModel GetByActualId(string actualId)
    {
      var func = DeviceFunctions<DeviceModel>.ContainsActualId(actualId);

      return base.SelectedRepository
        .Get(func);
    }

    public DeviceModel GetDefaultCommunications()
    {
      return this
        .GetAllCommunications()
        .FirstOrDefault(x => x.IsDefault);
    }

    public DeviceModel GetDefaultConsole()
    {
      return this
        .GetAllConsole()
        .FirstOrDefault(x => x.IsDefault);
    }

    public DeviceModel GetDefaultMultimedia()
    {
      return this
        .GetAllMultimedia()
        .FirstOrDefault(x => x.IsDefault);
    }

    public IEnumerable<DeviceModel> GetAllAbsent()
    {
      var func = DeviceFunctions<DeviceModel>.IsAbsent;

      return base.SelectedRepository
        .GetRange(func);
    }

    public IEnumerable<DeviceModel> GetAllAlphabetical()
    {
      return base.SelectedRepository
        .GetAll()
        .OrderBy(x => x.Name);
    }

    public IEnumerable<DeviceModel> GetAllAlphabeticalDescending()
    {
      return base.SelectedRepository
        .GetAll()
        .OrderByDescending(x => x.Name);
    }

    public IEnumerable<DeviceModel> GetAllCapture()
    {
      var func = DeviceFunctions<DeviceModel>.IsCapture;

      return base.SelectedRepository
        .GetRange(func);
    }

    public IEnumerable<DeviceModel> GetAllCommunications()
    {
      var func = DeviceFunctions<DeviceModel>.IsCommunications;

      return base.SelectedRepository
        .GetRange(func);
    }

    public IEnumerable<DeviceModel> GetAllConsole()
    {
      var func = DeviceFunctions<DeviceModel>.IsConsole;

      return base.SelectedRepository
        .GetRange(func);
    }

    public IEnumerable<DeviceModel> GetAllDefault()
    {
      var func = DeviceFunctions<DeviceModel>.IsDefault;

      return base.SelectedRepository
        .GetRange(func);
    }

    public IEnumerable<DeviceModel> GetAllDisabled()
    {
      var func = DeviceFunctions<DeviceModel>.IsDisabled;

      return base.SelectedRepository
        .GetRange(func);
    }

    public IEnumerable<DeviceModel> GetAllEnabled()
    {
      var func = DeviceFunctions<DeviceModel>.IsEnabled;

      return base.SelectedRepository
        .GetRange(func);
    }

    public IEnumerable<DeviceModel> GetAllMultimedia()
    {
      var func = DeviceFunctions<DeviceModel>.IsMultimedia;

      return base.SelectedRepository
        .GetRange(func);
    }

    public IEnumerable<DeviceModel> GetAllMuted()
    {
      var func = DeviceFunctions<DeviceModel>.IsMuted;

      return base.SelectedRepository
        .GetRange(func);
    }

    public IEnumerable<DeviceModel> GetAllPresent()
    {
      var func = DeviceFunctions<DeviceModel>.IsPresent;

      return base.SelectedRepository
        .GetRange(func);
    }

    public IEnumerable<DeviceModel> GetAllRender()
    {
      var func = DeviceFunctions<DeviceModel>.IsRender;

      return base.SelectedRepository
        .GetRange(func);
    }

    public IEnumerable<DeviceModel> GetAllUnmuted()
    {
      var func = DeviceFunctions<DeviceModel>.IsUnmuted;

      return base.SelectedRepository
        .GetRange(func);
    }

    public void Restart(uint id)
    {
      var func = BaseFunctions<DeviceModel>.ContainsId(id);

      base.SelectedService
        .DoAction
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
      var func = BaseFunctions<DeviceModel>.ContainsIdEnumerable(idEnumerable);

      base.SelectedService
      .DoActionRange
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
      var func = BaseFunctions<DeviceModel>.ContainsIdRange
        (
          startId,
          endId
        );

      base.SelectedService
        .DoActionRange
        (
          this.Restart,
          func
        );
    }

    public void Start(uint id)
    {
      var func = BaseFunctions<DeviceModel>.ContainsId(id);

      base.SelectedService
        .DoAction
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
      var func = BaseFunctions<DeviceModel>.ContainsIdEnumerable(idEnumerable);

      base.SelectedService
        .DoActionRange
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
      var func = BaseFunctions<DeviceModel>.ContainsIdRange
        (
          startId,
          endId
        );

      base.SelectedService
        .DoActionRange
        (
          this.Start,
          func
        );
    }

    public void Stop(uint id)
    {
      var func = BaseFunctions<DeviceModel>.ContainsId(id);

      base.SelectedService
        .DoAction
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
      var func = BaseFunctions<DeviceModel>.ContainsIdEnumerable(idEnumerable);

      base.SelectedService
        .DoActionRange
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
      var func = BaseFunctions<DeviceModel>.ContainsIdRange
        (
          startId,
          endId
        );

      base.SelectedService
        .DoActionRange
        (
          this.Stop,
          func
        );
    }

    public void Update(uint id)
    {
      var func = BaseFunctions<DeviceModel>.ContainsId(id);

      base.SelectedService
        .DoAction
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
      var func = BaseFunctions<DeviceModel>.ContainsIdEnumerable(idEnumerable);

      base.SelectedService
        .DoActionRange
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
      var func = BaseFunctions<DeviceModel>.ContainsIdRange
        (
          startId,
          endId
        );

      base.SelectedService
        .DoActionRange
        (
          this.Update,
          func
        );
    }

    public void UpdateSelectedService()
    {
      var service = new BaseService<DeviceModel>();

      var enumerable = this.MMDeviceService
        .GetAll();

      foreach (MMDevice mMDevice in enumerable)
      {
        var device = this.CoreAudioService
          .Get(mMDevice.ID);

        uint id = 0;

        try
        {
          id = base.SelectedRepository
            .NextId;
        }

        catch
        {
        }

        var deviceModel = DeviceFunctions<DeviceModel>.GetDeviceModel
          (
            id,
            mMDevice,
            device,
            null
          );

        service.Repository
          .Add(deviceModel);
      }

      base.Update(service);
    }

    #endregion
  }
}