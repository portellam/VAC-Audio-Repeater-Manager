using System.Diagnostics.CodeAnalysis;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Functions;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Services
{
  public class BaseService<TRepository, TBaseModel> :
    ReadonlyService<BaseRepository<TBaseModel>, TBaseModel>,
    IBaseService<BaseRepository<TBaseModel>, TBaseModel> where TRepository :
    BaseRepository<TBaseModel> where TBaseModel :
    BaseModel
  {
    #region Parameters

    public BaseRepository<TBaseModel> BaseRepository
    { 
      get
      {
        return (BaseRepository<TBaseModel>)this.Repository;
      }
      set
      {
        this.Repository = value;
        this.OnPropertyChanged(nameof(BaseRepository));
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
      this.Repository = new BaseRepository<TBaseModel>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repository">The repository</param>
    [ExcludeFromCodeCoverage]
    public BaseService(BaseRepository<TBaseModel> repository) :
      base(repository)
    {
      this.Repository = repository;
    }

    public bool Remove(uint id)
    {
      var func = BaseFunctions<TBaseModel>.ContainsId(id);
      
      return this.BaseRepository
        .Remove(func);
    }

    public IEnumerable<TBaseModel> GetAllById(IEnumerable<uint> idEnumerable)
    {
      var func = BaseFunctions<TBaseModel>.ContainsIdEnumerable(idEnumerable);

      return this.Repository
        .GetRange(func);
    }

    public IEnumerable<bool> RemoveRange(IEnumerable<uint> idEnumerable)
    {
      var func = BaseFunctions<TBaseModel>.ContainsIdEnumerable(idEnumerable);
      
      return this.BaseRepository
        .RemoveRange(func);
    }

    public IEnumerable<bool> RemoveRange
    (
      uint startId,
      uint endId
    )
    {
      var func = BaseFunctions<TBaseModel>.ContainsIdRange
        (
          startId,
          endId
        );

      return this.BaseRepository
        .RemoveRange(func);
    }

    public TBaseModel? Get(uint id)
    {
      var func = BaseFunctions<TBaseModel>.ContainsId(id);

      return this.BaseRepository
        .Get(func);
    }

    public IEnumerable<TBaseModel> GetRange
    (
      uint startId,
      uint endId
    )
    {
      var func = BaseFunctions<TBaseModel>.ContainsIdRange
        (
          startId,
          endId
        );

      return this.BaseRepository
        .GetRange(func);
    }

    public IEnumerable<TBaseModel> GetRange(IEnumerable<uint> idEnumerable)
    {
      var func = BaseFunctions<TBaseModel>.ContainsIdEnumerable(idEnumerable);

      return this.BaseRepository
        .GetRange(func);
    }

    #endregion
  }
}