using Microsoft.EntityFrameworkCore;
using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Services
{
  public partial class Service
  {
    #region Parameters

    public static readonly int MinId = 0;

    protected IQueryable<TBaseModel?> Queryable
    { 
      get
      {
        var queryable = DbSet
          .AsQueryable();

        queryable = queryable.Include(x => (x as BaseModel).Id);
        return queryable;
      }
    }


    #endregion

    #region Logic

    protected virtual bool Validate(TBaseModel model)
    {
      if (model == null)
      {
        return false;
      }

      if (model.Id < MinId)
      {
        return false;
      }

      return true;
    }

    public virtual async Task<bool> ValidateAsync(int id)
    {
      var link = await GetAsync(id);
      var result = Validate(link);
      return result;
    }

    public async Task<TBaseModel?> GetAsync(int id)
    {
      return await Queryable
        .FirstOrDefaultAsync(l => l.Id == id);
    }

    public async Task<TBaseModel?> GetAsync(Func<TBaseModel, bool> func)
    {
      return await Queryable
        .FirstOrDefaultAsync(x => func(x));
    }

    public async Task<bool>

    #endregion
  }
}