using VACARM.Application.Services;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Infrastructure.Services
{
  /// <summary>
  /// The repository of <typeparamref name="TBaseService"/>(s).
  /// </summary>
  public class BaseGroupService
    <
      TBaseService,
      TBaseRepository,
      TBaseModel
    > :
    ReadonlyGroupService
    <
      ReadonlyRepository
      <
        BaseService
        <
          TBaseRepository,
          TBaseModel
        >
      >,
      BaseService
      <
        TBaseRepository,
        TBaseModel
      >,
      BaseRepository<TBaseModel>,
      TBaseModel
    >,
    IBaseGroupService
    <
      BaseService
      <
        BaseRepository<TBaseModel>,
        TBaseModel
      >,
      BaseRepository<TBaseModel>,
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

    /// <summary>
    /// The list of all <typeparamref name="TService"/>(s).
    /// </summary>
    protected List<BaseService<BaseRepository<TBaseModel>, TBaseModel>> BaseServiceList
    {
      get
      {
        return this.ReadonlyServiceList;
      }
      set
      {
        this.ReadonlyServiceList = value;
        this.OnPropertyChanged(nameof(BaseServiceList));
      }
    }

    public BaseService<BaseRepository<TBaseModel>, TBaseModel>? SelectedBaseService
    {
      get
      {
        try
        {
          return this.BaseServiceList
            .ElementAt(this.SelectedIndex);
        }
        catch
        {
          return null;
        }
      }
    }

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    public BaseGroupService() :
      base()
    {
      this.BaseServiceList = 
        new List<BaseService<BaseRepository<TBaseModel>, TBaseModel>>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="baseServiceList">The service list</param>
    /// <param name="maxCount">The maximum count of service(s)</param>
    public BaseGroupService
    (
      List<BaseService<BaseRepository<TBaseModel>, TBaseModel>> baseServiceList,
      int maxCount
    )
    {
      this.BaseServiceList = baseServiceList;
      this.MaxCount = maxCount;
    }

    public bool Add(BaseService<BaseRepository<TBaseModel>, TBaseModel> baseService)
    {
      if (this.Enumerable.Count() >= this.MaxCount)
      {
        return false;
      }

      this.BaseServiceList
        .Add(baseService);

      return true;
    }

    public bool Remove(int index)
    {
      if (!this.ContainsIndex(index))
      {
        return false;
      }

      this.BaseServiceList
        .RemoveAt(index);

      return true;
    }

    public BaseService<BaseRepository<TBaseModel>, TBaseModel>? Get(int index)
    {
      if (this.IsNullOrEmpty(this.BaseServiceList))
      {
        return null;
      }

      try
      {
        return this.BaseServiceList
          .ElementAt(index);
      }
      catch
      {
        return null;
      }
    }

    #endregion
  }
}