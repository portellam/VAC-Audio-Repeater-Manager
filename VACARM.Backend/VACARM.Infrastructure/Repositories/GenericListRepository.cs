using System.ComponentModel;
using System.Diagnostics;

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
    public IList<T> List
    {
      get
      {
        return Enumerable.ToList();
      }
      set
      {
        Enumerable = value;
        OnPropertyChanged(nameof(List));
      }
    }

    /// <summary>
    /// The enumerable of all <typeparamref name="T"/> item(s).
    /// </summary>
    public override IEnumerable<T> Enumerable { get; set; }

    public new event PropertyChangedEventHandler PropertyChanged;

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
    public GenericListRepository()
    {
      Enumerable = new List<T>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="enumerable">The enumerable of item(s)</param>
    public GenericListRepository(List<T> enumerable)
    {
      Enumerable = enumerable;
    }

    public void Remove(T item)
    {
      if (Enumerable == null)
      {
        return;
      }

      if (Enumerable.Count() == 0)
      {
        return;
      }

      List.Remove(item);
    }

    public void Remove(Func<T, bool> func)
    {
      if (func == null)
      {
        return;
      }

      T? t = Enumerable.FirstOrDefault(func);

      if (t == null)
      {
        return;
      }

      Remove(t);
    }

    public void RemoveRange(Func<T, bool> func)
    {
      if (Enumerable == null)
      {
        return;
      }

      if (Enumerable.Count() == 0)
      {
        return;
      }

      if (func == null)
      {
        return;
      }

      IList<T> enumerable = Enumerable
        .Where(func)
        .ToList();

      RemoveRange(enumerable);
    }

    public void RemoveRange(IList<T> enumerable)
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