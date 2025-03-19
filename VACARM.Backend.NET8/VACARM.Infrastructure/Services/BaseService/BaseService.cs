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
      IEnumerable<TBaseModel>,
      TBaseModel
    >,
    IBaseService<TBaseModel>
    where TBaseModel :
    BaseModel
  {
    #region Parameters

    private BaseRepository<TBaseModel> repository { get; set; }

    public BaseModel Model { get; private set; }

    public override Repository
      <
        IEnumerable<TBaseModel>,
        TBaseModel
      > Repository
    {
      get
      {
        return this.repository;
      }
      set
      {
        this.repository = (BaseRepository<TBaseModel>)value;
        base.OnPropertyChanged(nameof(this.Repository));
      }
    }

    public string FilePathName { get; set; } = string.Empty;

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="model">The base model</param>
    /// <param name="repository">The repository</param>
    /// <param name="filePathName">The file path name</param>
    [ExcludeFromCodeCoverage]
    public BaseService
    (
      BaseModel model,
      BaseRepository<TBaseModel> repository,
      string filePathName = null
    ) :
      base(repository)
    {
      this.Model = model;

      if (string.IsNullOrWhiteSpace(filePathName))
      {
        filePathName = string.Empty;
      }

      this.FilePathName = filePathName;
    }

    #endregion
  }
}