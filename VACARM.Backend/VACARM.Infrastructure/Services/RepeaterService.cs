﻿using System.Diagnostics.CodeAnalysis;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Functions;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Services
{
  public partial class RepeaterService<TRepository, TRepeaterModel> :
    BaseService<BaseRepository<TRepeaterModel>, TRepeaterModel>,
    IRepeaterService<BaseRepository<TRepeaterModel>, TRepeaterModel>
    where TRepository :
    BaseRepository<TRepeaterModel> where TRepeaterModel :
    RepeaterModel
  {
    #region Parameters

    private bool preferLegacyExecutable { get; set; } = false;

    private DeviceService<BaseRepository<DeviceModel>, DeviceModel>
      deviceService
    { get; set; } = new DeviceService<BaseRepository<DeviceModel>, DeviceModel>();

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

    public DeviceService<BaseRepository<DeviceModel>, DeviceModel> DeviceService
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
    public RepeaterService() :
      base()
    {
      this.Repository = new BaseRepository<TRepeaterModel>();

      this.DeviceService =
        new DeviceService<BaseRepository<DeviceModel>, DeviceModel>();
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
      BaseRepository<TRepeaterModel> repository,
      DeviceService<BaseRepository<DeviceModel>, DeviceModel> deviceService,
      string customExecutablePathName
    ) :
      base(repository)
    {
      this.Repository = repository;
      this.DeviceService = deviceService;
      this.CustomExecutablePathName = customExecutablePathName;
    }

    public IEnumerable<TRepeaterModel> GetAllAlphabetical()
    {
      return this.Repository
        .GetAll()
        .OrderBy(x => x.WindowName);
    }

    public IEnumerable<TRepeaterModel> GetAllByDeviceId(uint deviceId)
    {
      var func = RepeaterFunctions<TRepeaterModel>.ContainsDeviceId(deviceId);

      return this.Repository
        .GetRange(func);
    }

    public IEnumerable<TRepeaterModel> GetAllByDeviceName(string deviceName)
    {
      var func = RepeaterFunctions<TRepeaterModel>.ContainsDeviceName(deviceName);

      return this.Repository
        .GetRange(func);
    }

    public IEnumerable<TRepeaterModel> GetAllStarted()
    {
      var func = RepeaterFunctions<TRepeaterModel>.IsStarted;

      return this.Repository
        .GetRange(func);
    }

    public IEnumerable<TRepeaterModel> GetAllStopped()
    {
      var func = RepeaterFunctions<TRepeaterModel>.IsStopped;

      return this.Repository
        .GetRange(func);
    }

    #endregion
  }
}