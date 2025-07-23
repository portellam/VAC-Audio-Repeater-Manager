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

    Task<int?> RestartAsync(uint? id)
    {
      throw new NotImplementedException();
    }

    Task<int?> StartAsync(uint? id)
    {
      throw new NotImplementedException();
    }

    Task<int?> StopAsync(uint? id)
    {
      throw new NotImplementedException();
    }

    IAsyncEnumerable<int?> RestartAllAsync()
    {
      throw new NotImplementedException();
    }

    IAsyncEnumerable<int?> RestartRangeAsync(IEnumerable<uint> idEnumerable)
    {
      throw new NotImplementedException();
    }

    IAsyncEnumerable<int?> RestartRangeAsync
    (
      uint startId, 
      uint endId
    )
    {
      throw new NotImplementedException();
    }

    IAsyncEnumerable<int?> StartAllAsync()
    {
      throw new NotImplementedException();
    }

    IAsyncEnumerable<int?> StartRangeAsync(IEnumerable<uint> idEnumerable)
    {
      throw new NotImplementedException();
    }

    IAsyncEnumerable<int?> StartRangeAsync
    (
      uint startId,
      uint endId
    )
    {
      throw new NotImplementedException();
    }

    IAsyncEnumerable<int?> StopAllAsync()
    {
      throw new NotImplementedException();
    }

    IAsyncEnumerable<int?> StopRangeAsync(IEnumerable<uint> idEnumerable)
    {
      throw new NotImplementedException();
    }

    IAsyncEnumerable<int?> StopRangeAsync
    (
      uint startId,
      uint endId
    )
    {
      throw new NotImplementedException();
    }

    #endregion
  }
}