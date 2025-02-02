using System.ComponentModel;
using System.Diagnostics;

namespace VACARM.Infrastructure.Repositories
{
  public class GenericRepository<T> :
    IGenericRepository<T> where T :
    class
  {
    #region Parameters

    private HashSet<T> hashSet { get; set; } = new HashSet<T>();

    /// <summary>
    /// The enumerable of all <typeparamref name="T"/> item(s).
    /// </summary>
    private HashSet<T> HashSet
    {
      get
      {
        return hashSet;
      }
      set
      {
        hashSet = value;
        OnPropertyChanged(nameof(HashSet));
      }
    }

    public virtual event PropertyChangedEventHandler PropertyChanged;

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
      HashSet = new HashSet<T>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="hashSet">the hashSet of <typeparamref name="T"/></param>
    public GenericRepository(HashSet<T> hashSet)
    {
      HashSet = hashSet;
    }

    public T? Get(Func<T, bool> func)
    {
      return HashSet.FirstOrDefault(x => func(x));
    }

    public IEnumerable<T> GetAll()
    {
      return HashSet.AsEnumerable();
    }

    public IEnumerable<T> GetRange(Func<T, bool> func)
    {
      return HashSet
        .Where(x => func(x))
        .AsEnumerable();
    }

    public void Add(T t)
    {
      if (t == null)
      {
        return;
      }

      if (HashSet.Contains(t))
      {
        return;
      }

      if (HashSet == null)
      {
        HashSet = new HashSet<T>();
      }

      HashSet.Add(t);
    }

    public void AddRange(IEnumerable<T> enumerable)
    {
      if (enumerable == null)
      {
        return;
      }

      if (enumerable.Count() == 0)
      {
        return;
      }

      if (HashSet == null)
      {
        HashSet = new HashSet<T>();
      }

      foreach (var t in enumerable)
      {
        Add(t);
      }
    }

    public void Remove(T t)
    {
      if (HashSet == null)
      {
        return;
      }

      if (HashSet.Count() == 0)
      {
        return;
      }

      HashSet.Remove(t);
    }

    public void Remove(Func<T, bool> func)
    {
      if (func == null)
      {
        return;
      }

      T? t = HashSet.FirstOrDefault(func);

      if (t == null)
      {
        return;
      }

      Remove(t);
    }

    public void RemoveAll()
    {
      if (HashSet == null)
      {
        return;
      }

      if (HashSet.Count() == 0)
      {
        return;
      }

      HashSet.Clear();
    }

    public void RemoveRange(Func<T, bool> func)
    {
      if (HashSet == null)
      {
        return;
      }

      if (HashSet.Count() == 0)
      {
        return;
      }

      if (func == null)
      {
        return;
      }

      HashSet
        .Where(x => func(x))
        .ToList()
        .ForEach(x => Remove(x));
    }

    public void RemoveRange(IEnumerable<T> enumerable)
    {
      if (enumerable == null)
      {
        return;
      }

      if (enumerable.Count() == 0)
      {
        return;
      }

      foreach (var t in enumerable)
      {
        Remove(t);
      }
    }

    #endregion
  }
}