using System.Linq.Expressions;
using VACARM.Core.Entities;

namespace VACARM.Core.Repositories
{
  public interface IRepository<T>
  {
    #region Logic

    T? Get(Func<T, bool> predicate);
    IEnumerable<T> GetAll();
    IEnumerable<T> GetRange(Func<T, bool> predicate);
    IQueryable<T> Queryable();
    void Remove(T t);
    void RemoveRange(Predicate<T> predicate);
    void RemoveRange(IEnumerable<T> enumerable);
    void Set(T t);
    void SetRange(IEnumerable<T> enumerable);
    void Update(T t);
    void UpdateRange(IEnumerable<T> enumerable);

    #endregion
  }
}
