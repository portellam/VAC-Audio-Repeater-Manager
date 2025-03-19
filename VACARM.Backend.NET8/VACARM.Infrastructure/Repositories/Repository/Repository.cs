using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Infrastructure.Repositories
{
  /// <summary>
  /// The repository to be inherited and overrided.
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

    private TEnumerable enumerable { get; set; }

    /// <summary>
    /// The enumerable of item(s).
    /// </summary>
    internal TEnumerable Enumerable
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
        return Enumerable.IsNullOrEmpty();
      }
    }

    public int Count
    {
      get
      {
        return this.Enumerable
          .Count();
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

    public TItem Get(int index)
    {
      return this.Enumerable
        .ElementAtOrDefault(index);
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

    public virtual void Add(TItem item)
    {
      lock (this.Enumerable)
      {
        if (item == null)
        {
          return;
        }

        enumerable.Append(item);
      }
    }

    public virtual void AddRange(IEnumerable<TItem> enumerable)
    {
      lock (this.Enumerable)
      {
        if (enumerable.IsNullOrEmpty())
        {
          return;
        }

        enumerable.Concat(enumerable);
      }
    }

    #endregion
  }
}