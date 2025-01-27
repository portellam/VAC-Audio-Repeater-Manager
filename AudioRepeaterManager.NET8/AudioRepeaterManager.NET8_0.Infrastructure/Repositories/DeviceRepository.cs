using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using NAudio.CoreAudioApi;
using AudioSwitcher.AudioApi.CoreAudio;                                         // NAudio Issue #421: AudioSwitcher must succeed NAudio.
using AudioRepeaterManager.NET8_0.Domain.Models;
using AudioRepeaterManager.NET8_0.Domain.Repositories;
using AudioRepeaterManager.NET8_0.Domain.Shared;

namespace AudioRepeaterManager.NET8_0.Infrastructure.Repositories
{
  public class DeviceRepository : IDeviceRepository
  {
    #region Parameters

    /// <summary>
    /// The controller of actual audio devices.
    /// </summary>
    private CoreAudioController CoreAudioController { get; set; }

    /// <summary>
    /// The collection of audio devices.
    /// </summary>
    private HashSet<DeviceModel> HashSet { get; set; }

    /// <summary>
    /// The collection of actual audio devices.
    /// </summary>
    private MMDeviceRepository MMDeviceRepository { get; set; }

    /// <summary>
    /// The list of IDs.
    /// </summary>
    private List<uint> IdList
    {
      get
      {
        List<uint> idList =
          HashSet
            .Select(x => x.Id)
            .ToList();

        idList.Sort();
        return idList;
      }
    }

    /// <summary>
    /// The max ID.
    /// </summary>
    private uint MaxId
    {
      get
      {
        return Global.MaxEndpointCount;
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
    /// Constructor
    /// </summary>
    [ExcludeFromCodeCoverage]
    public DeviceRepository()
    {
      HashSet = new HashSet<DeviceModel>();
      MMDeviceRepository = new MMDeviceRepository();                            // NAudio Issue #421: AudioSwitcher must succeed NAudio.
      CoreAudioController = new CoreAudioController();                          // NAudio Issue #421: AudioSwitcher must succeed NAudio.
      uint id = 0;

      MMDeviceRepository
        .GetAll()
        .ForEach
        (
          x =>
          {
            DeviceModel model =
              new DeviceModel
              (
                id,
                x.ID,
                x.FriendlyName,
                x.DataFlow is DataFlow.Capture,
                x.DataFlow is DataFlow.Render,
                IsPresent(x.State)
              );

            HashSet
              .Add(model);

            if (id == uint.MaxValue)
            {
              return;
            }

            id++;
          }
        );
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="modelList">the audio device list</param>
    [ExcludeFromCodeCoverage]
    public DeviceRepository(List<DeviceModel> modelList)
    {
      HashSet = new HashSet<DeviceModel>();
      MMDeviceRepository = new MMDeviceRepository();                            // NAudio Issue #421: AudioSwitcher must succeed NAudio.
      CoreAudioController = new CoreAudioController();                          // NAudio Issue #421: AudioSwitcher must succeed NAudio.

      modelList
        .ForEach
        (
          x =>
          HashSet.Add(x)
        );

      uint id = NextId;

      MMDeviceRepository
        .GetAll()
        .ForEach
        (
          x =>
          {
            DeviceModel model =
              new DeviceModel
              (
                id,
                x.ID,
                x.FriendlyName,
                x.DataFlow == DataFlow.Capture,
                x.DataFlow == DataFlow.Render,
                IsPresent(x.State)
              );

            HashSet
              .Add(model);

            if (id == uint.MaxValue)
            {
              return;
            }

            id++;
          }
        );
    }

    /// <summary>
    /// Is the audio device present.
    /// </summary>
    /// <param name="deviceState">the audio device state</param>
    /// <returns>true/false is the audio device present.</returns>
    private bool IsPresent(DeviceState deviceState)
    {
      return deviceState == DeviceState.Active
        || deviceState == DeviceState.Disabled
        || deviceState == DeviceState.Unplugged;
    }

    /// <summary>
    /// Get the audio device.
    /// </summary>
    /// <param name="actualId">the actual audio device ID</param>
    /// <returns>the audio device.</returns>
    public DeviceModel Get(string actualId)
    {
      if (string.IsNullOrWhiteSpace(actualId))
      {
        Debug.WriteLine
        (
          "Failed to get audio device. " +
          "Actual audio device ID is either null or whitespace."
        );

        return null;
      }

      return HashSet.FirstOrDefault(x => x.ActualId == actualId);
    }

    /// <summary>
    /// Get the audio device.
    /// </summary>
    /// <param name="id">the audio device ID</param>
    /// <returns>the audio device to get.</returns>
    public DeviceModel? Get(uint? id)
    {
      DeviceModel? model = null;

      if (id is null)
      {
        Debug.WriteLine
        (
          "Failed to get audio device. " +
          "Device ID is null."
        );

        return model;
      }

      model = HashSet.FirstOrDefault(x => x.Id == id);

      if (model is null)
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Failed to get audio device. " +
            "Device is null\t=> ID: {0}",
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
            "Got audio device\t=> ID: {0}",
            model.Id
          )
        );
      }

      return model;
    }

