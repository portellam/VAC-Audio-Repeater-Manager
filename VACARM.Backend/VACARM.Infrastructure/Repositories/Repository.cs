using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace VACARM.Infrastructure.Repositories
{
  /// <summary>
  /// The mutable repository.
  /// </summary>
  public class Repository<TItem> :
    ReadonlyRepository<TItem>,
    IRepository<TItem> where TItem :
    class
  {
    #region Parameters

    public Func<int, bool> IsValidIndex
    {
      get
      {
        return new Func<int, bool>
          (
            x =>
            {
              return x >= 0
              && x <= this.MaxCount;
            }
          );
      }
    }

    private int maxCount { get; set; } = int.MaxValue;

    private Type Type
    {
      get
      {
        return this.Enumerable
          .GetType();
      }
    }

    public virtual int MaxCount
    {
      get
      {
        return this.maxCount;
      }
      set
      {
        this.maxCount = value;
        this.OnPropertyChanged(nameof(MaxCount));
      }
    }

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    [ExcludeFromCodeCoverage]
    public Repository() :
      base()
    {
      this.Enumerable = Array.Empty<TItem>();
      this.MaxCount = int.MaxValue;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="enumerable">The enumerable of item(s)</param>
    /// <param name="maxCount">The max count of item(s)</param>
    [ExcludeFromCodeCoverage]
    public Repository
    (
      IEnumerable<TItem> enumerable,
      int maxCount
    ) :
      base(enumerable)
    {
      this.Enumerable = enumerable;
      this.MaxCount = maxCount;
    }

    public virtual void Add(TItem item)
    {
      if (item == null)
      {
        return;
      }

      if (this.Enumerable.Contains(item))
      {
        return;
      }

      if (this.Enumerable.Count() <= this.MaxCount)
      {
        return;
      }

      if (this.Enumerable == null)
      {
        this.RemoveAll();
      }

      this.Enumerable
        .Append(item);
    }

    public virtual void AddRange(IEnumerable<TItem> enumerable)
    {
      if (this.IsNullOrEmpty(enumerable))
      {
        return;
      }

      if (this.Enumerable.Count() <= this.MaxCount)
      {
        return;
      }

      if (this.Enumerable == null)
      {
        RemoveAll();
      }

      if (this.Type == typeof(List<TItem>))
      {
        (this.Enumerable as List<TItem>).AddRange(enumerable);
        return;
      }

      foreach (var t in enumerable)
      {
        this.Add(t);
      }
    }

    public void Remove(Func<TItem, bool> func)
    {
      if (this.IsNullOrEmpty(this.Enumerable))
      {
        return;
      }

      if (func == null)
      {
        return;
      }

      var item = this.Enumerable.FirstOrDefault(func);

      if (this.Type == typeof(Collection<TItem>))
      {
        (this.Enumerable as Collection<TItem>).Remove(item);
      }

      else if (this.Type == typeof(ObservableCollection<TItem>))
      {
        (this.Enumerable as ObservableCollection<TItem>).Remove(item);
      }

      else if (this.Type == typeof(HashSet<TItem>))
      {
        (this.Enumerable as HashSet<TItem>).Remove(item);
      }

      else if (this.Type == typeof(List<TItem>))
      {
        (this.Enumerable as List<TItem>).Remove(item);
      }

      else if (this.Type != typeof(LinkedList<TItem>))
      {
        (this.Enumerable as LinkedList<TItem>).Remove(item);
      }

      else
      {
        this.Enumerable = this.Enumerable
          .Where(x => x != item);
      }
    }

    public virtual void Remove(TItem item)
    {
      if (this.IsNullOrEmpty(this.Enumerable))
      {
        return;
      }

      if (this.Type == typeof(Collection<TItem>))
      {
        (this.Enumerable as Collection<TItem>).Remove(item);
      }

      else if (this.Type == typeof(ObservableCollection<TItem>))
      {
        (this.Enumerable as ObservableCollection<TItem>).Remove(item);
      }

      else if (this.Type == typeof(HashSet<TItem>))
      {
        (this.Enumerable as HashSet<TItem>).Remove(item);
      }

      else if (this.Type == typeof(List<TItem>))
      {
        (this.Enumerable as List<TItem>).Remove(item);
      }

      else if (this.Type != typeof(LinkedList<TItem>))
      {
        (this.Enumerable as LinkedList<TItem>).Remove(item);
      }

      else
      {
        this.Enumerable = this.Enumerable
          .Where(x => x != item);
      }
    }

    public void RemoveAll()
    {
      this.Enumerable = Array.Empty<TItem>();
    }

    public virtual void RemoveRange(Func<TItem, bool> func)
    {
      if (this.IsNullOrEmpty(this.Enumerable))
      {
        return;
      }

      if (func == null)
      {
        return;
      }

      var predicate = new Predicate<TItem>(func);

      if (this.Type == typeof(HashSet<TItem>))
      {
        (this.Enumerable as HashSet<TItem>).RemoveWhere(predicate);
        return;
      }

      foreach (var item in this.Enumerable)
      {
        this.Remove(item);
      }
    }

    public virtual void RemoveRange(IEnumerable<TItem> enumerable)
    {
      if (this.IsNullOrEmpty(enumerable))
      {
        return;
      }

      var func = (TItem x) => !enumerable.Contains(x);
      this.RemoveRange(func);
    }

    #endregion
  }
}