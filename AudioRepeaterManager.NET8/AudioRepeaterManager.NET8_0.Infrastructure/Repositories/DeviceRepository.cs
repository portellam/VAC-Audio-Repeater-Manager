using NAudio.CoreAudioApi;
using AudioSwitcher.AudioApi.CoreAudio;                                         // NAudio Issue #421: AudioSwitcher must succeed NAudio.
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using AudioRepeaterManager.NET8_0.Domain.Models;
using AudioRepeaterManager.NET8_0.Domain.Repositories;

namespace AudioRepeaterManager.NET8_0.Infrastructure.Repositories
{
  public class DeviceRepository :
    IDeviceRepository,
    INotifyPropertyChanged
  {
    #region Parameters

    /// <summary>
    /// The controller of actual devices.
    /// </summary>
    private CoreAudioController CoreAudioController { get; set; }

    /// <summary>
    /// The collection of devices.
    /// </summary>
    private HashSet<DeviceModel> HashSet { get; set; }

    /// <summary>
    /// The collection of actual devices.
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

    public event PropertyChangedEventHandler? PropertyChanged;

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
    /// <param name="modelList">The device list</param>
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
    /// Is a device present.
    /// </summary>
    /// <param name="deviceState">The device state</param>
    /// <returns>True/false is the device present.</returns>
    private bool IsPresent(DeviceState deviceState)
    {
      return deviceState == DeviceState.Active
        || deviceState == DeviceState.Disabled
        || deviceState == DeviceState.Unplugged;
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
    /// Get the device.
    /// </summary>
    /// <param name="actualId">the actual device ID</param>
    /// <returns>The device to get.</returns>
    public DeviceModel Get(string actualId)
    {
      if (string.IsNullOrWhiteSpace(actualId))
      {
        Debug.WriteLine
        (
          "Failed to get device. " +
          "Actual device ID is either null or whitespace."
        );

        return null;
      }

      return HashSet
        .FirstOrDefault(x => x.ActualId == actualId);
    }

    /// <summary>
    /// Get the device.
    /// </summary>
    /// <param name="id">the device ID</param>
    /// <returns>The device to get.</returns>
    public DeviceModel Get(uint? id)
    {
      if (id is null)
      {
        Debug.WriteLine
        (
          "Failed to get device. " +
          "Device ID is null."
        );

        return null;
      }

      DeviceModel model = HashSet
        .FirstOrDefault(x => x.Id == id);


      if (model is null)
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Failed to get device. " +
            "Device is null\t=> Id: {0}",
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
            "Got device\t=> Id: {0}",
            model.Id
          )
        );
      }

