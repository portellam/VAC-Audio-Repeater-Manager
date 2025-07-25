using Microsoft.EntityFrameworkCore;
using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Services
{
  public partial class Service :
    IService
  {
    #region Logic

    public async Task<bool> ValidateAsync(int id)
    {
      var link = await this.Context
        .
        .Include(l => l.InputDevice)
        .Include(l => l.OutputDevice)
        .FirstOrDefaultAsync(l => l.Id == id);

      if (link == null) return false;
      if (link.InputDeviceId == link.OutputDeviceId) return false;

      return true;
    }

    public async Task<IBaseModel> CreateAsync()
    {
      // Validate input/output devices
      // Create RepeaterDeviceLink
      // Save changes
      throw new NotImplementedException();
    }

    public async Task<IBaseModel> RemoveAsync()
    {
      throw new NotImplementedException();
    }

    /*
     * TODO:
     * - [ ] use generic objects (TBaseModel) to achieve this here?
     * 
     */

    private async IAsyncEnumerable<BaseModel> GetAllAsync
    (DbSet<TBaseModel> dbSet)
    {
      await foreach (var model in dbSet.AsAsyncEnumerable())
      {
        yield return model;
      }
    }

    public async IAsyncEnumerable<DeviceModel> GetAllAsync()
    {
      await return this.GetAllAsync(this.Context.DeviceDbSet);
    }

    public async Task<int?> DoActionAsync
    (
      Func<IBaseModel, Task<int?>> actionFunc,
      Func<IBaseModel, bool> matchFunc
    )
    {
      throw new NotImplementedException();
    }

    public async Task<int?> DoActionAsync
    (
      Func<IBaseModel, Task<int?>> actionFunc,
      IBaseModel item
    )
    {
      throw new NotImplementedException();
    }

    public async IAsyncEnumerable<int?> DoActionAllAsync
    (Func<IBaseModel, Task<int?>> actionFunc)
    {
      throw new NotImplementedException();
    }

    public async IAsyncEnumerable<int?> DoActionRangeAsync
    (
      Func<IBaseModel, Task<int?>> actionFunc,
      IEnumerable<IBaseModel> enumerable
    )
    {
      throw new NotImplementedException();
    }

    public async IAsyncEnumerable<int?> DoActionRangeAsync
    (
      Func<IBaseModel, Task<int?>> actionFunc,
      Func<IBaseModel, bool> matchFunc
    )
    {
      throw new NotImplementedException();
    }

    #endregion
  }
}