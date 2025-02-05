using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Repositories
{
  public class BaseRepository<T> :
    GenericListRepository<T>,
    IBaseRepository<T> where T : BaseModel
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
        uint id = IdList.Max();
        id++;
        return id;
      }
    }

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    public BaseRepository()
    {
      List = new List<T>();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="maxCount">The maximum count of item(s)</param>
    public BaseRepository(int maxCount)
    {
      List = new List<T>();
      MaxCount = maxCount;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="list">The list of item(s)</param>
    public BaseRepository(List<T> list)
    {
      List = list;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="list">The list of item(s)</param>
    /// <param name="maxCount">The maximum count of item(s)</param>
    public BaseRepository
    (
      List<T> list,
      int maxCount
    )
    {
      List = list;
      MaxCount = maxCount;
    }

    public void Add(IBaseModel model)
    {
      if (model == null)
      {
        return;
      }

      if (List.Contains(model))
      {
        return;
      }

      if (IdList.Contains(model.Id))
      {
        model.Id = NextId;
      }

      Add(model);
    }

    public IBaseModel? Get(uint id)
    {
      Func<IBaseModel, bool> func = (IBaseModel x) => x.Id == id;
      return Get(func);
    }

    public IEnumerable<IBaseModel> GetRange
    (
      uint startId,
      uint endId
    )
    {
      Func<IBaseModel, bool> func = (IBaseModel x) =>
        x.Id >= startId
        && x.Id <= endId;

      return GetRange(func);
    }

    public IEnumerable<IBaseModel> GetRange(IEnumerable<uint> idEnumerable)
    {
      Func<IBaseModel, bool> func = (IBaseModel x) => idEnumerable.Contains(x.Id);
      return GetRange(func);
    }

    public void Remove(uint id)
    {
      Func<IBaseModel, bool> func = (IBaseModel x) => x.Id == id;
      Remove(func);
    }

    public void RemoveRange(uint id)
    {
      Func<IBaseModel, bool> func = (IBaseModel x) => x.Id == id;
      RemoveRange(func);
    }

    public void RemoveRange
    (
      uint startId,
      uint endId
    )
    {
      Func<IBaseModel, bool> func = (IBaseModel x) =>
        x.Id >= startId
        && x.Id <= endId;

      RemoveRange(func);
    }

    public void RemoveRange(IEnumerable<uint> idEnumerable)
    {
      Func<IBaseModel, bool> func = (IBaseModel x) => idEnumerable.Contains(x.Id);
      RemoveRange(func);
    }

    public void Update
    (
      uint id,
      IBaseModel model
    )
    {
      Func<IBaseModel, bool> func = (IBaseModel x) => x.Id == id;

      Update
      (
        func,
        (T)model
      );
    }

    public void UpdateRange
    (
       IEnumerable<uint> idEnumerable,
       IBaseModel model
    )
    {
      Func<IBaseModel, bool> func = (IBaseModel x) => idEnumerable.Contains(x.Id);

      UpdateRange
      (
        func,
        (T)model
      );
    }

    #endregion
  }
}