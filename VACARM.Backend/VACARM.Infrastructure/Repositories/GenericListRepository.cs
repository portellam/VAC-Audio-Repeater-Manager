using VACARM.Infrastructure.Extensions;

namespace VACARM.Infrastructure.Repositories
{
  public class GenericListRepository<T> :
    GenericRepository<T>,
    IGenericListRepository<T> where T :
    class
  {
    #region Parameters

    /// <summary>
    /// The list of all <typeparamref name="T"/> item(s).
    /// </summary>
    public virtual List<T> List
    {
      get
      {
        return base.Enumerable.ToList();
      }
      set
      {
        base.Enumerable = value;
        OnPropertyChanged(nameof(List));
      }
    }

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    public GenericListRepository()
    {
      List = new List<T>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="list">The list of item(s)</param>
    public GenericListRepository(List<T> list)
    {
      List = list;
    }

    public T? Get(int index)
    {
      return List.ElementAt(index);
    }

    public int? GetIndex(Func<T, bool> func)
    {
      T? item = Get(func);

      if (item == null)
      {
        return null;
      }

      return List.IndexOf(item);
    }

    public int? GetIndex(T item)
    {
      return List.IndexOf(item);
    }

    public IEnumerable<int> GetIndexRange(Func<T, bool> func)
    {
      if (func == null)
      {
        yield break;
      }

      IEnumerable<T> enumerable = GetRange(func);

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

    public IEnumerable<T> GetRange(IEnumerable<int> indexEnumerable)
    {
      if (IEnumerableExtension<int>.IsNullOrEmpty(indexEnumerable))
      {
        yield break;
      }

      foreach (int index in indexEnumerable)
      {
        T? item = Get(index);

        if (item == null)
        {
          continue;
        }

        yield return item;
      }
    }

    public IEnumerable<T> GetRange
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
        T? item = Get(index);

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
      T item
    )
    {
      if (List == null)
      {
        List = new List<T>();
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

    public override void Remove(T item)
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

    public void Remove(Func<T, bool> func)
    {
      if (IsNullOrEmpty(List))
      {
        return;
      }

      if (func == null)
      {
        return;
      }

      T? item = List.FirstOrDefault(func);

      if (item == null)
      {
        return;
      }

      Remove(item);
    }

    public override void RemoveRange(Func<T, bool> func)
    {
      if (IsNullOrEmpty(List))
      {
        return;
      }

      if (func == null)
      {
        return;
      }

      IList<T> list = List
        .Where(func)
        .ToList();

      RemoveRange(list);
    }

    public override void RemoveRange(IEnumerable<T> enumerable)
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
      T item
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
      Func<T, bool> func,
      T newItem
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

      T? oldItem = Get(func);

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
      Func<T, bool> func,
      T item
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