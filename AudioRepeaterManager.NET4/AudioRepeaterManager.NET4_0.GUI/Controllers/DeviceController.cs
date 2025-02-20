using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using AudioRepeaterManager.NET4_0.Backend.Models;
using AudioRepeaterManager.NET4_0.Backend.Repositories;

namespace AudioRepeaterManager.NET4_0.GUI.Controllers
{
  public class DeviceController :
    IDeviceController,
    INotifyPropertyChanged
  {
    #region Parameters

    private DeviceRepository DeviceRepository;

    public event PropertyChangedEventHandler PropertyChanged;

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="deviceRepository">The device repository</param>
    [ExcludeFromCodeCoverage]
    public DeviceController(DeviceRepository deviceRepository)
    {
      DeviceRepository = deviceRepository;      
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
      return
        DeviceRepository
          .Get(actualId);
    }

    /// <summary>
    /// Get the device.
    /// </summary>
    /// <param name="id">the device ID</param>
    /// <returns>The device to get.</returns>
    public DeviceModel Get(uint? id)
    {
      return
        DeviceRepository
          .Get(id);
    }

    /// <summary>
    /// Get the device list.
    /// </summary>
    /// <returns>The device list.</returns>
    public List<DeviceModel> GetAll()
    {
      return
        DeviceRepository
          .GetAll();
    }

    /// <summary>
    /// Get the absent device list.
    /// </summary>
    /// <returns>The absent device list.</returns>
    public List<DeviceModel> GetAllAbsent()
    {
      return
        DeviceRepository
          .GetAllAbsent();
    }

    /// <summary>
    /// Get the disabled device list.
    /// </summary>
    /// <returns>The disabled device list.</returns>
    public List<DeviceModel> GetAllDisabled()
    {
      return
        DeviceRepository
          .GetAllDisabled();
    }

    /// <summary>
    /// Get the duplex device list.
    /// </summary>
    /// <returns>The duplex device list.</returns>
    public List<DeviceModel> GetAllDuplex()
    {
      return
        DeviceRepository
          .GetAllDuplex();
    }

    /// <summary>
    /// Get the enabled device list.
    /// </summary>
    /// <returns>The enabled device list.</returns>
    public List<DeviceModel> GetAllEnabled()
    {
      return
        DeviceRepository
          .GetAllEnabled();
    }

    /// <summary>
    /// Get the input device list.
    /// </summary>
    /// <returns>The input device list.</returns>
    public List<DeviceModel> GetAllInput()
    {
      return
        DeviceRepository
          .GetAllInput();
    }

    /// <summary>
    /// Get the output device list.
    /// </summary>
    /// <returns>The output device list.</returns>
    public List<DeviceModel> GetAllOutput()
    {
      return
        DeviceRepository
          .GetAllOutput();
    }

    /// <summary>
    /// Get the present device list.
    /// </summary>
    /// <returns>The present device list.</returns>
    public List<DeviceModel> GetAllPresent()
    {
      return
        DeviceRepository
          .GetAllPresent();
    }

    /// <summary>
    /// Get a device list.
    /// </summary>
    /// <param name="actualIdList">the list of actual device IDs</param>
    /// <returns>The device list.</returns>
    public List<DeviceModel> GetRange(List<string> actualIdList)
    {
      return
        DeviceRepository
          .GetRange(actualIdList);
    }

    /// <summary>
    /// Get a device list.
    /// </summary>
    /// <param name="idList">the list of device IDs</param>
    /// <returns>The device list.</returns>
    public List<DeviceModel> GetRange(List<uint?> idList)
    {
      return
        DeviceRepository
          .GetRange(idList);
    }

    /// <summary>
    /// Disable the actual device.
    /// </summary>
    /// <param name="actualId">The actual device ID</param>
    public void DisableActual(string actualId)
    {
      DeviceRepository.EnableActual(actualId);
    }

    /// <summary>
    /// Enable the actual device.
    /// </summary>
    /// <param name="actualId">The actual device ID</param>
    public void EnableActual(string actualId)
    {
      DeviceRepository.EnableActual(actualId);
    }

    /// <summary>
    /// Add the actual device.
    /// </summary>
    /// <param name="actualId">The actual device ID</param>
    /// <param name="name">The device name</param>
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
      DeviceRepository
        .Insert
        (
          actualId,
          name,
          isInput,
          isOutput,
          isPresent
        );
    }

    /// <summary>
    /// Remove the device.
    /// </summary>
    /// <param name="id">The device ID</param>
    public void Remove(uint? id)
    {
      DeviceRepository
        .Remove(id);
    }

    /// <summary>
    /// Remove the device.
    /// </summary>
    /// <param name="actualId">The actual device ID</param>
    public void Remove(string actualId)
    {
      DeviceRepository
        .Remove(actualId);
    }
    #endregion
  }
}