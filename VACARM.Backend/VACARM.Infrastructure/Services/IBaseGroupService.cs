using VACARM.Application.Services;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Infrastructure.Services
{
  /// <summary>
  /// The <typeparamref name="TBaseRepository"/> service.
  /// </summary>
  public interface IBaseGroupService
    <
      TBaseService,
      TBaseRepository,
      TBaseModel
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
    /// Add a <typeparamref name="TService"/>.
    /// </summary>
    /// <param name="baseService">The service</param>
    /// <returns>True/false result.</returns>
    bool Add(BaseService<BaseRepository<TBaseModel>, TBaseModel> baseService);

    /// <summary>
    /// Get a <typeparamref name="TService"/>.
    /// </summary>
    /// <param name="index">The index</param>
    /// <returns>The service.</returns>
    BaseService<BaseRepository<TBaseModel>, TBaseModel>? Get(int index);

    #endregion
  }
}