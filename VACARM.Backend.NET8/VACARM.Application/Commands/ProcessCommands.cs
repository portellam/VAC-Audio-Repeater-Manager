#warning Differs from projects of earlier NET revisions (below Framework 4.6).

using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace VACARM.Application.Commands
{
  /// <summary>
  /// Run or kill a <typeparamref name="Process"/>.
  /// </summary>
  public static class ProcessCommands
  {
    #region Logic

    /// <summary>
    /// Kill a process asynchronously.
    /// </summary>
    /// <param name="process">The process</param>
    /// <returns>The exit code.</returns>
    public async static Task<int> KillAsync(Process process)
    {
      return await Kill(process)
        .ConfigureAwait(false);
    }

    /// <summary>
    /// Kill a process.
    /// </summary>
    /// <param name="process">the process</param>
    /// <returns>The exit code.</returns>
    private static Task<int> Kill(Process process)
    {
      TaskCompletionSource<int> taskCompletionSource =
        new TaskCompletionSource<int>();

      Task<int> task;
      int passCode = 0;
      int failCode = 1;

      if (process == null)
      {
        Debug.WriteLine
        (
          "Failed to kill the process. " +
          "The process is null."
        );

        taskCompletionSource.SetResult(passCode);
        task = taskCompletionSource.Task;
        return task;
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Killing the process\t=> Process: {0}",
          process
        )
      );

      process.Exited +=
        (
          sender,
          arguments
        ) => taskCompletionSource
          .SetResult(process.ExitCode);

      process.OutputDataReceived +=
        (
          sender,
          arguments
        ) => Debug.WriteLine
        (
          string.Format
          (
            "Output\t=> Arguments: {0}",
            arguments.Data
          )
        );

      process.ErrorDataReceived +=
        (
          sender,
          arguments
        ) => Debug.WriteLine
        (
          string.Format
          (
            "Error\t=> Arguments: {0}",
            arguments.Data
          )
        );

      try
      {
        process.Kill();
      }

      catch (Exception exception)
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Error\t=> Exception: {0}",
            exception.Message
          )
        );
      }

      if (!process.HasExited)
      {
        Debug.WriteLine("Failed to kill process.");
        taskCompletionSource.SetResult(failCode);
      }

      else
      {
        Debug.WriteLine("Killed process.");
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();
      }

      task = taskCompletionSource.Task;
      return task;
    }

    /// <summary>
    /// Run a process asynchronously.
    /// </summary>
    /// <param name="process">The process</param>
    /// <returns>The exit code.</returns>
    public async static Task<int> RunAsync(Process process)
    {
      return await Run(process)
        .ConfigureAwait(false);
    }

    /// <summary>
    /// Run a process.
    /// </summary>
    /// <param name="process">The process</param>
    /// <returns>The exit code.</returns>
    public static Task<int> Run(Process process)
    {
      TaskCompletionSource<int> taskCompletionSource =
        new TaskCompletionSource<int>();

      Task<int> task;
      int failCode = 1;

      if (process == null)
      {
        Debug.WriteLine
        (
          "Failed to run the process. " +
          "The process is null."
        );

        taskCompletionSource.SetResult(failCode);
        task = taskCompletionSource.Task;
        return task;
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Running the process\t=> Process: {0}",
          process
        )
      );

      process.Exited +=
        (
          sender,
          arguments
        ) => taskCompletionSource
          .SetResult(process.ExitCode);

      process.OutputDataReceived +=
        (
          sender,
          arguments
        ) => Debug.WriteLine
        (
          string.Format
          (
            "Output\t=> Arguments: {0}",
            arguments.Data
          )
        );

      process.ErrorDataReceived +=
        (
          sender,
          arguments
        ) => Debug.WriteLine
        (
          string.Format
          (
            "Error\t=> Arguments: {0}",
            arguments.Data
          )
        );

      bool isRunning = false;

      try
      {
        isRunning = process.Start();
      }

      catch (Exception exception)
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Error\t=> Exception: {0}",
            exception.Message
          )
        );

        isRunning = false;
      }

      if (!isRunning)
      {
        Debug.WriteLine("Failed to run process.");
        taskCompletionSource.SetResult(failCode);
      }

      else
      {
        Debug.WriteLine("Running process.");
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();
      }

      task = taskCompletionSource.Task;
      return task;
    }

    #endregion
  }
}