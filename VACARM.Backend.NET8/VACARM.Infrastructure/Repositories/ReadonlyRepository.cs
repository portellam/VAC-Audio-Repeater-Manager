using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using VACARM.Infrastructure.Extensions;

namespace VACARM.Infrastructure.Repositories
{
  public class ReadonlyRepository<TItem> :
    IDisposable,
    IReadonlyRepository<TItem>
    where TItem :
    class
  {
    #region Parameters

    private IEnumerable<TItem> enumerable { get; set; }

    /// <summary>
    /// The enumerable of all <typeparamref name="TItem"/>(s).
    /// </summary>
    protected virtual IEnumerable<TItem> Enumerable
    {
      get
      {
        return this.enumerable;
      }
      set
      {
        this.enumerable = value;
        this.OnPropertyChanged(nameof(Enumerable));
      }
    }

    protected virtual bool HasDisposed { get; set; }

    public virtual bool IsNullOrEmpty
    {
      get
      {
        return IEnumerableExtension<TItem>.IsNullOrEmpty(this.Enumerable);
      }
    }

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

    public virtual event PropertyChangedEventHandler PropertyChanged;

    #endregion

    #region Logic

    /// <summary>
    /// Logs event when property has changed.
    /// </summary>
    /// <param name="propertyName">The property name</param>
    internal void OnPropertyChanged(string propertyName)
    {
      this.PropertyChanged?
        .Invoke
        (
          this,
          new PropertyChangedEventArgs(propertyName)
        );

      Debug.WriteLine
      (
        string.Format
        (
          "PropertyChanged: {0}",
          propertyName
        )
      );
    }

    /// <summary>
    /// Constructor
    /// </summary>
    [ExcludeFromCodeCoverage]
    public ReadonlyRepository()
    {
      this.Enumerable = Array.Empty<TItem>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="enumerable">The enumerable of item(s)</param>
    [ExcludeFromCodeCoverage]
    public ReadonlyRepository(IEnumerable<TItem> enumerable)
    {
      this.Enumerable = enumerable;
    }

    /// <summary>
    /// Dispose of unmanaged objects and true/false dispose of managed objects.
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
        this.Enumerable = null;
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

    public TItem? Get(Func<TItem, bool> func)
    {
      if (this.IsNullOrEmpty)
      {
        return null;
      }

      return this.Enumerable
        .FirstOrDefault(func);
    }

    public IEnumerable<TItem> GetAll()
    {
      if (this.IsNullOrEmpty)
      {
        return Array.Empty<TItem>();
      }

      return this.Enumerable;
    }

    public IEnumerable<TItem> GetRange(Func<TItem, bool> func)
    {
      if (this.IsNullOrEmpty)
      {
        return Array.Empty<TItem>();
      }

      return this.Enumerable
        .Where(x => func(x));
    }

    #endregion
  }
}