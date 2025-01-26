using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using AudioRepeaterManager.NET8_0.Domain.Repositories;

namespace AudioRepeaterManager.NET8_0.Infrastructure.Repositories
{
  public class ProcessRepository :
    INotifyPropertyChanged,
    IProcessRepository
  {
    #region Parameters

    /// <summary>
    /// The list of processes.
    /// </summary>
    private List<Process> List { get; set; }

    private List<string> executableNameList { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// The executable name list.
    /// </summary>
    public List<string> ExecutableNameList
    {
      get
      {
        return executableNameList;
      }
      set
      {
        if
        (
          value is null
          || value.Count() == 0
        )
        {
          value = new List<string>();
        }

        else
        {
          value
          .Select
          (
            x =>
            {
              return !string.IsNullOrEmpty(x)
                || !string.IsNullOrWhiteSpace(x);
            }
          );
        }

        executableNameList = value;
        OnPropertyChanged(nameof(ExecutableNameList));
      }
    }

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    /// </summary>
    /// <param name="executableNameList">the executable name list</param>
    [ExcludeFromCodeCoverage]
    public ProcessRepository(List<string> executableNameList)
    {
      List = new List<Process>();
      ExecutableNameList = executableNameList;
      Update();
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
    /// Get a process.
    /// </summary>
    /// <param name="id">the process ID</param>
    /// <returns>The process.</returns>
    public Process Get(int id)
    {
      if (id < 0)
      {
        Debug.WriteLine
        (
          "Failed to get process. " +
          "Process ID is less than zero."
        );

        return new Process();
      }

      Process? process = List
        .FirstOrDefault(x => x.Id == id);

      if (process is null)
      {
        Debug.WriteLine("Process is null.");
        process = new Process();
      }

      else
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Got process\t=> ID: {0}",
            process.Id
          )
        );
      }

      return process;
    }

    /// <summary>
    /// Get the list of processes.
    /// </summary>
    /// <returns>The list of processes.</returns>
    public List<Process> GetAll()
    {
      if
      (
        List is null
        || List.Count == 0
      )
      {
        Debug.WriteLine
        (
          "Failed to get process(es). " +
          "Process list is null or empty."
        );

        return new List<Process>();
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Got process(es) => Count: {0}",
          List.Count()
        )
      );

