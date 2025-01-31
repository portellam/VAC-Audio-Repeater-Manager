using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Repositories
{
  public interface IRepeaterRepository : IGenericRepository<RepeaterModel>
  {
    #region Logic

    List<RepeaterModel> GetAllStarted();
    List<RepeaterModel> GetAllStopped();

    #endregion
  }
}
