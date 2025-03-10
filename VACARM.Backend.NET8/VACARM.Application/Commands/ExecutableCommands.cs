using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace VACARM.Application.Commands
{
  /// <summary>
  /// Start, stop, and query an executable's process.
  /// </summary>
  public class ExecutableCommands
  {
    #region Logic

    /// <summary>
    /// The default process.
    /// </summary>
    private static Process DefaultProcess { get; set; } = new Process()
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
    private static Process Get(int? processId)
    {
      Process process = new Process();

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

        return process;
      }

      try
      {
        process = Process.GetProcessById((int)processId);

        Debug.WriteLine
        (
          string.Format
          (
            "Process: {0}",
            process
          )
        );
      }

      catch (Exception exception)
      {
        Debug.WriteLine
        (
          "Failed to get process. " +
          "Process ID is either null or less than zero."
        );

        process = new Process();
      }

      return process;
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
    /// <param name="filePathName">The file path name</param>
    /// <param name="startArguments">The start arguments</param>
    /// <param name="stopArguments">The stop arguments</param>
    /// <returns>The process ID.</returns>
    public async static Task<int?> RestartAsync
    (
      int? processId,
      string filePathName,
      string startArguments,
      string stopArguments
    )
    {
      Debug.WriteLine
      (
        string.Format
        (
          "Restarting the executable\t=> Process ID: {0}",
          processId
        )
      );

      int? result = await StopAsync
        (
          processId,
          stopArguments
        );

      bool isRunning = result == 0;

      if (isRunning)
      {
        Debug.WriteLine("Failed to restart the executable.");
        return result;
      }

      result = await StartAsync
        (
          processId,
          filePathName,
          startArguments
        );

      isRunning = IsRunning(result);

      if (!isRunning)
      {
        Debug.WriteLine("Failed to restart the executable.");
      }

      else
      {
        Debug.WriteLine("Restarted the executable.");
      }

      return result;
    }

    /// <summary>
    /// Start the executable.
    /// </summary>
    /// <param name="processId">The process ID</param>
    /// <param name="filePathName">The file path name</param>
    /// <param name="startArguments">The start arguments</param>
    /// <returns>The actual process ID.</returns>
    public async static Task<int?> StartAsync
    (
      int? processId,
      string filePathName,
      string startArguments
    )
    {
      Process process = Get(processId);

      if
      (
        string.IsNullOrEmpty(filePathName)
        || string.IsNullOrWhiteSpace(filePathName)
      )
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Failed to start executable. " +
            "File name is either empty, null, or whitespace\t=> " +
            "Process ID: {0}, File Name: {1}, Start Arguments: {2}",
            processId,
            filePathName,
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
            "Process ID: {0}, File Name: {1}, Start Arguments: {2}",
            processId,
            filePathName,
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
          "Process ID: {0}, File Name: {1}, Start Arguments: {2}",
          processId,
          filePathName,
          startArguments
        )
      );

      process = DefaultProcess;
      process.StartInfo.FileName = filePathName;

      bool startAnArgument =
        !(
          string.IsNullOrEmpty(startArguments)
          || string.IsNullOrWhiteSpace(startArguments)
        );

      if (startAnArgument)
      {
        process.StartInfo.Arguments = startArguments;
      }

      var result = await ProcessCommands.RunAsync(process);
      bool isRunning = IsRunning(result);

      if (isRunning)
      {
        Debug.WriteLine("Failed to start the executable.");
      }

      else
      {
        Debug.WriteLine("Started the executable.");
      }

      return process.Id;
    }

    /// <summary>
    /// Stop the executable.
    /// </summary>
    /// <param name="processId">The process ID</param>
    /// <param name="stopArguments">The stop arguments</param>
    /// <returns>The exit code.</returns>
    public async static Task<int> StopAsync
    (
      int? processId,
      string stopArguments
    )
    {
      int result = 1;
      Process process = Get(processId);

      if (process == null)
      {
        Debug.WriteLine
        (
          string.Format
          (
            "The executable has already stopped\t=> " +
            "Process ID: {0}, Stop Arguments: {1}",
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
          "Starting the executable\t=> " +
          "Process ID: {0}, Stop Arguments: {1}",
          processId,
          stopArguments
        )
      );

      bool killWithPrejudice =
        string.IsNullOrEmpty(stopArguments)
        || string.IsNullOrWhiteSpace(stopArguments);

      if (killWithPrejudice)
      {
        result = await ProcessCommands.KillAsync(process);
      }

      else
      {
        process.StartInfo.Arguments = stopArguments;
        result = await ProcessCommands.RunAsync(process);
      }

      bool isRunning = IsRunning(result);

      if (isRunning)
      {
        Debug.WriteLine("Failed to stop the executable.");
      }

      else
      {
        Debug.WriteLine("Stopped the executable.");
      }

      return result;
    }
  }

  #endregion
}