using System.Collections.Generic;
using System.Linq;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Functions;
using VACARM.Infrastructure.Repositories;
using VACARM.Infrastructure.Services.BaseGroupService;

namespace VACARM.Infrastructure.Services
{
  /// <summary>
  /// The repository of <typeparamref name="TBaseService"/>(s).
  /// </summary>
  public partial class BaseGroupService<TBaseModel> :
    Service
    <
      IList<BaseService<TBaseModel>>,
      BaseService<TBaseModel>
    >,
    IBaseGroupService<TBaseModel>
    where TBaseModel :
    BaseModel
  {
    #region Parameters

    private int maxCount { get; set; } = SafeMaxCount;
    private int selectedIndex { get; set; } = MinCount;
    private readonly static int MinCount = 0;

    public BaseRepository<TBaseModel> SelectedRepository
    {
      get
      {
        return (BaseRepository<TBaseModel>)this.SelectedService
          .Repository;
      }
      protected set
      {
        this.SelectedService
          .Repository = value;

        base.OnPropertyChanged(nameof(this.SelectedRepository));
      }
    }

    public BaseService<TBaseModel> SelectedService
    {
      get
      {
        return this.Get(this.SelectedIndex);
      }
      protected set
      {
        this.Update
          (
            this.SelectedIndex, 
            value
          );

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

    /// <summary>
    /// Constructor
    /// </summary>
    public BaseGroupService() :
      base(new List<BaseService<TBaseModel>>())
    {
      this.Add(new BaseService<TBaseModel>());
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="list">The list of services(s)</param>
    /// <param name="maxCount">The maximum count of service(s)</param>
    public BaseGroupService
    (
      IList<BaseService<TBaseModel>> list,
      int maxCount
    ) :
      base(list)
    {
      this.MaxCount = maxCount;
    }

    public BaseService<TBaseModel> Get(int index)
    {
      return this.Repository
        .Enumerable
        .ElementAtOrDefault(index);
    }

    public bool Add(BaseService<TBaseModel> baseService)
    {
      if
      (
        this.Repository
          .IsNullOrEmpty
      )
      {
        this.Repository =
          new Repository<IList<BaseService<TBaseModel>>, BaseService<TBaseModel>>
            (new List<BaseService<TBaseModel>>());
      }

      if
      (
        this.Repository
          .Count >= this.MaxCount
      )
      {
        return false;
      }

      this.Repository
        .Add(baseService);

      return true;
    }

    public bool Remove(int index)
    {
      if
      (
        this.Repository
          .IsNullOrEmpty
      )
      {
        return false;
      }

      if
      (
        !this.Repository
          .ContainsIndex(index)
      )
      {
        return false;
      }

      this.Repository
        .Enumerable
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

    public TBaseModel Get(uint id)
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

    public void Update
    (
      int index,
      BaseService<TBaseModel> baseService
    )
    {
      if
      (
        !this.Repository
          .ContainsIndex(index)
      )
      {
        return;
      }

      this.Repository
        .Enumerable
        .RemoveAt(index);

      this.Repository
        .Enumerable
        .Insert
        (
          index,
          baseService
        );
    }

    #endregion
  }
}