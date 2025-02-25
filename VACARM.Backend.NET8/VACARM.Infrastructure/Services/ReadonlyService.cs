using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using VACARM.Infrastructure.Extensions;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Services
{
  /// <summary>
  /// The readonly service for <typeparamref name="TRepository"/>.
  /// </summary>
  public partial class ReadonlyService
    <
      TRepository,
      TItem
    > :
    IDisposable,
    IReadonlyService
    <
      ReadonlyRepository<TItem>,
      TItem
    >
    where TRepository :
    ReadonlyRepository<TItem>
    where TItem :
    class
  {
    #region Parameters

    private ReadonlyRepository<TItem> readonlyRepository { get; set; } =
      new ReadonlyRepository<TItem>();

    protected virtual bool HasDisposed { get; set; }

    protected virtual ReadonlyRepository<TItem> Repository
    {
      get
      {
        return this.readonlyRepository;
      }
      set
      {
        this.readonlyRepository = value;
        OnPropertyChanged(nameof(Repository));
      }
    }

    public virtual event PropertyChangedEventHandler PropertyChanged;

    #endregion

    #region Logic

    /// <summary>
    /// Logs event when property has changed.
    /// </summary>
    /// <param name="propertyName">The property name</param>
    internal virtual void OnPropertyChanged(string propertyName)
    {
      this
        .PropertyChanged?
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
    public ReadonlyService()
    {
      this.Repository = new ReadonlyRepository<TItem>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repository">The repository</param>
    public ReadonlyService(ReadonlyRepository<TItem> repository)
    {
      this.Repository = repository;
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
        this.Repository
          .Dispose();

        this.Repository = null;
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

    public void DoWork
    (
      Action<TItem> action,
      Func<TItem, bool> func
    )
    {
      if (func == null)
      {
        return;
      }

      var item = this.Repository
        .Get(func);

      this.DoWork
        (
          action,
          item
        );
    }

    public void DoWork
    (
      Action<TItem> action,
      TItem item
    )
    {
      if (action == null)
      {
        return;
      }

      if (item == null)
      {
        return;
      }

      action(item);
    }

    public void DoWorkAll(Action<TItem> action)
    {
      var enumerable = this.Repository
        .GetAll();

      this.DoWorkRange
        (
          action,
          enumerable
        );
    }

    public void DoWorkRange
    (
      Action<TItem> action,
      IEnumerable<TItem> enumerable
    )
    {
      if (action == null)
      {
        return;
      }

      if (IEnumerableExtension<TItem>.IsNullOrEmpty(enumerable))
      {
        return;
      }

      foreach (var item in enumerable)
      {
        action(item);
      }
    }

    public void DoWorkRange
    (
      Action<TItem> action,
      Func<TItem, bool> func
    )
    {
      if (func == null)
      {
        return;
      }

      var enumerable = this.Repository
        .GetRange(func);

      this.DoWorkRange
      (
        action,
        enumerable
      );
    }

    #endregion
  }
}