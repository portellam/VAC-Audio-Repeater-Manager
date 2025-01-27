using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using AudioRepeaterManager.NET8_0.Application.Commands;
using AudioRepeaterManager.NET8_0.Domain.Repositories;

namespace AudioRepeaterManager.NET8_0.Infrastructure.Repositories
{
  public class ProcessRepository :
    INotifyPropertyChanged,
    IProcessRepository
  {
    #region Parameters

    /// <summary>
    /// The list of process(es).
    /// </summary>
    private List<Process> List { get; set; }

    private List<string> fileNameList { get; set; } = new List<string>();

    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// The file name list.
    /// </summary>
    public List<string> FileNameList
    {
      get
      {
        return fileNameList;
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

        fileNameList = value;
        OnPropertyChanged(nameof(FileNameList));
      }
    }

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="fileNameList">the file name list</param>
    [ExcludeFromCodeCoverage]
    public ProcessRepository(List<string> fileNameList)
    {
      List = new List<Process>();
      FileNameList = fileNameList;
      Update();
    }

    /// <summary>
    /// Kill a list of process(es).
    /// </summary>
    /// <param name="processList">the process list</param>
    /// <returns>The exit code.</returns>
    private async static Task<int[]> KillRange(List<Process> processList)
    {
      int[] resultArray = new int[] { 1 };

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

        return resultArray;
      }

      resultArray = await Task.WhenAll
        (
          processList.Select
          (
            async x =>
            await ProcessCommands.RunAsync(x)
          )
        );

      bool hasAnyFailed = resultArray
        .ToList()
        .Any
        (
          x =>
          x != 0
        );

      int count = processList.Count;

      if (hasAnyFailed)
      {
        int difference = count - resultArray
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
            "Failed to kill some process(es)\t=> Count: {0}",
            difference
          )
        );
      }

      else
      {
        resultArray = new int[] { 0 };
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Killed process(es)\t=> Count: {0}",
          count
        )
      );

      return resultArray;
    }

    /// <summary>
    /// Run a range of process(es).
    /// </summary>
    /// <param name="processList">the process list</param>
    /// <returns>The exit code.</returns>
    private async static Task<int[]> RunRange(List<Process> processList)
    {
      int[] resultArray = new int[] { 1 };

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

        return resultArray;
      }

      resultArray = await Task.WhenAll
        (
          processList.Select
          (
            async x =>
            await ProcessCommands.RunAsync(x)
          )
        );

      bool hasAnyFailed = resultArray
        .ToList()
        .Any
        (
          x =>
          x != 0
        );

      int count = processList.Count;

      if (hasAnyFailed)
      {
        int difference = count - resultArray
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
            "Failed to run some process(es)\t=> Count: {0}",
            difference
          )
        );
      }

      else
      {
        resultArray = new int[] { 0 };
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Ran process(es)\t=> Count: {0}",
          count
        )
      );

      return resultArray;
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
    /// Get the list of process(es).
    /// </summary>
    /// <returns>The list of process(es).</returns>
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
          "Got process(es)\t=> Count: {0}",
          List.Count()
        )
      );

      return List;
    }

    /// <summary>
    /// Get range of process(es).
    /// </summary>
    /// <param name="idList">The process ID list</param>
    /// <returns>The list of process(es).</returns>
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
          "Got process(es)\t=> Count: {0}",
          list.Count()
        )
      );

      return list;
    }

    /// <summary>
    /// Kill a process.
    /// </summary>
    /// <param name="id">the process ID</param>
    /// <returns>The exit code.</returns>
    public async Task<int> Kill(int id)
    {
      Process process = Get(id);
      return await ProcessCommands.KillAsync(process);
    }

    /// <summary>
    /// Kill all process(es).
    /// </summary>
    /// <returns></returns>
    public async Task<int[]> KillAll()
    {
      return await KillRange(List)
        .ConfigureAwait(false);
    }

    /// <summary>
    /// Kill a list of process(es).
    /// </summary>
    /// <param name="idList">the process ID list</param>
    /// <returns>The exit code.</returns>
    public async Task<int[]> KillRange(List<int> idList)
    {
      List<Process> processList = GetRange(idList);

      return await KillRange(processList)
        .ConfigureAwait(false);
    }

    /// <summary>
    /// Run a process.
    /// </summary>
    /// <param name="id">the process ID</param>
    /// <returns>The exit code.</returns>
    public async Task<int> Run(int id)
    {
      Process process = Get(id);
      return await ProcessCommands.RunAsync(process);
    }

    /// <summary>
    /// Run all process(es).
    /// </summary>
    /// <returns></returns>
    public async Task<int[]> RunAll()
    {
      return await RunRange(List)
        .ConfigureAwait(false);
    }

    /// <summary>
    /// Run a range of process(es).
    /// </summary>
    /// <param name="idList">the process ID list</param>
    /// <returns>The exit code.</returns>
    public async Task<int[]> RunRange(List<int> idList)
    {
      List<Process> processList = GetRange(idList);

      return await RunRange(processList)
        .ConfigureAwait(false);
    }
        
    /// <summary>
    /// Set the process list.
    /// </summary>
    public void Update()
    {
      List.Clear();

      FileNameList
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
                  "Found no process(es)\t=> FileName: {0}",
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
                 "Found process(es)\t=> FileName: {0}, Count: {1}",
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