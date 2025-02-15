using VACARM.Infrastructure.Extensions;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Services
{
  public partial class Service<TRepository, TItem> :
    IService<Repository<TItem>, TItem> where TRepository :
    Repository<TItem> where TItem :
    class
  {
    #region Logic

    public async Task<bool> DoWorkAsync
    (
      Func<TItem, Task<bool>> actionFunc,
      Func<TItem, bool> matchFunc
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

      var item = this.Repository.Get(matchFunc);

      if (item == null)
      {
        return false;
      }

      return await actionFunc(item)
        .ConfigureAwait(false);
    }

    public async Task<bool> DoWorkAsync
    (
      Func<TItem, Task<bool>> actionFunc,
      TItem item
    )
    {
      if (actionFunc == null)
      {
        return false;
      }

      if (item == null)
      {
        return false;
      }

      return await actionFunc(item)
        .ConfigureAwait(false);
    }

    public async IAsyncEnumerable<bool> DoWorkAllAsync
    (Func<TItem, Task<bool>> actionFunc)
    {
      if (actionFunc == null)
      {
        yield return false;
      }

      foreach (var item in this.Repository.GetAll())
      {
        if (item == null)
        {
          continue;
        }

        Task<bool> task = Task.Run(() => actionFunc(item));
        await task.ConfigureAwait(false);
        yield return task.Result;
      }
    }

    public async IAsyncEnumerable<bool> DoWorkRangeAsync
    (
      Func<TItem, Task<bool>> actionFunc,
      IEnumerable<TItem> enumerable
    )
    {
      if (actionFunc == null)
      {
        yield return false;
      }

      if (IEnumerableExtension<TItem>.IsNullOrEmpty(enumerable))
      {
        yield return false;
      }

      foreach (var item in enumerable)
      {
        if (item == null)
        {
          continue;
        }

        Task<bool> task = Task.Run(() => actionFunc(item));
        await task.ConfigureAwait(false);
        yield return task.Result;
      }
    }

    public async IAsyncEnumerable<bool> DoWorkRangeAsync
    (
      Func<TItem, Task<bool>> actionFunc,
      Func<TItem, bool> matchFunc
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

      var enumerable = this.Repository.GetRange(matchFunc);

      if (IEnumerableExtension<TItem>.IsNullOrEmpty(enumerable))
      {
        yield return false;
      }

      foreach (var item in enumerable)
      {
        if (item == null)
        {
          continue;
        }

        Task<bool> task = Task.Run(() => actionFunc(item));
        await task.ConfigureAwait(false);
        yield return task.Result;
      }
    }

    #endregion
  }
}