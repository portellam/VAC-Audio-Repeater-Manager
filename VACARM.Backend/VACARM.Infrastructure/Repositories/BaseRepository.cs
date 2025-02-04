using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Repositories
{
  public class BaseRepository<T> :
    GenericListRepository<T>,
    IBaseRepository<T> where T : BaseModel
  {
    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    public BaseRepository()
    {
      List = new List<T>();
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