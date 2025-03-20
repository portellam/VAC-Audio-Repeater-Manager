using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Infrastructure.Services
{
  /// <summary>
  /// The repository for <typeparamref name="TBaseModel"/>.
  /// </summary>
  /// <typeparam name="TBaseModel"></typeparam>
  public partial class BaseService<TBaseModel> :
    Service
    <
      ObservableCollection<TBaseModel>,
      TBaseModel
    >,
    IBaseModel,
    IBaseService<TBaseModel>
    where TBaseModel :
    class,
    IBaseModel,
    new()
  {
    #region Parameters

    private uint id { get; set; }

    public new BaseRepository<TBaseModel> Repository
    {
      get
      {
        return (BaseRepository<TBaseModel>)base.Repository;
      }
      set
      {
        base.Repository = value;
        base.OnPropertyChanged(nameof(this.Repository));
      }
    }

    public string FilePathName { get; set; } = string.Empty;

    public uint Id
    {
      get
      {
        return id;
      }
      set
      {
        id = value;
        base.OnPropertyChanged(nameof(this.Id));
      }
    }

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="id">The ID</param>
    /// <param name="filePathName">The file path name</param>
    [ExcludeFromCodeCoverage]
    public BaseService
    (
      uint id,
      string filePathName = null
    ) :
      base(new BaseRepository<TBaseModel>())
    {
      this.Id = id;

      if (string.IsNullOrWhiteSpace(filePathName))
      {
        filePathName = string.Empty;
      }

      this.FilePathName = filePathName;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="id">The ID</param>
    /// <param name="repository">The repository</param>
    /// <param name="filePathName">The file path name</param>
    [ExcludeFromCodeCoverage]
    public BaseService
    (
      uint id,
      BaseRepository<TBaseModel> repository,
      string filePathName = null
    ) :
      base(repository)
    {
      this.Id = id;

      if (string.IsNullOrWhiteSpace(filePathName))
      {
        filePathName = string.Empty;
      }

      this.FilePathName = filePathName;
    }

    #endregion
  }
}