      return model;
    }

    /// <summary>
    /// Get the device list.
    /// </summary>
    /// <returns>The device list.</returns>
    public List<DeviceModel> GetAll()
    {
      if (HashSet is null)
      {
        Debug.WriteLine
          (
            "Failed to get device(s). " +
            "Device collection is null."
          );

        return new List<DeviceModel>();
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Got device(s) => Count: {0}",
          HashSet.Count()
        )
      );

      return HashSet.ToList();
    }

    /// <summary>
    /// Get the absent device list.
    /// </summary>
    /// <returns>The absent device list.</returns>
    public List<DeviceModel> GetAllAbsent()
    {
      if (HashSet is null)
      {
        Debug.WriteLine
          (
            "Failed to get absent device(s). " +
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
    /// Get the disabled device list.
    /// </summary>
    /// <returns>The disabled device list.</returns>
    public List<DeviceModel> GetAllDisabled()
    {
      if (MMDeviceRepository is null)
      {
        Debug.WriteLine
          (
            "Failed to get disabled device(s). " +
            "Device collection is null."
          );

        return new List<DeviceModel>();
      }

      List<string> actualIdList = MMDeviceRepository
        .GetAllDisabled()
        .Select(x => x.ID)
        .ToList();

      List<DeviceModel> modelList =
        GetRange(actualIdList);

      Debug.WriteLine
      (
        string.Format
        (
          "Got disabled device(s) => Count: {0}",
          modelList.Count()
        )
      );

      return modelList;
    }

    /// <summary>
    /// Get the duplex device list.
    /// </summary>
    /// <returns>The duplex device list.</returns>
    public List<DeviceModel> GetAllDuplex()
    {
      if (HashSet is null)
      {
        Debug.WriteLine
          (
            "Failed to get duplex device(s). " +
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
          "Got duplex device(s) => Count: {0}",
          modelList.Count()
        )
      );

      return modelList;
    }

    /// <summary>
    /// Get the enabled device list.
    /// </summary>
    /// <returns>The enabled device list.</returns>
    public List<DeviceModel> GetAllEnabled()
    {
      if (MMDeviceRepository is null)
      {
        Debug.WriteLine
          (
            "Failed to get enabled device(s). " +
            "Device collection is null."
          );

        return new List<DeviceModel>();
      }

      List<string> actualIdList = MMDeviceRepository
        .GetAllEnabled()
        .Select(x => x.ID)
        .ToList();

      List<DeviceModel> modelList =
        GetRange(actualIdList);

      Debug.WriteLine
      (
        string.Format
        (
          "Got enabled device(s) => Count: {0}",
          modelList.Count()
        )
      );

      return modelList;
    }

    /// <summary>
    /// Get the input device list.
    /// </summary>
    /// <returns>The input device list.</returns>
    public List<DeviceModel> GetAllInput()
    {
      if (HashSet is null)
      {
        Debug.WriteLine
          (
            "Failed to get input device(s). " +
            "Device collection is null."
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
          "Got input device(s) => Count: {0}",
          modelList.Count()
        )
      );

      return modelList;
    }

    /// <summary>
    /// Get the output device list.
    /// </summary>
    /// <returns>The output device list.</returns>
    public List<DeviceModel> GetAllOutput()
    {
      if (HashSet is null)
      {
        Debug.WriteLine
          (
            "Failed to get output device(s). " +
            "Device collection is null."
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
          "Got output device(s) => Count: {0}",
          modelList.Count()
        )
      );

      return modelList;
    }

    /// <summary>
    /// Get the present device list.
    /// </summary>
    /// <returns>The present device list.</returns>
    public List<DeviceModel> GetAllPresent()
    {
      if (HashSet is null)
      {
        Debug.WriteLine
          (
            "Failed to get present device(s). " +
            "Device collection is null."
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
          "Got present device(s) => Count: {0}",
          modelList.Count()
        )
      );

      return modelList;
    }

    /// <summary>
    /// Get a device list.
    /// </summary>
    /// <param name="actualIdList">the list of actual device IDs</param>
    /// <returns>The device list.</returns>
    public List<DeviceModel> GetRange(List<string> actualIdList)
    {
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
          "Failed to get device(s). " +
          "Device ID list is either null or empty, " +
          "or device collection is either null or empty."
        );

        return new List<DeviceModel>();
      }

      List<DeviceModel> modelList = new List<DeviceModel>();

      actualIdList
        .ForEach
        (
          x =>
          {
            DeviceModel model = Get(x);

            if (!(model is null))
            {
              modelList
                .Add(model);
            }
          }
        );

      return modelList;
    }

    /// <summary>
    /// Get a device list.
    /// </summary>
    /// <param name="idList">the list of device IDs</param>
    /// <returns>The device list.</returns>
    public List<DeviceModel> GetRange(List<uint?> idList)
    {
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
          "Failed to get device(s). " +
          "Device ID list is either null or empty, " +
          "or device collection is either null or empty."
        );

        return null;
      }

      List<DeviceModel> modelList = new List<DeviceModel>();

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
          "Got device(s) => Count: {0}",
          modelList.Count()
        )
      );

      return modelList;
    }

    /// <summary>
    /// Disable the actual device.
    /// </summary>
    /// <param name="actualId">The actual device ID</param>
    public void DisableActual(string actualId)
    {
      MMDeviceRepository.Disable(actualId);
    }

    /// <summary>
    /// Enable the actual device.
    /// </summary>
    /// <param name="actualId">The actual device ID</param>
    public void EnableActual(string actualId)
    {
      MMDeviceRepository.Enable(actualId);
    }

    /// <summary>
    /// Insert a device.
    /// </summary>
    /// <param name="model">The device</param>
    public void Insert(DeviceModel model)
    {
      if (model is null)
      {
        Debug.WriteLine
          (
            "Failed to insert device. " +
            "Device is null."
          );

        return;
      }

      if (HashSet.Count() >= MaxId)
      {
        Console.WriteLine
        (
          string.Format
          (
            "Failed to insert device. " +
            "Device list will exceed maximum of {0}.",
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
            "Device ID is not valid\t=> Id: {0}",
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
            "Failed to insert device\t=> Id: {0}",
            id
          )
        );

        return;
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Inserted device\t=> Id: {0}",
          id
        )
      );
    }

    /// <summary>
    /// Insert a device.
    /// </summary>
    /// <param name="mMDevice">The actual device</param>
    public void Insert(MMDevice mMDevice)
    {
      if (mMDevice is null)
      {
        Debug.WriteLine
          (
            "Failed to update actual device. " +
            "Device is null."
          );

        return;
      }

      Insert
      (
        mMDevice.ID,
        mMDevice.FriendlyName,
        mMDevice.DataFlow == DataFlow.Capture,
        mMDevice.DataFlow == DataFlow.Render,
        IsPresent(mMDevice.State)
      );
    }

    /// <summary>
    /// Insert a device.
    /// </summary>
    /// <param name="actualId">The actual device ID</param>
    /// <param name="name">The actual device name</param>
    /// <param name="isInput">True/false is an input device</param>
    /// <param name="isOutput">True/false is an output device</param>
    /// <param name="isPresent">True/false is the device present</param>
    public void Insert
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

      Insert(model);
    }

    /// <summary>
    /// Remove a device.
    /// </summary>
    /// <param name="id">The device ID</param>
    public void Remove(uint? id)
    {
      if (id is null)
      {
        Debug.WriteLine
          (
            "Failed to remove device. " +
            "Device ID is null."
          );

        return;
      }

      int count = HashSet
        .RemoveWhere(x => x.Id == id);

      if (count == 0)
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Failed to remove device. " +
            "Device does not exist\t=> Id: {0}",
            id
          )
        );

        return;
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Removed device\t=> Id: {0}",
          id
        )
      );
    }

    /// <summary>
    /// Remove device(s).
    /// </summary>
    /// <param name="actualId">The actual device ID</param>
    public void Remove(string actualId)
    {
      if (string.IsNullOrWhiteSpace(actualId))
      {
        Debug.WriteLine
        (
          "Failed to remove device. " +
          "Actual device ID is null or whitespace."
        );

        return;
      }

      int count = HashSet
        .RemoveWhere(x => x.ActualId == actualId);

      if (count == 0)
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Failed to remove device. " +
            "Device does not exist\t=> ActualId: {0}",
            actualId
          )
        );

        return;
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Removed device(s)\t=> Count: {0}",
          count
        )
      );
    }

    /// <summary>
    /// Remove a list of devices.
    /// </summary>
    /// <param name="name">The device name</param>
    public void RemoveRange(string name)
    {
      if (string.IsNullOrWhiteSpace(name))
      {
        Debug.WriteLine
        (
          "Failed to remove device. " +
          "Device name is null or whitespace."
        );

        return;
      }

      int count = HashSet
        .RemoveWhere(x => x.Name == name);

      if (count == 0)
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Failed to remove device. Device does not exist\t=> Name: {0}",
            name
          )
        );

        return;
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Removed device(s)\t=> Count: {0}",
          count
        )
      );
    }

    /// <summary>
    /// Set the actual device as default.
    /// </summary>
    /// <param name="actualId">the actual device ID</param>
    public void SetAsDefault(string actualId)
    {
      MMDevice mMDevice = MMDeviceRepository.Get(actualId);

      if (mMDevice is null)
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Failed to set actual device as default. " +
            "Actual device does not exist\t => Id: {0}",
            actualId
          )
        );

        return;
      }

      CoreAudioDevice coreAudioDevice = CoreAudioController
        .GetDevice
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
            "Failed to set actual device as default. " +
            "An exception occurred\t=> Ex: {0}",
            exception
          )
        );
      }

      Debug.WriteLine
      (
        "Set actual device as default. " +
        "Actual Device is null."
      );
    }

    /// <summary>
    /// Update a device.
    /// </summary>
    /// <param name="model">The device</param>
    public void Update(DeviceModel model)
    {
      if (model is null)
      {
        Debug.WriteLine
          (
            "Failed to update device. " +
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
            "Failed to update device. " +
            "Device does not exist\t=> Id: {0}",
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
            "Failed to update device\t=> Id: {0}",
            model.Id
          )
        );

        return;
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Updated device\t=> Id: {0}",
          model.Id
        )
      );
    }

    /// <summary>
    /// Update a device.
    /// </summary>
    /// <param name="id">The device ID</param>
    /// <param name="mMDevice">The actual device</param>
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
            "Failed to update device. " +
            "Actual device is null\t=> Id: {0}",
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
    /// Update a device.
    /// </summary>
    /// <param name="id">The device ID</param>
    /// <param name="actualId">The actual device ID</param>
    /// <param name="name">The actual device name</param>
    /// <param name="isInput">True/false is an input device</param>
    /// <param name="isOutput">True/false is an output device</param>
    /// <param name="isPresent">True/false is the device present</param>
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