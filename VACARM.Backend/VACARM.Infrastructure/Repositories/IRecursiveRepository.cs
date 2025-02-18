namespace VACARM.Infrastructure.Repositories
{
  public interface IRecursiveRepository
    <
      TRecursiveRepository,
      TRepository,
      TItem
    >
    where TRecursiveRepository :
    ReadonlyRepository<TRepository>
    where TRepository :
    ReadonlyRepository<TItem>
    where TItem :
    class
  {
    #region Parameters

    int SelectedIndex { get; set; }
    ReadonlyRepository<TItem> SelectedRepository { get; }

    #endregion

    #region Logic

    /// <summary>
    /// Add a <typeparamref name="TRepository"/>.
    /// </summary>
    /// <param name="repository">The repository</param>
    /// <returns>True/false result.</returns>
    bool Add(TRepository repository);

    /// <summary>
    /// Remove a <typeparamref name="TRepository"/>.
    /// </summary>
    /// <param name="index">The index</param>
    /// <returns>True/false result.</returns>
    bool Remove(int index);

    /// <summary>
    /// Get a <typeparamref name="TRepository"/>.
    /// </summary>
    /// <param name="index">The index</param>
    /// <returns>The repository.</returns>
    ReadonlyRepository<TItem>? Get(int index);

    #endregion
  }
}