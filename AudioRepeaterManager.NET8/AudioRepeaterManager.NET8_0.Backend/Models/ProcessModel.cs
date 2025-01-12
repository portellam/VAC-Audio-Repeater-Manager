using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace AudioRepeaterManager.NET8_0.Backend.Models
{
  public class ProcessModel :
    IProcessModel,
    INotifyPropertyChanged
  {
    #region Parameters

    private int id { get; set; } = 0;

    private Process process { get; set; } = new Process()
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

    private Process Process
    {
      get
      {
        return process;
      }
      set
      {
        process = value;
        OnPropertyChanged(nameof(Process));
      }
    }

    private string? startArguments { get; set; } = string.Empty;
    private string? stopArguments { get; set; } = string.Empty;
    private string fileName { get; set; } = string.Empty;

    /// <summary>
    /// Primary Key
    /// </summary>
    public int Id
    {
      get
      {
        return id;
      }
      set
      {
        id = value;
        OnPropertyChanged(nameof(Id));
      }
    }

    /// <summary>
    /// Is the process running.
    /// </summary>
    public bool IsRunning
    {
      get
      {
        if (process is null)
        {
          Debug.WriteLine("Process is not running.");
          return false;
        }

        Debug.WriteLine("Process is running.");
        return true;
      }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// The start arguments.
    /// </summary>
    public string? StartArguments
    {
      get
      {
        return startArguments;
      }
      set
      {
        startArguments = value;
        OnPropertyChanged(nameof(StartArguments));
      }
    }

    /// <summary>
    /// The stop arguments.
    /// </summary>
    public string? StopArguments
    {
      get
      {
        return stopArguments;
      }
      set
      {
        stopArguments = value;
        OnPropertyChanged(nameof(StopArguments));
      }
    }

    /// <summary>
    /// The process file name.
    /// </summary>
    public string FileName
    {
      get
      {
        return fileName;
      }
      set
      {
        fileName = value;

        if (process is not null)
        {
          process.StartInfo.FileName = fileName;
          OnPropertyChanged(nameof(process));
        }


        OnPropertyChanged(nameof(FileName));
      }
    }

    /// <summary>
    /// The process priority.
    /// </summary>
    public string Priority
    {
      get
      {
        return process
          .PriorityClass
          .ToString();
      }
    }

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="fileName">the file name</param>
    [ExcludeFromCodeCoverage]
    public ProcessModel(string fileName)
    {
      FileName = fileName;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="fileName">the file name</param>
    /// <param name="startArguments">the start arguments</param>
    [ExcludeFromCodeCoverage]
    public ProcessModel
    (
      string fileName,
      string startArguments
    )
    {
      FileName = fileName;
      StartArguments = startArguments;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="fileName">the file name</param>
    /// <param name="startArguments">the start arguments</param>
    /// <param name="stopArguments">the stop arguments</param>
    [ExcludeFromCodeCoverage]
    public ProcessModel
    (
      string fileName,
      string startArguments,
      string stopArguments
    )
    {
      FileName = fileName;
      StartArguments = startArguments;
      StopArguments = stopArguments;
    }

    /// <summary>
    /// Run process.
    /// </summary>
    /// <returns>The exit code.</returns>
    private Task<int> Run()
    {
      TaskCompletionSource<int> taskCompletionSource =
        new TaskCompletionSource<int>();

      Task<int> task;
      int failCode = 1;

      if (process is null)
      {
        Debug.WriteLine
        (
          "Failed to start process. " +
           "The process is null."
        );

        taskCompletionSource.SetResult(failCode);
        task = taskCompletionSource.Task;
        return task;
      }

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
        Debug.WriteLine("Failed to start process: " + process);
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

    /// <summary>
    /// Run process asynchronously.
    /// </summary>
    /// <returns>The exit code.</returns>
    private async Task<int> RunAsync()
    {
      return await Run().ConfigureAwait(false);
    }

    /// <summary>
    /// Logs event when property has changed.
    /// </summary>
    /// <param name="propertyName">The property name</param>
    private void OnPropertyChanged(string propertyName)
    {
      PropertyChanged?.Invoke
      (
        this,
        new PropertyChangedEventArgs(propertyName)
      );

      Debug.WriteLine
      (
        string.Format
        (
          "PropertyChanged: {0}",
          propertyName
        )
      );
    }

    /// <summary>
    /// Restart the process.
    /// </summary>
    /// <returns>The exit code.</returns>
    public async Task<int> Restart()
    {
      var result = await Stop();

      if (IsRunning)
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Failed to restart the process\t=> Id: {0}",
            id
          )
        );

        return result;
      }

      result = await Start();

      if (IsRunning)
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Failed to restart the process\t=> Id: {0}",
            id
          )
        );
      }

      else
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Restarted the process\t=> Id: {0}",
            id
          )
        );
      }

      return result;
    }

    /// <summary>
    /// Start the process.
    /// </summary>
    /// <returns>The exit code.</returns>
    public async Task<int> Start()
    {
      process.StartInfo.Arguments = StartArguments;
      var result = await RunAsync();
      Update();

      if (!IsRunning)
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Failed to start the process\t=> Id: {0}",
            id
          )
        );
      }

      else
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Started the process\t=> Id: {0}",
            id
          )
        );
      }

      return result;
    }

    /// <summary>
    /// Stop the process.
    /// </summary>
    /// <returns>The exit code.</returns>
    public async Task<int> Stop()
    {
      int result = 1;

      if
      (
        string.IsNullOrEmpty(StopArguments)
        || string.IsNullOrWhiteSpace(StopArguments)
      )
      {
        process.Kill();
        Update();
        result = Convert.ToInt32(IsRunning);
      }

      else
      {
        process.StartInfo.Arguments = StopArguments;
        result = await RunAsync();
        Update();
      }

      if (IsRunning)
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Failed to stop the process\t=> Id: {0}",
            id
          )
        );
      }

      else
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Stopped the process\t=> Id: {0}",
            id
          )
        );
      }

      return result;
    }

    /// <summary>
    /// Update the process.
    /// </summary>
    public void Update()
    {
      Id = process.Id;
      Process = Process.GetProcessById(Id);
    }

    #endregion
  }
}
