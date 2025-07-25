using Microsoft.EntityFrameworkCore;
using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Services
{
  public partial class Service
  {
    #region Parameters

    protected new IQueryable<RepeaterModel?> Queryable
    {
      get
      {
        var queryable = base.Queryable;
        queryable = queryable.Include(x => (x as RepeaterModel).LinkId);
        return queryable;
      }
    }

    #endregion

    #region Logic

    protected virtual bool Validate(RepeaterModel model)
    {
      if (!base.Validate(model))
      {
        return false;
      }

      if ((model as RepeaterModel).LinkId < MinId)
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

    public async Task<RepeaterModel?> GetAsyncByLinkId(int linkId)
    {
      var func = new Func<RepeaterModel?, bool>(x => x.LinkId == linkId);

      return await GetAsync(func);
    }

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