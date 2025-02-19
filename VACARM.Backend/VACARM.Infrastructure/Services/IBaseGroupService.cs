using VACARM.Application.Services;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Infrastructure.Services
{
  public interface IBaseGroupService
    <
      TServiceRepository,
      TBaseService,
      TBaseRepository,
      TBaseModel
    >
    where TServiceRepository :
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
    
    BaseService<BaseRepository<TBaseModel>, TBaseModel>? SelectedBaseService
    { get; }

    #endregion

    #region Logic

    /// <summary>
    /// Add a <typeparamref name="TBaseService"/>.
    /// </summary>
    /// <param name="baseService">The service</param>
    /// <returns>True/false result.</returns>
    bool Add(BaseService<BaseRepository<TBaseModel>, TBaseModel> baseService);

    /// <summary>
    /// Remove a <typeparamref name="TBaseService"/>.
    /// </summary>
    /// <param name="index">The index</param>
    /// <returns>True/false result.</returns>
    bool Remove(int index);

    /// <summary>
    /// Get a <typeparamref name="TBaseService"/>.
    /// </summary>
    /// <param name="index">The index</param>
    /// <returns>The repository.</returns>
    BaseService<BaseRepository<TBaseModel>, TBaseModel>? Get(int index);


    #endregion
  }
}