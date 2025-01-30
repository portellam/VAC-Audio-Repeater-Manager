using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Repositories
{
  /// <summary>
  /// Constructor
  /// </summary>
  /// <param name="list">the device list</param>
  public class RepeaterRepository :
    Repository<RepeaterModel>,
    IRepeaterRepository
  {
  }
}