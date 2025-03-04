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
    [ExcludeFromCodeCoverage]
    public BaseService(BaseRepository<TBaseModel> repository) :
      base(repository)
    {
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