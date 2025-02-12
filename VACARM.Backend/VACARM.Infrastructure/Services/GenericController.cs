using System.ComponentModel;
using System.Diagnostics;
using VACARM.Infrastructure.Extensions;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Services
{
  public partial class GenericService<TRepository, TItem> :
    IGenericService<TRepository, TItem> where TRepository :
    GenericRepository<TItem> where TItem :
    class
  {
    #region Parameters

    private GenericRepository<TItem> repository { get; set; } =
      new GenericRepository<TItem>();

    internal virtual TRepository Repository
    {
      get
      {
        return (TRepository)repository;
      }
      set
      {
        repository = value;
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
      PropertyChanged?.Invoke
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
    public GenericService()
    {
      Repository = new GenericRepository<TItem>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repository">The repository</param>
    public GenericService(GenericRepository<TItem> repository)
    {
      Repository = repository;
    }

    public TItem? Get(Func<TItem, bool> func)
    {
      return Repository.Get(func);
    }

    public IEnumerable<TItem> GetAll()
    {
      return Repository.GetAll();
    }

    public IEnumerable<TItem> GetRange(Func<TItem, bool> func)
    {
      return Repository.GetRange(func);
    }

    public void Add(TItem item)
    {
      Repository.Add(item);
    }

    public void AddRange(IEnumerable<TItem> enumerable)
    {
      Repository.AddRange(enumerable);
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

      var item = Repository.Get(func);

      DoWork
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

      foreach (var item in GetAll())
      {
        DoWork
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
        DoWork
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

      DoWorkRange
      (
        action,
        Repository.GetRange(func)
      );
    }

    public void Remove(TItem item)
    {
      Repository.Remove(item);
    }

    public void RemoveAll()
    {
      Repository.RemoveAll();
    }

    public void RemoveRange(Func<TItem, bool> func)
    {
      RemoveRange(func);
    }

    public void RemoveRange(IEnumerable<TItem> enumerable)
    {
      Repository.RemoveRange(enumerable);
    }

    #endregion
  }
}