using System.Diagnostics.CodeAnalysis;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Functions;
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

    public bool Remove(uint id)
    {
      var func = BaseFunctions<TBaseModel>.ContainsId(id);

      return this.Repository
        .Remove(func);
    }

    public IEnumerable<bool> RemoveRange(IEnumerable<uint> idEnumerable)
    {
      var func = BaseFunctions<TBaseModel>.ContainsIdEnumerable(idEnumerable);

      return this.Repository
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

      return this.Repository
        .RemoveRange(func);
    }

    public TBaseModel? Get(uint id)
    {
      var func = BaseFunctions<TBaseModel>.ContainsId(id);

      return base.Repository
        .Get(func);
    }

    public IEnumerable<TBaseModel> GetAllById(IEnumerable<uint> idEnumerable)
    {
      var func = BaseFunctions<TBaseModel>.ContainsIdEnumerable(idEnumerable);

      return base.Repository
        .GetRange(func);
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

      return base.Repository
        .GetRange(func);
    }

    public IEnumerable<TBaseModel> GetRange(IEnumerable<uint> idEnumerable)
    {
      var func = BaseFunctions<TBaseModel>.ContainsIdEnumerable(idEnumerable);

      return base.Repository
        .GetRange(func);
    }

    public IEnumerable<uint> GetAllId()
    {
      return base.Repository
        .GetAll()
        .Select(x => x.Id);
    }

    #endregion
  }
}