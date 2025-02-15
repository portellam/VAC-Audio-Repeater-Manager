using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using VACARM.Infrastructure.Extensions;

namespace VACARM.Infrastructure.Repositories
{
  /// <summary>
  /// The <typeparamref name="ObservableCollectionRepository"/> repository.
  /// </summary>
  public class ObservableCollectionRepository<TItem> :
    Repository<TItem> where TItem :
    class
  {
    #region Parameters

    /// <summary>
    /// The <typeparamref name="ObservableCollection"/> of all
    /// <typeparamref name="TItem"/> item(s).
    /// </summary>
    protected ObservableCollection<TItem> Collection
    {
      get
      {
        return new ObservableCollection<TItem>(base.Enumerable);
      }
      set
      {
        base.Enumerable = value;
        base.OnPropertyChanged(nameof(Collection));
      }
    }

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    [ExcludeFromCodeCoverage]
    public ObservableCollectionRepository()
    {
      this.Collection = new ObservableCollection<TItem>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="observableCollection">The collection of item(s)</param>
    [ExcludeFromCodeCoverage]
    public ObservableCollectionRepository
      (ObservableCollection<TItem> observableCollection)
    {
      this.Collection = observableCollection;
    }

    public override void Add(TItem? item)
    {
      if (item == null)
      {
        return;
      }

      if (this.Collection.Contains(item))
      {
        return;
      }

      this.Collection
        .Add(item);
    }

    public TItem? Get(int index)
    {
      return this.Collection
        .ElementAt(index);
    }

    public int? GetIndex(Func<TItem, bool> func)
    {
      var item = this.Get(func);

      if (item == null)
      {
        return null;
      }

      return this.Collection.IndexOf(item);
    }

    public int? GetIndex(TItem item)
    {
      return this.Collection.IndexOf(item);
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
        var item = this.Get(index);

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
      if (startIndex > endIndex)
      {
        yield break;
      }

      if (!this.ContainsIndex(startIndex))
      {
        yield break;
      }

      if (!this.ContainsIndex(endIndex))
      {
        yield break;
      }

      for (int index = startIndex; index <= endIndex; index++)
      {
        var item = this.Get(index);

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
      if (this.Collection == null)
      {
        this.Collection = new ObservableCollection<TItem>();
      }

      if (this.Collection.Count() <= this.MaxCount)
      {
        return;
      }

      var previousIndex = index - 1;

      if (!this.ContainsIndex(previousIndex))
      {
        return;
      }

      this.Collection.Insert
        (
          index,
          item
        );
    }

    public override void Remove(TItem item)
    {
      if (this.IsNullOrEmpty(this.Collection))
      {
        return;
      }

      this.Collection.Remove(item);
    }

    public void Remove(int index)
    {
      if (this.IsValidIndex(index))
      {
        return;
      }

      if (!this.ContainsIndex(index))
      {
        return;
      }

      this.Collection.RemoveAt(index);
    }

    public void Remove(Func<TItem, bool> func)
    {
      if (this.IsNullOrEmpty(this.Collection))
      {
        return;
      }

      if (func == null)
      {
        return;
      }

      var item = this.Collection
        .FirstOrDefault(func);

      if (item == null)
      {
        return;
      }

      this.Remove(item);
    }

    public override void RemoveRange(Func<TItem, bool> func)
    {
      if (this.IsNullOrEmpty(this.Collection))
      {
        return;
      }

      if (func == null)
      {
        return;
      }

      var enumerable = this.Collection
        .Where(func);

      this.RemoveRange(enumerable);
    }

    public override void RemoveRange(IEnumerable<TItem> enumerable)
    {
      if (this.IsNullOrEmpty(this.Collection))
      {
        return;
      }

      if (this.IsNullOrEmpty(enumerable))
      {
        return;
      }

      foreach (var item in enumerable)
      {
        this.Remove(item);
      }
    }

    public void RemoveRange(IEnumerable<int> indexEnumerable)
    {
      if (this.IsNullOrEmpty(this.Collection))
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
      if (startIndex > this.Collection.Count())
      {
        return;
      }

      if (endIndex > this.Collection.Count())
      {
        return;
      }

      if (!this.ContainsIndex(startIndex))
      {
        return;
      }

      if (!this.ContainsIndex(endIndex))
      {
        return;
      }

      if (startIndex > endIndex)
      {
        return;
      }

      for (int index = startIndex; index <= endIndex; index++)
      {
        this.Collection
          .RemoveAt(index);
      }
    }

    public void Update
    (
      int index,
      TItem item
    )
    {
      if (this.IsNullOrEmpty(this.Collection))
      {
        return;
      }

      if (item == null)
      {
        return;
      }

      if (!this.ContainsIndex(index))
      {
        return;
      }

      this.Collection[index] = item;
    }

    public void Update
    (
      Func<TItem, bool> func,
      TItem newItem
    )
    {
      if (this.IsNullOrEmpty(this.Collection))
      {
        return;
      }

      if (func == null)
      {
        return;
      }

      var oldItem = this.Get(func);

      if (oldItem == null)
      {
        return;
      }

      var index = this.Collection
        .IndexOf(oldItem);

      this.Collection[index] = newItem;
    }

    public void UpdateRange
    (
      Func<TItem, bool> func,
      TItem item
    )
    {
      if (IsNullOrEmpty(this.Collection))
      {
        return;
      }

      if (func == null)
      {
        return;
      }

      var indexEnumerable = this.GetIndexRange(func);

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