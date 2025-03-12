#warning Differs from projects of earlier NET revisions (below v4.8).

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VACARM.Infrastructure.Services
{
  public partial class ReadonlyService
    <
      TRepository,
      TItem
    >
  {
    #region Logic

    public async Task<IEnumerable<int?>> DoActionAll
    (Func<TItem, Task<int?>> actionFunc)
    {
      var enumerable = Array.Empty<int?>();

      if (actionFunc == null)
      {
        return enumerable;
      }

      var itemEnumerable = this.Repository
        .GetAll();

      foreach (var item in itemEnumerable)
      {
        if (item == null)
        {
          continue;
        }

        Task<int?> task = Task.Run(() => actionFunc(item));
        await task.ConfigureAwait(false);
        enumerable.Append(task.Result);
      }

      return enumerable;
    }

    public async Task<IEnumerable<int?>> DoActionRange
    (
      Func<TItem, Task<int?>> actionFunc,
      IEnumerable<TItem> itemEnumerable
    )
    {
      var enumerable = Array.Empty<int?>();

      if (actionFunc == null)
      {
        return enumerable;
      }

      if (itemEnumerable.IsNullOrEmpty())
      {
        return enumerable;
      }

      foreach (var item in itemEnumerable)
      {
        if (item == null)
        {
          continue;
        }

        Task<int?> task = Task.Run(() => actionFunc(item));
        await task.ConfigureAwait(false);
        enumerable.Append(task.Result);
      }

      return enumerable;
    }

    public async Task<IEnumerable<int?>> DoActionRange
    (
      Func<TItem, Task<int?>> actionFunc,
      Func<TItem, bool> matchFunc
    )
    {
      var enumerable = Array.Empty<int?>();

      if (actionFunc == null)
      {
        return enumerable;
      }

      if (matchFunc == null)
      {
        return enumerable;
      }

      var itemEnumerable = this.Repository
        .GetRange(matchFunc);

      if (itemEnumerable.IsNullOrEmpty())
      {
        return enumerable;
      }

      foreach (var item in itemEnumerable)
      {
        if (item == null)
        {
          continue;
        }

        Task<int?> task = Task.Run(() => actionFunc(item));
        await task.ConfigureAwait(false);
        enumerable.Append(task.Result);
      }

      return enumerable;
    }

    #endregion
  }
}