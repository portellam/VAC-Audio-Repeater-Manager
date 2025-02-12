using VACARM.Domain.Models;
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

    private DeviceRepository<DeviceModel> deviceRepository { get; set; }
      = new DeviceRepository<DeviceModel>();

    private DeviceRepository<DeviceModel> DeviceRepository
    {
      get
      {
        return this.deviceRepository;
      }
      set
      {
        deviceRepository = value;
        base.OnPropertyChanged(nameof(DeviceRepository));
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
      this.DeviceRepository = new DeviceRepository<DeviceModel>();
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
    /// <param name="deviceRepository">The device repository</param>
    public RepeaterService
    (
      RepeaterRepository<TRepeaterModel> repository,
      DeviceRepository<DeviceModel> deviceRepository
    )
    {
      base._Repository = repository;
      this.DeviceRepository = deviceRepository;
    }

    public void Restart(uint id)
    {
      throw new NotImplementedException();
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