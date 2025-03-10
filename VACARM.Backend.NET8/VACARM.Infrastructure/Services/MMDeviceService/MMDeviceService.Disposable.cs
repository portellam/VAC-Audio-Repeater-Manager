using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace VACARM.Infrastructure.Services
{
  public partial class MMDeviceService
    <
      TRepository,
      TMMDevice
    >
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