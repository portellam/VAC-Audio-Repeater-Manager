using System.Collections.Generic;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Infrastructure.Services.BaseGroupService
{
  public interface IBaseGroupService<TBaseModel>
    where TBaseModel :
    class,
    IBaseModel,
    new()
  {
    #region Parameters

    BaseRepository<TBaseModel> SelectedRepository { get; }
    BaseService<TBaseModel> SelectedService { get; }
    uint SelectedId { get; set; }

    #endregion

    #region Logic

    /// <summary>
    /// Export a service.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <param name="filePathName">The file path name</param>
    void Export
    (
      uint id,
      string filePathName = null
    );

    /// <summary>
    /// Import a service.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <param name="filePathName">The file path name</param>
    void Import
    (
      uint id,
      string filePathName = null
    );

    #endregion
  }
}