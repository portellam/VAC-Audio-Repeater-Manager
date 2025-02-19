using VACARM.Application.Services;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Infrastructure.Services
{
  /// <summary>
  /// The <typeparamref name="TRepository"/> service.
  /// </summary>
  public interface IGroupService
    <
      TServiceRepository,
      TService,
      TRepository,
      TItem
    >
    where TServiceRepository :
    ReadonlyRepository
    <
      ReadonlyService
      <
        ReadonlyRepository<TItem>,
        TItem
      >
    >
    where TService :
    ReadonlyService
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

    int MaxCount { get; }
    int SelectedIndex { get; set; }
    ReadonlyService<TRepository, TItem>? SelectedService { get; }

    #endregion

    #region Logic

    /// <summary>
    /// Add a <typeparamref name="TService"/>.
    /// </summary>
    /// <param name="service">The service</param>
    /// <returns>True/false result.</returns>
    bool Add(ReadonlyService<TRepository, TItem> service);

    /// <summary>
    /// Remove a <typeparamref name="TService"/>.
    /// </summary>
    /// <param name="index">The index</param>
    /// <returns>True/false result.</returns>
    bool Remove(int index);

    /// <summary>
    /// Get a <typeparamref name="TService"/>.
    /// </summary>
    /// <param name="index">The index</param>
    /// <returns>The repository.</returns>
    ReadonlyService<TRepository, TItem>? Get(int index);

    #endregion
  }
}