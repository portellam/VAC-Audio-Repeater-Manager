using System;
using System.Threading.Tasks;

namespace VACARM.Infrastructure.Services
{
  public partial class Service
    <
      TEnumerable,
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

    #endregion
  }
}