namespace VACARM.Backend.Infrastructure.Repositories
{
  public interface IRepository<T>
  {
    #region Logic

    T? Get(Func<T, bool> predicate);
    List<T> GetAll();
    List<T> GetRange(Func<T, bool> predicate);
    IQueryable<T> Queryable();
    void Remove(T t);
    void RemoveAll();
    void RemoveRange(Func<T, bool> predicate);
    void Set(T t);
    void SetRange(IEnumerable<T> enumerable);
    void Update(T t);
    void UpdateRange(IEnumerable<T> enumerable);

    #endregion
  }
}
