namespace VACARM.Core.Repositories
{
  public class ListRepository<T> : IRepository<T>
  {
    private List<T> List = new List<T>();

    public ListRepository(List<T> list)
    {
      List = list;
    }

    public T? Get(Func<T, bool> predicate)
    {
      return List.FirstOrDefault(predicate ?? (x => true));
    }

    public IEnumerable<T> GetAll()
    {
      return List.AsEnumerable();
    }

    public IEnumerable<T> GetRange(Func<T, bool> predicate)
    {
      return List.Where(predicate);
    }

    public IQueryable<T> Queryable()
    {
      return List.AsQueryable();
    }

    public void Remove(T t)
    {
      List.Remove(t);
    }
   
    public void RemoveRange(Predicate<T> predicate)
    {
      throw new NotImplementedException();
    }
    public void RemoveRange(IEnumerable<T> enumerable)
    {
      throw new NotImplementedException();
    }

    public void Set(T t)
    {
      if (List.Contains(t))
      {
        return;
      }

      List.Add(t);
    }

    public void SetRange(IEnumerable<T> enumerable)
    {
      foreach (var t in enumerable)
      {
        Set(t);
      }
    }

    public void Update(T t)
    {
      if (!List.Contains(t))
      {
        return;
      }

      Remove(t);
      Set(t);
    }

    public void UpdateRange(IEnumerable<T> enumerable)
    {
      foreach (var t in enumerable)
      {
        Update(t);
      }
    }
  }
}
