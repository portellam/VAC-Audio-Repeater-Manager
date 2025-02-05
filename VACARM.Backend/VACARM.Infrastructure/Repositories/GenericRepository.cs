using System.ComponentModel;
using System.Diagnostics;
using VACARM.Infrastructure.Extensions;

namespace VACARM.Infrastructure.Repositories
{
  public class GenericRepository<T> :
    IGenericRepository<T> where T :
    class
  {
    #region Parameters

    private IEnumerable<T> enumerable { get; set; }
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

    public virtual IEnumerable<T> Enumerable
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

    public event PropertyChangedEventHandler PropertyChanged;

    #endregion

    #region Logic

    /// <summary>
    /// Logs event when property has changed.
    /// </summary>
    /// <param name="propertyName">The property name</param>
    private void OnPropertyChanged(string propertyName)
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
    public GenericRepository()
    {
      Enumerable = Array.Empty<T>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="enumerable">The enumerable of item(s)</param>
    public GenericRepository(IEnumerable<T> enumerable)
    {
      Enumerable = enumerable;
    }

    public bool IsNullOrEmpty(IEnumerable<T> enumerable)
    {
      return IEnumerableExtension<T>.IsNullOrEmpty(enumerable);
    }

    public bool IsValidIndex(int index)
    {
      return index >= 0
        && index <= MaxCount;
    }

    public T? Get(Func<T, bool> func)
    {
      if (IsNullOrEmpty(Enumerable))
      {
        return null;
      }

      return Enumerable.FirstOrDefault(func);
    }

    public IEnumerable<T> GetAll()
    {
      if (IsNullOrEmpty(Enumerable))
      {
        return Array.Empty<T>();
      }

      return Enumerable.AsEnumerable();
    }

    public IEnumerable<T> GetRange(Func<T, bool> func)
    {
      if (IsNullOrEmpty(Enumerable))
      {
        return Array.Empty<T>();
      }

      return Enumerable
        .Where(x => func(x))
        .AsEnumerable();
    }

    public void Add(T item)
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

    public void AddRange(IEnumerable<T> enumerable)
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

    public void Remove(T item)
    {
      if (IsNullOrEmpty(Enumerable))
      {
        return;
      }

      Enumerable = Enumerable.Where(x => x != item);
    }

    public void RemoveRange(Func<T, bool> func)
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

    public void RemoveRange(IEnumerable<T> enumerable)
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

    public void RemoveAll()
    {
      Enumerable = Array.Empty<T>();
    }

    #endregion
  }
}