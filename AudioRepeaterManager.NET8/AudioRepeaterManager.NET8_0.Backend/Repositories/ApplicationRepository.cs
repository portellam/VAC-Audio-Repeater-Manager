using AudioRepeaterManager.NET8_0.Backend.Models;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace AudioRepeaterManager.NET8_0.Backend.Repositories
{
  public class ApplicationRepository :
    IApplicationRepository,
    INotifyPropertyChanged

  {
    #region Parameters

    private Dictionary<uint, bool> idToPreferLegacyApplicationDictionary
   ;

    private Dictionary<uint, RepeaterRepository> idToRepeaterRepositoryDictionary
   ;

    private List<ApplicationModel> list
   ;

    public Dictionary<uint, bool> IdToPreferLegacyApplicationDictionary
    {
      get
      {
        return idToPreferLegacyApplicationDictionary;
      }
      set
      {
        idToPreferLegacyApplicationDictionary = value;
        OnPropertyChanged(nameof(IdToPreferLegacyApplicationDictionary));
      }
    }

    public Dictionary<uint, RepeaterRepository> IdToRepeaterRepositoryDictionary
    {
      get
      {
        return idToRepeaterRepositoryDictionary;
      }
      set
      {
        idToRepeaterRepositoryDictionary = value;
        OnPropertyChanged(nameof(IdToPreferLegacyApplicationDictionary));
      }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public List<ApplicationModel> List
    {
      get
      {
        return list;
      }
      set
      {
        list = value;
        OnPropertyChanged(nameof(List));
      }
    }

    public List<uint> ProcessIdList
    {
      get
      {
        Process[] processList = Process
          .GetProcessesByName(Global.ExecutableName);

        if
        (
          processList is null
          || processList.Count() == 0
        )
        {
          Debug.WriteLine
            (
              "No running processes of the application were found. " +
              "Skipped update of process ID list."
            );

          return new List<uint>();
        }

        List<uint> processIdList = new List<uint>();

        processIdList
          .AddRange
          (
            processList
              .ToList()
              .Select(x => (uint)x.Id)
          );

        Debug.WriteLine
        (
          string.Format
        (
            "Got process ID(s) => Count: {0}",
            processList.Count()
          )
        );

        return ProcessIdList;
      }
    }

    /*
     * NOTES:
     * order of operations:
     * 1. assume no repeaters are running.
     * 2. list all repeaters
     * 3. create process, track each repeater's process.
     * 4. if done synchronously, start each repepater one by one.
     *  Note the unique process that is not assigned to a repeater in the dictionary.
     * 4. if done async, I have no idea how.
     */

    /*
     * how to query:
     * - check VACARM original repo. I swear that the code was able to restart and stop repeaters.
     * - perthaps what it did was kill all repeater instances. But how did it do this?
     * - the window name.
     * 
     * 
     */

    /// <summary>
    /// Constructor
    /// </summary>
    [ExcludeFromCodeCoverage]
    public ApplicationRepository()
    {
      list =
        new List<ApplicationModel>() { };

      idToRepeaterRepositoryDictionary =
        new Dictionary<uint, RepeaterRepository>() { };

      idToPreferLegacyApplicationDictionary =
        new Dictionary<uint, bool>() { };
    }

    /// <summary>
    /// The list of IDs.
    /// </summary>
    private List<uint> IdList
    {
      get
      {
        List<uint> list = List
          .Select(x => x.Id)
          .ToList();

        list.Sort();
        return list;
      }
    }

    /// <summary>
    /// The max ID.
    /// </summary>
    private uint MaxId
    {
      get
      {
        return Global.MaxRepeaterCount;
      }
    }

    /// <summary>
    /// The next valid ID.
    /// </summary>
    private uint NextId
    {
      get
      {
        uint id = IdList.LastOrDefault();
        id++;
        return id;
      }
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
    /// Insert a repeater repository.
    /// </summary>
    /// <param name="repeaterRepository">The repeater repository</param>
    /// <param name="preferLegacyApplication">
    /// True/false prefer legacy application
    /// </param>
    public void Insert
    (
      RepeaterRepository repeaterRepository,
      bool preferLegacyApplication
    )
    {
      if (list.Count >= MaxId)
      {
        Console.WriteLine
        (
          string.Format
          (
            "Failed to insert repeater repository. " +
            "Repeater repository count will exceed maximum of {0}.",
            MaxId
          )
        );

        return;
      }

      if (IdToRepeaterRepositoryDictionary.ContainsValue(repeaterRepository))
      {
        Debug.WriteLine
          (
            "Failed to insert repeater repository. " +
            "Repeater repository already exists."
          );

        return;
      }

      repeaterRepository.GetAll()
        .ForEach
        (
          x =>
          {
            ApplicationModel model = new ApplicationModel
              (
                NextId,
                x
              );

            list.Add(model);
            Debug.WriteLine("Inserted repeater.");
          }
        );

      idToRepeaterRepositoryDictionary.Add
        (
          NextId,
          repeaterRepository
        );

      idToPreferLegacyApplicationDictionary.Add
        (
          NextId,
          preferLegacyApplication
        );

      Debug
          .WriteLine
          (
            string.Format
            (
              "Inserted repeater repository\t=>Id: {0}.",
              NextId
            )
          );
    }

    /// <summary>
    /// Update a repeater repository.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <param name="preferLegacyApplication">
    /// True/false prefer the legacy application
    /// </param>
    public void Update
    (
      uint id,
      bool preferLegacyApplication
    )
    {
      if
        (
          !IdToRepeaterRepositoryDictionary.ContainsKey(id)
        )
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Failed to update repeater repository. " +
            "Repeater repository does not exist\t=>Id: {0}.",
            id
          )
        );

        return;
      }

      IdToPreferLegacyApplicationDictionary[id] = preferLegacyApplication;

      Debug.WriteLine
        (
          string.Format
          (
            "Updated repeater repository\t=>Id: {0}.",
            id
          )
        );
    }

    #endregion
  }
}

/*
 * NOTES:
 * - renamed the instance's object name from "xModel" or "xRepository" to "model" and "repository".
 * - added OnProperty calls.
 * - added debug.
 * - indent/newline on second piping or when parans contain newlines (more than one).
 * - ABC params and methods by accessor and datatype.
 * - newline string on consecutive lines.
 * - added MaxId.
 */