    /// <summary>
    /// Get the audio device list.
    /// </summary>
    /// <returns>the audio device list.</returns>
    public List<DeviceModel> GetAll()
    {
      if (HashSet is null)
      {
        Debug.WriteLine
          (
            "Failed to get the list of audio device(s). " +
            "The audio device collection is null."
          );

        return new List<DeviceModel>();
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Got audio device(s) => Count: {0}",
          HashSet.Count()
        )
      );

      return HashSet.ToList();
    }

    /// <summary>
    /// Get the absent audio device list.
    /// </summary>
    /// <returns>the absent audio device list.</returns>
    public List<DeviceModel> GetAllAbsent()
    {
      if (HashSet is null)
      {
        Debug.WriteLine
          (
            "Failed to get absent audio device(s). " +
            "Device collection is null."
          );

        return new List<DeviceModel>();
      }

      return
        HashSet
          .Where(x => !x.IsPresent)
          .ToList();
    }

    /// <summary>
    /// Get the disabled audio device list.
    /// </summary>
    /// <returns>the disabled audio device list.</returns>
    public List<DeviceModel> GetAllStopped()
    {
      if (MMDeviceRepository is null)
      {
        Debug.WriteLine
          (
            "Failed to get disabled audio device(s). " +
            "Device collection is null."
          );

        return new List<DeviceModel>();
      }

      List<string> actualIdList = MMDeviceRepository
        .GetAllStopped()
        .Select(x => x.ID)
        .ToList();

      List<DeviceModel> modelList =
        GetRange(actualIdList);

      Debug.WriteLine
      (
        string.Format
        (
          "Got disabled audio device(s) => Count: {0}",
          modelList.Count()
        )
      );

      return modelList;
    }

    /// <summary>
    /// Get the duplex audio device list.
    /// </summary>
    /// <returns>the duplex audio device list.</returns>
    public List<DeviceModel> GetAllDuplex()
    {
      if (HashSet is null)
      {
        Debug.WriteLine
          (
            "Failed to get duplex audio device(s). " +
            "Device collection is null."
          );

        return new List<DeviceModel>();
      }

      List<DeviceModel> modelList = HashSet
        .Where(x => x.IsDuplex)
        .ToList();

      Debug.WriteLine
      (
        string.Format
        (
          "Got duplex audio device(s) => Count: {0}",
          modelList.Count()
        )
      );

      return modelList;
    }

    /// <summary>
    /// Get the started audio device list.
    /// </summary>
    /// <returns>the started audio device list.</returns>
    public List<DeviceModel> GetAllStarted()
    {
      if (MMDeviceRepository is null)
      {
        Debug.WriteLine
          (
            "Failed to get the list of started audio device(s). " +
            "The audio device collection is null."
          );

        return new List<DeviceModel>();
      }

      List<string> actualIdList = MMDeviceRepository
        .GetAllStarted()
        .Select(x => x.ID)
        .ToList();

      List<DeviceModel> modelList =
        GetRange(actualIdList);

      Debug.WriteLine
      (
        string.Format
        (
          "Got the list of started audio device(s)\t=> Count: {0}",
          modelList.Count()
        )
      );

      return modelList;
    }

