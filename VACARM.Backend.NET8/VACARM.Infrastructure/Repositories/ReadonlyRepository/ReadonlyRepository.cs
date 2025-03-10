using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using VACARM.Infrastructure.Extensions;
using VACARM.Infrastructure.Repositories.ReadonlyRepository;

namespace VACARM.Infrastructure.Repositories
{
  public partial class ReadonlyRepository<TItem> :
    IReadonlyRepository<TItem>
    where TItem :
    class
  {
    #region Parameters

    private IEnumerable<TItem> enumerable { get; set; }

    /// <summary>
    /// The enumerable of all <typeparamref name="TItem"/>(s).
    /// </summary>
    protected virtual IEnumerable<TItem> Enumerable
    {
      get
      {
        return this.enumerable;
      }
      set
      {
        this.enumerable = value;
        this.OnPropertyChanged(nameof(this.Enumerable));
      }
    }

    public virtual bool IsNullOrEmpty
    {
      get
      {
        return IEnumerableExtension<TItem>.IsNullOrEmpty(this.Enumerable);
      }
    }

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    [ExcludeFromCodeCoverage]
    public ReadonlyRepository()
    {
      this.Enumerable = Array.Empty<TItem>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="enumerable">The enumerable of item(s)</param>
    [ExcludeFromCodeCoverage]
    public ReadonlyRepository(IEnumerable<TItem> enumerable)
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