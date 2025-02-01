using System;
using System.ComponentModel;
using System.Diagnostics;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Controllers
{
  public class GenericController<T> : IGenericController<T>
  {
    #region Parameters

    private IGenericRepository<T> repository { get; set; } = new GenericRepository<T>();

    public IGenericRepository<T> Repository
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
    public GenericController()
    {
      Repository = new GenericRepository<T>();
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

    public async Task<bool> DoWorkAsync
    (
      Func<T, Task<bool>> actionfunc,
      Func<T, bool> matchFunc
    )
    {
      if (actionfunc == null)
      {
        return false;
      }

      if (matchFunc == null)
      {
        return false;
      }

      var t = Repository.Get(matchFunc);

      if (t == null)
      {
        return false;
      }

      return await actionfunc(t)
        .ConfigureAwait(false);
    }

    public async Task<bool> DoWorkAsync
    (
      Func<T, Task<bool>> actionfunc,
      T t
    )
    {
      if (actionfunc == null)
      {
        return false;
      }

      if (t == null)
      {
        return false;
      }

      return await actionfunc(t)
        .ConfigureAwait(false);
    }

    public void DoWork
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

      DoWork
      (
        action,
        t
      );
    }

    public void DoWork
    (
      Action<T> action,
      T t
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

    public void DoWorkAll(Action<T> action)
    {
      if (action == null)
      {
        return;
      }

      foreach (var t in Repository.GetAll())
      {
        DoWork
        (
          action,
          t
        );
      }
    }

    public void DoWorkRange
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
        DoWork
        (
          action,
          t
        );
      }
    }

    public void DoWorkRange
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

      DoWorkRange
      (
        action,
        Repository.GetRange(func)
      );
    }

    #endregion
  }
}