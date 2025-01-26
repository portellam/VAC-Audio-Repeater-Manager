using System.Diagnostics;

namespace AudioRepeaterManager.NET8_0.Application.Commands
{
  public class ExecutableCommands
  {
    #region Logic

    /// <summary>
    /// Restart the executable.
    /// </summary>
    /// <param name="process">The process</param>
    /// <param name="startArguments">The start arguments</param>
    /// <returns>The exit code.</returns>
    public async static Task<int> Restart
    (
      Process? process,
      string startArguments,
      string stopArguments
    )
    {
      Debug.WriteLine
      (
        string.Format
        (
          "Restarting executable\t=> Process: {0}",
          process
        )
      );

      var result = await Stop
        (
          process,
          stopArguments
        );

      bool isStarted = result == 0;

      if (isStarted)
      {
        Debug.WriteLine("Failed to restart executable.");
        return result;
      }

      result = await Start
        (
          process,
          startArguments
        );

      isStarted = result == 0;

      if (!isStarted)
      {
        Debug.WriteLine("Failed to restart executable.");
      }

      else
      {
        Debug.WriteLine("Restarted executable.");
      }

      return result;
    }

    /// <summary>
    /// Start the executable.
    /// </summary>
    /// <param name="process">The process</param>
    /// <param name="startArguments">The start arguments</param>
    /// <returns>The exit code.</returns>
    public async static Task<int> Start
    (
      Process? process,
      string startArguments
    )
    {
      var result = 1;

      if (process is null)
      {
        Debug.WriteLine
        (
          "Failed to start executable. " +
          "Process is null."
        );

        return result;
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Starting executable\t=> Process: {0}, StartArguments: {1}",
          process,
          startArguments
        )
      );

      bool isArgumentsValid =
        !(
          string.IsNullOrEmpty(startArguments)
          || string.IsNullOrWhiteSpace(startArguments)
        );

      if (isArgumentsValid)
      {
        process.StartInfo.Arguments = startArguments;
      }

      result = await ProcessCommands.RunAsync(process);

      bool isRunning = result == 0;

      if (isRunning)
      {
        Debug.WriteLine("Failed to start executable.");
      }

      else
      {
        Debug.WriteLine("Started executable.");
      }

      return result;
    }

    /// <summary>
    /// Stop the executable.
    /// </summary>
    /// <param name="process">The process</param>
    /// <param name="stopArguments">The stop arguments</param>
    /// <returns>The exit code.</returns>
    public async static Task<int> Stop
    (
      Process? process,
      string stopArguments
    )
    {
      int result = 1;

      if (process is null)
      {
        Debug.WriteLine
        (
          "Failed to stop executable. " +
          "Process is null."
        );

        return result;
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Stopping executable\t=> Process: {0}, StopArguments: {1}",
          process,
          stopArguments
        )
      );

      if
      (
        string.IsNullOrEmpty(stopArguments)
        || string.IsNullOrWhiteSpace(stopArguments)
      )
      {
        try
        {
          process.Kill();
          result = 0;
        }
        catch (Exception exception)
        {
          Debug.WriteLine
          (
            string.Format
            (
              "Error\t=> Exception: {0}",
              exception
            )
          );

          result = 1;
        }
      }

      else
      {
        process.StartInfo.Arguments = stopArguments;
        result = await ProcessCommands.RunAsync(process);
      }

      bool isRunning = result == 0;

      if (isRunning)
      {
        Debug.WriteLine("Failed to stop executable.");
      }

      else
      {
        Debug.WriteLine("Stopped executable.");
      }

      return result;
    }
  }

  #endregion
}