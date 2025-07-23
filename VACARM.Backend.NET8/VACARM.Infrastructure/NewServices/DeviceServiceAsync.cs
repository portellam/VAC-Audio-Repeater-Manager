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

    public async IAsyncEnumerable<bool> MuteRangeAsync(IEnumerable<int> idEnumerable)
    {
      throw new NotImplementedException();
    }

    public async IAsyncEnumerable<bool> MuteRangeAsync
    (
      int startId, 
      int endId
    )
    {
      throw new NotImplementedException();
    }

    public async IAsyncEnumerable<bool> UnmuteAllAsync()
    {
      throw new NotImplementedException();
    }

    public async IAsyncEnumerable<bool> UnmuteRangeAsync
    (IEnumerable<int> idEnumerable)
    {
      throw new NotImplementedException();
    }

    public async IAsyncEnumerable<bool> UnmuteRangeAsync
    (
      int startId,
      int endId
    )
    {
      throw new NotImplementedException();
    }

    public async Task<bool> MuteAsync(int id)
    {
      throw new NotImplementedException();
    }

    public async Task<bool> SetAsDefaultAsync(int id)
    {
      throw new NotImplementedException();
    }

    public async Task<bool> SetAsDefaultCommunicationsAsync(int id)
    {
      throw new NotImplementedException();
    }

    public async Task<bool> SetVolumeAsync
    (
      int id, 
      double? volume
    )
    {
      throw new NotImplementedException();
    }

    public async Task<bool> UnmuteAsync(int id)
    {
      throw new NotImplementedException();
    }

    public async Task<bool> UpdateServiceAsync()
    {
      throw new NotImplementedException();
    }

    public async Task<DeviceModel?> GetDefaultCommunicationsAsync
    (
      bool isInput, 
      bool isOutput
    )
    {
      throw new NotImplementedException();
    }

    public async Task<DeviceModel?> GetDefaultConsoleAsync
    (
      bool isInput,
      bool isOutput
    )
    {
      throw new NotImplementedException();
    }

    public async Task<DeviceModel?> GetDefaultMultimediaAsync
    (
      bool isInput,
      bool isOutput
    )
    {
      throw new NotImplementedException();
    }

    #endregion
  }
}