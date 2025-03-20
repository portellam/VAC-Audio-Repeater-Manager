using System;

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
        foreach (IDisposable item in this.Enumerable)
        {
          item.Dispose();
        }

        this.Enumerable = Structs.BaseRepository<TBaseModel>.EmptyEnumerable;
        
        this.SelectedIdEnumerable
          .Clear();
      }

      this.HasDisposed = true;
    }

    #endregion
  }
}