      return List;
    }

    /// <summary>
    /// Get range of processes.
    /// </summary>
    /// <param name="idList">The process ID list</param>
    /// <returns>The list of processes.</returns>
    public List<Process> GetRange(List<int> idList)
    {
      if
      (
        idList is null
        || idList.Count == 0
        || List is null
        || List.Count == 0
      )
      {
        Debug.WriteLine
        (
          "Failed to get process(es). " +
          "Either process ID list is null or empty, " +
          "or process list is null or empty."
        );

        return new List<Process>();
      }

      List<Process> list = new List<Process>();

      idList
        .ForEach
        (
          id =>
          list
            .Add(Get(id))
        );

      Debug.WriteLine
      (
        string.Format
        (
          "Got process(es) => Count: {0}",
          list.Count()
        )
      );

      return list;
    }

    /// <summary>
    /// Kill a process.
    /// </summary>
    /// <param name="process">the process</param>
    /// <returns>The exit code.</returns>
    private static Task<int> Kill(Process? process)
    {
      TaskCompletionSource<int> taskCompletionSource =
        new TaskCompletionSource<int>();

      Task<int> task;
      int passCode = 0;
      int failCode = 1;

      if (process is null)
      {
        Debug.WriteLine
        (
          "Failed to kill process. " +
           "The process is null."
        );

        taskCompletionSource.SetResult(passCode);
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

      process.Kill();

      if (!process.HasExited)
      {
        Debug.WriteLine("Failed to kill process: " + process);
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
    /// Kill a process.
    /// </summary>
    /// <param name="id">the process ID</param>
    /// <returns>The exit code.</returns>
    public async Task<int> Kill(int id)
    {
      Process process = Get(id);

      return await Kill(process)
        .ConfigureAwait(false);
    }

    /// <summary>
    /// Kill all processes.
    /// </summary>
    /// <returns></returns>
    public async Task<int> KillAll()
    {
      return await KillRange(List)
        .ConfigureAwait(false);
    }

    /// <summary>
    /// Kill a list of processes.
    /// </summary>
    /// <param name="idList">the process ID list</param>
    /// <returns>The exit code.</returns>
    public async Task<int> KillRange(List<int> idList)
    {
      List<Process> processList = GetRange(idList);

      return await KillRange(processList)
        .ConfigureAwait(false);
    }

    /// <summary>
    /// Kill a list of processes.
    /// </summary>
    /// <param name="processList">the process list</param>
    /// <returns>The exit code.</returns>
    public async Task<int> KillRange(List<Process> processList)
    {
      int result = 1;

      if
      (
        processList is null
        || processList.Count == 0
      )
      {
        Debug.WriteLine
        (
          "Failed to kill process(es). " +
          "Process list is null or empty."
        );

        return result;
      }

      var taskList = processList
        .Select
        (
          x =>
          Kill(x)
        );

      var resultList = await Task.WhenAll(taskList);

      bool hasAnyFailed = resultList
        .ToList()
        .Any
        (
          x =>
          x != 0
        );

      int count = processList.Count;

      if (hasAnyFailed)
      {
        int difference = count - resultList
          .ToList()
          .Count
          (
            x =>
            x != 0
          );

        Debug.WriteLine
        (
          string.Format
          (
            "Failed to kill some process(es) => Count: {0}",
            difference
          )
        );
      }

      else
      {
        result = 0;
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Killed process(es) => Count: {0}",
          count
        )
      );

      return result;
    }

    /// <summary>
    /// Run a process.
    /// </summary>
    /// <param name="process">the process</param>
    /// <returns>The exit code.</returns>
    private static Task<int> Run(Process? process)
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
    /// Run a process.
    /// </summary>
    /// <param name="id">the process ID</param>
    /// <returns>The exit code.</returns>
    public async Task<int> Run(int id)
    {
      Process process = Get(id);

      return await Run(process)
        .ConfigureAwait(false);
    }

    /// <summary>
    /// Run all processes.
    /// </summary>
    /// <returns></returns>
    public async Task<int> RunAll()
    {
      return await RunRange(List)
        .ConfigureAwait(false);
    }

    /// <summary>
    /// Run a range of processes.
    /// </summary>
    /// <param name="idList">the process ID list</param>
    /// <returns>The exit code.</returns>
    public async Task<int> RunRange(List<int> idList)
    {
      List<Process> processList = GetRange(idList);

      return await RunRange(processList)
        .ConfigureAwait(false);
    }

    /// <summary>
    /// Run a range of processes.
    /// </summary>
    /// <param name="processList">the process list</param>
    /// <returns>The exit code.</returns>
    public async Task<int> RunRange(List<Process> processList)
    {
      int result = 1;

      if
      (
        processList is null
        || processList.Count == 0
      )
      {
        Debug.WriteLine
        (
          "Failed to run process(es). " +
          "Process list is null or empty."
        );

        return result;
      }

      var taskList = processList
        .Select
        (
          x =>
          Run(x)
        );

      var resultList = await Task.WhenAll(taskList);

      bool hasAnyFailed = resultList
        .ToList()
        .Any
        (
          x =>
          x != 0
        );

      int count = processList.Count;

      if (hasAnyFailed)
      {
        int difference = count - resultList
          .ToList()
          .Count
          (
            x =>
            x != 0
          );

        Debug.WriteLine
        (
          string.Format
          (
            "Failed to run some process(es) => Count: {0}",
            difference
          )
        );
      }

      else
      {
        result = 0;
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Ran process(es) => Count: {0}",
          count
        )
      );

      return result;
    }

    /// <summary>
    /// Set the process list.
    /// </summary>
    public void Update()
    {
      List.Clear();

      ExecutableNameList
        .ForEach
        (
          x =>
          {
            List<Process> list = Process
              .GetProcessesByName(x)
              .ToList();

            if (list.Count() == 0)
            {
              Debug.WriteLine
              (
                string.Format
                (
                  "Found no processes\t=> ExecutableName: {0}",
                  x
                )
              );
            }

            else
            {
              Debug.WriteLine
              (
               string.Format
               (
                 "Found processes\t=> ExecutableName: {0}, Count: {1}",
                 x,
                 list.Count()
               )
             );
            }

            List.AddRange(list);
          }
        );

      Debug.WriteLine
        (
          string.Format
          (
            "Updated process list\t=> Count: {0}",
            List.Count()
          )
        );
    }
  }

  #endregion
}