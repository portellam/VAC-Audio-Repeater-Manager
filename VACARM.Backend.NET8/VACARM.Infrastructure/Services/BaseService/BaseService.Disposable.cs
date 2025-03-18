using System;

namespace VACARM.Infrastructure.Services
{
  public partial class BaseService<TBaseModel> :
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

        this.Repository
          .Dispose();

        this.Repository = null;
      }

      this.HasDisposed = true;
    }

    #endregion
  }
}