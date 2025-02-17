using System.Diagnostics.CodeAnalysis;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Functions;

namespace VACARM.Infrastructure.Repositories
{
  public class BaseRepository<TBaseModel> :
    ReadonlyRepository<TBaseModel>,
    IBaseRepository<TBaseModel> where TBaseModel :
    BaseModel
  {
    #region Parameters

    public Func<int, bool> IsValidIndex
    {
      get
      {
        return new Func<int, bool>
          (
            x =>
            {
              return x >= 0
              && x <= this.MaxCount;
            }
          );
      }
    }

    /// <summary>
    /// The enumerable of ID(s).
    /// </summary>
    private IEnumerable<uint> IdEnumerable
    {
      get
      {
        IEnumerable<uint> idEnumerable = this.Enumerable
          .Select(x => x.Id);

        idEnumerable.Order();
        return idEnumerable;
      }
    }

    private int maxCount { get; set; } = int.MaxValue;

    private Type Type
    {
      get
      {
        return this.Enumerable
          .GetType();
      }
    }

    /// <summary>
    /// The next valid ID.
    /// </summary>
    private uint NextId
    {
      get
      {
        uint id = this.IdEnumerable
          .Max();

        id++;
        return id;
      }
    }

    protected List<TBaseModel> List
    {
      get
      {
        return this.Enumerable
          .ToList();
      }
      set
      {
        this.Enumerable = value;
        this.OnPropertyChanged(nameof(List));
      }
    }

    public virtual int MaxCount
    {
      get
      {
        return this.maxCount;
      }
      set
      {
        this.maxCount = value;
        this.OnPropertyChanged(nameof(MaxCount));
      }
    }

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    [ExcludeFromCodeCoverage]
    public BaseRepository() : 
      base()
    {
      this.List = new List<TBaseModel>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="list">The list of item(s)</param>
    /// <param name="maxCount">The maximum count of item(s)</param>
    [ExcludeFromCodeCoverage]
    public BaseRepository
    (
      List<TBaseModel> list,
      int maxCount
    ) : 
      base
      (
        list,
        maxCount
      )
    {
      this.Enumerable = list;
      this.MaxCount = maxCount;
    }

    public override void Add(TBaseModel? model)
    {
      if (model == null)
      {
        return;
      }

      if (model.GetType() != typeof(TBaseModel))
      {
        return;
      }

      if (IdEnumerable.Contains(model.Id))
      {
        model.Id = NextId;
      }

      base.Add(model);
    }

    public TBaseModel? Get(uint id)
    {
      var func = BaseFunctions<TBaseModel>.ContainsId(id);
      return base.Get(func);
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

      return base.GetRange(func);
    }

    public IEnumerable<TBaseModel> GetRange(IEnumerable<uint> idEnumerable)
    {
      var func = BaseFunctions<TBaseModel>.ContainsIdEnumerable(idEnumerable);
      return base.GetRange(func);
    }

    public void Remove(uint id)
    {
      var func = BaseFunctions<TBaseModel>.ContainsId(id);
      base.Remove(func);
    }

    public void RemoveRange(uint id)
    {
      var func = BaseFunctions<TBaseModel>.ContainsId(id);
      base.RemoveRange(func);
    }

    public void RemoveRange
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

      base.RemoveRange(func);
    }

    public void RemoveRange(IEnumerable<uint> idEnumerable)
    {
      var func = BaseFunctions<TBaseModel>.ContainsIdEnumerable(idEnumerable);
      this.RemoveRange(func);
    }

    public void Update(TBaseModel model)
    {
      if (model == null)
      {
        return;
      }

      var func = BaseFunctions<TBaseModel>.ContainsId(model.Id);

      if (!func(model))
      {
        return;
      }

      this.Remove(func);
      this.Add(model);
    }

    public void UpdateRange(IEnumerable<TBaseModel> enumerable)
    {
      var idEnumerable = enumerable.Select(x => x.Id);
      var func = BaseFunctions<TBaseModel>.ContainsIdEnumerable(idEnumerable);

      foreach (var item in enumerable)
      {
        this.Update(item);
      }
    }

    #endregion
  }
}