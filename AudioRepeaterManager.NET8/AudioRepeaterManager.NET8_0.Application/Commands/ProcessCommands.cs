using System.Diagnostics;

namespace AudioRepeaterManager.NET8_0.Application.Commands
{
  public static class ProcessCommands
  {
    #region Logic

    /// <summary>
    /// Run process asynchronously.
    /// </summary>
    /// <param name="process">The process</param>
    /// <returns>The exit code.</returns>
    public async static Task<int> RunAsync(Process process)
    {
      return await Run(process)
        .ConfigureAwait(false);
    }

    /// <summary>
    /// Run process.
    /// </summary>
    /// <param name="process">The process</param>
    /// <returns>The exit code.</returns>
    public static Task<int> Run(Process process)
    {
      TaskCompletionSource<int> taskCompletionSource =
        new TaskCompletionSource<int>();

      Task<int> task;
      int failCode = 1;

      if (process is null)
      {
        Debug.WriteLine
        (
          "Failed to run process. " +
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
          "Running process\t=> Process: {0}",
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

      bool isStarted = process.Start();

      if (!isStarted)
      {
        taskCompletionSource.SetResult(failCode);
      }

      else
      {
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();
      }

      task = taskCompletionSource.Task;
      return task;
    }

    #endregion
  }
}