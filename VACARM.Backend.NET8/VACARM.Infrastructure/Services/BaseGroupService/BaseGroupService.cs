using VACARM.Domain.Models;
using VACARM.Infrastructure.Functions;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Infrastructure.Services.BaseGroupService
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
        return list;
      }
      set
      {
        list = value;
        base.Enumerable = value;
        OnPropertyChanged(nameof(List));
      }
    }

    public BaseRepository<TBaseModel>? SelectedRepository
    {
      get
      {
        return SelectedService
          .Repository;
      }
      protected set
      {
        SelectedService
          .Repository = value;

        OnPropertyChanged(nameof(SelectedRepository));
      }
    }

    public BaseService<BaseRepository<TBaseModel>, TBaseModel>?
    SelectedService
    {
      get
      {
        return List
          .ElementAt(SelectedIndex);
      }
      protected set
      {
        List[SelectedIndex] = value;
        OnPropertyChanged(nameof(SelectedService));
      }
    }

    public int SelectedIndex
    {
      get
      {
        return selectedIndex;
      }
      set
      {
        if (value < MinCount)
        {
          value = MinCount;
        }

        selectedIndex = value;
        OnPropertyChanged(nameof(SelectedIndex));
      }
    }

    public readonly static int SafeMaxCount = byte.MaxValue;
        
    public virtual int MaxCount
    {
      get
      {
        return maxCount;
      }
      internal set
      {
        if (value < MinCount)
        {
          value = MinCount;
        }

        maxCount = value;
        OnPropertyChanged(nameof(MaxCount));
      }
    }

    #endregion

    #region Logic

    protected override void Dispose(bool isDisposed)
    {
      if (HasDisposed)
      {
        return;
      }

      if (isDisposed)
      {
        Dispose();
        List = null;
      }

      HasDisposed = true;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    public BaseGroupService() :
      base()
    {
      List = new List<BaseService<BaseRepository<TBaseModel>, TBaseModel>>();
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
      List = list;
      MaxCount = maxCount;
    }

    public BaseService<BaseRepository<TBaseModel>, TBaseModel>? Get(int index)
    {
      try
      {
        return List
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
        List =
          new List<BaseService<BaseRepository<TBaseModel>, TBaseModel>>();
      }

      if
      (
        List
          .Count() >= MaxCount
      )
      {
        return false;
      }

      List
        .Add(baseService);

      return true;
    }

    public bool Remove(int index)
    {
      if (base.IsNullOrEmpty)
      {
        return false;
      }

      if (!ContainsIndex(index))
      {
        return false;
      }

      List
        .RemoveAt(index);

      return true;
    }

    public bool Remove(uint id)
    {
      var func = BaseFunctions<TBaseModel>.ContainsId(id);

      return SelectedRepository
        .Remove(func);
    }

    public IEnumerable<bool> RemoveRange(IEnumerable<uint> idEnumerable)
    {
      var func = BaseFunctions<TBaseModel>.ContainsIdEnumerable(idEnumerable);

      return SelectedRepository
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

      return SelectedRepository
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

      return SelectedRepository
        .GetRange(func);
    }

    public IEnumerable<TBaseModel> GetAntiRange(IEnumerable<uint> idEnumerable)
    {
      var func = BaseFunctions<TBaseModel>.NotContainsIdEnumerable(idEnumerable);

      return SelectedRepository
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

      return SelectedRepository
        .GetRange(func);
    }

    public IEnumerable<TBaseModel> GetRange(IEnumerable<uint> idEnumerable)
    {
      var func = BaseFunctions<TBaseModel>.ContainsIdEnumerable(idEnumerable);

      return SelectedRepository
        .GetRange(func);
    }

    public IEnumerable<uint> GetAllId()
    {
      return SelectedRepository
        .GetAll()
        .Select(x => x.Id);
    }

    public TBaseModel? Get(uint id)
    {
      var func = BaseFunctions<TBaseModel>.ContainsId(id);

      return SelectedRepository
        .Get(func);
    }

    public void Deselect(uint id)
    {
      var func = BaseFunctions<TBaseModel>.ContainsId(id);

      SelectedRepository
        .Deselect(func);
    }

    public void DeselectRange(IEnumerable<uint> idEnumerable)
    {
      var func = BaseFunctions<TBaseModel>.ContainsIdEnumerable(idEnumerable);

      SelectedRepository
        .DeselectRange(func);
    }

    public void Select(uint id)
    {
      var func = BaseFunctions<TBaseModel>.ContainsId(id);

      SelectedRepository
        .Select(func);
    }

    public void SelectRange(IEnumerable<uint> idEnumerable)
    {
      var func = BaseFunctions<TBaseModel>.ContainsIdEnumerable(idEnumerable);

      SelectedRepository
        .SelectRange(func);
    }

    #endregion
  }
}