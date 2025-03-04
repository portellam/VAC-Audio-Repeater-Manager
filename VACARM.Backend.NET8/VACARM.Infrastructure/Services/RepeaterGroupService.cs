using System.Diagnostics.CodeAnalysis;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Functions;
using VACARM.Infrastructure.Repositories;

//TODO: HashSet of deviceIDs, and names. Allow to be updated?

namespace VACARM.Infrastructure.Services
{
  /// <summary>
  /// The service to manage multiple configurations of audio repeaters. 
  /// Configurations are user-defined,
  /// and may be from a foreign system or a previous state of the current system.
  /// Manages <typeparamref name="DeviceGroupService"/>.
  /// </summary>
  public partial class RepeaterGroupService
    <
      TGroupReadonlyRepository,
      TBaseService,
      TBaseRepository,
      TRepeaterModel
    > :
    BaseGroupService
    <
      ReadonlyRepository
      <
        BaseService
        <
          BaseRepository<TRepeaterModel>,
          TRepeaterModel
        >
      >,
      BaseService
      <
        BaseRepository<TRepeaterModel>,
        TRepeaterModel
      >,
      BaseRepository<TRepeaterModel>,
      TRepeaterModel
    >,
    IRepeaterGroupService
    <
      ReadonlyRepository
      <
        BaseService
        <
          BaseRepository<TRepeaterModel>,
          TRepeaterModel
        >
      >,
      BaseService
      <
        BaseRepository<TRepeaterModel>,
        TRepeaterModel
      >,
      BaseRepository<TRepeaterModel>,
      TRepeaterModel
    >
    where TGroupReadonlyRepository :
    ReadonlyRepository
    <
      BaseService
      <
        BaseRepository<TRepeaterModel>,
        TRepeaterModel
      >
    >
    where TBaseService :
    BaseService
    <
      BaseRepository<TRepeaterModel>,
      TRepeaterModel
    >
    where TBaseRepository :
    BaseRepository<TRepeaterModel>
    where TRepeaterModel :
    RepeaterModel
  {
    #region Parameters

    //TODO: remove or replace?
    private bool preferLegacyExecutable { get; set; } = false;

    private string customExecutablePathName { get; set; } =
      Common.Info.ExpectedExecutablePathName;

    //TODO: remove or replace?
    private string ExecutableName
    {
      get
      {
        if (this.PreferLegacyExecutable)
        {
          return Common.Info.MMEExecutableName;
        }

        return Common.Info.KSExecutableName;
      }
    }

    //TODO: remove or replace?
    public bool PreferLegacyExecutable
    {
      get
      {
        return this.preferLegacyExecutable;
      }
      set
      {
        this.preferLegacyExecutable = value;
        base.OnPropertyChanged(nameof(this.PreferLegacyExecutable));
      }
    }

    public string CustomExecutablePathName
    {
      get
      {
        return this.customExecutablePathName;
      }
      set
      {
        if
        (
          string.IsNullOrEmpty(value)
          || string.IsNullOrWhiteSpace(value)
        )
        {
          value = Common.Info.ExpectedExecutablePathName;
        }

        this.customExecutablePathName = value;
        base.OnPropertyChanged(nameof(this.CustomExecutablePathName));
      }
    }

    public string ExecutableFullPathName
    {
      get
      {
        return CustomExecutablePathName + ExecutableName;
      }
    }

    #endregion

    #region Logic

    // TODO: specify a default file name value? Or generate one given index?

    /// <summary>
    /// Constructor
    /// </summary>
    [ExcludeFromCodeCoverage]
    public RepeaterGroupService() :
      base()
    {
      this.List =
        new List<BaseService<BaseRepository<TRepeaterModel>, TRepeaterModel>>();

      var service = new BaseService<BaseRepository<TRepeaterModel>, TRepeaterModel>
        (
          new BaseRepository<TRepeaterModel>(),
          string.Empty
        );

      base.Add(service);
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="list">The list of service(s)</param>
    /// <param name="maxCount">The maximum count of service(s)</param>
    public RepeaterGroupService
    (
      List<BaseService<BaseRepository<TRepeaterModel>, TRepeaterModel>> list,
      int maxCount
    ) :
      base
      (
        list,
        maxCount
      )
    {
      if (base.IsNullOrEmpty)
      {
        var service = 
          new BaseService<BaseRepository<TRepeaterModel>, TRepeaterModel>
          (
            new BaseRepository<TRepeaterModel>(),
            string.Empty
          );

        base.List
          .Add(service);
      }
    }

    protected override void Dispose(bool isDisposed)
    {
      if (base.HasDisposed)
      {
        return;
      }

      if (isDisposed)
      {
        base.Dispose();
      }

      base.HasDisposed = true;
    }

    public IEnumerable<TRepeaterModel> GetAllAlphabetical()
    {
      return base.SelectedRepository
        .GetAll()
        .OrderBy(x => x.WindowName);
    }

    public IEnumerable<TRepeaterModel> GetAllByDeviceId(uint deviceId)
    {
      var func = RepeaterFunctions<TRepeaterModel>.ContainsDeviceId(deviceId);

      return base.SelectedRepository
        .GetRange(func);
    }

    public IEnumerable<TRepeaterModel> GetAllByDeviceName(string deviceName)
    {
      var func = RepeaterFunctions<TRepeaterModel>.ContainsDeviceName(deviceName);

      return base.SelectedRepository
        .GetRange(func);
    }

    public IEnumerable<TRepeaterModel> GetAllStarted()
    {
      var func = RepeaterFunctions<TRepeaterModel>.IsStarted;

      return base.SelectedRepository
        .GetRange(func);
    }

    public IEnumerable<TRepeaterModel> GetAllStopped()
    {
      var func = RepeaterFunctions<TRepeaterModel>.IsStopped;

      return base.SelectedRepository
        .GetRange(func);
    }

    #endregion
  }
}