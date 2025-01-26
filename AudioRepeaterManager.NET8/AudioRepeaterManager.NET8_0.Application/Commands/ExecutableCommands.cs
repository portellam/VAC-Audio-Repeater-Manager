using System.Diagnostics;

namespace AudioRepeaterManager.NET8_0.Application.Commands
{
  public class ExecutableCommands
  {
    #region Logic

    /// <summary>
    /// Is the executable running.
    /// </summary>
    /// <param name="process">The process</param>
    /// <returns>True/false is the executable running.</returns>
    private static bool IsRunning(Process process)
    {
      if (process is null)
      {
        Debug.WriteLine
        (
          "Executable is not running. " +
          "Process is null."
        );

        return false;
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Executable is running\t=> Process:{0}",
          process
        )
      );

      return true;
    }

    /// <summary>
    /// Restart the executable.
    /// </summary>
    /// <param name="process">The process</param>
    /// <param name="startArguments">The start arguments</param>
    /// <returns>The exit code.</returns>
    public async static Task<int> Restart
    (
      Process process,
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
      Process process,
      string startArguments
    )
    {
      bool isRunning = IsRunning(process);
      var result = 1;

      if (isRunning)
      {
        result = 0;
        return result;
      }

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

      Debug.WriteLine
      (
        string.Format
        (
          "Starting executable\t=> StartArguments: {0}",
          startArguments
        )
      );

      isRunning = result == 0;

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
      Process process,
      string stopArguments
    )
    {
      int result = 1;
      bool isRunning = IsRunning(process);

      if (!isRunning)
      {
        result = 0;
        return result;
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Stopping executable\t=> StopArguments: {0}",
          stopArguments
        )
      );

      if
      (
        string.IsNullOrEmpty(stopArguments)
        || string.IsNullOrWhiteSpace(stopArguments)
      )
      {
        process.Kill();
        isRunning = IsRunning(process);
        result = Convert.ToInt32(isRunning);
      }

      else
      {
        process.StartInfo.Arguments = stopArguments;
        result = await ProcessCommands.RunAsync(process);
      }

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