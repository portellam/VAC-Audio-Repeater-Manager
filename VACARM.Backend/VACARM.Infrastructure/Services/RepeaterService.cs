﻿using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Services
{
  /// <summary>
  /// The service of the <typeparamref name="RepeaterRepository"/>.
  /// </summary>
  public class RepeaterService<TRepository, TRepeaterModel> :
    BaseService<RepeaterRepository<TRepeaterModel>, TRepeaterModel>,
    IRepeaterService<RepeaterRepository<TRepeaterModel>, TRepeaterModel>
    where TRepository :
    RepeaterRepository<TRepeaterModel> where TRepeaterModel :
    RepeaterModel
  {
    #region Parameters

    private DeviceService<DeviceRepository<DeviceModel>, DeviceModel>
      deviceService
    { get; set; } = new DeviceService<DeviceRepository<DeviceModel>, DeviceModel>();

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

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    public RepeaterService()
    {
      base._Repository = new RepeaterRepository<TRepeaterModel>();
      
      this.DeviceService = 
        new DeviceService<DeviceRepository<DeviceModel>, DeviceModel>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repository">The repository</param>
    public RepeaterService(RepeaterRepository<TRepeaterModel> repository)
    {
      base._Repository = repository;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repository">The repository</param>
    /// <param name="deviceService">The device service</param>
    public RepeaterService
    (
      RepeaterRepository<TRepeaterModel> repository,
      DeviceService<DeviceRepository<DeviceModel>, DeviceModel> deviceService
    )
    {
      base._Repository = repository;
      this.DeviceService = deviceService;
    }

    public void Restart(uint id)
    {
      TRepeaterModel? model = base._Repository.Get(id);
    }

    public void RestartAll()
    {
      throw new NotImplementedException();
    }

    public void RestartRange
    (
      uint startId,
      uint endId
    )
    {
      throw new NotImplementedException();
    }

    public void RestartRange(List<uint> idList)
    {
      throw new NotImplementedException();
    }

    public void Start(uint id)
    {
      throw new NotImplementedException();
    }

    public void StartAll()
    {
      throw new NotImplementedException();
    }

    public void StartRange
    (
      uint startId,
      uint endId
    )
    {
      throw new NotImplementedException();
    }

    public void StartRange(List<uint> idList)
    {
      throw new NotImplementedException();
    }

    public void Stop(uint id)
    {
      throw new NotImplementedException();
    }

    public void StopAll()
    {
      throw new NotImplementedException();
    }

    public void StopRange
    (
      uint startId,
      uint endId
    )
    {
      throw new NotImplementedException();
    }

    public void StopRange(List<uint> idList)
    {
      throw new NotImplementedException();
    }

    #endregion
  }
}