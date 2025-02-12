using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Services
{
  /// <summary>
  /// The service of the <typeparamref name="RepeaterRepository"/>.
  /// </summary>
  public class RepeaterService<TRepository, TRepeaterModel> :
    BaseService<RepeaterRepository<TRepeaterModel>, TRepeaterModel>,
    IRepeaterService<RepeaterRepository<TRepeaterModel>, TRepeaterModel> where TRepository :
    RepeaterRepository<TRepeaterModel> where TRepeaterModel :
    RepeaterModel
  {
    #region Parameters

    #endregion

    #region Logic

    public RepeaterService()
    {
      base._Repository = new RepeaterRepository<TRepeaterModel>();
    }

    #endregion
  }
}