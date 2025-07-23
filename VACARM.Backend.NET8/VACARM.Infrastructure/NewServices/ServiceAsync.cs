using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Contexts;

namespace VACARM.Infrastructure.Services
{
  public partial class Service :
    IService
  {
    #region Logic

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

    public async Task<IBaseModel> ValidateAsync()
    {
      throw new NotImplementedException();
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