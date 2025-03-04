using AudioSwitcher.AudioApi;
using NAudio.CoreAudioApi;
using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Functions
{
  public static class DeviceFunctions<TDeviceModel> 
    where TDeviceModel :
    DeviceModel
  {
    #region Parameters

    public readonly static Func<TDeviceModel, bool> IsAbsent =
      (TDeviceModel x) => !x.IsPresent;

    public readonly static Func<TDeviceModel, bool> IsCapture =
      (TDeviceModel x) => x.IsCapture;

    public readonly static Func<TDeviceModel, bool> IsCommunications =
      (TDeviceModel x) => x.Role == "Communications";

    public readonly static Func<TDeviceModel, bool> IsConsole =
      (TDeviceModel x) => x.Role == "Console";

    public readonly static Func<TDeviceModel, bool> IsDefault =
      (TDeviceModel x) => x.IsDefault;

    public readonly static Func<TDeviceModel, bool> IsDisabled =
      (TDeviceModel x) => !x.IsEnabled;

    public readonly static Func<TDeviceModel, bool> IsDuplex =
      (TDeviceModel x) => x.IsDuplex;

    public readonly static Func<TDeviceModel, bool> IsEnabled =
      (TDeviceModel x) => x.IsEnabled;

    public readonly static Func<TDeviceModel, bool> IsMultimedia =
      (TDeviceModel x) => x.Role == "Multimedia";

    public readonly static Func<TDeviceModel, bool> IsMuted =
      (TDeviceModel x) => x.IsMuted;

    public readonly static Func<TDeviceModel, bool> IsPresent =
      (TDeviceModel x) => x.IsPresent;

    public readonly static Func<TDeviceModel, bool> IsRender =
      (TDeviceModel x) => x.IsRender;

    public readonly static Func<TDeviceModel, bool> IsUnmuted =
      (TDeviceModel x) => !x.IsMuted;

    #endregion

    #region Logic

    /// <summary>
    /// Get a <typeparamref name="DeviceModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <param name="role">The role</param>
    /// <returns>The device model.</returns>
    public static TDeviceModel GetDeviceModel
    (
      uint id,
      string actualId,
      bool isCapture,
      string? name,
      string? role
    )
    {
      DeviceModel model = new DeviceModel
        (
          id,
          actualId,
          name,
          isCapture
        )
      {
        IsCapture = isCapture
      };

      return (TDeviceModel)model;
    }

    /// <summary>
    /// Get a <typeparamref name="DeviceModel"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <param name="mMDevice">The device</param>
    /// <param name="device">The device</param>
    /// <param name="role">The role</param>
    /// <returns>The device model.</returns>
    public static TDeviceModel GetDeviceModel
    (
      uint id,
      MMDevice mMDevice,
      Device? device,
      string? role
    )
    {
      DeviceModel model = new DeviceModel
        (
          id,
          mMDevice.ID,
          mMDevice.FriendlyName,
          MMDeviceFunctions<MMDevice>.IsCapture(mMDevice)
        )
      {
        IsDefault = CoreAudioDeviceFunctions<Device>.IsDefault(device),
        IsEnabled = MMDeviceFunctions<MMDevice>.IsEnabled(mMDevice),
        IsMuted = CoreAudioDeviceFunctions<Device>.IsMuted(device),
        IsPresent = MMDeviceFunctions<MMDevice>.IsPresent(mMDevice),
        Role = role,
      };

      return (TDeviceModel)model;
    }

    /// <summary>
    /// Match a <typeparamref name="TDeviceModel"/> actual ID.
    /// </summary>
    /// <param name="actualId">The actual ID</param>
    /// <returns>The function</returns>
    public static Func<TDeviceModel, bool> ContainsActualId(string actualId)
    {
      return (TDeviceModel x) => x.ActualId == actualId;
    }

    /// <summary>
    /// Match a <typeparamref name="TDeviceModel"/> name.
    /// </summary>
    /// <param name="name">The name</param>
    /// <returns>The function</returns>
    public static Func<TDeviceModel, bool> ContainsName(string name)
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
    public static Func<TDeviceModel, bool> ContainsRole(string role)
    {
      return (TDeviceModel x) => x.Role
        .ToLower() == role.ToLower();
    }

    #endregion
  }
}