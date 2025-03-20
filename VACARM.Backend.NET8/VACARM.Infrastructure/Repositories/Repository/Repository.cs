using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Infrastructure.Repositories
{
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

    private TEnumerable EmptyEnumerable
    {
      get
      {
        if (this.Type == null)
        {
          throw new ArgumentNullException(nameof(this.Type));
        }

        var constructorInfoArray = this.Type
          .GetConstructors(BindingFlags.Public);

        var enumerable = constructorInfoArray[0].Invoke(new object[] { });
        return (TEnumerable)enumerable;
      }
    }

    private TEnumerable enumerable { get; set; }

    private Type Type
    {
      get
      {
        return this.Enumerable
          .GetType();
      }
    }

    /// <summary>
    /// The enumerable of item(s).
    /// </summary>
    protected TEnumerable Enumerable
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

    public bool IsNullOrEmpty
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

    public TItem Get(Func<TItem, bool> func)
    {
      return this.Enumerable
        .FirstOrDefault(func);
    }

    public TItem Get(int index)
    {
      return this.Enumerable
        .ElementAtOrDefault(index);
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

    public IEnumerable<TItem> GetRange(IEnumerable<int> indexEnumerable)
    {
      if (this.IsNullOrEmpty)
      {
        yield break;
      }

      if (indexEnumerable.IsNullOrEmpty())
      {
        yield break;
      }

      foreach (var index in indexEnumerable)
      {
        yield return this.Get(index);
      }
    }

    public virtual void Add(TItem item)
    {
      if (item == null)
      {
        return;
      }

      enumerable.Append(item);
    }

    public virtual void AddRange(IEnumerable<TItem> enumerable)
    {
      if (enumerable.IsNullOrEmpty())
      {
        return;
      }

      enumerable.Concat(enumerable);
    }
    
    public void RemoveAll()
    {
      this.Dispose();
    }

    #endregion
  }
}