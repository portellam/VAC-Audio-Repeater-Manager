using System;

namespace VACARM.Infrastructure.Repositories
{
  public partial class Repository
    <
      TEnumerable,
      TItem
    > :
    IDisposable
  {
    #region Parameters

    protected virtual bool HasDisposed { get; set; }

    #endregion

    #region Logic

    /// <summary>
    /// Dispose of unmanaged objects and true/false dispose of managed objects.
    /// Should the <typeparamref name="TEnumerable"/> be a
    /// <typeparamref name="Array"/>, it may not dispose of and/or remove its
    /// <typeparamref name="TItem"/>(s).
    /// </summary>
    /// <param name="isDisposed">True/false</param>
    protected virtual void Dispose(bool isDisposed)
    {
      if (this.HasDisposed)
      {
        return;
      }

      if (isDisposed)
      {
        var type = typeof(TEnumerable);

        if (type == typeof(IDisposable))
        {
          (this.Enumerable as IDisposable).Dispose();
        }

        if (type == typeof(IEnumerable<IDisposable>))
        {
          foreach (IDisposable item in this.Enumerable)
          {
            item.Dispose();
          }
        }

        if (type == typeof(ICollection<TItem>))
        {
          (this.Enumerable as ICollection<TItem>).Clear();
        }

        if (type == typeof(IList<TItem>))
        {
          (this.Enumerable as IList<TItem>).Clear();
        }

        if (type == typeof(ISet<TItem>))
        {
          (this.Enumerable as ISet<TItem>).Clear();
        }
      }

      this.HasDisposed = true;
    }

    /// <summary>
    /// Do not change this code. 
    /// Put cleanup code in Dispose(<paramref name="bool"/>
    ///  <typeparamref name="isDisposed"/>) method.
    /// </summary>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize(this);
    }

    #endregion
  }
}