﻿using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using AudioRepeaterManager.NET4_0.Backend.Models;
using AudioSwitcher.AudioApi.CoreAudio;

/*
 * TODO:
 * 
 * -organize methods by type, then name, then overrides.
 * -refactor comments
 * -device ability
 * -refer DeviceModel as abstract.
 * -refer MMDevice as actual.
 * 
 */

namespace AudioRepeaterManager.NET4_0.Backend.Repositories
{
  public class DeviceRepository :
    IDeviceRepository,
    INotifyPropertyChanged
  {
    #region Parameters

    /// <summary>
    /// The controller of actual devices.
    /// </summary>
    private CoreAudioController coreAudioController;

    /// <summary>
    /// The collection of devices.
    /// </summary>
    private HashSet<DeviceModel> DeviceModelHashSet;

    /// <summary>
    /// The collection of actual devices.
    /// </summary>
    private MMDeviceRepository MMDeviceRepository;

    /// <summary>
    /// The list of device IDs.
    /// </summary>
    private List<uint> IdList
    {
      get
      {
        List<uint> list =
          DeviceModelHashSet
            .Select(x => x.Id)
            .ToList();

        list.Sort();
        return list;
      }
    }

    /// <summary>
    /// The next valid device ID.
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

    public event PropertyChangedEventHandler PropertyChanged;

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    [ExcludeFromCodeCoverage]
    public DeviceRepository()
    {
      coreAudioController = new CoreAudioController();
      DeviceModelHashSet = new HashSet<DeviceModel>();
      MMDeviceRepository = new MMDeviceRepository();
      uint id = 0;

      MMDeviceRepository
        .GetAll()
        .ForEach
        (
          x =>
          {
            DeviceModel deviceModel =
              new DeviceModel
              (
                id,
                x.ID,
                x.FriendlyName,
                x.DataFlow == DataFlow.Capture,
                x.DataFlow == DataFlow.Render,
                IsPresent(x.State)
              );

            DeviceModelHashSet
              .Add(deviceModel);

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
    /// <param name="deviceModelList">The device list</param>
    [ExcludeFromCodeCoverage]
    public DeviceRepository(List<DeviceModel> deviceModelList)
    {
      coreAudioController = new CoreAudioController();
      MMDeviceRepository = new MMDeviceRepository();
      DeviceModelHashSet = new HashSet<DeviceModel>();

      deviceModelList
        .ForEach
        (
          x =>
          DeviceModelHashSet.Add(x)
        );

      uint id = NextId;

      MMDeviceRepository
        .GetAll()
        .ForEach
        (
          x =>
          {
            DeviceModel deviceModel =
              new DeviceModel
              (
                id,
                x.ID,
                x.FriendlyName,
                x.DataFlow == DataFlow.Capture,
                x.DataFlow == DataFlow.Render,
                IsPresent(x.State)
              );

            DeviceModelHashSet
              .Add(deviceModel);

            if (id == uint.MaxValue)
            {
              return;
            }

            id++;
          }
        );
    }

    /// <summary>
    /// Construct a device.
    /// </summary>
    /// <param name="id">The device ID</param>
    /// <param name="mMDevice"></param>
    /// <returns></returns>
    private DeviceModel Construct
    (
      uint id,
      MMDevice mMDevice
    )
    {
      return new DeviceModel
      (
        id,
        mMDevice.ID,
        mMDevice.FriendlyName,
        mMDevice.DataFlow == DataFlow.Capture,
        mMDevice.DataFlow == DataFlow.Render,
        IsPresent(mMDevice.State)
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
          "PropertyChanged: '{1}'" +
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

      return
        DeviceModelHashSet
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

      DeviceModel deviceModel = DeviceModelHashSet
        .FirstOrDefault(x => x.Id == id);


      if (deviceModel is null)
      {
        Debug.WriteLine("Device is null.");
      }

      else
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Got device\t=> Id: '{1}'",
            deviceModel.Id
          )
        );
      }

      return deviceModel;
    }

    /// <summary>
    /// Get the device list.
    /// </summary>
    /// <returns>The device list.</returns>
    public List<DeviceModel> GetAll()
    {
      if (DeviceModelHashSet is null)
      {
        Debug.WriteLine("Failed to get device(s). Device collection is null.");
        return new List<DeviceModel>();
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Got device(s) => Count: {1}",
          DeviceModelHashSet.Count()
        )
      );

      return DeviceModelHashSet.ToList();
    }

    /// <summary>
    /// Get the absent device list.
    /// </summary>
    /// <returns>The absent device list.</returns>
    public List<DeviceModel> GetAllAbsent()
    {
      if (DeviceModelHashSet is null)
      {
        return new List<DeviceModel>();
      }

      return
        DeviceModelHashSet
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
          "Failed to get disabled device(s)." +
          "Actual device collection is null."
        );

        return new List<DeviceModel>();
      }

