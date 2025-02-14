using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using VACARM.Infrastructure.Extensions;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Services
{
  public partial class Service<TRepository, TItem> :
    IService<Repository<TItem>, TItem> where TRepository :
    Repository<TItem> where TItem :
    class
  {
    #region Parameters

    private Repository<TItem> repository { get; set; } =
      new Repository<TItem>();

    protected virtual Repository<TItem> WritableRepository
    {
      get
      {
        return this.repository;
      }
      set
      {
        this.repository = value;
        OnPropertyChanged(nameof(WritableRepository));
      }
    }

    public Repository<TItem> Repository
    {
      get
      {
        return this.WritableRepository;
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
    public Service()
    {
      this.WritableRepository = new Repository<TItem>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repository">The repository</param>
    public Service(Repository<TItem> repository)
    {
      this.WritableRepository = repository;
    }

    public void DoWork
    (
      Action<TItem> action,
      Func<TItem, bool> func
    )
    {
      if (action == null)
      {
        return;
      }

      if (func == null)
      {
        return;
      }

      TItem? item = this
        .Repository
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
      if (action == null)
      {
        return;
      }

      IEnumerable<TItem> enumerable = this
        .Repository
        .GetAll();

      foreach (var item in enumerable)
      {
        this.DoWork
        (
          action,
          item
        );
      }
    }

    public void DoWorkRange
    (
      Action<TItem> action,
      IEnumerable<TItem> enumerable
    )
    {
      if (IEnumerableExtension<TItem>.IsNullOrEmpty(enumerable))
      {
        return;
      }

      foreach (var item in enumerable)
      {
        this.DoWork
        (
          action,
          item
        );
      }
    }

    public void DoWorkRange
    (
      Action<TItem> action,
      Func<TItem, bool> func
    )
    {
      if (action == null)
      {
        return;
      }

      if (func == null)
      {
        return;
      }

      this.DoWorkRange
      (
        action,
        this.Repository.GetRange(func)
      );
    }

    #endregion
  }
}