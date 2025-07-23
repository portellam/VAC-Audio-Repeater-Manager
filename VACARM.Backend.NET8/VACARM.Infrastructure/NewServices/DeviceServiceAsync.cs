using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Contexts;

namespace VACARM.Infrastructure.Services
{
  public partial class Service
  {
    #region Parameters

    #endregion

    #region Logic

    public async IAsyncEnumerable<bool> MuteAllAsync()
    {
      throw new NotImplementedException();
    }

    public async IAsyncEnumerable<bool> MuteRangeAsync(IEnumerable<uint> idEnumerable)
    {
      throw new NotImplementedException();
    }

    public async IAsyncEnumerable<bool> MuteRangeAsync(uint startId, uint endId)
    {
      throw new NotImplementedException();
    }

    public async IAsyncEnumerable<bool> UnmuteAllAsync()
    {
      throw new NotImplementedException();
    }

    public async IAsyncEnumerable<bool> UnmuteRangeAsync(IEnumerable<uint> idEnumerable)
    {
      throw new NotImplementedException();
    }

    public async IAsyncEnumerable<bool> UnmuteRangeAsync(uint startId, uint endId)
    {
      throw new NotImplementedException();
    }

    public async Task<bool> MuteAsync(uint id)
    {
      throw new NotImplementedException();
    }

    public async Task<bool> SetAsDefaultAsync(uint id)
    {
      throw new NotImplementedException();
    }

    public async Task<bool> SetAsDefaultCommunicationsAsync(uint id)
    {
      throw new NotImplementedException();
    }

    public async Task<bool> SetVolumeAsync(uint id, double? volume)
    {
      throw new NotImplementedException();
    }

    public async Task<bool> UnmuteAsync(uint id)
    {
      throw new NotImplementedException();
    }

    public async Task<bool> UpdateServiceAsync()
    {
      throw new NotImplementedException();
    }

    public async Task<DeviceModel?> GetDefaultCommunicationsAsync(bool isInput, bool isOutput)
    {
      throw new NotImplementedException();
    }

    public async Task<DeviceModel?> GetDefaultConsoleAsync(bool isInput, bool isOutput)
    {
      throw new NotImplementedException();
    }

    public async Task<DeviceModel?> GetDefaultMultimediaAsync(bool isInput, bool isOutput)
    {
      throw new NotImplementedException();
    }

    #endregion
  }
}