using System.Collections.Generic;
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

    public RepeaterController()
    {
      base.Repository = new RepeaterRepository<RepeaterModel>();

      RepeaterModel? model = (RepeaterModel)base.Get((uint)0);
    }

    public IEnumerable<RepeaterModel> GetAllAlphabeticalOrder()
    {
      return base.GetAll()
        .OrderBy(x => x.WindowName);
    }

    public IEnumerable<RepeaterModel> GetAllByDeviceId(uint deviceId)
    {
      return base.GetAll()
        .Where
        (
          x =>
          x.InputDeviceId == deviceId
          || x.OutputDeviceId == deviceId
        );
    }

    public IEnumerable<RepeaterModel> GetAllReverseAlphabeticalOrder()
    {
      return base.GetAll()
        .OrderByDescending(x => x.WindowName);
    }

    public IEnumerable<RepeaterModel> GetAllStarted()
    {
      return base.GetAll()
        .Where(x => x.IsStarted);
    }

    public IEnumerable<RepeaterModel> GetAllStopped()
    {
      return base.GetAll()
        .Where(x => !x.IsStarted);
    }

    #endregion
  }
}