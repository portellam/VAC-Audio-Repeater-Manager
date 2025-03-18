using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Infrastructure.Services
{
  /// <summary>
  /// The service for <typeparamref name="TItem"/>.
  /// </summary>
  public partial class Service
    <
      TEnumerable,
      TItem
    > :
    IService
    <
      IEnumerable<TItem>,
      TItem
    >
    where TEnumerable : 
    IEnumerable<TItem>
    where TItem :
    class
  {
    #region Parameters

    private Repository
      <
        TEnumerable,
        TItem
      > repository
    { get; set; }

    public virtual Repository
      <
        TEnumerable,
        TItem
      > Repository
    {
      get
      {
        return this.repository;
      }
      set
      {
        this.repository = value;
        this.OnPropertyChanged(nameof(this.Repository));
      }
    }

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repository">The enumerable</param>
    [ExcludeFromCodeCoverage]
    public Service(TEnumerable enumerable)
    {
      this.Repository = new Repository
        <
          TEnumerable,
          TItem
        >(enumerable);
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repository">The repository</param>
    public Service
    (
      Repository
      <
        TEnumerable,
        TItem
      > repository
    )
    {
      this.Repository = repository;
    }

    public void DoAction
    (
      Action<TItem> action,
      Func<TItem, bool> func
    )
    {
      if (func == null)
      {
        return;
      }

      var item = this.Repository
        .Get(func);

      this.DoAction
        (
          action,
          item
        );
    }

    public void DoAction
    (
      Action<TItem> action,
      TItem item
    )
    {
      if (action == null)
      {
        return;
      }

      if (item == null)
      {
        return;
      }

      action(item);
    }

    public void DoActionAll(Action<TItem> action)
    {
      var enumerable = this.Repository
        .GetAll();

      this.DoActionRange
        (
          action,
          enumerable
        );
    }

    public void DoActionRange
    (
      Action<TItem> action,
      IEnumerable<TItem> enumerable
    )
    {
      if (action == null)
      {
        return;
      }

      if (enumerable.IsNullOrEmpty())
      {
        return;
      }

      foreach (var item in enumerable)
      {
        action(item);
      }
    }

    public void DoActionRange
    (
      Action<TItem> action,
      Func<TItem, bool> func
    )
    {
      if (func == null)
      {
        return;
      }

      var enumerable = this.Repository
        .GetRange(func);

      this.DoActionRange
      (
        action,
        enumerable
      );
    }

    #endregion
  }
}