using System.ComponentModel;
using System.Diagnostics;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Controllers
{
  public class Controller<T> : IController<T>
  {
    #region Parameters

    private IRepository<T> repository { get; set; } = new Repository<T>();

    public IRepository<T> Repository
    {
      get
      {
        return repository;
      }
      set
      {
        repository = value;
        OnPropertyChanged(nameof(repository));
      }
    }

    public virtual event PropertyChangedEventHandler PropertyChanged;

    #endregion

    #region Logic

    /// <summary>
    /// Logs event when property has changed.
    /// </summary>
    /// <param name="propertyName">The property name</param>
    private void OnPropertyChanged(string propertyName)
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
    public Controller()
    {
      Repository = new Repository<T>();
    }

    public T? Get(Func<T, bool> func)
    {
      return Repository.Get(func);
    }

    public IEnumerable<T> GetAll()
    {
      return Repository.GetAll();
    }

    public IEnumerable<T> GetRange(Func<T, bool> func)
    {
      return Repository.GetRange(func);
    }

    public IQueryable<T> Queryable()
    {
      return Repository.Queryable();
    }

    public void Do
    (
      Action<T> action,
      Func<T, bool> func
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

      var t = Repository.Get(func);

      Do
      (
        action,
        t
      );
    }

    public void Do
    (
      Action<T> action,
      T? t
    )
    {
      if (action == null)
      {
        return;
      }

      if (t == null)
      {
        return;
      }

      action(t);
    }

    public void DoAll(Action<T> action)
    {
      if (action == null)
      {
        return;
      }

      foreach (var t in Repository.GetAll())
      {
        Do
        (
          action,
          t
        );
      }
    }

    public void DoRange
    (
      Action<T> action,
      IEnumerable<T> enumerable
    )
    {
      if (enumerable == null)
      {
        return;
      }

      if (enumerable.Count() == 0)
      {
        return;
      }

      foreach (var t in enumerable)
      {
        Do
        (
          action,
          t
        );
      }
    }

    public void DoRange
    (
      Action<T> action,
      Func<T, bool> func
    )
    {
      if (action is null)
      {
        return;
      }

      if (func is null)
      {
        return;
      }

      DoRange
      (
        action,
        Repository.GetRange(func)
      );
    }

    #endregion
  }
}