using VACARM.Infrastructure.Extensions;

namespace VACARM.Infrastructure.Services
{
  public partial class ReadonlyService
    <
      TRepository, 
      TItem
    >
  {
    #region Logic

    public async Task<int?> DoActionAsync
    (
      Func<TItem, Task<int?>> actionFunc,
      Func<TItem, bool> matchFunc
    )
    {
      int? result = 1;

      if (actionFunc == null)
      {
        return result;
      }

      if (matchFunc == null)
      {
        return result;
      }

      var item = this.Repository
        .Get(matchFunc);

      if (item == null)
      {
        return result;
      }

      result = await actionFunc(item)
        .ConfigureAwait(false);

      return result;
    }

    public async Task<int?> DoActionAsync
    (
      Func<TItem, Task<int?>> actionFunc,
      TItem item
    )
    {
      int? result = 1;

      if (actionFunc == null)
      {
        return result;
      }

      if (item == null)
      {
        return result;
      }

      result = await actionFunc(item)
        .ConfigureAwait(false);

      return result;
    }

    public async IAsyncEnumerable<int?> DoActionAllAsync
    (Func<TItem, Task<int?>> actionFunc)
    {
      int? result = 1;

      if (actionFunc == null)
      {
        yield return result;
      }

      var enumerable = this.Repository
        .GetAll();

      foreach (var item in enumerable)
      {
        if (item == null)
        {
          continue;
        }

        Task<int?> task = Task.Run(() => actionFunc(item));
        await task.ConfigureAwait(false);
        result = task.Result;
        yield return task.Result;
      }
    }

    public async IAsyncEnumerable<int?> DoActionRangeAsync
    (
      Func<TItem, Task<int?>> actionFunc,
      IEnumerable<TItem> enumerable
    )
    {
      int? result = 1;

      if (actionFunc == null)
      {
        yield return result;
      }

      if (IEnumerableExtension<TItem>.IsNullOrEmpty(enumerable))
      {
        yield return result;
      }

      foreach (var item in enumerable)
      {
        if (item == null)
        {
          continue;
        }

        Task<int?> task = Task.Run(() => actionFunc(item));
        await task.ConfigureAwait(false);
        result = task.Result;
        yield return task.Result;
      }
    }

    public async IAsyncEnumerable<int?> DoActionRangeAsync
    (
      Func<TItem, Task<int?>> actionFunc,
      Func<TItem, bool> matchFunc
    )
    {
      int? result = 1;

      if (actionFunc == null)
      {
        yield return result;
      }

      if (matchFunc == null)
      {
        yield return result;
      }

      var enumerable = this.Repository
        .GetRange(matchFunc);

      if (IEnumerableExtension<TItem>.IsNullOrEmpty(enumerable))
      {
        yield return result;
      }

      foreach (var item in enumerable)
      {
        if (item == null)
        {
          continue;
        }

        Task<int?> task = Task.Run(() => actionFunc(item));
        await task.ConfigureAwait(false);
        result = task.Result;
        yield return task.Result;
      }
    }

    #endregion
  }
}