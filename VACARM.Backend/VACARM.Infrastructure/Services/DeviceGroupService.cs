#warning AudioSwitcher.AudioApi must initialize after NAudio.CoreAudioApi

using NAudio.CoreAudioApi;
using AudioSwitcher.AudioApi;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;
using System.Diagnostics.CodeAnalysis;

namespace VACARM.Application.Services
{
  /// <summary>
  /// The service to manage multiple configurations of system audio device(s). 
  /// Configurations may be from a foreign system or a previous state of the 
  /// current system.
  /// Manages <typeparamref name="DeviceService"/>,
  ///  <typeparamref name="CoreAudioService"/>,
  ///  and <typeparamref name="MMDeviceService"/>.
  /// </summary>
  public partial class DeviceGroupService
    <
      TParentRepository,
      TRepository,
      TDeviceModel
    > :
    IDeviceGroupService
    <
      GroupService
      <
        DeviceService
        <
          BaseRepository<TDeviceModel>,
          TDeviceModel
        >,
        BaseRepository<TDeviceModel>,
        TDeviceModel
      >,
      BaseRepository<TDeviceModel>,
      TDeviceModel
    >
    where TParentRepository :
    GroupService
    <
      DeviceService
      <
        BaseRepository<TDeviceModel>,
        TDeviceModel
      >,
      BaseRepository<TDeviceModel>,
      TDeviceModel
    >
    where TRepository :
    DeviceService
    <
      BaseRepository<TDeviceModel>,
      TDeviceModel
    >
    where TDeviceModel :
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
    public DeviceGroupService() :
      base()
    {
      this.

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
    public DeviceGroupService
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

    #endregion
  }
}