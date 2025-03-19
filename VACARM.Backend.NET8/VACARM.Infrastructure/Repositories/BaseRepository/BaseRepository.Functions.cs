using System;

namespace VACARM.Infrastructure.Repositories
{
  public partial class BaseRepository<TBaseModel>
  {
    #region Parameters

    public override Func<int, bool> IsValidIndex
    {
      get
      {
        return new Func<int, bool>
          (
            x =>
            {
              return x >= 0
              && x <= this.MaxCount;
            }
          );
      }
    }

    #endregion
  }
}