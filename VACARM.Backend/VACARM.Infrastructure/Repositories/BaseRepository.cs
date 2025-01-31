using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Repositories
{
  public class BaseRepository :
    GenericRepository<BaseModel>,
    IBaseRepository
  {
    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    public BaseRepository()
    {
    }

    public BaseModel? Get(uint id)
    {
      Func<BaseModel, bool> func = (BaseModel x) => x.Id == id;
      return Get(func);
    }

    public List<BaseModel> GetRange
    (
      uint startId, 
      uint endId
    )
    {
      Func<BaseModel, bool> func = (BaseModel x) =>
        x.Id >= startId
        && x.Id <= endId;

      return GetRange(func)
        .ToList();
    }

    public List<BaseModel> GetRange(List<uint> idList)
    {
      List<BaseModel> modelList = new List<BaseModel>();

      idList
        .ForEach
        (
          x =>
          {
            BaseModel? model = Get(x);

            if (model != null)
            {
              modelList.Add(model);
            }
          }
        );

      return modelList;
    }

    public void RemoveRange
    (
      uint startId,
      uint endId
    )
    {
      Func<BaseModel, bool> func = (BaseModel x) =>
        x.Id >= startId
        && x.Id <= endId;

      RemoveRange(func);
    }

    public void RemoveRange(List<uint> idList)
    {
      List<BaseModel> modelList = new List<BaseModel>();

      idList
        .ForEach
        (
          x =>
          {
            BaseModel? model = Get(x);

            if (model != null)
            {
              modelList.Add(model);
            }
          }
        );

      RemoveRange(modelList);
    }

    #endregion
  }
}