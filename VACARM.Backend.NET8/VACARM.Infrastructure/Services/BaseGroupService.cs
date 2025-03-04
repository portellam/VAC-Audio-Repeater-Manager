using VACARM.Domain.Models;
using VACARM.Infrastructure.Functions;
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
        base.OnPropertyChanged(nameof(this.List));
      }
    }

    public BaseRepository<TBaseModel>? SelectedRepository
    {
      get
      {
        return this.SelectedService
          .Repository;
      }
      protected set
      {
        this.SelectedService
          .Repository = value;

        base.OnPropertyChanged(nameof(this.SelectedRepository));
      }
    }

    public BaseService<BaseRepository<TBaseModel>, TBaseModel>?
    SelectedService
    {
      get
      {
        return this.List
          .ElementAt(this.SelectedIndex);
      }
      protected set
      {
        this.List[this.SelectedIndex] = value;
        base.OnPropertyChanged(nameof(this.SelectedService));
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
        base.OnPropertyChanged(nameof(this.SelectedIndex));
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
        base.OnPropertyChanged(nameof(this.MaxCount));
      }
    }

    #endregion

    #region Logic

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

    public bool Remove(uint id)
    {
      var func = BaseFunctions<TBaseModel>.ContainsId(id);

      return this.SelectedRepository
        .Remove(func);
    }

    public IEnumerable<bool> RemoveRange(IEnumerable<uint> idEnumerable)
    {
      var func = BaseFunctions<TBaseModel>.ContainsIdEnumerable(idEnumerable);

      return this.SelectedRepository
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

      return this.SelectedRepository
        .RemoveRange(func);
    }

    public IEnumerable<TBaseModel> GetAntiRange
    (
      uint startId,
      uint endId
    )
    {
      var func = BaseFunctions<TBaseModel>.NotContainsIdRange
        (
          startId,
          endId
        );

      return this.SelectedRepository
        .GetRange(func);
    }

    public IEnumerable<TBaseModel> GetAntiRange(IEnumerable<uint> idEnumerable)
    {
      var func = BaseFunctions<TBaseModel>.NotContainsIdEnumerable(idEnumerable);

      return this.SelectedRepository
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

      return this.SelectedRepository
        .GetRange(func);
    }

    public IEnumerable<TBaseModel> GetRange(IEnumerable<uint> idEnumerable)
    {
      var func = BaseFunctions<TBaseModel>.ContainsIdEnumerable(idEnumerable);

      return this.SelectedRepository
        .GetRange(func);
    }

    public IEnumerable<uint> GetAllId()
    {
      return this.SelectedRepository
        .GetAll()
        .Select(x => x.Id);
    }

    public TBaseModel? Get(uint id)
    {
      var func = BaseFunctions<TBaseModel>.ContainsId(id);

      return this.SelectedRepository
        .Get(func);
    }

    public void Deselect(uint id)
    {
      var func = BaseFunctions<TBaseModel>.ContainsId(id);

      this.SelectedRepository
        .Deselect(func);
    }

    public void DeselectRange(IEnumerable<uint> idEnumerable)
    {
      var func = BaseFunctions<TBaseModel>.ContainsIdEnumerable(idEnumerable);

      this.SelectedRepository
        .DeselectRange(func);
    }

    public void Select(uint id)
    {
      var func = BaseFunctions<TBaseModel>.ContainsId(id);

      this.SelectedRepository
        .Select(func);
    }

    public void SelectRange(IEnumerable<uint> idEnumerable)
    {
      var func = BaseFunctions<TBaseModel>.ContainsIdEnumerable(idEnumerable);

      this.SelectedRepository
        .SelectRange(func);
    }

    #endregion
  }
}