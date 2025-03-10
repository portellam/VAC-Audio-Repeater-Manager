﻿using AudioSwitcher.AudioApi;
using NAudio.CoreAudioApi;
using System.Collections.Generic;
using System.Threading.Tasks;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Infrastructure.Services
{
  public partial class DeviceGroupService
    <
      TGroupReadonlyRepository,
      TBaseService,
      TBaseRepository,
      TDeviceModel
    >
  {
    #region Logic

    public async Task<bool> UpdateServiceAsync()
    {
      if (this.MMDeviceService == null)
      {
        this.MMDeviceService =
          new MMDeviceService<ReadonlyRepository<MMDevice>, MMDevice>();
      }

      else
      {
        this.MMDeviceService
          .UpdateAll();
      }

      if (this.CoreAudioService == null)
      {
        this.CoreAudioService =
          new CoreAudioService<ReadonlyRepository<Device>, Device>();

        return true;
      }

      return await this.CoreAudioService
        .UpdateServiceAsync()
        .ConfigureAwait(false);
    }

    public async Task<TDeviceModel> GetDefaultCommunicationsAsync
    (
      bool isInput,
      bool isOutput
    )
    {
      if (this.CoreAudioService == null)
      {
        return null;
      }

      var device = await this
         .CoreAudioService
         .GetDefaultCommunicationsAsync
         (
           isInput,
           isOutput
         )
         .ConfigureAwait(false);

      if (device == null)
      {
        return null;
      }

      var actualId = device.Id
        .ToString();

      return this.GetByActualId(actualId);
    }

    public async Task<TDeviceModel> GetDefaultConsoleAsync
    (
      bool isInput,
      bool isOutput
    )
    {
      if (this.CoreAudioService == null)
      {
        return null;
      }

      var device = await this.CoreAudioService
        .GetDefaultConsoleAsync
        (
          isInput,
          isOutput
        )
        .ConfigureAwait(false);

      if (device == null)
      {
        return null;
      }

      var actualId = device.Id
        .ToString();

      return this.GetByActualId(actualId);
    }

    public async Task<TDeviceModel> GetDefaultMultimediaAsync
    (
      bool isInput,
      bool isOutput
    )
    {
      if (this.CoreAudioService == null)
      {
        return null;
      }

      var device = await this.CoreAudioService
        .GetDefaultMultimediaAsync
        (
          isInput,
          isOutput
        )
        .ConfigureAwait(false);

      if (device == null)
      {
        return null;
      }

      var actualId = device.Id
        .ToString();

      return this.GetByActualId(actualId);
    }

    #endregion
  }
}