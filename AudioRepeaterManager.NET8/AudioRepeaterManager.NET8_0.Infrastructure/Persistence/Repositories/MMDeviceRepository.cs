using NAudio.CoreAudioApi;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using AudioRepeaterManager.NET8_0.Domain.Repositories;

namespace AudioRepeaterManager.NET8_0.Infrastructure.Persistence.Repositories
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
      UpdateAll();
    }

    /// <summary>
    /// Disable an audio device.
    /// </summary>
    /// <param name="model">the audio device</param>
    private void Disable(MMDevice? model)
    {
      if (model is null)
      {
        Debug.WriteLine
          (
            "Failed to disable audio device. " +
            "Audio device is null."
          );

        return;
      }

      if (model.State != DeviceState.Disabled)
      {
        Debug
          .WriteLine
          (
            string
            .Format
            (
              "Audio device is already disabled\t=> Name: {0}.",
              model.FriendlyName
            )
          );

        return;
      }

      try
      {
        model.AudioClient
          .Start();
      }
      catch
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Failed to disable audio device\t=> Name: {0}.",
            model.FriendlyName
          )
        );

        return;
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Disable audio device\t=> Name: {0}.",
          model.FriendlyName
        )
      );

      Update(model);
    }

    /// <summary>
    /// Enable the audio device.
    /// </summary>
    /// <param name="model">the audio device</param>
    private void Enable(MMDevice? model)
    {
      if (model is null)
      {
        Debug.WriteLine
          (
            "Failed to enable audio device. " +
            "Audio device is null."
          );

        return;
      }

      if (model.State != DeviceState.Disabled)
      {
        Debug
          .WriteLine
          (
            string
            .Format
            (
              "Audio device is already enabled\t=> Name: {0}.",
              model.FriendlyName
            )
          );

        return;
      }

      try
      {
        model.AudioClient
          .Start();
      }
      catch
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Failed to enable audio device\t=> Name: {0}.",
            model.FriendlyName
          )
        );

        return;
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Enabled audio device\t=> Name: {0}.",
          model.FriendlyName
        )
      );

      Update(model);
    }

    /// <summary>
    /// Update an audio device.
    /// </summary>
    /// <param name="model">The audio device</param>
    private void Update(MMDevice? model)
    {
      if (model is null)
      {
        Debug.WriteLine
          (
            "Failed to refresh audio device. " +
            "Audio device is null."
          );

        return;
      }

      try
      {
        model.AudioSessionManager
          .RefreshSessions();
      }
      catch
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Failed to refresh audio device\t=> Name: {0}.",
            model.FriendlyName
          )
        );

        return;
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Refreshed audio device\t=> Name: {0}.",
          model.FriendlyName
        )
      );
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
          "Failed to get audio device. " +
          "Audio device is either null or does not exist in list."
        );

        return model;
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Got audio device\t=> ID: {0}",
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
          "Failed to get audio device(s). " +
          "Audio device list is null or empty."
        );

        return new List<MMDevice>();
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Got audio device(s)\t=> Count: {0}",
          List.Count()
        )
      );

      return List;
    }

    /// <summary>
    /// Get a range of disabled audio devices.
    /// </summary>
    /// <returns>The list of disabled audio devices.</returns>
    public List<MMDevice> GetAllDisabled()
    {
      if
      (
        List is null
        || List.Count == 0
      )
      {
        Debug.WriteLine
        (
          "Failed to get disabled audio device(s). " +
          "Audio device list is null or empty."
        );

        return new List<MMDevice>();
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Got disabled audio device(s)\t=> Count: {0}",
          List.Where
            (
              x =>
              x.State == DeviceState.Disabled
            ).Count()
        )
      );

      return List;
    }

    /// <summary>
    /// Get a range of enabled audio devices.
    /// </summary>
    /// <returns>The list of enabled audio devices.</returns>
    public List<MMDevice> GetAllEnabled()
    {
      if
      (
        List is null
        || List.Count == 0
      )
      {
        Debug.WriteLine
        (
          "Failed to get enabled audio device(s). " +
          "Audio device list is null or empty."
        );

        return new List<MMDevice>();
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Got enabled audio device(s)\t=> Count: {0}",
          List.Where
            (
              x =>
              x.State != DeviceState.Disabled
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
          "Failed to get audio device(s). " +
          "Either audio device ID list is null or empty, " +
          "or audio device list is null or empty."
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
    /// Disable all audio devices.
    /// </summary>
    public void DisableAll()
    {
      if
      (
        List is null
        || List.Count == 0
      )
      {
        Debug.WriteLine
        (
          "Failed to disable audio device(s). " +
          "Audio device list is null or empty."
        );

        return;
      }

      List.ForEach
        (
          x =>
          Disable((MMDevice?)x)
        );
    }

    /// <summary>
    /// Disable a range of audio devices.
    /// </summary>
    /// <param name="idList">The audio device ID list</param>
    public void DisableRange(List<string> idList)
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
          "Failed to disable audio device(s). " +
          "Either audio device ID list is null or empty, " +
          "or audio device list is null or empty."
        );

        return;
      }

      List<MMDevice> modelList = new List<MMDevice>();

      idList.ForEach
        (
          id =>
          {
            MMDevice? model = Get(id);
            Disable(model);
          }
        );
    }

    /// <summary>
    /// Disable an audio device.
    /// </summary>
    /// <param name="id">the audio device ID</param>
    public void Disable(string id)
    {
      MMDevice? model = Get(id);
      Disable(model);
    }

    /// <summary>
    /// Enable all audio devices.
    /// </summary>
    public void EnableAll()
    {
      if
      (
        List is null
        || List.Count == 0
      )
      {
        Debug.WriteLine
        (
          "Failed to enable audio device(s). " +
          "Audio device list is null or empty."
        );

        return;
      }

      List.ForEach
        (
          x =>
          Enable((MMDevice?)x)
        );
    }

    /// <summary>
    /// Enable a range of audio devices.
    /// </summary>
    /// <param name="idList">The audio device ID list</param>
    public void EnableRange(List<string> idList)
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
          "Failed to enable audio device(s). " +
          "Either audio device ID list is null or empty, " +
          "or audio device list is null or empty."
        );

        return;
      }

      List<MMDevice> modelList = new List<MMDevice>();

      idList.ForEach
        (
          id =>
          {
            MMDevice? model = Get(id);
            Enable(model);
          }
        );
    }

    /// <summary>
    /// Enable an audio device.
    /// </summary>
    /// <param name="id">the audio device ID</param>
    public void Enable(string id)
    {
      MMDevice? model = Get(id);
      Enable(model);
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