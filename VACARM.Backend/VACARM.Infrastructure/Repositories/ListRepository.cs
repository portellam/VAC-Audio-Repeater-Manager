namespace VACARM.Infrastructure.Repositories
{
  public class ListRepository<T> : IRepository<T>
  {
    private IList<T> List = new List<T>();

    public ListRepository(List<T> list)
    {
      List = list;
    }

    public T? Get(Func<T, bool> predicate)
    {
      return List.FirstOrDefault(predicate ?? (x => true));
    }

    public List<T> GetAll()
    {
      return List.ToList();
    }

    public List<T> GetRange(Func<T, bool> predicate)
    {
      return List
        .Where(predicate)
        .ToList();
    }

    public IQueryable<T> Queryable()
    {
      return List.AsQueryable();
    }

    public void Remove(T t)
    {
      if (t == null)
      {
        return;
      }

      List.Remove(t);
    }
   
    public void RemoveRange(Func<T, bool> predicate)
    {
      GetRange(predicate)
        .ForEach
        (
          x => Remove(x)
        );
    }

    public void RemoveAll()
    {
      List.Clear();
    }

    public void Set(T t)
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

    public void SetRange(IEnumerable<T> enumerable)
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
        Set(t);
      }
    }

    public void Update(T t)
    {
      if (t == null)
      {
        return;
      }

      if (!List.Contains(t))
      {
        return;
      }

      Remove(t);
      Set(t);
    }

    public void UpdateRange(IEnumerable<T> enumerable)
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
        Update(t);
      }
    }
  }
}