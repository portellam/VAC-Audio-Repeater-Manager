using System.Buffers;
using System.Linq.Expressions;

namespace VACARM.Infrastructure.Extensions
{
  /// <summary>
  /// <see cref="https://asontu.github.io/2020/04/02/a-better-way-to-do-dynamic-orderby-in-c-sharp.html"/>
  /// </summary>
  /// <typeparam name="T1">The generic object</typeparam>
  /// <typeparam name="T2">The generic object property</typeparam>
  public class OrderBy<T1, T2> : IOrderBy
  {
    #region Parameters

    private readonly Expression<Func<T1, T2>> expression;
    public dynamic Expression => this.expression;

    #endregion

    #region Logic

    public OrderBy(Expression<Func<T1, T2>> expression)
    {
      this.expression = expression;
    }

    #endregion
  }
}