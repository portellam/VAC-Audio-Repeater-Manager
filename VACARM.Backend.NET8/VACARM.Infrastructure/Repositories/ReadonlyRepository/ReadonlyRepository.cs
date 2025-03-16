using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using VACARM.Infrastructure.Repositories.ReadonlyRepository;

namespace VACARM.Infrastructure.Repositories
{
  public partial class Repository<TItem> :
    IRepository<TItem>
    where TItem :
    class
  {
    #region Parameters

    /// <summary>
    /// The enumerable of all <typeparamref name="TItem"/>(s).
    /// </summary>
    protected virtual IEnumerable<TItem> Enumerable { get; set; }

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
    public Repository(IEnumerable<TItem> enumerable)
    {
      this.Enumerable = enumerable;
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