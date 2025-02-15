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

    protected virtual Repository<TItem> Repository
    {
      get
      {
        return this.repository;
      }
      set
      {
        this.repository = value;
        OnPropertyChanged(nameof(Repository));
      }
    }

    public Repository<TItem> ItemRepository
    {
      get
      {
        return this.Repository;
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
      this.Repository = new Repository<TItem>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repository">The repository</param>
    public Service(Repository<TItem> repository)
    {
      this.Repository = repository;
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

      var item = this.ItemRepository
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
      var enumerable = this.ItemRepository
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

      var enumerable = this.ItemRepository
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