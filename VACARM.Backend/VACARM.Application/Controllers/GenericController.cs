using System.ComponentModel;
using System.Diagnostics;
using VACARM.Infrastructure.Extensions;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Controllers
{
  public partial class GenericController<T1, T2> :
    IGenericController<T1, T2> where T1 :
    GenericRepository<T2> where T2 :
    class
  {
    #region Parameters

    private GenericRepository<T2> repository { get; set; } =
      new GenericRepository<T2>();

    internal virtual GenericRepository<T2> Repository
    {
      get
      {
        return repository;
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
    public GenericController()
    {
      Repository = new GenericRepository<T2>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repository">The repository</param>
    public GenericController(GenericRepository<T2> repository)
    {
      Repository = repository;
    }

    public T2? Get(Func<T2, bool> func)
    {
      return Repository.Get(func);
    }

    public IEnumerable<T2> GetAll()
    {
      return Repository.GetAll();
    }

    public IEnumerable<T2> GetRange(Func<T2, bool> func)
    {
      return Repository.GetRange(func);
    }

    public void Add(T2 item)
    {
      Repository.Add(item);
    }

    public void AddRange(IEnumerable<T2> enumerable)
    {
      Repository.AddRange(enumerable);
    }

    public void DoWork
    (
      Action<T2> action,
      Func<T2, bool> func
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
      Action<T2> action,
      T2 item
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

    public void DoWorkAll(Action<T2> action)
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
      Action<T2> action,
      IEnumerable<T2> enumerable
    )
    {
      if (IEnumerableExtension<T2>.IsNullOrEmpty(enumerable))
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
      Action<T2> action,
      Func<T2, bool> func
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

    public void Remove(T2 item)
    {
      Repository.Remove(item);
    }

    public void RemoveAll()
    {
      Repository.RemoveAll();
    }

    public void RemoveRange(Func<T2, bool> func)
    {
      RemoveRange(func);
    }

    public void RemoveRange(IEnumerable<T2> enumerable)
    {
      Repository.RemoveRange(enumerable);
    }

    #endregion
  }
}