      List<string> actualIdList = MMDeviceRepository
        .GetAllDisabled()
        .Select(x => x.ID)
        .ToList();  

      List<DeviceModel> deviceModelList =
        GetRange(actualIdList);

      Debug.WriteLine
      (
        string.Format
        (
          "Got disabled device(s) => Count: {1}",
          deviceModelList.Count()
        )
      );

      return deviceModelList;
    }

    /// <summary>
    /// Get the duplex device list.
    /// </summary>
    /// <returns>The duplex device list.</returns>
    public List<DeviceModel> GetAllDuplex()
    {
      if (DeviceModelHashSet is null)
      {
        Debug.WriteLine("Failed to get duplex device(s). Device collection is null.");
        return new List<DeviceModel>();
      }

      List<DeviceModel> deviceModelList = DeviceModelHashSet
        .Where(x => x.IsDuplex)
        .ToList();

      Debug.WriteLine
      (
        string.Format
        (
          "Got duplex device(s) => Count: {1}",
          deviceModelList.Count()
        )
      );

      return deviceModelList;
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
          "Failed to get enabled device(s)." +
          "Actual device collection is null."
        );

        return new List<DeviceModel>();
      }

      List<string> actualIdList = MMDeviceRepository
        .GetAllEnabled()
        .Select(x => x.ID)
        .ToList();

      List<DeviceModel> deviceModelList =
        GetRange(actualIdList);

      Debug.WriteLine
      (
        string.Format
        (
          "Got enabled device(s) => Count: {1}",
          deviceModelList.Count()
        )
      );

      return deviceModelList;
    }

    /// <summary>
    /// Get the input device list.
    /// </summary>
    /// <returns>The input device list.</returns>
    public List<DeviceModel> GetAllInput()
    {
      if (DeviceModelHashSet is null)
      {
        Debug.WriteLine("Failed to get input device(s). Device collection is null.");
        return new List<DeviceModel>();
      }

      List<DeviceModel> deviceModelList = DeviceModelHashSet
        .Where(x => x.IsInput)
        .ToList();

      Debug.WriteLine
      (
        string.Format
        (
          "Got input device(s) => Count: {1}",
          deviceModelList.Count()
        )
      );

      return deviceModelList;
    }

    /// <summary>
    /// Get the output device list.
    /// </summary>
    /// <returns>The output device list.</returns>
    public List<DeviceModel> GetAllOutput()
    {
      if (DeviceModelHashSet is null)
      {
        Debug.WriteLine("Failed to get output device(s). Device collection is null.");
        return new List<DeviceModel>();
      }

      List<DeviceModel> deviceModelList = DeviceModelHashSet
        .Where(x => x.IsOutput)
        .ToList();

      Debug.WriteLine
      (
        string.Format
        (
          "Got output device(s) => Count: {1}",
          deviceModelList.Count()
        )
      );

      return deviceModelList;
    }

    /// <summary>
    /// Get the present device list.
    /// </summary>
    /// <returns>The present device list.</returns>
    public List<DeviceModel> GetAllPresent()
    {
      if (DeviceModelHashSet is null)
      {
        Debug.WriteLine("Failed to get present device(s). Device collection is null.");
        return new List<DeviceModel>();
      }

      List<DeviceModel> deviceModelList = DeviceModelHashSet
        .Where(x => x.IsPresent)
        .ToList();

      Debug.WriteLine
      (
        string.Format
        (
          "Got present device(s) => Count: {1}",
          deviceModelList.Count()
        )
      );

      return deviceModelList;
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
        DeviceModelHashSet is null
        || DeviceModelHashSet.Count == 0
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

      List<DeviceModel> deviceModelList = new List<DeviceModel>();

      actualIdList
        .ForEach
        (
          x =>
          {
            DeviceModel deviceModel = Get(x);

            if (!(deviceModel is null))
            {
              deviceModelList
                .Add(deviceModel);
            }
          }
        );

      return deviceModelList;
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
        || DeviceModelHashSet is null
        || DeviceModelHashSet.Count == 0
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

      List<DeviceModel> deviceModelList = new List<DeviceModel>();

      idList
        .ForEach
        (
          id =>
          deviceModelList
            .Add(Get(id))
        );

      Debug.WriteLine
      (
        string.Format
        (
          "Got device(s) => Count: {1}",
          deviceModelList.Count()
        )
      );

      return deviceModelList;
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
    /// <param name="deviceModel">The device</param>
    public void Insert(DeviceModel deviceModel)
    {
      if (deviceModel is null)
      {
        Debug.WriteLine("Failed to insert device. Device is null.");
        return;
      }

      if (DeviceModelHashSet.Count() >= Global.MaxEndpointCount)
      {
        Console.WriteLine
        (
          string.Format
          (
            "Failed to insert device. Device list will exceed maximum of {1}.",
            Global.MaxEndpointCount
          )
        );

        return;
      }

      uint id = deviceModel.Id;

      if (IdList.Contains(id))
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Device ID is not valid\t=> Id: '{1}'",
            id
          )
        );

        id = NextId;
      }

      if (!DeviceModelHashSet.Add(deviceModel))
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Failed to insert device\t=> Id: '{1}'",
            id
          )
        );

        return;
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Inserted device\t=> Id: '{1}'",
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
        Debug.WriteLine("Failed to update actual device. Device is null.");
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
      DeviceModel deviceModel = new DeviceModel
      (
        NextId,
        actualId,
        name,
        isInput,
        isOutput,
        isPresent
      );

      Insert(deviceModel);
    }

    /// <summary>
    /// Remove a device.
    /// </summary>
    /// <param name="id">The device ID</param>
    public void Remove(uint? id)
    {
      if (id is null)
      {
        Debug.WriteLine("Failed to remove device. Device ID is null.");
        return;
      }

      int count = DeviceModelHashSet
        .RemoveWhere(x => x.Id == id);

      if (count == 0)
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Failed to remove device. Device does not exist\t=> Id: '{1}'",
            id
          )
        );

        return;
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Removed device\t=> Id: '{1}'",
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

      int count = DeviceModelHashSet
        .RemoveWhere(x => x.ActualId == actualId);

      if (count == 0)
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Failed to remove device. Device does not exist\t=> ActualId: '{1}'",
            actualId
          )
        );

        return;
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Removed device(s)\t=> Count: '{1}'",
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

      int count = DeviceModelHashSet
        .RemoveWhere(x => x.Name == name);

      if (count == 0)
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Failed to remove device. Device does not exist\t=> Name: '{1}'",
            name
          )
        );

        return;
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Removed device(s)\t=> Count: '{1}'",
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
        return;
      }

      CoreAudioDevice coreAudioDevice = coreAudioController
        .GetDevice
        (
          Guid.Parse(actualId)
        );

      coreAudioDevice.SetAsDefault();
    }

    /// <summary>
    /// Update a device.
    /// </summary>
    /// <param name="deviceModel">The device</param>
    public void Update(DeviceModel deviceModel)
    {
      if (deviceModel is null)
      {
        Debug.WriteLine("Failed to update device. Device is null.");
        return;
      }

      if
      (
        DeviceModelHashSet
          .RemoveWhere
          (x => x.Id == deviceModel.Id) == 0
      )
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Failed to update device. Device does not exist\t=> Id: '{1}'",
            deviceModel.Id
          )
        );

        return;
      }

      if (!DeviceModelHashSet.Add(deviceModel))
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Failed to update device\t=> Id: '{1}'",
            deviceModel.Id
          )
        );

        return;
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Updated device\t=> Id: '{1}'",
          deviceModel.Id
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
            "Failed to update device. Actual device is null\t=> Id: '{1}'",
            id
          )
        );
        return;
      }

      DeviceModel deviceModel = new DeviceModel
      (
        id,
        mMDevice.ID,
        mMDevice.FriendlyName,
        mMDevice.DataFlow == DataFlow.Capture,
        mMDevice.DataFlow == DataFlow.Render,
        IsPresent(mMDevice.State)
      );

      Update(deviceModel);
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
      DeviceModel deviceModel = new DeviceModel
        (
          id,
          actualId,
          name,
          isInput,
          isOutput,
          isPresent
        );

      Update(deviceModel);
    }

    #endregion
  }
}