namespace VACARM.Infrastructure.Repositories
{
  public class Repository<T> : IRepository<T>
  {
    public List<T> List = new List<T>();

    /// <summary>
    /// Constructor
    /// </summary>
    public Repository()
    {
      List = new List<T>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="list">the list of <typeparamref name="T"/></param>
    public Repository(List<T> list)
    {
      List = list;
    }

    public T? Get(Func<T, bool> func)
    {
      return List.FirstOrDefault(x => func(x));
    }

    public IEnumerable<T> GetAll()
    {
      return List.AsEnumerable();
    }

    public IEnumerable<T> GetRange(Func<T, bool> predicate)
    {
      return List
        .Where(x => predicate(x))
        .AsEnumerable();
    }

    public IQueryable<T> Queryable()
    {
      return List.AsQueryable();
    }

    public void Add(T t)
    {
      if (t == null)
      {
        return;
      }

      if (List.Contains(t))
      {
        return;
      }

      List.Add(t);
    }

    public void AddRange(IEnumerable<T> enumerable)
    {
      foreach (var t in enumerable)
      {
        Add(t);
      }
    }

    public void Remove(T t)
    {
      List.Remove(t);
    }

    public void Remove(Func<T, bool> predicate)
    {
      T? t = List.FirstOrDefault(predicate);

      if (t == null)
      {
        return;
      }

      Remove(t);
    }

    public void RemoveAll()
    {
      List.Clear();
    }

    public void RemoveRange(Func<T, bool> predicate)
    {
      List<T> list = List
        .Where(x => predicate(x))
        .ToList();

      foreach (var t in list)
      {
        Remove(t);
      }
    }

    public void RemoveRange(IEnumerable<T> enumerable)
    {
      foreach (var t in enumerable)
      {
        Remove(t);
      }
    }
  }
}
