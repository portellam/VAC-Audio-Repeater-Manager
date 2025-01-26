using System.Diagnostics;
using AudioRepeaterManager.NET8_0.Domain.Models;

namespace AudioRepeaterManager.NET8_0.Application.Commands
{
  public class RepeaterCommands
  {
    #region Logic

    /// <summary>
    /// Get the process for the repeater.
    /// </summary>
    /// <param name="processId">The process ID</param>
    /// <returns>The process</returns>
    private static Process? Get(int processId)
    {
      try
      {
        return Process.GetProcessById(processId);
      }
      catch
      {
        return null;
      }
    }

    /// <summary>
    /// Is the repeater running.
    /// </summary>
    /// <param name="processId">The process ID</param>
    /// <returns>True/false is the repeater running.</returns>
    public static bool IsRunning(int processId)
    {
      return Get(processId) != null;
    }

    /// <summary>
    /// Restart the repeater.
    /// </summary>
    /// <param name="model">The repeater model</param>
    /// <returns>The exit code.</returns>
    public async static Task<int> Restart(RepeaterModel model)
    {
      int result = 1;

      if (model is null)
      {
        Debug.WriteLine
        (
          "Failed to restart the repeater. " +
          "The repeater is null."
        );

        return 1;
      }

      Debug.WriteLine
        (
          string.Format
          (
            "Restarting the repeater\t=> RepeaterModel: {0}",
            model
          )
        );

      result = await Stop(model);

      if (result != 0)
      {
        Debug.WriteLine("Failed to restart the repeater.");
        return result;
      }

      result = await Start(model);

      if (result != 0)
      {
        Debug.WriteLine("Failed to restart the repeater.");
      }

      else
      {
        Debug.WriteLine("Restarted the repeater.");
      }

      return result;
    }

    /// <summary>
    /// Start the repeater.
    /// </summary>
    /// <param name="model">The repeater model</param>
    /// <returns>The exit code.</returns>
    public async static Task<int> Start(RepeaterModel model)
    {
      int result = 1;

      if (model is null)
      {
        Debug.WriteLine
        (
          "Failed to start the repeater. " +
          "The repeater is null."
        );

        return 1;
      }

      Debug.WriteLine
        (
          string.Format
          (
            "Starting the repeater\t=> RepeaterModel: {0}",
            model
          )
        );

      Process? process = Get(model);

      result = await ExecutableCommands.Start
        (
          process,
          model.StartArguments
        );

      if (result != 0)
      {
        Debug.WriteLine("Failed to start the repeater.");
      }

      else
      {
        Debug.WriteLine("Started the repeater.");
      }

      return result;
    }

    /// <summary>
    /// Stop the repeater.
    /// </summary>
    /// <param name="model">The repeater model</param>
    /// <returns>The exit code.</returns>
    public async static Task<int> Stop(RepeaterModel model)
    {
      int result = 1;

      if (model is null)
      {
        Debug.WriteLine
        (
          "Failed to stop the repeater. " +
          "The repeater is null."
        );

        return 1;
      }

      Debug.WriteLine
        (
          string.Format
          (
            "Stopping the repeater\t=> RepeaterModel: {0}",
            model
          )
        );

      Process? process = Get(model);

      result = await ExecutableCommands.Stop
        (
          process,
          model.StartArguments
        );

      if (result != 0)
      {
        Debug.WriteLine("Failed to stop the repeater.");
      }

      else
      {
        Debug.WriteLine("Stopped the repeater.");
      }

      return result;
    }

    #endregion
  }
}