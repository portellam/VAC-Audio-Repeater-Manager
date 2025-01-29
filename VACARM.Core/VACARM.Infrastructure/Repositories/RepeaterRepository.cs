using VACARM.Core.Models;

namespace VACARM.Core.Repositories
{
  /// <summary>
  /// Constructor
  /// </summary>
  /// <param name="list">the device list</param>
  public class RepeaterRepository<T>(List<RepeaterModel> list)
    where T : RepeaterModel
  {
    #region Parameters

    private ListRepository<RepeaterModel> ListRepository =
      new ListRepository<RepeaterModel>(list);

    #endregion
  }
}