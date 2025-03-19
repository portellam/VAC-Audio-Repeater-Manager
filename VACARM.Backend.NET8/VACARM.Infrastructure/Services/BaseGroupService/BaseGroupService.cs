using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Functions;
using VACARM.Infrastructure.Repositories;
using VACARM.Infrastructure.Services.BaseGroupService;

namespace VACARM.Infrastructure.Services
{
  /// <summary>
  /// The repository of <typeparamref name="TBaseModel"/> service(s).
  /// </summary>
  public partial class BaseGroupService<TBaseModel> :
    Service
    <
      IEnumerable<BaseService<TBaseModel>>,
      BaseService<TBaseModel>
    >
    where TBaseModel :
    BaseModel
  {
    #region Parameters

    /// <summary>
    /// The next valid ID.
    /// </summary>
    internal uint NextId
    {
      get
      {
        uint id = this.IdEnumerable
          .Max();

        id++;
        return id;
      }
    }

    /// <summary>
    /// The enumerable of ID(s).
    /// </summary>
    private IEnumerable<uint> IdEnumerable
    {
      get
      {
        var func = BaseServiceFunctions
          <
            BaseService<TBaseModel>,
            TBaseModel
          >.GetId;

        var idEnumerable = this.Repository
          .GetAll()
          .Select(x => func(x));

        idEnumerable.OrderBy(x => x);
        return idEnumerable;
      }
    }

    private int maxCount { get; set; } = int.MaxValue;

    private uint selectedId { get; set; } = MinCount;

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
        return this.Get(this.IsValidIndex);
      }
      protected set
      {
        this.UpdateService
          (
            this.SelectedId,
            value
          );

        base.OnPropertyChanged(nameof(this.SelectedService));
      }
    }

    public readonly static uint MinCount = uint.MinValue;
    public readonly static int SafeMaxCount = byte.MaxValue;

    public uint SelectedId
    {
      get
      {
        return this.selectedId;
      }
      set
      {
        if (value < MinCount)
        {
          value = MinCount;
        }

        this.selectedId = value;
        base.OnPropertyChanged(nameof(this.SelectedId));
      }
    }

    public virtual int MaxCount
    {
      get
      {
        return this.maxCount;
      }
      internal set
      {
        this.maxCount = value;
        base.OnPropertyChanged(nameof(this.MaxCount));
      }
    }

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="maxCount">The maximum count of service(s)</param>
    public BaseGroupService(int maxCount) :
      base
      (
        new HashSet<BaseService<TBaseModel>>()
      )
    {
      var model = new BaseModel(this.SelectedId);
      var enumerable = new ObservableCollection<TBaseModel>();
      var baseRepository = new BaseRepository<TBaseModel>(enumerable);

      var service = new BaseService<TBaseModel>
        (
          model,
          baseRepository,
          string.Empty
        );

      this.Add(service);
      this.MaxCount = maxCount;
    }

    public BaseService<TBaseModel> Get(Func<BaseService<TBaseModel>, bool> func)
    {
      return base.Repository
        .Get(func);
    }

    public BaseService<TBaseModel> Get(uint id)
    {
      var func = BaseServiceFunctions
        <
          BaseService<TBaseModel>,
          TBaseModel
        >.ContainsId(id);

      return this.Get(func);
    }

    public bool Add(BaseService<TBaseModel> service)
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
        .Add(service);

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

    public async void ExportService
    (
      int index,
      string filePathName = null
    )
    {
      if
      (
        index < MinCount
        || index > MaxCount
      )
      {
        return;
      }

      var service = this.Get(index);

      if (service == null)
      {
        return;
      }

      if (string.IsNullOrWhiteSpace(service.FilePathName))
      {
        if (string.IsNullOrWhiteSpace(filePathName))
        {
          return;
        }

        service.FilePathName = filePathName;
      }

      await service.WriteAllToFile();
    }

    public async void ImportService
    (
      int index,
      string filePathName = null
    )
    {
      if
      (
        !this.ContainsIndex(index)
      )
      {
        return;
      }

      var service = this.Get(index);

      if (string.IsNullOrWhiteSpace(service.FilePathName))
      {
        if (string.IsNullOrWhiteSpace(filePathName))
        {
          return;
        }

        service.FilePathName = filePathName;
      }

      await service.ReadRangeFromFile();

      this.UpdateService
        (
          index,
          service
        );
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

    public void UpdateService
    (
      uint id,
      BaseService<TBaseModel> service
    )
    {

      this.Enumerable
        .Insert
        (
          index,
          service
        );
    }

    public void UpdateService
    (
      int index,
      IEnumerable<TBaseModel> enumerable
    )
    {
      if (enumerable.IsNullOrEmpty())
      {
        return;
      }

      if
      (
        !this.Repository
          .ContainsIndex(index)
      )
      {
        return;
      }

      var service = this.Get(index);

      if (service == null)
      {
        service = new BaseService<TBaseModel>();
      }

      else
      {
        service.Repository
          .Dispose();
      }

      service.Repository
        .AddRange(enumerable);
    }

    #endregion
  }
}