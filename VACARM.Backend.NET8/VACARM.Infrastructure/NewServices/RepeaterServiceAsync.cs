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

    public async Task<int?> RestartAsync(int? id)
    {
      throw new NotImplementedException();
    }

    public async Task<int?> StartAsync(int? id)
    {
      throw new NotImplementedException();
    }

    public async Task<int?> StopAsync(int? id)
    {
      throw new NotImplementedException();
    }

    public async IAsyncEnumerable<int?> RestartAllAsync()
    {
      throw new NotImplementedException();
    }

    public async IAsyncEnumerable<int?> RestartRangeAsync
    (IEnumerable<int> idEnumerable)
    {
      throw new NotImplementedException();
    }

    public async IAsyncEnumerable<int?> RestartRangeAsync
    (
      int startId, 
      int endId
    )
    {
      throw new NotImplementedException();
    }

    public async IAsyncEnumerable<int?> StartAllAsync()
    {
      throw new NotImplementedException();
    }

    public async IAsyncEnumerable<int?> StartRangeAsync
    (IEnumerable<int> idEnumerable)
    {
      throw new NotImplementedException();
    }

    public async IAsyncEnumerable<int?> StartRangeAsync
    (
      int startId,
      int endId
    )
    {
      throw new NotImplementedException();
    }

    public async IAsyncEnumerable<int?> StopAllAsync()
    {
      throw new NotImplementedException();
    }

    public async IAsyncEnumerable<int?> StopRangeAsync
    (IEnumerable<int> idEnumerable)
    {
      throw new NotImplementedException();
    }

    public async IAsyncEnumerable<int?> StopRangeAsync
    (
      int startId,
      int endId
    )
    {
      throw new NotImplementedException();
    }

    #endregion
  }
}