using System.ComponentModel;
using System.Diagnostics;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Controllers
{
  public class GenericController<T> : IGenericController<T> where T : class
  {
    #region Parameters

    private IGenericRepository<T> repository { get; set; } =
      new GenericRepository<T>();

    internal IGenericRepository<T> Repository
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

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repository">The repository</param>
    public GenericController(IGenericRepository<T> repository)
    {
      Repository = repository;
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
      Func<T, Task<bool>> actionFunc,
      Func<T, bool> matchFunc
    )
    {
      if (actionFunc == null)
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

      return await actionFunc(t)
        .ConfigureAwait(false);
    }

    public async Task<bool> DoWorkAsync
    (
      Func<T, Task<bool>> actionFunc,
      T t
    )
    {
      if (actionFunc == null)
      {
        return false;
      }

      if (t == null)
      {
        return false;
      }

      return await actionFunc(t)
        .ConfigureAwait(false);
    }

    public async IAsyncEnumerable<bool> DoWorkAllAsync
    (Func<T, Task<bool>> actionFunc)
    {
      if (actionFunc == null)
      {
        yield return false;
      }

      foreach (var t in GetAll())
      {
        if (t == null)
        {
          continue;
        }

        Task<bool> task = Task.Run(() => actionFunc(t));
        await task.ConfigureAwait(false);
        yield return task.Result;
      }
    }

    public async IAsyncEnumerable<bool> DoWorkRangeAsync
    (
      Func<T, Task<bool>> actionFunc,
      IEnumerable<T> enumerable
    )
    {
      if (actionFunc == null)
      {
        yield return false;
      }

      if (enumerable == null)
      {
        yield return false;
      }

      if (enumerable.Count() == 0)
      {
        yield return false;
      }

      foreach (var t in enumerable)
      {
        if (t == null)
        {
          continue;
        }

        Task<bool> task = Task.Run(() => actionFunc(t));
        await task.ConfigureAwait(false);
        yield return task.Result;
      }
    }

    public async IAsyncEnumerable<bool> DoWorkRangeAsync
    (
      Func<T, Task<bool>> actionFunc,
      Func<T, bool> matchFunc
    )
    {
      if (actionFunc == null)
      {
        yield return false;
      }

      if (matchFunc == null)
      {
        yield return false;
      }

      var enumerable = Repository.GetRange(matchFunc);

      if (enumerable == null)
      {
        yield return false;
      }

      if (enumerable.Count() == 0)
      {
        yield return false;
      }

      foreach (var t in enumerable)
      {
        if (t == null)
        {
          continue;
        }

        Task<bool> task = Task.Run(() => actionFunc(t));
        await task.ConfigureAwait(false);
        yield return task.Result;
      }
    }

    public void Add(T t)
    {
      Repository.Add(t);
    }

    public void AddRange(IEnumerable<T> enumerable)
    {
      Repository.AddRange(enumerable);
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

      foreach (var t in GetAll())
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

    public void Remove(T t)
    {
      Repository.Remove(t);
    }

    public void Remove(Func<T, bool> func)
    {
      Repository.Remove(func);
    }

    public void RemoveAll()
    {
      Repository.RemoveAll();
    }

    public void RemoveRange(Func<T, bool> func)
    {
      RemoveRange(func);
    }

    public void RemoveRange(IEnumerable<T> enumerable)
    {
      foreach (var t in enumerable)
      {
        Remove(t);
      }
    }

    #endregion
  }
}