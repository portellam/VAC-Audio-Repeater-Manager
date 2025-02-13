using System.Diagnostics.CodeAnalysis;
using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Repositories
{
  /// <summary>
  /// The <typeparamref name="TBaseModel"/> repository.
  /// </summary>
  public class BaseRepository<TBaseModel> :
    ListRepository<TBaseModel>,
    IBaseRepository<TBaseModel> where TBaseModel :
    BaseModel
  {
    #region Parameters


    /// <summary>
    /// The list of IDs.
    /// </summary>
    private List<uint> IdList
    {
      get
      {
        List<uint> idList = List
          .Select(x => x.Id)
          .ToList();

        idList.Order();
        return idList;
      }
    }

    /// <summary>
    /// The next valid ID.
    /// </summary>
    private uint NextId
    {
      get
      {
        uint id = this.IdList.Max();
        id++;
        return id;
      }
    }

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    [ExcludeFromCodeCoverage]
    public BaseRepository()
    {
      this.List = new List<TBaseModel>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="maxCount">The maximum count of item(s)</param>
    [ExcludeFromCodeCoverage]
    public BaseRepository(int maxCount)
    {
      this.List = new List<TBaseModel>();
      this.MaxCount = maxCount;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="list">The list of item(s)</param>
    [ExcludeFromCodeCoverage]
    public BaseRepository(List<TBaseModel> list)
    {
      this.List = list;
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
    )
    {
      this.List = list;
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

      if (IdList.Contains(model.Id))
      {
        model.Id = NextId;
      }

      base.Add(model);
    }

    public TBaseModel? Get(uint id)
    {
      Func<TBaseModel, bool> func = (TBaseModel x) => x.Id == id;
      return base.Get(func);
    }

    public IEnumerable<TBaseModel> GetAllById()
    {
      return base
        .GetAll()
        .OrderBy(x => x.Id);
    }

    public IEnumerable<TBaseModel> GetAllByIdDescending()
    {
      return base
        .GetAll()
        .OrderByDescending(x => x.Id);
    }

    public IEnumerable<TBaseModel> GetRange
    (
      uint startId,
      uint endId
    )
    {
      Func<TBaseModel, bool> func = (TBaseModel x) =>
        x.Id >= startId
        && x.Id <= endId;

      return base.GetRange(func);
    }

    public IEnumerable<TBaseModel> GetRange(IEnumerable<uint> idEnumerable)
    {
      Func<TBaseModel, bool> func = (TBaseModel x) => idEnumerable.Contains(x.Id);
      return base.GetRange(func);
    }

    public void Remove(uint id)
    {
      Func<TBaseModel, bool> func = (TBaseModel x) => x.Id == id;
      base.Remove(func);
    }

    public void RemoveRange(uint id)
    {
      Func<TBaseModel, bool> func = (TBaseModel x) => x.Id == id;
      base.RemoveRange(func);
    }

    public void RemoveRange
    (
      uint startId,
      uint endId
    )
    {
      Func<TBaseModel, bool> func = (TBaseModel x) =>
        x.Id >= startId
        && x.Id <= endId;

      base.RemoveRange(func);
    }

    public void RemoveRange(IEnumerable<uint> idEnumerable)
    {
      Func<TBaseModel, bool> func = (TBaseModel x) => idEnumerable.Contains(x.Id);
      base.RemoveRange(func);
    }

    public void Update
    (
      uint id,
      TBaseModel model
    )
    {
      Func<TBaseModel, bool> func = (TBaseModel x) => x.Id == id;

      base.Update
      (
        func,
        (TBaseModel)model
      );
    }

    public void UpdateRange
    (
       IEnumerable<uint> idEnumerable,
       TBaseModel model
    )
    {
      Func<TBaseModel, bool> func = (TBaseModel x) => idEnumerable.Contains(x.Id);

      base.UpdateRange
      (
        func,
        (TBaseModel)model
      );
    }

    #endregion
  }
}