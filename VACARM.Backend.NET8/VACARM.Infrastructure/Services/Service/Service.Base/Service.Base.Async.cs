using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Services
{
  public partial class Service
  {
    #region Parameters

    public static readonly int MinId = 0;

    private static readonly Expression
      <
        Func
        <
          BaseModel,
          object
        >
      > BaseSelector =
     x => x.Id;

    #endregion

      #region Logic

    private IQueryable<BaseModel?> Queryable(ref DbSet<BaseModel> dbSet)
    {
      var queryable = dbSet.AsQueryable();
      queryable = queryable.Include(Selector);
      return queryable;
    }

    #endregion
  }
}