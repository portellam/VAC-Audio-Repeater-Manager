using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Functions
{
  internal class DeviceModelFunctions<TDeviceModel> :
    BaseFunctions<TDeviceModel> where TDeviceModel :
    DeviceModel
  {
    #region Parameters

    internal readonly static Func<TDeviceModel, bool> IsCapture =
      (TDeviceModel x) => x.IsCapture;

    internal readonly static Func<TDeviceModel, bool> IsDefault =
      (TDeviceModel x) => x.IsDefault;

    internal readonly static Func<TDeviceModel, bool> IsDuplex =
      (TDeviceModel x) => x.IsDuplex;

    internal readonly static Func<TDeviceModel, bool> IsEnabled =
      (TDeviceModel x) => x.IsEnabled;

    internal readonly static Func<TDeviceModel, bool> IsMuted =
      (TDeviceModel x) => x.IsMuted;

    internal readonly static Func<TDeviceModel, bool> IsPresent =
      (TDeviceModel x) => x.IsPresent;

    internal readonly static Func<TDeviceModel, bool> IsRender =
      (TDeviceModel x) => x.IsRender;

    #endregion

    #region Logic


    /// <summary>
    /// Match a <typeparamref name="TDeviceModel"/> item actual ID.
    /// </summary>
    /// <param name="actualId">The actual ID</param>
    /// <returns>The function</returns>
    internal static Func<TDeviceModel, bool> ContainsActualId(string actualId)
    {
      return (TDeviceModel x) => x.ActualId == actualId;
    }

    /// <summary>
    /// Match a <typeparamref name="TDeviceModel"/> item name.
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
    /// Match a <typeparamref name="TDeviceModel"/> item role.
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