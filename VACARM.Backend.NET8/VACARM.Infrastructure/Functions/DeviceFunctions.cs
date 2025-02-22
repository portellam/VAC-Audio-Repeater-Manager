﻿using AudioSwitcher.AudioApi;
using NAudio.CoreAudioApi;
using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Functions
{
  internal static class DeviceFunctions<TDeviceModel> 
    where TDeviceModel :
    DeviceModel
  {
    #region Parameters

    internal readonly static Func<TDeviceModel, bool> IsAbsent =
      (TDeviceModel x) => !x.IsPresent;

    internal readonly static Func<TDeviceModel, bool> IsCapture =
      (TDeviceModel x) => x.IsCapture;

    internal readonly static Func<TDeviceModel, bool> IsCommunications =
      (TDeviceModel x) => x.Role == "Communications";

    internal readonly static Func<TDeviceModel, bool> IsConsole =
      (TDeviceModel x) => x.Role == "Console";

    internal readonly static Func<TDeviceModel, bool> IsDefault =
      (TDeviceModel x) => x.IsDefault;

    internal readonly static Func<TDeviceModel, bool> IsDisabled =
      (TDeviceModel x) => !x.IsEnabled;

    internal readonly static Func<TDeviceModel, bool> IsDuplex =
      (TDeviceModel x) => x.IsDuplex;

    internal readonly static Func<TDeviceModel, bool> IsEnabled =
      (TDeviceModel x) => x.IsEnabled;

    internal readonly static Func<TDeviceModel, bool> IsMultimedia =
      (TDeviceModel x) => x.Role == "Multimedia";

    internal readonly static Func<TDeviceModel, bool> IsMuted =
      (TDeviceModel x) => x.IsMuted;

    internal readonly static Func<TDeviceModel, bool> IsPresent =
      (TDeviceModel x) => x.IsPresent;

    internal readonly static Func<TDeviceModel, bool> IsRender =
      (TDeviceModel x) => x.IsRender;

    internal readonly static Func<TDeviceModel, bool> IsUnmuted =
      (TDeviceModel x) => !x.IsMuted;

    #endregion

    #region Logic

    /// <summary>
    /// Get a <typeparamref name="DeviceModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <param name="mMDevice">The device</param>
    /// <param name="device">The device</param>
    /// <param name="role">The role</param>
    /// <returns>The device model.</returns>
    internal static TDeviceModel GetDeviceModel
    (
      uint id,
      MMDevice mMDevice,
      Device? device,
      string? role
    )
    {
      return new DeviceModel
        (
          id,
          mMDevice.ID,
          mMDevice.DeviceFriendlyName,
          MMDeviceFunctions<MMDevice>.IsCapture(mMDevice),
          (bool?)CoreAudioDeviceFunctions<Device>.IsDefault(device),
          MMDeviceFunctions<MMDevice>.IsDisabled(mMDevice),
          (bool?)CoreAudioDeviceFunctions<Device>.IsMuted(device),
          MMDeviceFunctions<MMDevice>.IsPresent(mMDevice),
          MMDeviceFunctions<MMDevice>.IsRender(mMDevice),
          role
        ) as TDeviceModel;
    }

    /// <summary>
    /// Match a <typeparamref name="TDeviceModel"/> actual ID.
    /// </summary>
    /// <param name="actualId">The actual ID</param>
    /// <returns>The function</returns>
    internal static Func<TDeviceModel, bool> ContainsActualId(string actualId)
    {
      return (TDeviceModel x) => x.ActualId == actualId;
    }

    /// <summary>
    /// Match a <typeparamref name="TDeviceModel"/> name.
    /// </summary>
    /// <param name="name">The name</param>
    /// <returns>The function</returns>
    internal static Func<TDeviceModel, bool> ContainsName(string name)
    {
      return (TDeviceModel x) => x.Name
        .ToLower()
        .Contains(name.ToLower());
    }

    /// <summary>
    /// Match a <typeparamref name="TDeviceModel"/> role.
    /// </summary>
    /// <param name="role">The role</param>
    /// <returns>The function</returns>
    internal static Func<TDeviceModel, bool> ContainsRole(string role)
    {
      return (TDeviceModel x) => x.Role
        .ToLower() == role.ToLower();
    }

    #endregion
  }
}