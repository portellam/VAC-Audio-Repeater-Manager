using Microsoft.EntityFrameworkCore;
using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Services
{
  public partial class Service
  {
    #region Parameters

    protected new IQueryable<DeviceModel?> Queryable
    {
      get
      {
        var queryable = base.Queryable;
        queryable = queryable.Include(x => (x as DeviceModel).ActualId);
        return queryable;
      }
    }

    #endregion

    #region Logic

    protected virtual bool Validate(DeviceModel model)
    {
      if (!base.Validate(model))
      {
        return false;
      }

      if (string.IsNullOrWhiteSpace((model as DeviceModel).ActualId))
      {
        return false;
      }

      return true;
    }

    public override async Task<bool> ValidateAsync(int id)
    {
      var link = await GetAsync(id);
      var result = Validate(link);
      return result;
    }

    public async Task<DeviceModel?> GetAsync(string actualId)
    {
      var func = new Func<DeviceModel?, bool>(x => x.ActualId == actualId);
      return await GetAsync(func);
    }

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