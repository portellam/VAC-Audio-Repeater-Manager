#warning Differs from projects of earlier NET revisions (below Framework 4.6).

using System.Collections.Generic;
using System.Threading.Tasks;

namespace VACARM.Infrastructure.Services
{
  public partial interface IDeviceGroupService
    <
      TGroupReadonlyRepository,
      TBaseService,
      TBaseRepository,
      TDeviceModel
    >
  {
    #region Logic


    /// <summary>
    /// Update the service.
    /// </summary>
    /// <returns>True/false result.</returns>
    Task<bool> UpdateServiceAsync();

    /// <summary>
    /// Get the default communications <typeparamref name="TDeviceModel"/>.
    /// </summary>
    /// <param name="isInput">True/false is an input</param>
    /// <param name="isOutput">True/false is an output</param>
    /// <returns>The item.</returns>
    Task<TDeviceModel> GetDefaultCommunicationsAsync
    (
      bool isInput,
      bool isOutput
    );

    /// <summary>
    /// Get the default console <typeparamref name="TDeviceModel"/>.
    /// </summary>
    /// <param name="isInput">True/false is an input</param>
    /// <param name="isOutput">True/false is an output</param>
    /// <returns>The item.</returns>
    Task<TDeviceModel> GetDefaultConsoleAsync
    (
      bool isInput,
      bool isOutput
    );

    /// <summary>
    /// Get the default multimedia <typeparamref name="TDeviceModel"/>.
    /// </summary>
    /// <param name="isInput">True/false is an input</param>
    /// <param name="isOutput">True/false is an output</param>
    /// <returns>The item.</returns>
    Task<TDeviceModel> GetDefaultMultimediaAsync
    (
      bool isInput,
      bool isOutput
    );

    #endregion
  }
}