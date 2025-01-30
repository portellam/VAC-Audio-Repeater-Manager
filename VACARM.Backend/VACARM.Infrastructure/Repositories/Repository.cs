namespace VACARM.Infrastructure.Repositories
{
  public class Repository<T> : IRepository<T>
  {
    public HashSet<T> HashSet = new HashSet<T>();

    /// <summary>
    /// Constructor
    /// </summary>
    public Repository()
    {
      HashSet = new HashSet<T>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="hashSet">the hashSet of <typeparamref name="T"/></param>
    public Repository(HashSet<T> hashSet)
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

    public IQueryable<T> Queryable()
    {
      return HashSet.AsQueryable();
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

      HashSet.Add(t);
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
      HashSet.Remove(t);
    }

    public void Remove(Func<T, bool> func)
    {
      T? t = HashSet.FirstOrDefault(func);

      if (t == null)
      {
        return;
      }

      Remove(t);
    }

    public void RemoveAll()
    {
      HashSet.Clear();
    }

    public void RemoveRange(Func<T, bool> func)
    {
      HashSet
        .Where(x => func(x))
        .ToList()
        .ForEach(x => Remove(x));
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
