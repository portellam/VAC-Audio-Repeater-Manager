﻿using NAudio.CoreAudioApi;
using System.ComponentModel;
using AudioRepeaterManager.NET8_0.Domain.Models;

namespace AudioRepeaterManager.NET8_0.Domain.Repositories
{
  public interface IDeviceRepository
  {
    #region Parameters

    event PropertyChangedEventHandler PropertyChanged;

    #endregion

    #region Logic

    DeviceModel Get(string actualId);
    DeviceModel Get(uint? id);
    List<DeviceModel> GetAll();
    List<DeviceModel> GetAllAbsent();
    List<DeviceModel> GetAllDuplex();
    List<DeviceModel> GetAllInput();
    List<DeviceModel> GetAllOutput();
    List<DeviceModel> GetAllPresent();
    List<DeviceModel> GetAllStarted();
    List<DeviceModel> GetAllStopped();
    List<DeviceModel> GetRange(List<string> actualIdList);
    List<DeviceModel> GetRange(List<uint?> idList);

    void Add(DeviceModel model);

    void Add
    (
      uint id,
      string actualId,
      string name,
      bool? isInput,
      bool? isOutput,
      bool? isPresent
    );

    void Start(string actualId);
    void Stop(string actualId);
    void Insert(DeviceModel model);
    void Insert(MMDevice mMDevice);

    void Insert
    (
      string actualId,
      string name,
      bool? isInput,
      bool? isOutput,
      bool? isPresent
    );

    void Remove(uint? Id);
    void Remove(string actualId);
    void RemoveRange(string name);
    void SetAsDefault(string actualId);
    void Update(DeviceModel model);

    void Update
    (
      uint id,
      MMDevice mMDevice
    );

    void Update
    (
      uint id,
      string actualId,
      string name,
      bool? isInput,
      bool? isOutput,
      bool? isPresent
    );

    #endregion
  }
}
