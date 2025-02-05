using VACARM.Infrastructure.Extensions;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Controllers
{
  public partial class GenericListController<T1, T2> :
    IGenericListController<T1, T2> where T1 :
    GenericListRepository<T2> where T2 :
    class
  {
    #region Logic

    public async Task<bool> DoWorkAsync
    (
      Func<T2, Task<bool>> actionFunc,
      Func<T2, bool> matchFunc
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

      var item = Repository.Get(matchFunc);

      if (item == null)
      {
        return false;
      }

      return await actionFunc(item)
        .ConfigureAwait(false);
    }

    public async Task<bool> DoWorkAsync
    (
      Func<T2, Task<bool>> actionFunc,
      T2 item
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
    (Func<T2, Task<bool>> actionFunc)
    {
      if (actionFunc == null)
      {
        yield return false;
      }

      foreach (var item in GetAll())
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
      Func<T2, Task<bool>> actionFunc,
      IEnumerable<T2> enumerable
    )
    {
      if (actionFunc == null)
      {
        yield return false;
      }

      if (IEnumerableExtension<T2>.IsNullOrEmpty(enumerable))
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
      Func<T2, Task<bool>> actionFunc,
      Func<T2, bool> matchFunc
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

      if (IEnumerableExtension<T2>.IsNullOrEmpty(enumerable))
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