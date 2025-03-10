using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VACARM.Infrastructure.Extensions
{
  public class AsyncEnumerableExtension<T> : 
    IEnumerable<T>
  {
    #region Parameters

    private readonly Func<Task<IEnumerable<T>>> TaskFactory;

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="taskFactory">The task factory</param>
    public AsyncEnumerableExtension(Func<Task<IEnumerable<T>>> taskFactory)
    {
      this.TaskFactory = taskFactory;
    }

    public IEnumerator<T> GetEnumerator()
    {
      this.TaskFactory()
        .Wait();
      
      return this.TaskFactory()
        .Result
        .GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return this.GetEnumerator();
    }

    #endregion
  }
}