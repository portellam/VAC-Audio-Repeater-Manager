using System.Diagnostics.CodeAnalysis;
using VACARM.Infrastructure.Extensions;

namespace VACARM.Infrastructure.Repositories
{
  /// <summary>
  /// The <typeparamref name="List"/> repository.
  /// </summary>
  public class GenericListRepository<TItem> :
    GenericRepository<TItem>,
    IGenericListRepository<TItem> where TItem :
    class
  {
    #region Parameters

    /// <summary>
    /// The <typeparamref name="List"/> of all <typeparamref name="TItem"/> item(s).
    /// </summary>
    internal virtual IList<TItem> List
    {
      get
      {
        return base.Enumerable.ToList();
      }
      set
      {
        base.Enumerable = value;
        base.OnPropertyChanged(nameof(List));
      }
    }

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    [ExcludeFromCodeCoverage]
    public GenericListRepository()
    {
      List = new List<TItem>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="list">The list of item(s)</param>
    [ExcludeFromCodeCoverage]
    public GenericListRepository(List<TItem> list)
    {
      List = list;
    }

    public override void Add(TItem? item)
    {
      if (item == null)
      {
        return;
      }

      if (List.Contains(item))
      {
        return;
      }

      List.Add(item);
    }

    public TItem? Get(int index)
    {
      return List.ElementAt(index);
    }

    public int? GetIndex(Func<TItem, bool> func)
    {
      TItem? item = Get(func);

      if (item == null)
      {
        return null;
      }

      return List.IndexOf(item);
    }

    public int? GetIndex(TItem item)
    {
      return List.IndexOf(item);
    }

    public IEnumerable<int> GetIndexRange(Func<TItem, bool> func)
    {
      if (func == null)
      {
        yield break;
      }

      IEnumerable<TItem> enumerable = GetRange(func);

      if (IsNullOrEmpty(enumerable))
      {
        yield break;
      }

      foreach (var item in enumerable)
      {
        var index = GetIndex(item);

        if (index == null)
        {
          continue;
        }

        yield return (int)index;
      }
    }

    public IEnumerable<TItem> GetRange(IEnumerable<int> indexEnumerable)
    {
      if (IEnumerableExtension<int>.IsNullOrEmpty(indexEnumerable))
      {
        yield break;
      }

      foreach (int index in indexEnumerable)
      {
        TItem? item = Get(index);

        if (item == null)
        {
          continue;
        }

        yield return item;
      }
    }

    public IEnumerable<TItem> GetRange
    (
      int startIndex,
      int endIndex
    )
    {
      if (IsValidIndex(startIndex))
      {
        yield break;
      }

      if (IsValidIndex(endIndex))
      {
        yield break;
      }

      if (startIndex > endIndex)
      {
        yield break;
      }

      for (int index = startIndex; index <= endIndex; index++)
      {
        TItem? item = Get(index);

        if (item == null)
        {
          continue;
        }

        yield return item;
      }
    }

    public void Insert
    (
      int index,
      TItem item
    )
    {
      if (List == null)
      {
        List = new List<TItem>();
      }

      if (List.Count() <= MaxCount)
      {
        return;
      }

      if (IsValidIndex(index))
      {
        return;
      }

      List.Insert
        (
          index,
          item
        );
    }

    public override void Remove(TItem item)
    {
      if (IsNullOrEmpty(List))
      {
        return;
      }

      List.Remove(item);
    }

    public void Remove(int index)
    {
      if (IsValidIndex(index))
      {
        return;
      }

      List.RemoveAt(index);
    }

    public void Remove(Func<TItem, bool> func)
    {
      if (IsNullOrEmpty(List))
      {
        return;
      }

      if (func == null)
      {
        return;
      }

      TItem? item = List.FirstOrDefault(func);

      if (item == null)
      {
        return;
      }

      Remove(item);
    }

    public override void RemoveRange(Func<TItem, bool> func)
    {
      if (IsNullOrEmpty(List))
      {
        return;
      }

      if (func == null)
      {
        return;
      }

      IList<TItem> list = List
        .Where(func)
        .ToList();

      RemoveRange(list);
    }

    public override void RemoveRange(IEnumerable<TItem> enumerable)
    {
      if (IsNullOrEmpty(List))
      {
        return;
      }

      if (IsNullOrEmpty(enumerable))
      {
        return;
      }

      foreach (var t in enumerable)
      {
        Remove(t);
      }
    }

    public void RemoveRange(IEnumerable<int> indexEnumerable)
    {
      if (IsNullOrEmpty(List))
      {
        return;
      }

      if (IEnumerableExtension<int>.IsNullOrEmpty(indexEnumerable))
      {
        return;
      }

      foreach (var index in indexEnumerable)
      {
        Remove(index);
      }
    }

    public void RemoveRange
    (
      int startIndex,
      int endIndex
    )
    {
      if (IsValidIndex(startIndex))
      {
        return;
      }

      if (IsValidIndex(endIndex))
      {
        return;
      }

      if (startIndex > endIndex)
      {
        return;
      }

      for (int index = startIndex; index <= endIndex; index++)
      {
        List.RemoveAt(index);
      }
    }

    public void Update
    (
      int index,
      TItem item
    )
    {
      if (IsNullOrEmpty(List))
      {
        return;
      }

      if (IsValidIndex(index))
      {
        return;
      }

      if (item == null)
      {
        return;
      }

      Insert
        (
          index,
          item
        );
    }

    public void Update
    (
      Func<TItem, bool> func,
      TItem newItem
    )
    {
      if (IsNullOrEmpty(List))
      {
        return;
      }

      if (func == null)
      {
        return;
      }

      TItem? oldItem = Get(func);

      if (oldItem == null)
      {
        return;
      }

      int index = List.IndexOf(oldItem);
      Remove(func);

      Insert
        (
          index,
          newItem
        );
    }

    public void UpdateRange
    (
      Func<TItem, bool> func,
      TItem item
    )
    {
      if (IsNullOrEmpty(List))
      {
        return;
      }

      if (func == null)
      {
        return;
      }

      IEnumerable<int> indexEnumerable = GetIndexRange(func);

      if (indexEnumerable == null)
      {
        return;
      }

      foreach (var index in indexEnumerable)
      {
        Update
          (
            index,
            item
          );
      }
    }

    #endregion
  }
}