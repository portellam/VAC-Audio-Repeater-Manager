using System.Diagnostics.CodeAnalysis;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Infrastructure.Services
{
  public class BaseService
    <
      TRepository,
      TBaseModel
    > :
    ReadonlyService
    <
      BaseRepository<TBaseModel>,
      TBaseModel
    >,
    IBaseService
    <
      BaseRepository<TBaseModel>,
      TBaseModel
    >
    where TRepository :
    BaseRepository<TBaseModel>
    where TBaseModel :
    BaseModel
  {
    #region Parameters

    internal new virtual BaseRepository<TBaseModel> Repository
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

    // TODO: define a default value.
    public string FilePathName { get; set; } = string.Empty;

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    [ExcludeFromCodeCoverage]
    public BaseService() :
      base()
    {
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repository">The repository</param>
    /// <param name="filePathName">The file path name</param>
    [ExcludeFromCodeCoverage]
    public BaseService
    (
      BaseRepository<TBaseModel> repository,
      string filePathName
    ) :
      base(repository)
    {
      this.FilePathName = filePathName;
    }

    protected override void Dispose(bool isDisposed)
    {
      if (this.HasDisposed)
      {
        return;
      }

      if (isDisposed)
      {
        base.Repository
          .Dispose();

        this.Repository = null;
      }

      this.HasDisposed = true;
    }

    #endregion
  }
}