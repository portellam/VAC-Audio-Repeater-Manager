using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using NAudio.CoreAudioApi;
using AudioRepeaterManager.NET8_0.Application.Commands;

namespace AudioRepeaterManager.NET8_0.Infrastructure.Repositories
{
  public class MMDeviceRepository : IMMDeviceRepository
  {
    #region Parameters

    /// <summary>
    /// Is the audio device started.
    /// </summary>
    /// <param name="model">The audio device</param>
    /// <returns></returns>
    private bool IsStarted(MMDevice? model)
    {
      if (model is null)
      {
        return false;
      }

      return model.State != DeviceState.Stopd;
    }

    /// <summary>
    /// The list of audio devices.
    /// </summary>
    private List<MMDevice> List { get; set; } = new List<MMDevice>();

    /// <summary>
    /// The enumerator of audio devices.
    /// </summary>
    private MMDeviceEnumerator Enumerator { get; set; }

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    [ExcludeFromCodeCoverage]
    public MMDeviceRepository()
    {
      Enumerator = new MMDeviceEnumerator();
      UpdateAll();
    }

    /// <summary>
    /// Start the audio device.
    /// </summary>
    /// <param name="model">the audio device</param>
    private void Start(MMDevice? model)
    {
      DeviceCommands.Start(model);
    }

    /// <summary>
    /// Stop the audio device.
    /// </summary>
    /// <param name="model">the audio device</param>
    private void Stop(MMDevice? model)
    {
      DeviceCommands.Stop(model);
    }

    /// <summary>
    /// Update the audio device.
    /// </summary>
    /// <param name="model">The audio device</param>
    private void Update(MMDevice? model)
    {
      DeviceCommands.Update(model);
    }

    /// <summary>
    /// Get an audio device.
    /// </summary>
    /// <param name="id">the audio device ID</param>
    /// <returns>The audio device.</returns>
    public MMDevice? Get(string id)
    {
      MMDevice? model = null;

      if (string.IsNullOrWhiteSpace(id))
      {
        Debug.WriteLine
        (
          "Failed to get audio device. " +
          "Audio device ID is either null or whitespace."
        );

        return model;
      }

      try
      {
        model = List.FirstOrDefault(x => x.ID == id);

        if (model is null)
        {
          throw new NullReferenceException();
        }
      }
      catch
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Failed to get the audio device. " +
            "The audio device is either null or does not exist in list\t=> " +
            "ID: {0}",
            id
          )
        );