    /// <summary>
    /// Get the input audio device list.
    /// </summary>
    /// <returns>the input audio device list.</returns>
    public List<DeviceModel> GetAllInput()
    {
      if (HashSet is null)
      {
        Debug.WriteLine
          (
            "Failed to get the list of input audio device(s). " +
            "The audio device collection is null."
          );

        return new List<DeviceModel>();
      }

      List<DeviceModel> modelList = HashSet
        .Where(x => x.IsInput)
        .ToList();

      Debug.WriteLine
      (
        string.Format
        (
          "Got the list of input audio device(s) => Count: {0}",
          modelList.Count()
        )
      );

      return modelList;
    }

    /// <summary>
    /// Get the output audio device list.
    /// </summary>
    /// <returns>the output audio device list.</returns>
    public List<DeviceModel> GetAllOutput()
    {
      if (HashSet is null)
      {
        Debug.WriteLine
          (
            "Failed to get the list of output audio device(s). " +
            "The audio device collection is null."
          );

        return new List<DeviceModel>();
      }

      List<DeviceModel> modelList = HashSet
        .Where(x => x.IsOutput)
        .ToList();

      Debug.WriteLine
      (
        string.Format
        (
          "Got the list of output audio device(s) => Count: {0}",
          modelList.Count()
        )
      );

      return modelList;
    }

    /// <summary>
    /// Get the present audio device list.
    /// </summary>
    /// <returns>the present audio device list.</returns>
    public List<DeviceModel> GetAllPresent()
    {
      if (HashSet is null)
      {
        Debug.WriteLine
          (
            "Failed to get the list of present audio device(s). " +
            "The audio device collection is null."
          );

        return new List<DeviceModel>();
      }

      List<DeviceModel> modelList = HashSet
        .Where(x => x.IsPresent)
        .ToList();

      Debug.WriteLine
      (
        string.Format
        (
          "Got the list of present audio device(s)\t=> Count: {0}",
          modelList.Count()
        )
      );

      return modelList;
    }

    /// <summary>
    /// Get the audio device list.
    /// </summary>
    /// <param name="actualIdList">the list of actual audio device IDs</param>
    /// <returns>the audio device list.</returns>
    public List<DeviceModel> GetRange(List<string> actualIdList)
    {
      List<DeviceModel> modelList = new List<DeviceModel>();

      if
      (
        HashSet is null
        || HashSet.Count == 0
        || actualIdList is null
        || actualIdList.Count() == 0
      )
      {
        Debug.WriteLine
        (
          "Failed to get the list of audio device(s). " +
          "The audio device ID list is either null or empty, " +
          "or the audio device collection is either null or empty."
        );

        return modelList;
      }

      actualIdList
        .ForEach
        (
          x =>
          {
            DeviceModel model = Get(x);

            if (!(model is null))
            {
              modelList.Add(model);
            }
          }
        );

      Debug.WriteLine
      (
        string.Format
        (
          "Got the list of audio device(s)\t=> Count: {0}",
          modelList.Count()
        )
      );

      return modelList;
    }

    /// <summary>
    /// Get the audio device list.
    /// </summary>
    /// <param name="idList">the list of audio device IDs</param>
    /// <returns>the audio device list.</returns>
    public List<DeviceModel> GetRange(List<uint?> idList)
    {
      List<DeviceModel> modelList = new List<DeviceModel>();

      if
      (
        idList is null
        || idList.Count == 0
        || HashSet is null
        || HashSet.Count == 0
      )
      {
        Debug.WriteLine
        (
          "Failed to get list of audio device(s). " +
          "The audio device ID list is either null or empty, " +
          "or the audio device collection is either null or empty."
        );

        return modelList;
      }

      idList
        .ForEach
        (
          id =>
          {
            DeviceModel? model = Get(id);

            if (model != null)
            {
              modelList.Add(model);
            }
          }
        );

      Debug.WriteLine
      (
        string.Format
        (
          "Got the list of audio device(s)\t=> Count: {0}",
          modelList.Count()
        )
      );

      return modelList;
    }

    /// <summary>
    /// Start the audio device.
    /// </summary>
    /// <param name="actualId">the actual audio device ID</param>
    public void Start(string actualId)
    {
      MMDeviceRepository.Start(actualId);
    }

