using System.ComponentModel;
using VACARM.Application.Services;
using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Repositories
{
  public interface IBaseServiceRepository
    <
      TBaseService,
      TBaseModel
    >
    where TBaseService :
    BaseService<BaseRepository<TBaseModel>, TBaseModel>
    where TBaseModel :
    BaseModel
  {
    #region Parameters

    BaseService<BaseRepository<TBaseModel>, TBaseModel>? SelectedBaseService
    { get; }

    event PropertyChangedEventHandler PropertyChanged;
    int MaxCount { get; }
    int SelectedIndex { get; set; }

    #endregion

    #region Logic

    /// <summary>
    /// Get a <typeparamref name="TBaseService"/>.
    /// </summary>
    /// <param name="index">The index</param>
    /// <returns>The base service.</returns>
    BaseService<BaseRepository<TBaseModel>, TBaseModel>? Get(int index);

    /// <summary>
    /// Remove a <typeparamref name="TBaseService"/>.
    /// </summary>
    /// <param name="baseService">The base service</param>
    /// <returns>True/false result.</returns>
    bool Remove(int index);

    /// <summary>
    /// Add a <typeparamref name="TBaseService"/>.
    /// </summary>
    /// <param name="baseService">The base service</param>
    void Add(BaseService<BaseRepository<TBaseModel>, TBaseModel> baseService);

    #endregion
  }
}