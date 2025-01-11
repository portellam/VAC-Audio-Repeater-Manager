using AudioRepeaterManager.NET8_0.Backend.Extensions;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace AudioRepeaterManager.NET8_0.Backend.Models
{
  public class ApplicationModel :
    IApplicationModel,
    INotifyPropertyChanged
  {
    #region Parameters

    private uint id;
    private uint repeaterId;
    private bool isRunning;
    private string priority;
    private string startArguments;
    private string stopArguments;
    private string windowName;
    private uint processId;

    /// <summary>
    /// Primary Key
    /// </summary>
    public uint Id
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
    /// Foreign key
    /// </summary>
    public uint RepeaterId
    {
      get
      {
        return repeaterId;
      }
      set
      {
        repeaterId = value;
        OnPropertyChanged(nameof(RepeaterId));
      }
    }

    /// <summary>
    /// Arguments to start with the application.
    /// </summary>
    public string StartArguments
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
    /// Arguments to stop with the application.
    /// </summary>
    public string StopArguments
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
    /// The window name.
    /// </summary>
    public string WindowName
    {
      get
      {
        return windowName;
      }
      set
      {
        windowName = value;
        OnPropertyChanged(nameof(windowName));
      }
    }

    /// <summary>
    /// The priority level.
    /// </summary>
    public string Priority
    {
      get
      {
        return priority;
      }
      set
      {
        priority = value;
        OnPropertyChanged(nameof(priority));
      }
    }

    /// <summary>
    /// True/false is the process running.
    /// </summary>
    public bool IsRunning
    {
      get
      {
        return isRunning;
      }
      set
      {
        isRunning = value;
        OnPropertyChanged(nameof(IsRunning));
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// The process ID.
    /// </summary>
    public uint ProcessId
    {
      get
      {
        return processId;
      }
      set
      {
        processId = value;
        OnPropertyChanged(nameof(processId));
      }
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="id">The ID</param>
    /// <param name="repeaterId">The repeater ID</param>
    /// <param name="windowName">The window name</param>
    /// <param name="startArguments">The start arguments</param>
    /// <param name="stopArguments">The stop arguments</param>
    [ExcludeFromCodeCoverage]
    public ApplicationModel
    (
      uint id,
      uint repeaterId,
      string windowName,
      string startArguments,
      string stopArguments
    )
    {
      Id = id;
      RepeaterId = repeaterId;
      WindowName = windowName;
      StartArguments = startArguments;
      StopArguments = stopArguments;
    }

    /// <summary>
    /// Update the application.
    /// </summary>
    public void Update()
    {
      var process = Process.GetProcessById((int)Id);

      if (process is null)
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Failed to update the application. " +
            "Process does not exist\t=> Id: {0}",
            id
          )
        );

        return;
      }

      Priority = process.PriorityClass
        .ToString();

      Debug.WriteLine
        (
          string.Format
          (
            "Updated the application\t=> Id: {0}, Priority: {1}",
            id,
            Priority
          )
        );
    }

    #endregion

    #region Logic

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
    /// Restart the repeater.
    /// </summary>
    /// <returns>The exit code.</returns>
    public int Restart()
    {
      Stop();
      return Start();
    }

    /// <summary>
    /// Start the repeater.
    /// </summary>
    /// <returns>The exit code.</returns>
    public int Start()
    {
      Task<int> task = AsyncProcess.RunProcessAsync
        (
          Global.ExpectedExecutableFullPathName,
          StartArguments
        );

      ProcessId = (uint)task.Id;
      IsRunning = task.Status == TaskStatus.Running;

      if (!IsRunning)
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Failed to start the application. \t=> Id: {0}",
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
            "Started the application. \t=> Id: {0}",
            id
          )
        );
      }

      return task.Result;
    }

    /// <summary>
    /// Stop the repeater.
    /// </summary>
    /// <returns>The exit code.</returns>
    public int Stop()
    {
      Task<int> task = AsyncProcess.RunProcessAsync
        (
          Global.ExpectedExecutableFullPathName,
          StopArguments
        );

      IsRunning = task.Status == TaskStatus.Running;

      if (IsRunning)
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Failed to stop the application. \t=> Id: {0}",
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
            "Stopped the application. \t=> Id: {0}",
            id
          )
        );
      }

      return task.Result;
    }

    #endregion
  }
}

/*
 * TODO:
 * -proper async methods!
 * 
 */