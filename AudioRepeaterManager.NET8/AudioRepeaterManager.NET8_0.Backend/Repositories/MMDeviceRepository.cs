using NAudio.CoreAudioApi;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace AudioRepeaterManager.NET8_0.Backend.Repositories
{
  public class MMDeviceRepository : IMMDeviceRepository
  {
    #region Parameters

    /// <summary>
    /// The list of actual devices.
    /// </summary>
    private List<MMDevice> List { get; set; }

    /// <summary>
    /// The enumerator of actual devices.
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
      Update();
    }

    /// <summary>
    /// Disable an actual device.
    /// </summary>
    /// <param name="model">the actual device</param>
    private void Disable(MMDevice model)
    {
      if (model is null)
      {
        Debug.WriteLine("Failed to get audio device. Audio device is null.");
        return;
      }

      if (model.State == DeviceState.Disabled)
      {
        Debug
          .WriteLine
          (
            string
            .Format
            (
              "Audio {0} device is already disabled.",
              model.FriendlyName
            )
          );

        return;
      }

      model
        .AudioClient
        .Stop();

      Debug
        .WriteLine
        (
          string
          .Format
          (
            "Audio {0} device disabled.",
            model.FriendlyName
          )
        );


      model
        .AudioClient
        .Reset();

      Debug
        .WriteLine
        ("Reset audio devices.");

      model
        .AudioSessionManager
        .RefreshSessions();

      Debug
        .WriteLine
        ("Refreshed audio devices.");
    }

    /// <summary>
    /// Enable an actual device.
    /// </summary>
    /// <param name="model">the actual device</param>
    private void Enable(MMDevice model)
    {
      if (model is null)
      {
        Debug.WriteLine("Failed to get audio device. Audio device is null.");
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
              "Audio {0} device is already enabled.",
              model.FriendlyName
            )
          );

        return;
      }

      model
        .AudioClient
        .Start();

      Debug
        .WriteLine
        (
          string
          .Format
          (
            "Audio {0} device enabled.",
            model.FriendlyName
          )
        );

      model
        .AudioSessionManager
        .RefreshSessions();

      Debug
        .WriteLine
        ("Refreshed audio devices.");
    }

    /// <summary>
    /// Get an actual device.
    /// </summary>
    /// <param name="id">the actual device ID</param>
    /// <returns>The actual device.</returns>
    public MMDevice Get(string id)
    {
      if (string.IsNullOrWhiteSpace(id))
      {
        Debug.WriteLine
        (
          "Failed to get audio device. " +
          "Actual device ID is either null or whitespace."
        );

        return null;
      }

      MMDevice model = List
        .FirstOrDefault(x => x.ID == id);

      if (model is null)
      {
        Debug.WriteLine("Audio device is null.");
      }

      else
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Got audio device\t=> ID: {0}",
            model.ID
          )
        );
      }

      return model;
    }

    /// <summary>
    /// Get the list of actual devices.
    /// </summary>
    /// <returns>The list of actual devices.</returns>
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
          "Got audio device(s) => Count: {0}",
          List.Count()
        )
      );

      return List;
    }

    /// <summary>
    /// Get the list of disabled actual devices.
    /// </summary>
    /// <returns>The list of disabled actual devices.</returns>
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
          "Got audio disabled device(s) => Count: {0}",
          List
            .Where
            (
              x =>
              x.State == DeviceState.Disabled
            ).Count()
        )
      );

      return List;
    }

    /// <summary>
    /// Get the list of enabled actual devices.
    /// </summary>
    /// <returns>The list of enabled actual devices.</returns>
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
          "Got audio enabled device(s) => Count: {0}",
          List
            .Where
            (
              x =>
              x.State != DeviceState.Disabled
            ).Count()
        )
      );

      return List;
    }

    /// <summary>
    /// Get a list of actual devices.
    /// </summary>
    /// <param name="idList">the actual device ID list</param>
    /// <returns>A list of actual devices.</returns>
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
          "Either actual ID list is null or empty, " +
          "or audio device list is null or empty."
        );

        return new List<MMDevice>();
      }

      List<MMDevice> modelList = new List<MMDevice>();

      idList
        .ForEach
        (
          id =>
          modelList
            .Add(Get(id))
        );

      Debug.WriteLine
      (
        string.Format
        (
          "Got audio device(s) => Count: {0}",
          modelList.Count()
        )
      );

      return modelList;
    }

    /// <summary>
    /// Disable an actual device.
    /// </summary>
    /// <param name="id">the actual device ID</param>
    public void Disable(string id)
    {
      MMDevice model = Get(id);
      Disable(model);
    }

    /// <summary>
    /// Enable an actual device.
    /// </summary>
    /// <param name="id">the actual device ID</param>
    public void Enable(string id)
    {
      MMDevice model = Get(id);
      Enable(model);
    }

    /// <summary>
    /// Set the actual device list.
    /// </summary>
    public void Update()
    {
      List = Enumerator
        .EnumerateAudioEndPoints
        (
          DataFlow.All,
          DeviceState.All
        )
        .Distinct()
        .OrderBy(x => x.ID)
        .ToList();

      Debug
        .WriteLine("Updated audio devices.");
    }

    #endregion
  }
}