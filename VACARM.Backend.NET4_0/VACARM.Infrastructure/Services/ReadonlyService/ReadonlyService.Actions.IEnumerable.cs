#warning Differs from projects of later NET revisions (above Framework 4.0).

using MoreLinq;
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
      var enumerable = ArrayExtension.Empty<int?>();

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

        var task = await Task.Factory
          .StartNew(() => actionFunc(item))
          .ConfigureAwait(false);

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
      var enumerable = ArrayExtension.Empty<int?>();

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

        var task = await Task.Factory
          .StartNew(() => actionFunc(item))
          .ConfigureAwait(false);

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
      var enumerable = ArrayExtension.Empty<int?>();

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

        var task = await Task.Factory
           .StartNew(() => actionFunc(item))
           .ConfigureAwait(false);

        enumerable.Append(task.Result);
      }

      return enumerable;
    }

    #endregion
  }
}