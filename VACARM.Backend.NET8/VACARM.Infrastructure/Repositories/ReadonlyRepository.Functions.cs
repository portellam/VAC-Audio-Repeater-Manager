﻿using System;
using System.Linq;

namespace VACARM.Infrastructure.Repositories
{
  public partial class ReadonlyRepository<TItem>
  {
    #region Parameters

    public Func<int, bool> ContainsIndex
    {
      get
      {
        return new Func<int, bool>
          (
            x =>
            {
              return x >= 0
                && x <= this.Enumerable
                  .Count();
            }
          );
      }
    }

    #endregion
  }
}