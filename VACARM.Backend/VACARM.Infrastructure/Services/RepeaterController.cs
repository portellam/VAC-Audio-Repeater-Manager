using VACARM.Domain.Models;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Services
{
  /// <summary>
  /// The service of the <typeparamref name="RepeaterRepository"/>.
  /// </summary>
  public class RepeaterService<TRepository, TItem> :
    BaseService<RepeaterRepository<RepeaterModel>, RepeaterModel>,
    IRepeaterService<RepeaterRepository<RepeaterModel>, RepeaterModel> where TRepository :
    RepeaterRepository<RepeaterModel> where TItem :
    RepeaterModel
  {
    #region Parameters

    #endregion

    #region Logic

    public RepeaterService()
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