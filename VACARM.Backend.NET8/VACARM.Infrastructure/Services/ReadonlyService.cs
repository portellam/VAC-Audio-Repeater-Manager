using System.Diagnostics.CodeAnalysis;
using VACARM.Infrastructure.Extensions;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Infrastructure.Services
{
  /// <summary>
  /// The readonly service for <typeparamref name="TRepository"/>.
  /// </summary>
  public partial class ReadonlyService
    <
      TRepository,
      TItem
    > :
    IReadonlyService
    <
      ReadonlyRepository<TItem>,
      TItem
    >
    where TRepository :
    ReadonlyRepository<TItem>
    where TItem :
    class
  {
    #region Parameters

    private ReadonlyRepository<TItem> readonlyRepository { get; set; } =
      new ReadonlyRepository<TItem>();

    protected virtual ReadonlyRepository<TItem> Repository
    {
      get
      {
        return this.readonlyRepository;
      }
      set
      {
        this.readonlyRepository = value;
        OnPropertyChanged(nameof(this.Repository));
      }
    }

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    [ExcludeFromCodeCoverage]
    public ReadonlyService()
    {
      this.Repository = new ReadonlyRepository<TItem>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repository">The repository</param>
    public ReadonlyService(ReadonlyRepository<TItem> repository)
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

      if (IEnumerableExtension<TItem>.IsNullOrEmpty(enumerable))
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