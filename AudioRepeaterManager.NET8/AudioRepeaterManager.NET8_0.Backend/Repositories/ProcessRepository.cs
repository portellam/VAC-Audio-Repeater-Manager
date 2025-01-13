using NAudio.CoreAudioApi;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace AudioRepeaterManager.NET8_0.Backend.Repositories
{
  public class ProcessRepository :
    INotifyPropertyChanged,
    IProcessRepository
  {
    #region Parameters

    public event PropertyChangedEventHandler? PropertyChanged;

    private List<string> ExecutableNameList
    {
      get
      {
        return new List<string>
        {
          Global.KSExecutableName,
          Global.MMEExecutableName,
        };
      }
    }

    /// <summary>
    /// The list of processes.
    /// </summary>
    private List<Process> List;

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    [ExcludeFromCodeCoverage]
    public ProcessRepository()
    {
      List = new List<Process>();
      Update();
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