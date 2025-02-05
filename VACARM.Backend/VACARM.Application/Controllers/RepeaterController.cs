using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Controllers
{
  /// <summary>
  /// The controller of the <typeparamref name="RepeaterRepository"/>.
  /// </summary>
  public class RepeaterController<T1, T2> :
    BaseController<RepeaterRepository<RepeaterModel>, RepeaterModel>,
    IRepeaterController<RepeaterRepository<RepeaterModel>, RepeaterModel> where T1 :
    RepeaterRepository<RepeaterModel> where T2 :
    RepeaterModel
  {
    #region Parameters

    #endregion

    #region Logic

    #endregion
  }
}