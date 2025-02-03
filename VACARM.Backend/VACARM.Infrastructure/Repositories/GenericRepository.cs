using System.ComponentModel;
using System.Diagnostics;

namespace VACARM.Infrastructure.Repositories
{
  public class GenericRepository<T> :
    IGenericRepository<T> where T :
    class
  {
    #region Parameters

    private IEnumerable<T> enumerable { get; set; }

    /// <summary>
    /// The enumerable of all <typeparamref name="T"/> item(s).
    /// </summary>
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

    public T? Get(Func<T, bool> func)
    {
      return Enumerable.FirstOrDefault(func);
    }

    public IEnumerable<T> GetAll()
    {
      return Enumerable.AsEnumerable();
    }

    public IEnumerable<T> GetRange(Func<T, bool> func)
    {
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

      if (Enumerable == null)
      {
        RemoveAll();
      }

      Enumerable.Append(item);
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

      if (Enumerable == null)
      {
        RemoveAll();
      }

      foreach (var t in enumerable)
      {
        Add(t);
      }
    }

    public void RemoveAll()
    {
      Enumerable = Array.Empty<T>();
    }

    #endregion
  }
}