    /// <summary>
    /// Stop the audio device.
    /// </summary>
    /// <param name="actualId">the actual audio device ID</param>
    public void Stop(string actualId)
    {
      MMDeviceRepository.Stop(actualId);
    }

    /// <summary>
    /// Add the audio device.
    /// </summary>
    /// <param name="mMDevice">the actual audio device</param>
    public void Add(MMDevice mMDevice)
    {
      if (mMDevice is null)
      {
        Debug.WriteLine
          (
            "Failed to update the audio device. " +
            "Device is null."
          );

        return;
      }

      Add
      (
        mMDevice.ID,
        mMDevice.FriendlyName,
        mMDevice.DataFlow == DataFlow.Capture,
        mMDevice.DataFlow == DataFlow.Render,
        IsPresent(mMDevice.State)
      );
    }

    /// <summary>
    /// Add the audio device.
    /// </summary>
    /// <param name="actualId">the actual audio device ID</param>
    /// <param name="name">the audio device name</param>
    /// <param name="isInput">true/false is the audio device an input</param>
    /// <param name="isOutput">true/false is the audio device an output</param>
    /// <param name="isPresent">true/false is the audio device present</param>
    public void Add
    (
      string actualId,
      string name,
      bool? isInput,
      bool? isOutput,
      bool? isPresent
    )
    {
      DeviceModel model = new DeviceModel
        (
          NextId,
          actualId,
          name,
          isInput,
          isOutput,
          isPresent
        );

      HashSet.Add(model);

      Debug.WriteLine
        (
          string.Format
          (
            "Added the audio device\t=> Id: {0}",
            model.Id
          )
        );
    }

