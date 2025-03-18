﻿using System;

namespace VACARM.Infrastructure.Repositories
{
  public partial class BaseRepository<TBaseModel> :
    IDisposable
  {
    #region Logic

    protected override void Dispose(bool isDisposed)
    {
      if (this.HasDisposed)
      {
        return;
      }

      if (isDisposed)
      {
        base.Dispose();
        
        this.SelectedIdHashSet
          .Clear();
      }

      this.HasDisposed = true;
    }

    #endregion
  }
}