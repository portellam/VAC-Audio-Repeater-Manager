using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AudioRepeaterManager.NET4_0.Backend.Models;

namespace AudioRepeaterManager.NET4_0.Backend.Controllers
{
  public class RepeaterController
  {
    #region Parameters

    private RepeaterModel RepeaterModel;
    private RepeaterViewModel RepeaterViewModel;
    #endregion

    #region Logic
    /// <summary>
    /// Controller
    /// </summary>
    /// <param name="repeaterModel">The repeater model</param>
    /// <param name="repeaterViewModel">The repeater view model</param>
    [ExcludeFromCodeCoverage]
    public RepeaterController
      (
        RepeaterModel repeaterModel,
        RepeaterViewModel repeaterViewModel
      )
    {
      RepeaterModel = repeaterModel;
      RepeaterViewModel = repeaterViewModel;
    }

    /// <summary>
    /// Add the repeater.
    /// </summary>
    public void AddRepeater()
    {
      // add repeater to repository?
      // add repeater to view
    }

    /// <summary>
    /// Delete the repeater.
    /// </summary>
    public void DeleteRepeater()
    {
      // TODO: in View, have StopRepeater precede DeleteRepeater?
      // remove repeater from view
      // remove repeater from repository?
    }

    /// <summary>
    /// Restart the repeater.
    /// </summary>
    /// <returns>0 if successful, 1 if failed.</returns>
    public async Task<byte> RestartRepeater()
    {
      if (
        StopRepeater().Result != 0
        || StartRepeater().Result != 0
        )
      {
        return 1;
      }

      return 0;
    }

    /// <summary>
    /// Start the repeater.
    /// </summary>
    /// <returns>0 if successful, 1 if failed.</returns>
    public async Task<byte> StartRepeater()
    {
      // try to start repeater.
      // verify if repeater is started.
      // async?

      return 1;
    }

    /// <summary>
    /// Stop the repeater.
    /// </summary>
    /// <returns>0 if successful, 1 if failed.</returns>
    public async Task<byte> StopRepeater()
    {
      // try to stop repeater.
      // verify if repeater is stopped.
      // async?

      return 1;
    }

    /// <summary>
    /// Update the repeater.
    /// </summary>
    /// <param name="doStopRepeater">True/false stop the repeater</param>
    public async void UpdateRepeater(bool doStopRepeater)
    {
      // update repeater in repository?
      // update repeater in view
      // restart repeater
    }

    #endregion
  }
}