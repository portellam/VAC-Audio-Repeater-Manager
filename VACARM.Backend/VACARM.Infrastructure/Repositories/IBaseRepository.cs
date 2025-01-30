using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Repositories
{
  public interface IBaseRepository : IRepository<BaseModel>
  {
    #region Logic

    BaseModel? Get(uint id);
    
    List<BaseModel> GetRange
    (
      uint startId,
      uint endId
    );

    List<BaseModel> GetRange(List<uint> idList);

    void RemoveRange
    (
      uint startId,
      uint endId
    );

    void RemoveRange(List<uint> idList);

    #endregion
  }
}
