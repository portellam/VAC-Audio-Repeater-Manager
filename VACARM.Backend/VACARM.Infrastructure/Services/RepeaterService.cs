using System.Diagnostics.CodeAnalysis;
using VACARM.Application.Commands;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Functions;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Services
{
  /// <summary>
  /// The service of the <typeparamref name="RepeaterRepository"/>.
  /// </summary>
  public partial class RepeaterService<TRepository, TRepeaterModel> :
    BaseService<RepeaterRepository<TRepeaterModel>, TRepeaterModel>,
    IRepeaterService<RepeaterRepository<TRepeaterModel>, TRepeaterModel>
    where TRepository :
    RepeaterRepository<TRepeaterModel> where TRepeaterModel :
    RepeaterModel
  {
    #region Parameters

    private bool preferLegacyExecutable { get; set; } = false;

    private DeviceService<DeviceRepository<DeviceModel>, DeviceModel>
      deviceService
    { get; set; } = new DeviceService<DeviceRepository<DeviceModel>, DeviceModel>();

    private string customExecutablePathName { get; set; } =
      Common.Info.ExpectedExecutablePathName;

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
    
    public bool PreferLegacyExecutable
    {
      get
      {
        return this.preferLegacyExecutable;
      }
      set
      {
        this.preferLegacyExecutable = value;
        this.OnPropertyChanged(nameof(PreferLegacyExecutable));
      }
    }

    public DeviceService<DeviceRepository<DeviceModel>, DeviceModel> DeviceService
    {
      get
      {
        return this.deviceService;
      }
      private set
      {
        this.deviceService = value;
        base.OnPropertyChanged(nameof(DeviceService));
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
        this.OnPropertyChanged(nameof(CustomExecutablePathName));
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

    /// <summary>
    /// Constructor
    /// </summary>
    [ExcludeFromCodeCoverage]
    public RepeaterService()
    {
      base.Repository = new RepeaterRepository<TRepeaterModel>();
      
      this.DeviceService = 
        new DeviceService<DeviceRepository<DeviceModel>, DeviceModel>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repository">The repository</param>
    /// <param name="deviceService">The device service</param>
    /// <param name="customExecutablePathName">The custom executable path name
    /// </param>
    [ExcludeFromCodeCoverage]
    public RepeaterService
    (
      RepeaterRepository<TRepeaterModel> repository,
      DeviceService<DeviceRepository<DeviceModel>, DeviceModel> deviceService,
      string customExecutablePathName
    )
    {
      base.Repository = repository;
      this.DeviceService = deviceService;
      this.CustomExecutablePathName = customExecutablePathName;
    }

    #endregion
  }
}