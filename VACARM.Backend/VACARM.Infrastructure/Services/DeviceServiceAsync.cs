using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;

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
    #region Logic

    public Task<DeviceModel?> GetDefaultCommunications(bool isInput, bool isOutput)
    {
      throw new NotImplementedException();
    }

    public Task<DeviceModel?> GetDefaultConsole(bool isInput, bool isOutput)
    {
      throw new NotImplementedException();
    }

    public Task<DeviceModel?> GetDefaultMultimedia(bool isInput, bool isOutput)
    {
      throw new NotImplementedException();
    }

    public Task<bool> Mute(uint id)
    {
      throw new NotImplementedException();
    }

    public Task<bool> MuteAll()
    {
      throw new NotImplementedException();
    }

    public Task<bool> MuteRange(uint startId, uint endId)
    {
      throw new NotImplementedException();
    }

    public Task<bool> Restart(uint id)
    {
      throw new NotImplementedException();
    }

    public Task<bool> RestartAll()
    {
      throw new NotImplementedException();
    }

    public Task<bool> RestartRange(uint startId, uint endId)
    {
      throw new NotImplementedException();
    }

    public Task<bool> RestartRange(List<uint> idList)
    {
      throw new NotImplementedException();
    }

    public Task<bool> SetAsDefault(uint id)
    {
      throw new NotImplementedException();
    }

    public Task<bool> SetAsDefaultCommunications(uint id)
    {
      throw new NotImplementedException();
    }

    public Task<bool> SetVolume(string id, double? volume)
    {
      throw new NotImplementedException();
    }

    public Task<bool> Start(uint id)
    {
      throw new NotImplementedException();
    }

    public Task<bool> StartAll()
    {
      throw new NotImplementedException();
    }

    public Task<bool> StartRange(uint startId, uint endId)
    {
      throw new NotImplementedException();
    }

    public Task<bool> StartRange(List<uint> idList)
    {
      throw new NotImplementedException();
    }

    public Task<bool> Stop(uint id)
    {
      throw new NotImplementedException();
    }

    public Task<bool> StopAll()
    {
      throw new NotImplementedException();
    }

    public Task<bool> StopRange(uint startId, uint endId)
    {
      throw new NotImplementedException();
    }

    public Task<bool> StopRange(List<uint> idList)
    {
      throw new NotImplementedException();
    }

    public Task<bool> Unmute(uint id)
    {
      throw new NotImplementedException();
    }

    public Task<bool> UnmuteAll()
    {
      throw new NotImplementedException();
    }

    public Task<bool> UnmuteRange(uint startId, uint endId)
    {
      throw new NotImplementedException();
    }

    public Task<bool> Update(uint id)
    {
      throw new NotImplementedException();
    }

    public Task<bool> UpdateAll()
    {
      throw new NotImplementedException();
    }

    public Task<bool> UpdateRange(uint startId, uint endId)
    {
      throw new NotImplementedException();
    }

    public Task<bool> UpdateRange(List<uint> idList)
    {
      throw new NotImplementedException();
    }

    #endregion
  }
}