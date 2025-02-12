﻿using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using VACARM.Infrastructure.Extensions;

namespace VACARM.Infrastructure.Repositories
{
  /// <summary>
  /// The <typeparamref name="Enumerable"/> repository.
  /// </summary>
  public class GenericRepository<TItem> :
    IGenericRepository<TItem> where TItem :
    class
  {
    #region Parameters

    /// <summary>
    /// The <typeparamref name="Enumerable"/> of all
    /// <typeparamref name="TItem"/> item(s).
    /// </summary>
    internal virtual IEnumerable<TItem> Enumerable
    {
      get
      {
        return this.enumerable;
      }
      set
      {
        this.enumerable = value;
        OnPropertyChanged(nameof(Enumerable));
      }
    }

    private IEnumerable<TItem> enumerable { get; set; }
    private int maxCount { get; set; } = int.MaxValue;

    public virtual int MaxCount
    {
      get
      {
        return this.maxCount;
      }
      set
      {
        this.maxCount = value;
        OnPropertyChanged(nameof(MaxCount));
      }
    }

    public virtual event PropertyChangedEventHandler PropertyChanged;

    #endregion

    #region Logic

    /// <summary>
    /// Logs event when property has changed.
    /// </summary>
    /// <param name="propertyName">The property name</param>
    internal void OnPropertyChanged(string propertyName)
    {
      PropertyChanged?.Invoke
      (
        this,
        new PropertyChangedEventArgs(propertyName)
      );

      Debug.WriteLine
      (
        string.Format
        (
          "PropertyChanged: {0}",
          propertyName
        )
      );
    }

    /// <summary>
    /// Constructor
    /// </summary>
    [ExcludeFromCodeCoverage]
    public GenericRepository()
    {
      this.Enumerable = Array.Empty<TItem>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="enumerable">The enumerable of item(s)</param>
    [ExcludeFromCodeCoverage]
    public GenericRepository(IEnumerable<TItem> enumerable)
    {
      this.Enumerable = enumerable;
    }

    public bool IsNullOrEmpty(IEnumerable<TItem> enumerable)
    {
      return IEnumerableExtension<TItem>.IsNullOrEmpty(enumerable);
    }

    public bool IsValidIndex(int index)
    {
      return index >= 0
        && index <= this.MaxCount;
    }

    public TItem? Get(Func<TItem, bool> func)
    {
      if (IsNullOrEmpty(this.Enumerable))
      {
        return null;
      }

      return this.Enumerable.FirstOrDefault(func);
    }

    public IEnumerable<TItem> GetAll()
    {
      if (IsNullOrEmpty(this.Enumerable))
      {
        return Array.Empty<TItem>();
      }

      return this.Enumerable.AsEnumerable();
    }

    public IEnumerable<TItem> GetRange(Func<TItem, bool> func)
    {
      if (IsNullOrEmpty(this.Enumerable))
      {
        return Array.Empty<TItem>();
      }

      return this.Enumerable
        .Where(x => func(x))
        .AsEnumerable();
    }

    public virtual void Add(TItem item)
    {
      if (item == null)
      {
        return;
      }

      if (this.Enumerable.Contains(item))
      {
        return;
      }

      if (this.Enumerable.Count() <= this.MaxCount)
      {
        return;
      }

      if (this.Enumerable == null)
      {
        RemoveAll();
      }

      this.Enumerable.Append(item);
    }

    public void AddRange(IEnumerable<TItem> enumerable)
    {
      if (IsNullOrEmpty(enumerable))
      {
        return;
      }

      if (this.Enumerable.Count() <= this.MaxCount)
      {
        return;
      }

      if (this.Enumerable == null)
      {
        RemoveAll();
      }

      foreach (var t in enumerable)
      {
        Add(t);
      }
    }

    public virtual void Remove(TItem item)
    {
      if (IsNullOrEmpty(Enumerable))
      {
        return;
      }

      this.Enumerable = this.Enumerable.Where(x => x != item);
    }

    public void RemoveAll()
    {
      Enumerable = Array.Empty<TItem>();
    }

    public virtual void RemoveRange(Func<TItem, bool> func)
    {
      if (IsNullOrEmpty(Enumerable))
      {
        return;
      }

      if (func == null)
      {
        return;
      }

      this.Enumerable = this.Enumerable.Where(func);
    }

    public virtual void RemoveRange(IEnumerable<TItem> enumerable)
    {
      if (IsNullOrEmpty(Enumerable))
      {
        return;
      }

      if (IsNullOrEmpty(enumerable))
      {
        return;
      }

      this.Enumerable = this.Enumerable.Where(x => !enumerable.Contains(x));
    }

    #endregion
  }
}