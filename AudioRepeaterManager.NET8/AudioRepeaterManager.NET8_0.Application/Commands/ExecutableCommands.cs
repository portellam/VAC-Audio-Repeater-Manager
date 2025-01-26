using System.Diagnostics;

namespace AudioRepeaterManager.NET8_0.Application.Commands
{
  public class ExecutableCommands
  {
    #region Logic

    private static Process defaultProcess { get; set; } = new Process()
    {
      EnableRaisingEvents = true,

      StartInfo =
        {
          CreateNoWindow = true,
          RedirectStandardError = true,
          RedirectStandardOutput = true,
          UseShellExecute = false,
          WindowStyle = ProcessWindowStyle.Hidden,
        },
    };

    /// <summary>
    /// Get the process for the executable.
    /// </summary>
    /// <param name="processId">The process ID</param>
    /// <returns>The process</returns>
    private static Process? Get(int? processId)
    {
      if
      (
        processId is null
        || processId < 0
      )
      {
        Debug.WriteLine
        (
          "Process ID is either null or less than zero."
        );

        return null;
      }

      try
      {
        return Process.GetProcessById((int)processId);
      }
      catch
      {
        return null;
      }
    }

    /// <summary>
    /// Is the executable running.
    /// </summary>
    /// <param name="processId">The process ID</param>
    /// <returns>True/false is the executable running.</returns>
    public static bool IsRunning(int? processId)
    {
      return Get(processId) != null;
    }

    /// <summary>
    /// Restart the executable.
    /// </summary>
    /// <param name="processId">The process ID</param>
    /// <param name="fileName">The executable file name</param>
    /// <param name="startArguments">The start arguments</param>
    /// <param name="stopArguments">The stop arguments</param>
    /// <returns>The process ID.</returns>
    public async static Task<int?> Restart
    (
      int? processId,
      string fileName,
      string startArguments,
      string stopArguments
    )
    {
      Debug.WriteLine
      (
        string.Format
        (
          "Restarting executable\t=> Process ID: {0}",
          processId
        )
      );

      int? result = await Stop
        (
          processId,
          stopArguments
        );

      bool isRunning = result == 0;

      if (isRunning)
      {
        Debug.WriteLine("Failed to restart executable.");
        return result;
      }

      result = await Start
        (
          processId,
          fileName,
          startArguments
        );

      isRunning = IsRunning(result);

      if (!isRunning)
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
    /// <param name="processId">The process ID</param>
    /// <param name="fileName">The executable file name</param>
    /// <param name="startArguments">The start arguments</param>
    /// <returns>The actual process ID.</returns>
    public async static Task<int?> Start
    (
      int? processId,
      string fileName,
      string startArguments
    )
    {
      Process? process = Get(processId);

      if
      (
        string.IsNullOrEmpty(fileName)
        || string.IsNullOrWhiteSpace(fileName)
      )
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Failed to start executable. " +
            "File name is either empty, null, or whitespace\t=> " +
            "ProcessId: {0}, FileName: {1}, StartArguments: {2}",
            processId,
            fileName,
            startArguments
          )
        );

        return null;
      }

      if (process != null)
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Executable already started\t=> " +
            "ProcessId: {0}, FileName: {1}, StartArguments: {2}",
            processId,
            fileName,
            startArguments
          )
        );

        return processId;
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Starting executable\t=> " +
          "ProcessId: {0}, FileName: {1}, StartArguments: {2}",
          processId,
          fileName,
          startArguments
        )
      );

      process = defaultProcess;
      process.StartInfo.FileName = fileName;
        
      bool isArgumentsValid =
        !(
          string.IsNullOrEmpty(startArguments)
          || string.IsNullOrWhiteSpace(startArguments)
        );

      if (isArgumentsValid)
      {
        process.StartInfo.Arguments = startArguments;
      }

      var result = await ProcessCommands.RunAsync(process);
      bool isRunning = IsRunning(result);

      if (isRunning)
      {
        Debug.WriteLine("Failed to start executable.");
      }

      else
      {
        Debug.WriteLine("Started executable.");
      }

      return process.Id;
    }

    /// <summary>
    /// Stop the executable.
    /// </summary>
    /// <param name="processId">The process ID</param>
    /// <param name="stopArguments">The stop arguments</param>
    /// <returns>The exit code.</returns>
    public async static Task<int> Stop
    (
      int? processId,
      string stopArguments
    )
    {
      int result = 1;
      Process? process = Get(processId);

      if (process is null)
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Executable already stopped\t=> " +
            "ProcessId: {0}, StopArguments: {1}",
            processId,
            stopArguments
          )
        );

        result = 0;
        return result;
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Starting executable\t=> " +
          "ProcessId: {0}, StopArguments: {1}",
          processId,
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

      bool isRunning = IsRunning(result);

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