using System.Collections.Generic;
using System.Threading.Tasks;

namespace VACARM.Infrastructure.Services
{
  public partial interface IRepeaterGroupService
    <
      TGroupReadonlyRepository,
      TBaseService,
      TBaseRepository,
      TRepeaterModel
    >
  {
    #region Logic

    /// <summary>
    /// Restart an enumerable of all <typeparamref name="TRepeaterModel"/>(s).
    /// </summary>
    Task<IEnumerable<int?>> RestartAll();

    /// <summary>
    /// Start an enumerable of all <typeparamref name="TRepeaterModel"/>(s).
    /// </summary>
    Task<IEnumerable<int?>> StartAll();

    /// <summary>
    /// Stop an enumerable of all <typeparamref name="TRepeaterModel"/>(s).
    /// </summary>
    Task<IEnumerable<int?>> StopAll();

    #endregion
  }
}