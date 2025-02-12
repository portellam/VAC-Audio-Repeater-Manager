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
      this.List = new List<TItem>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="list">The list of item(s)</param>
    [ExcludeFromCodeCoverage]
    public GenericListRepository(List<TItem> list)
    {
      this.List = list;
    }

    public override void Add(TItem? item)
    {
      if (item == null)
      {
        return;
      }

      if (this.List.Contains(item))
      {
        return;
      }

      this.List.Add(item);
    }

    public TItem? Get(int index)
    {
      return this.List.ElementAt(index);
    }

    public int? GetIndex(Func<TItem, bool> func)
    {
      TItem? item = this.Get(func);

      if (item == null)
      {
        return null;
      }

      return this.List.IndexOf(item);
    }

    public int? GetIndex(TItem item)
    {
      return this.List.IndexOf(item);
    }

    public IEnumerable<int> GetIndexRange(Func<TItem, bool> func)
    {
      if (func == null)
      {
        yield break;
      }

      IEnumerable<TItem> enumerable = this.GetRange(func);

      if (this.IsNullOrEmpty(enumerable))
      {
        yield break;
      }

      foreach (var item in enumerable)
      {
        var index = this.GetIndex(item);

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
        TItem? item = this.Get(index);

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
      if (this.IsValidIndex(startIndex))
      {
        yield break;
      }

      if (this.IsValidIndex(endIndex))
      {
        yield break;
      }

      if (startIndex > endIndex)
      {
        yield break;
      }

      for (int index = startIndex; index <= endIndex; index++)
      {
        TItem? item = this.Get(index);

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
      if (this.List == null)
      {
        List = new List<TItem>();
      }

      if (this.List.Count() <= this.MaxCount)
      {
        return;
      }

      if (this.IsValidIndex(index))
      {
        return;
      }

      this.List.Insert
        (
          index,
          item
        );
    }

    public override void Remove(TItem item)
    {
      if (this.IsNullOrEmpty(List))
      {
        return;
      }

      this.List.Remove(item);
    }

    public void Remove(int index)
    {
      if (this.IsValidIndex(index))
      {
        return;
      }

      this.List.RemoveAt(index);
    }

    public void Remove(Func<TItem, bool> func)
    {
      if (this.IsNullOrEmpty(List))
      {
        return;
      }

      if (func == null)
      {
        return;
      }

      TItem? item = this.List.FirstOrDefault(func);

      if (item == null)
      {
        return;
      }

      this.Remove(item);
    }

    public override void RemoveRange(Func<TItem, bool> func)
    {
      if (this.IsNullOrEmpty(List))
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

      this.RemoveRange(list);
    }

    public override void RemoveRange(IEnumerable<TItem> enumerable)
    {
      if (this.IsNullOrEmpty(List))
      {
        return;
      }

      if (this.IsNullOrEmpty(enumerable))
      {
        return;
      }

      foreach (var t in enumerable)
      {
        this.Remove(t);
      }
    }

    public void RemoveRange(IEnumerable<int> indexEnumerable)
    {
      if (this.IsNullOrEmpty(List))
      {
        return;
      }

      if (IEnumerableExtension<int>.IsNullOrEmpty(indexEnumerable))
      {
        return;
      }

      foreach (var index in indexEnumerable)
      {
        this.Remove(index);
      }
    }

    public void RemoveRange
    (
      int startIndex,
      int endIndex
    )
    {
      if (this.IsValidIndex(startIndex))
      {
        return;
      }

      if (this.IsValidIndex(endIndex))
      {
        return;
      }

      if (startIndex > endIndex)
      {
        return;
      }

      for (int index = startIndex; index <= endIndex; index++)
      {
        this.List.RemoveAt(index);
      }
    }

    public void Update
    (
      int index,
      TItem item
    )
    {
      if (this.IsNullOrEmpty(List))
      {
        return;
      }

      if (this.IsValidIndex(index))
      {
        return;
      }

      if (item == null)
      {
        return;
      }

      this.Insert
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
      if (this.IsNullOrEmpty(List))
      {
        return;
      }

      if (func == null)
      {
        return;
      }

      TItem? oldItem = this.Get(func);

      if (oldItem == null)
      {
        return;
      }

      int index = this.List.IndexOf(oldItem);
      this.Remove(func);

      this.Insert
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
      if (IsNullOrEmpty(this.List))
      {
        return;
      }

      if (func == null)
      {
        return;
      }

      IEnumerable<int> indexEnumerable = this.GetIndexRange(func);

      if (indexEnumerable == null)
      {
        return;
      }

      foreach (var index in indexEnumerable)
      {
        this.Update
          (
            index,
            item
          );
      }
    }

    #endregion
  }
}