    /// <summary>
    /// Insert the audio device.
    /// </summary>
    /// <param name="model">the audio device</param>
    public void Insert(DeviceModel model)
    {
      if (model is null)
      {
        Debug.WriteLine
          (
            "Failed to insert the audio device. " +
            "The audio device is null."
          );

        return;
      }

      if (HashSet.Count() >= MaxId)
      {
        Console.WriteLine
        (
          string.Format
          (
            "Failed to insert the audio device. " +
            "The audio device list will exceed the maximum\t=> Maximum: {0}.",
            MaxId
          )
        );

        return;
      }

      uint id = model.Id;

      if (IdList.Contains(id))
      {
        Debug.WriteLine
        (
          string.Format
          (
            "The audio device ID is not valid\t=> ID: {0}",
            id
          )
        );

        id = NextId;
      }

      if (!HashSet.Add(model))
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Failed to insert the audio device\t=> ID: {0}",
            id
          )
        );

        return;
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Inserted the audio device\t=> ID: {0}",
          id
        )
      );
    }

    /// <summary>
    /// Insert the audio device.
    /// </summary>
    /// <param name="id">the audio device ID</param>
    /// <param name="actualId">the actual audio device ID</param>
    /// <param name="name">the actual audio device name</param>
    /// <param name="isInput">true/false is the input audio device</param>
    /// <param name="isOutput">true/false is the output audio device</param>
    /// <param name="isPresent">true/false is the audio device present</param>
    public void Insert
    (
      uint id,
      string actualId,
      string name,
      bool? isInput,
      bool? isOutput,
      bool? isPresent
    )
    {
      DeviceModel model = new DeviceModel
      (
        id,
        actualId,
        name,
        isInput,
        isOutput,
        isPresent
      );

      Insert(model);
    }

    /// <summary>
    /// Remove the audio device.
    /// </summary>
    /// <param name="id">the audio device ID</param>
    public void Remove(uint? id)
    {
      if (id is null)
      {
        Debug.WriteLine
          (
            "Failed to remove the audio device. " +
            "The audio device ID is null."
          );

        return;
      }

      int count = HashSet.RemoveWhere(x => x.Id == id);

      if (count == 0)
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Failed to remove the audio device. " +
            "The audio device does not exist\t=> ID: {0}",
            id
          )
        );

        return;
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Removed the audio device\t=> ID: {0}",
          id
        )
      );
    }

    /// <summary>
    /// Remove audio device(s).
    /// </summary>
    /// <param name="actualId">the actual audio device ID</param>
    public void Remove(string actualId)
    {
      if (string.IsNullOrWhiteSpace(actualId))
      {
        Debug.WriteLine
        (
          "Failed to remove the audio device. " +
          "The audio device ID is null or whitespace."
        );

        return;
      }

      int count = HashSet.RemoveWhere(x => x.ActualId == actualId);

      if (count == 0)
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Failed to remove the audio device. " +
            "The audio device does not exist\t=> Actual ID: {0}",
            actualId
          )
        );

        return;
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Removed audio device(s)\t=> Count: {0}",
          count
        )
      );
    }

    /// <summary>
    /// Remove the list of audio devices.
    /// </summary>
    /// <param name="name">the audio device name</param>
    public void RemoveRange(string name)
    {
      if (string.IsNullOrWhiteSpace(name))
      {
        Debug.WriteLine
        (
          "Failed to remove audio device. " +
          "Device name is null or whitespace."
        );

        return;
      }

      int count = HashSet.RemoveWhere(x => x.Name == name);

      if (count == 0)
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Failed to remove the audio device. " +
            "The audio device does not exist\t=> Name: {0}",
            name
          )
        );

        return;
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Removed audio device(s)\t=> Count: {0}",
          count
        )
      );
    }

    /// <summary>
    /// Set the audio device as default.
    /// </summary>
    /// <param name="actualId">the actual audio device ID</param>
    public void SetAsDefault(string actualId)
    {
      MMDevice? mMDevice = MMDeviceRepository.Get(actualId);

      if (mMDevice is null)
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Failed to set the audio device as default. " +
            "The audio device does not exist\t => ID: {0}",
            actualId
          )
        );

        return;
      }

      CoreAudioDevice coreAudioDevice = CoreAudioController.GetDevice
        (
          Guid.Parse(actualId)
        );

      try
      {
        coreAudioDevice.SetAsDefault();
      }

      catch (Exception exception)
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Failed to set the audio device as default\t=> Exception: {0}",
            exception.Message
          )
        );

        return;
      }

      Debug.WriteLine
      (
        "Set the audio device as default. " +
        "The audio device is null."
      );
    }

    /// <summary>
    /// Update the audio device.
    /// </summary>
    /// <param name="model">the audio device</param>
    public void Update(DeviceModel model)
    {
      if (model is null)
      {
        Debug.WriteLine
          (
            "Failed to update audio device. " +
            "Device is null."
          );

        return;
      }

      if
      (
        HashSet
          .RemoveWhere
          (x => x.Id == model.Id) == 0
      )
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Failed to update audio device. " +
            "Device does not exist\t=> ID: {0}",
            model.Id
          )
        );

        return;
      }

      if (!HashSet.Add(model))
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Failed to update audio device\t=> ID: {0}",
            model.Id
          )
        );

        return;
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Updated audio device\t=> ID: {0}",
          model.Id
        )
      );
    }

    /// <summary>
    /// Update the audio device.
    /// </summary>
    /// <param name="id">the audio device ID</param>
    /// <param name="mMDevice">the actual audio device</param>
    public void Update
    (
      uint id,
      MMDevice mMDevice
    )
    {
      if
      (
        mMDevice is null
      )
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Failed to update audio device. " +
            "Actual audio device is null\t=> ID: {0}",
            id
          )
        );
        return;
      }

      DeviceModel model = new DeviceModel
      (
        id,
        mMDevice.ID,
        mMDevice.FriendlyName,
        mMDevice.DataFlow == DataFlow.Capture,
        mMDevice.DataFlow == DataFlow.Render,
        IsPresent(mMDevice.State)
      );

      Update(model);
    }

    /// <summary>
    /// Update the audio device.
    /// </summary>
    /// <param name="id">the audio device ID</param>
    /// <param name="actualId">the actual audio device ID</param>
    /// <param name="name">the actual audio device name</param>
    /// <param name="isInput">true/false is the input audio device</param>
    /// <param name="isOutput">true/false is the output audio device</param>
    /// <param name="isPresent">true/false is the audio device present</param>
    public void Update
    (
      uint id,
      string actualId,
      string name,
      bool? isInput,
      bool? isOutput,
      bool? isPresent
    )
    {
      DeviceModel model = new DeviceModel
        (
          id,
          actualId,
          name,
          isInput,
          isOutput,
          isPresent
        );

      Update(model);
    }

    #endregion
  }
}