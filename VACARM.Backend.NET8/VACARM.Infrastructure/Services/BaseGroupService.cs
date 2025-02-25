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
      TGroupReadonlyRepository,
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
    where TGroupReadonlyRepository :
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

    private List<BaseService<BaseRepository<TBaseModel>, TBaseModel>>?
    list
    { get; set; }

    protected List<BaseService<BaseRepository<TBaseModel>, TBaseModel>>?
    List
    {
      get
      {
        return this.list;
      }
      set
      {
        this.list = value;
        base.Enumerable = value;
        base.OnPropertyChanged(nameof(List));
      }
    }

    public BaseRepository<TBaseModel>? SelectedRepository
    {
      get
      {
        try
        {
          return this.SelectedService
            .Repository;
        }

        catch (ArgumentOutOfRangeException exception)
        {
          this.SelectedRepository = new BaseRepository<TBaseModel>();

          return this.SelectedService
            .Repository;
        }
      }
      protected set
      {
        try
        {
          this.SelectedService
            .Repository = value;
        }

        catch (ArgumentOutOfRangeException exception)
        {
          this.SelectedService =
            new BaseService<BaseRepository<TBaseModel>, TBaseModel>();

          this.SelectedService
            .Repository = value;
        }

        base.OnPropertyChanged(nameof(SelectedRepository));
      }
    }

    public BaseService<BaseRepository<TBaseModel>, TBaseModel>?
    SelectedService
    {
      get
      {
        try
        {
          return this.List
            .ElementAt(this.SelectedIndex);
        }

        catch (ArgumentOutOfRangeException exception)
        {
          this.SelectedService =
            new BaseService<BaseRepository<TBaseModel>, TBaseModel>();

          return this.List
            .ElementAt(this.SelectedIndex);
        }
      }
      protected set
      {
        try
        {
          this.List[this.SelectedIndex] = value;
        }

        catch (ArgumentOutOfRangeException exception)
        {
          this.List
            .Add(value);

          this.SelectedIndex = this.List
            .Count();
        }

        base.OnPropertyChanged(nameof(SelectedService));
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
        base.OnPropertyChanged(nameof(SelectedIndex));
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
        base.OnPropertyChanged(nameof(MaxCount));
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
      this.List = new List<BaseService<BaseRepository<TBaseModel>, TBaseModel>>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="list">The list of services(s)</param>
    /// <param name="maxCount">The maximum count of service(s)</param>
    public BaseGroupService
    (
      List<BaseService<BaseRepository<TBaseModel>, TBaseModel>> list,
      int maxCount
    )
    {
      this.List = list;
      this.MaxCount = maxCount;
    }

    protected override void Dispose(bool isDisposed)
    {
      if (this.HasDisposed)
      {
        return;
      }

      if (isDisposed)
      {
        base.Dispose();
        this.List = null;
      }

      this.HasDisposed = true;
    }

    public bool Add
    (BaseService<BaseRepository<TBaseModel>, TBaseModel> baseService)
    {
      if (base.IsNullOrEmpty)
      {
        this.List =
          new List<BaseService<BaseRepository<TBaseModel>, TBaseModel>>();
      }

      if
      (
        this.List
          .Count() >= this.MaxCount
      )
      {
        return false;
      }

      this.List
        .Add(baseService);

      return true;
    }

    public bool Remove(int index)
    {
      if (base.IsNullOrEmpty)
      {
        return false;
      }

      if (!base.ContainsIndex(index))
      {
        return false;
      }

      this.List
        .RemoveAt(index);

      return true;
    }

    public BaseService<BaseRepository<TBaseModel>, TBaseModel>? Get(int index)
    {
      try
      {
        return this.List
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