using VACARM.Application.Services;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Infrastructure.Services
{
  public interface IBaseGroupService
    <
      TGroupReadonlyRepository,
      TBaseService,
      TBaseRepository,
      TBaseModel
    >
    where TGroupReadonlyRepository :
    ReadonlyRepository
    <
      BaseService
      <
        BaseRepository<TBaseModel>,
        TBaseModel
      >
    >
    where TBaseService :
    BaseService
    <
      BaseRepository<TBaseModel>,
      TBaseModel
    >
    where TBaseRepository :
    BaseRepository<TBaseModel>
    where TBaseModel :
    BaseModel
  {
    #region Parameters

    int MaxCount { get; }
    int SelectedIndex { get; set; }

    #endregion

    #region Logic

    /// <summary>
    /// Add a <typeparam><typeparamref name="TBaseService"/></typeparam>.
    /// </summary>
    /// <param name="baseService">The service</param>
    /// <returns>True/false result.</returns>
    bool Add(BaseService<BaseRepository<TBaseModel>, TBaseModel> baseService);

    /// <summary>
    /// Remove a <typeparam><typeparamref name="TBaseService"/></typeparam>.
    /// </summary>
    /// <param name="index">The index</param>
    /// <returns>True/false result.</returns>
    bool Remove(int index);

    /// <summary>
    /// Get a <typeparam><typeparamref name="TBaseService"/></typeparam>.
    /// </summary>
    /// <param name="index">The index</param>
    /// <returns>The repository.</returns>
    BaseService<BaseRepository<TBaseModel>, TBaseModel>? Get(int index);

    #endregion
  }
}