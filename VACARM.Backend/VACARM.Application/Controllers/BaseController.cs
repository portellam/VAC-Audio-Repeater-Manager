using VACARM.Domain.Models;

namespace VACARM.Application.Controllers
{
  public class BaseController :
    GenericController<BaseModel>,
    IBaseController
  {
    #region Logic

    public BaseModel? Get(uint id)
    {
      Func<BaseModel, bool> func = (BaseModel x) => x.Id == id;
      return Get(func);
    }

    public IEnumerable<BaseModel> GetRange(List<uint> idList)
    {

    }

    public IEnumerable<BaseModel> GetRange
    (
      uint startId,
      uint endId
    )
    {

    }

    #endregion
  }
}
