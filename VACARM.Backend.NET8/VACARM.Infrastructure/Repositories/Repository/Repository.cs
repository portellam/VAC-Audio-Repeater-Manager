using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
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

    public bool Remove(int index)
    {
      if (index < 0)
      {
        return false;
      }

      if (this.IsNullOrEmpty)
      {
        return false;
      }

      if (this.Type == typeof(IList<TItem>))
      {
        (this.Enumerable as IList<TItem>).RemoveAt(index);
        return true;
      }

      var itemToRemove = this.Get(index);
      var enumerable = this.EmptyEnumerable;

      foreach (var item in this.GetAll())
      {
        if (item == itemToRemove)
        {
          continue;
        }

        enumerable.Append(item);
      }

      this.Enumerable = enumerable;
      return true;
    }

    public bool Remove(TItem itemToRemove)
    {
      if (itemToRemove == null)
      {
        return false;
      }

      if (this.IsNullOrEmpty)
      {
        return false;
      }

      if (this.Type == typeof(ICollection<TItem>))
      {
        return (this.Enumerable as ICollection<TItem>).Remove(itemToRemove);
      }

      if (this.Type == typeof(IList<TItem>))
      {
        return (this.Enumerable as IList<TItem>).Remove(itemToRemove);
      }

      if (this.Type == typeof(ISet<TItem>))
      {
        return (this.Enumerable as ISet<TItem>).Remove(itemToRemove);
      }

      var enumerable = this.EmptyEnumerable;

      foreach (var item in this.GetAll())
      {
        if (item == itemToRemove)
        {
          continue;
        }

        enumerable.Append(item);
      }

      this.Enumerable = enumerable;
      return true;
    }

    public IEnumerable<bool> RemoveRange(IEnumerable<int> indexEnumerable)
    {
      if (indexEnumerable.IsNullOrEmpty())
      {
        yield return false;
      }

      if (this.IsNullOrEmpty)
      {
        yield return false;
      }

      if (this.Type == typeof(IList<TItem>))
      {
        foreach (var index in indexEnumerable)
        {
          (this.Enumerable as IList<TItem>).RemoveAt(index);
          yield return true;
        }
      }

      var enumerableToRemove = this.GetRange(indexEnumerable);
      var enumerable = this.EmptyEnumerable;

      foreach (var item in this.GetAll())
      {
        if (enumerableToRemove.Contains(item))
        {
          continue;
        }

        enumerable.Append(item);
        yield return true;
      }

      this.Enumerable = enumerable;
    }

    public IEnumerable<bool> RemoveRange(IEnumerable<TItem> enumerableToRemove)
    {
      if (enumerableToRemove.IsNullOrEmpty())
      {
        yield return false;
      }

      if (this.IsNullOrEmpty)
      {
        yield return false;
      }

      if (this.Type == typeof(ICollection<TItem>))
      {
        foreach (var index in enumerableToRemove)
        {
          yield return (this.Enumerable as ICollection<TItem>).Remove(index);
        }

        yield break;
      }

      if (this.Type == typeof(IList<TItem>))
      {
        foreach (var index in enumerableToRemove)
        {
          yield return (this.Enumerable as IList<TItem>).Remove(index);
        }

        yield break;
      }

      if (this.Type == typeof(ISet<TItem>))
      {
        foreach (var index in enumerableToRemove)
        {
          yield return (this.Enumerable as ISet<TItem>).Remove(index);
        }

        yield break;
      }

      var enumerable = this.EmptyEnumerable;

      foreach (var item in this.GetAll())
      {
        if (enumerableToRemove.Contains(item))
        {
          continue;
        }

        enumerable.Append(item);
        yield return true;
      }

      this.Enumerable = enumerable;
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