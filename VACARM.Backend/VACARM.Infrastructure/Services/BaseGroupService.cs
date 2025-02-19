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
      TServiceRepository,
      TBaseService,
      TBaseRepository,
      TBaseModel
    > :
    ReadonlyRepository
    <
      BaseService
      <
        BaseRepository<TBaseModel>,
        TBaseModel
      >
    >,
    IBaseGroupService
    <
      ReadonlyRepository
      <
        BaseService
        <
          BaseRepository<TBaseModel>,
          TBaseModel
        >
      >,
      BaseService
      <
        BaseRepository<TBaseModel>,
        TBaseModel
      >,
      BaseRepository<TBaseModel>,
      TBaseModel
    >
    where TServiceRepository :
    ReadonlyRepository
    <
      BaseService
      <
        BaseRepository<TBaseModel>,
        TBaseModel
      >
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

    private int maxCount { get; set; } = SafeMaxCount;
    private int selectedIndex { get; set; } = MinCount;
    private readonly static int MinCount = 0;

    /// <summary>
    /// The list of all <typeparamref name="TBaseService"/>(s).
    /// </summary>
    protected List<BaseService<BaseRepository<TBaseModel>, TBaseModel>>
      BaseServiceList
    {
      get
      {
        return this.Enumerable
          .ToList();
      }
      set
      {
        this.Enumerable = value;
        this.OnPropertyChanged(nameof(BaseServiceList));
      }
    }

    public int SelectedIndex
    {
      get
      {
        return this.selectedIndex;
      }
      set
      {
        if (value < MinCount)
        {
          value = MinCount;
        }

        this.selectedIndex = value;
        this.OnPropertyChanged(nameof(SelectedIndex));
      }
    }

    public BaseService<BaseRepository<TBaseModel>, TBaseModel>?
    SelectedBaseService
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

    public readonly static int SafeMaxCount = byte.MaxValue;

    public virtual int MaxCount
    {
      get
      {
        return this.maxCount;
      }
      internal set
      {
        if (value < MinCount)
        {
          value = MinCount;
        }

        this.maxCount = value;
        this.OnPropertyChanged(nameof(MaxCount));
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
    /// <param name="baseServiceList">The list of services(s)</param>
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

    public bool Add
    (BaseService<BaseRepository<TBaseModel>, TBaseModel> baseService)
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