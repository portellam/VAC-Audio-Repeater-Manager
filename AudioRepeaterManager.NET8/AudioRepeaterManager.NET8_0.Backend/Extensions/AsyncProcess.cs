using System.Diagnostics;

namespace AudioRepeaterManager.NET8_0.Backend.Extensions
{
  public class AsyncProcess : IAsyncProcess
  {
    #region Logic

    /// <summary>
    /// Run process asynchronously.
    /// </summary>
    /// <param name="fileName">the executable file name</param>
    /// <param name="arguments">the arguments</param>
    /// <returns>The async task.</returns>
    public static async Task<int> RunProcessAsync
    (
      string fileName,
      string arguments
    )
    {
      using
        (
          var process = new Process()
          {
            StartInfo =
            {
              Arguments = arguments,
              CreateNoWindow = true,
              FileName = fileName,
              RedirectStandardError = true,
              RedirectStandardOutput = true,
              UseShellExecute = false,
              WindowStyle = ProcessWindowStyle.Hidden,
            },

            EnableRaisingEvents = true
          }
        )
      {
        return await RunProcessAsync(process)
          .ConfigureAwait(false);
      }
    }

    /// <summary>
    /// Run process asynchronously.
    /// </summary>
    /// <param name="process">The process</param>
    /// <returns>The task.</returns>
    private static Task<int> RunProcessAsync(Process process)
    {
      TaskCompletionSource<int> taskCompletionSource = new TaskCompletionSource<int>();

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
        ) => Console.WriteLine(arguments.Data);

      process.ErrorDataReceived += 
        (
          sender,
          arguments
        ) => Console.WriteLine("ERR: " + arguments.Data);

      bool isStarted = process.Start();

      if (!isStarted)
      {
        Debug.WriteLine("Could not start process: " + process);
        int failCode = 1;
        taskCompletionSource.SetResult(failCode);
      }
      
      else
      {
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();
      }

      return taskCompletionSource.Task;
    }

    #endregion
  }
}