using System;

namespace VACARM.Infrastructure.Repositories
{
  public partial class BaseRepository<TBaseModel>
  {
    #region Parameters

    public Func<uint, bool> IsValidId
    {
      get
      {
        return new Func<uint, bool>
          (
            x =>
            {
              return x >= MinCount
              && x <= this.MaxCount;
            }
          );
      }
    }

    #endregion
  }
}