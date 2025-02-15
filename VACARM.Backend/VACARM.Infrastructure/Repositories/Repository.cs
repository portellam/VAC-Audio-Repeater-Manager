using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using VACARM.Infrastructure.Extensions;

namespace VACARM.Infrastructure.Repositories
{
  /// <summary>
  /// The <typeparamref name="Enumerable"/> repository.
  /// </summary>
  public class Repository<TItem> :
    IRepository<TItem> where TItem :
    class
  {
    #region Parameters

    /// <summary>
    /// The <typeparamref name="Enumerable"/> of all
    /// <typeparamref name="TItem"/> item(s).
    /// </summary>
    protected IEnumerable<TItem> Enumerable
    {
      get
      {
        return this.enumerable;
      }
      set
      {
        this.enumerable = value;
        OnPropertyChanged(nameof(Enumerable));
      }
    }

    private IEnumerable<TItem> enumerable { get; set; }
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
        OnPropertyChanged(nameof(MaxCount));
      }
    }

    public virtual event PropertyChangedEventHandler PropertyChanged;

    #endregion

    #region Logic

    /// <summary>
    /// Logs event when property has changed.
    /// </summary>
    /// <param name="propertyName">The property name</param>
    internal void OnPropertyChanged(string propertyName)
    {
      PropertyChanged?.Invoke
      (
        this,
        new PropertyChangedEventArgs(propertyName)
      );

      Debug.WriteLine
      (
        string.Format
        (
          "PropertyChanged: {0}",
          propertyName
        )
      );
    }

    /// <summary>
    /// Constructor
    /// </summary>
    [ExcludeFromCodeCoverage]
    public Repository()
    {
      this.Enumerable = Array.Empty<TItem>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="enumerable">The enumerable of item(s)</param>
    [ExcludeFromCodeCoverage]
    public Repository(IEnumerable<TItem> enumerable)
    {
      this.Enumerable = enumerable;
    }

    public bool ContainsIndex(int index)
    {
      return index >= 0
        && index <= this.Enumerable.Count();
    }

    public bool IsNullOrEmpty(IEnumerable<TItem> enumerable)
    {
      return IEnumerableExtension<TItem>.IsNullOrEmpty(enumerable);
    }

    public bool IsValidIndex(int index)
    {
      return index >= 0
        && index <= this.MaxCount;
    }

    public TItem? Get(Func<TItem, bool> func)
    {
      if (this.IsNullOrEmpty(this.Enumerable))
      {
        return null;
      }

      return this.Enumerable.
        FirstOrDefault(func);
    }

    public IEnumerable<TItem> GetAll()
    {
      if (IsNullOrEmpty(this.Enumerable))
      {
        return Array.Empty<TItem>();
      }

      return this.Enumerable
        .AsEnumerable();
    }

    public IEnumerable<TItem> GetRange(Func<TItem, bool> func)
    {
      if (IsNullOrEmpty(this.Enumerable))
      {
        return Array.Empty<TItem>();
      }

      return this.Enumerable
        .Where(x => func(x))
        .AsEnumerable();
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

    public void AddRange(IEnumerable<TItem> enumerable)
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

      foreach (var item in enumerable)
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