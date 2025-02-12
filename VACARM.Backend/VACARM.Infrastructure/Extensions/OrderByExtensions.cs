namespace VACARM.Infrastructure.Extensions
{
  public static class OrderByExtensions
  {
    #region Logic

    public static IOrderedQueryable<T> OrderBy<T>
    (
      this IQueryable<T> source, 
      IOrderBy orderBy
    )
    {
      return Queryable.OrderBy
        (
          source, orderBy.Expression
        );
    }

    public static IOrderedQueryable<T> OrderByDescending<T>
    (
      this IQueryable<T> source,
      IOrderBy orderBy
    )
    {
      return Queryable.OrderByDescending
        (
          source, orderBy.Expression
        );
    }

    public static IOrderedQueryable<T> ThenBy<T>
    (
      this IOrderedQueryable<T> source,
      IOrderBy orderBy
    )
    {
      return Queryable.ThenBy
        (
          source, orderBy.Expression
        );
    }

    public static IOrderedQueryable<T> ThenByDescending<T>
    (
      this IOrderedQueryable<T> source, 
      IOrderBy orderBy
    )
    {
      return Queryable.ThenByDescending
        (
          source, orderBy.Expression
        );
    }

    #endregion
  }
}