namespace VACARM.Infrastructure.Repositories
{
  public interface IRepository<T>
  {
    #region Logic

    T? Get(Func<T, bool> predicate);
    IEnumerable<T> GetAll();
    IEnumerable<T> GetRange(Func<T, bool> predicate);
    IQueryable<T> Queryable();
    void Add(T t);
    void AddRange(IEnumerable<T> enumerable);
    void Remove(T t);
    void Remove(Func<T, bool> predicate);
    void RemoveAll();
    void RemoveRange(Func<T, bool> predicate);
    void RemoveRange(IEnumerable<T> enumerable);

    #endregion
  }
}