        return model;
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Got the audio device\t=> ID: {0}",
          model.ID
        )
      );

      return model;
    }

    /// <summary>
    /// Get all of audio devices.
    /// </summary>
    /// <returns>The list of audio devices.</returns>
    public List<MMDevice> GetAll()
    {
      if
      (
        List is null
        || List.Count == 0
      )
      {
        Debug.WriteLine
        (
          "Failed to get the list of all audio device(s). " +
          "The audio device list is null or empty."
        );

        return new List<MMDevice>();
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Got list of all audio device(s)\t=> Count: {0}",
          List.Count()
        )
      );

      return List;
    }

    /// <summary>
    /// Get a range of stopped audio devices.
    /// </summary>
    /// <returns>The list of stopped audio devices.</returns>
    public List<MMDevice> GetAllStopped()
    {
      if
      (
        List is null
        || List.Count == 0
      )
      {
        Debug.WriteLine
        (
          "Failed to get list of stopped audio device(s). " +
          "The audio device list is null or empty."
        );

        return new List<MMDevice>();
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Got list of stopped audio device(s)\t=> Count: {0}",
          List.Where
            (
              x =>
              !IsStarted(x)
            ).Count()
        )
      );

      return List;
    }

    /// <summary>
    /// Get a range of started audio devices.
    /// </summary>
    /// <returns>The list of started audio devices.</returns>
    public List<MMDevice> GetAllStarted()
    {
      if
      (
        List is null
        || List.Count == 0
      )
      {
        Debug.WriteLine
        (
          "Failed to get list of started audio device(s). " +
          "The audio device list is null or empty."
        );

        return new List<MMDevice>();
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Got list of started audio device(s)\t=> Count: {0}",
          List.Where
            (
              x =>
              IsStarted(x)
            ).Count()
        )
      );

      return List;
    }

    /// <summary>
    /// Get a range of audio devices.
    /// </summary>
    /// <param name="idList">the audio device ID list</param>
    /// <returns>A list of audio devices.</returns>
    public List<MMDevice> GetRange(List<string> idList)
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
          "Failed to get the audio device(s). " +
          "Either the audio device ID list is null or empty, " +
          "or the audio device list is null or empty."
        );

        return new List<MMDevice>();
      }

      List<MMDevice> modelList = new List<MMDevice>();

      idList.ForEach
        (
          id =>
          {
            MMDevice? model = Get(id);

            if (model is not null)
            {
              modelList.Add(model);
            }
          }
        );

      Debug.WriteLine
      (
        string.Format
        (
          "Got audio device(s)\t=> Count: {0}",
          modelList.Count()
        )
      );

      return modelList;
    }

    /// <summary>
    /// Start an audio device.
    /// </summary>
    /// <param name="id">the audio device ID</param>
    public void Start(string id)
    {
      MMDevice? model = Get(id);
      Start(model);
    }

    /// <summary>
    /// Start all audio devices.
    /// </summary>
    public void StartAll()
    {
      if
      (
        List is null
        || List.Count == 0
      )
      {
        Debug.WriteLine
        (
          "Failed to start the audio device(s). " +
          "The audio device list is null or empty."
        );

        return;
      }

      List.ForEach
        (
          x =>
          Start((MMDevice?)x)
        );
    }

    /// <summary>
    /// Start a range of audio devices.
    /// </summary>
    /// <param name="idList">The audio device ID list</param>
    public void StartRange(List<string> idList)
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
          "Failed to start the audio device(s). " +
          "Either the audio device ID list is null or empty, " +
          "or the audio device list is null or empty."
        );

        return;
      }

      List<MMDevice> modelList = new List<MMDevice>();

      idList.ForEach
        (
          id => Start(id)
        );
    }

    /// <summary>
    /// Stop an audio device.
    /// </summary>
    /// <param name="id">the audio device ID</param>
    public void Stop(string id)
    {
      MMDevice? model = Get(id);
      Stop(model);
    }

    /// <summary>
    /// Stop all audio devices.
    /// </summary>
    public void StopAll()
    {
      if
      (
        List is null
        || List.Count == 0
      )
      {
        Debug.WriteLine
        (
          "Failed to stop the audio device(s). " +
          "The audio device list is null or empty."
        );

        return;
      }

      List.ForEach
        (
          x =>
          Stop((MMDevice?)x)
        );
    }

    /// <summary>
    /// Stop a range of audio devices.
    /// </summary>
    /// <param name="idList">The audio device ID list</param>
    public void StopRange(List<string> idList)
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
          "Failed to stop the audio device(s). " +
          "Either the audio device ID list is null or empty, " +
          "or the audio device list is null or empty."
        );

        return;
      }

      List<MMDevice> modelList = new List<MMDevice>();

      idList.ForEach
        (
          id => Stop(id)
        );
    }

    /// <summary>
    /// Update the audio device.
    /// </summary>
    /// <param name="id">the audio device ID</param>
    public void Update(string id)
    {
      MMDevice? model = Get(id);
      Update(model);
    }

    /// <summary>
    /// Update a range of audio devices.
    /// </summary>
    /// <param name="idList">The audio device ID list</param>
    public void UpdateRange(List<string> idList)
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
          "Failed to update the audio device(s). " +
          "Either the audio device ID list is null or empty, " +
          "or the audio device list is null or empty."
        );

        return;
      }

      List<MMDevice> modelList = new List<MMDevice>();

      idList.ForEach
        (
          id => Update(id)
        );
    }

    /// <summary>
    /// Set the audio device list.
    /// </summary>
    public void UpdateAll()
    {
      List = Enumerator.EnumerateAudioEndPoints
        (
          DataFlow.All,
          DeviceState.All
        )
        .Distinct()
        .OrderBy(x => x.ID)
        .ToList();

      Debug.WriteLine
      (
        string.Format
        (
          "Got audio device(s)\t=> Count: {0}",
          List.Count()
        )
      );
    }

    #endregion
  }
}