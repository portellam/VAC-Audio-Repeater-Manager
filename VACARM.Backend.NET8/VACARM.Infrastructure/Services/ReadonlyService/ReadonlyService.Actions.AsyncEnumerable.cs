using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VACARM.Extensions;

namespace VACARM.Infrastructure.Services
{
  public partial class ReadonlyService
    <
      TRepository,
      TItem
    >
  {
    #region Logic

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