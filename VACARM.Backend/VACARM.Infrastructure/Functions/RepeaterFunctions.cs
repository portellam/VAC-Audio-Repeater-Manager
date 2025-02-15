using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Functions
{
  internal static class RepeaterFunctions<TRepeaterModel> where TRepeaterModel :
    RepeaterModel
  {
    #region Parameters

    internal static Func<TRepeaterModel, bool> IsStarted =
      (TRepeaterModel x) => x.IsStarted;

    internal static Func<TRepeaterModel, bool> IsStopped =
      (TRepeaterModel x) => !x.IsStarted;

    #endregion

    #region Logic

    /// <summary>
    /// Match a <typeparamref name="TRepeaterModel"/> item device ID.
    /// </summary>
    /// <param name="deviceId">The device ID</param>
    /// <returns>The function</returns>
    internal static Func<TRepeaterModel, bool> ContainsDeviceId(uint deviceId)
    {
      return (TRepeaterModel x) => 
        x.InputDeviceId == deviceId
        || x.OutputDeviceId == deviceId;
    }

    /// <summary>
    /// Match a <typeparamref name="TRepeaterModel"/> item device name.
    /// </summary>
    /// <param name="deviceName">The device name</param>
    /// <returns>The function</returns>
    internal static Func<TRepeaterModel, bool> ContainsDeviceName(string deviceName)
    {
      return (TRepeaterModel x) =>
        x.InputDeviceName == deviceName
        || x.OutputDeviceName == deviceName;
    }

    /// <summary>
    /// Match a <typeparamref name="TRepeaterModel"/> item window name.
    /// </summary>
    /// <param name="windowName">The window name</param>
    /// <returns>The function</returns>
    internal static Func<TRepeaterModel, bool> ContainsWindowName(string windowName)
    {
      return (TRepeaterModel x) =>
        x.WindowName
          .ToLower()
          .Contains(windowName);
    }

    #endregion
  }
}