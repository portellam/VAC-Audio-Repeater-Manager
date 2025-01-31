using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using NAudio.CoreAudioApi;

namespace VACARM.Infrastructure.Repositories
{
  public class MMDeviceRepository : IMMDeviceRepository
  {
    #region Parameters

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
    }

    public bool IsStarted(MMDevice? model)
    {
      if (model == null)
      {
        return false;
      }

      DeviceState deviceState = model.State;
      return deviceState != DeviceState.Disabled;
    }

    public bool IsPresent(MMDevice? model)
    {
      if (model == null)
      {
        return false;
      }

      DeviceState deviceState = model.State;
      return deviceState == DeviceState.Active
        || deviceState == DeviceState.Disabled
        || deviceState == DeviceState.Unplugged;
    }

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

        if (model == null)
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

    public List<MMDevice> GetAllAbsent()
    {
      if
      (
        List is null
        || List.Count == 0
      )
      {
        Debug.WriteLine
        (
          "Failed to get the list of absent audio device(s). " +
          "The audio device list is null or empty."
        );

        return new List<MMDevice>();
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Got the list of absent audio device(s)\t=> Count: {0}",
          List.Where
            (
              x =>
              !IsPresent(x)
            ).Count()
        )
      );

      return List;
    }

    public List<MMDevice> GetAllPresent()
    {
      if
      (
        List is null
        || List.Count == 0
      )
      {
        Debug.WriteLine
        (
          "Failed to get the list of present audio device(s). " +
          "The audio device list is null or empty."
        );

        return new List<MMDevice>();
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Got the list of present audio device(s)\t=> Count: {0}",
          List.Where
            (
              x =>
              IsPresent(x)
            ).Count()
        )
      );

      return List;
    }

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
          "Failed to get the list of stopped audio device(s). " +
          "The audio device list is null or empty."
        );

        return new List<MMDevice>();
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Got the list of stopped audio device(s)\t=> Count: {0}",
          List.Where
            (
              x =>
              !IsStarted(x)
            ).Count()
        )
      );

      return List;
    }

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
          "Failed to get the list of started audio device(s). " +
          "The audio device list is null or empty."
        );

        return new List<MMDevice>();
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Got the list of started audio device(s)\t=> Count: {0}",
          List.Where
            (
              x =>
              IsStarted(x)
            ).Count()
        )
      );

      return List;
    }

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
          "Got the audio device(s)\t=> Count: {0}",
          modelList.Count()
        )
      );

      return modelList;
    }

    #endregion
  }
}