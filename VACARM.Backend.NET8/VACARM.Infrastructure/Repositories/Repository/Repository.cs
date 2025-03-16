using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Infrastructure.Repositories
{
  /// <summary>
  /// The generic repository to be inherited and overrided.
  /// </summary>
  /// <typeparam name="TItem"></typeparam>
  public partial class Repository
    <
      TEnumerable,
      TItem
    > :
    IRepository
    <
      TEnumerable,
      TItem
    >
    where TEnumerable :
    IEnumerable<TItem>
    where TItem :
    class
  {
    #region Parameters

    /// <summary>
    /// The enumerable of item(s).
    /// </summary>
    protected TEnumerable Enumerable { get; set; }

    public virtual bool IsNullOrEmpty
    {
      get
      {
        return Enumerable.IsNullOrEmpty();
      }
    }

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="enumerable">The enumerable of item(s)</param>
    [ExcludeFromCodeCoverage]
    public Repository(TEnumerable enumerable)
    {
      Enumerable = enumerable;
    }

    public TItem Get(Func<TItem, bool> func)
    {
      return this.Enumerable
        .FirstOrDefault(func);
    }

    public IEnumerable<TItem> GetAll()
    {
      if (this.IsNullOrEmpty)
      {
        return Array.Empty<TItem>();
      }

      return this.Enumerable;
    }

    public IEnumerable<TItem> GetRange(Func<TItem, bool> func)
    {
      if (this.IsNullOrEmpty)
      {
        return Array.Empty<TItem>();
      }

      return this.Enumerable
        .Where(x => func(x));
    }

    #endregion
  }
}