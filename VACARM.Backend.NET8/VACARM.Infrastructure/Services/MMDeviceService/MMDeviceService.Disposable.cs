using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace VACARM.Infrastructure.Services
{
  public partial class MMDeviceService<TMMDevice>
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

        this.DefaultCommunicationsRepository
          .Dispose();

        this.DefaultConsoleRepository
          .Dispose();

        this.DefaultMultimediaRepository
          .Dispose();

        this.Repository = null;
        this.DefaultCommunicationsRepository = null;
        this.DefaultConsoleRepository = null;
        this.DefaultMultimediaRepository = null;
        this.HasDisposed = true;
      }

      #endregion
    }
  }
}