using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using VACARM.Infrastructure.Extensions;

namespace VACARM.Infrastructure.Repositories
{
  /// <summary>
  /// The <typeparamref name="Enumerable"/> repository.
  /// </summary>
  public class GenericRepository<TItem> :
    IGenericRepository<TItem> where TItem :
    class
  {
    #region Parameters
    #region Parameters

    /// <summary>
    /// The <typeparamref name="Enumerable"/> of all
    /// <typeparamref name="TItem"/> item(s).
    /// </summary>
    internal virtual IEnumerable<TItem> Enumerable
    {
      get
      {
        return enumerable;
      }
      set
      {
        enumerable = value;
        OnPropertyChanged(nameof(Enumerable));
      }
    }

    private IEnumerable<TItem> enumerable { get; set; }
    private int maxCount { get; set; } = int.MaxValue;

    public virtual int MaxCount
    {
      get
      {
        return maxCount;
      }
      set
      {
        maxCount = value;
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
    public GenericRepository()
    {
      Enumerable = Array.Empty<TItem>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="enumerable">The enumerable of item(s)</param>
    [ExcludeFromCodeCoverage]
    public GenericRepository(IEnumerable<TItem> enumerable)
    {
      Enumerable = enumerable;
    }

    public bool IsNullOrEmpty(IEnumerable<TItem> enumerable)
    {
      return IEnumerableExtension<TItem>.IsNullOrEmpty(enumerable);
    }

    public bool IsValidIndex(int index)
    {
      return index >= 0
        && index <= MaxCount;
    }

    public TItem? Get(Func<TItem, bool> func)
    {
      if (IsNullOrEmpty(Enumerable))
      {
        return null;
      }

      return Enumerable.FirstOrDefault(func);
    }

    public IEnumerable<TItem> GetAll()
    {
      if (IsNullOrEmpty(Enumerable))
      {
        return Array.Empty<TItem>();
      }

      return Enumerable.AsEnumerable();
    }

    public IEnumerable<TItem> GetRange(Func<TItem, bool> func)
    {
      if (IsNullOrEmpty(Enumerable))
      {
        return Array.Empty<TItem>();
      }

      return Enumerable
        .Where(x => func(x))
        .AsEnumerable();
    }

    public virtual void Add(TItem item)
    {
      if (item == null)
      {
        return;
      }

      if (Enumerable.Contains(item))
      {
        return;
      }

      if (Enumerable.Count() <= MaxCount)
      {
        return;
      }

      if (Enumerable == null)
      {
        RemoveAll();
      }

      Enumerable.Append(item);
    }

    public void AddRange(IEnumerable<TItem> enumerable)
    {
      if (IsNullOrEmpty(enumerable))
      {
        return;
      }

      if (Enumerable.Count() <= MaxCount)
      {
        return;
      }

      if (Enumerable == null)
      {
        RemoveAll();
      }

      foreach (var t in enumerable)
      {
        Add(t);
      }
    }

    public virtual void Remove(TItem item)
    {
      if (IsNullOrEmpty(Enumerable))
      {
        return;
      }

      Enumerable = Enumerable.Where(x => x != item);
    }

    public void RemoveAll()
    {
      Enumerable = Array.Empty<TItem>();
    }

    public virtual void RemoveRange(Func<TItem, bool> func)
    {
      if (IsNullOrEmpty(Enumerable))
      {
        return;
      }

      if (func == null)
      {
        return;
      }

      Enumerable = Enumerable.Where(func);
    }

    public virtual void RemoveRange(IEnumerable<TItem> enumerable)
    {
      if (IsNullOrEmpty(Enumerable))
      {
        return;
      }

      if (IsNullOrEmpty(enumerable))
      {
        return;
      }

      Enumerable = Enumerable.Where(x => !enumerable.Contains(x));
    }

    #endregion
  }
}