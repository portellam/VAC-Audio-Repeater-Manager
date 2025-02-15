using NAudio.CoreAudioApi;

namespace VACARM.Infrastructure.Functions
{
  internal static class MMDeviceFunctions<TMMDevice> where TMMDevice : 
    MMDevice
  {
    #region Parameters

    private const DeviceState PresentDeviceState =
      DeviceState.Active
      | DeviceState.Disabled
      | DeviceState.Unplugged;

    public readonly static Func<TMMDevice, bool> IsAbsent =
      (TMMDevice x) => x.State != PresentDeviceState;

    public readonly static Func<TMMDevice, bool> IsCapture =
      (TMMDevice x) => x.DataFlow == DataFlow.Capture;

    public readonly static Func<TMMDevice, bool> IsDisabled =
      (TMMDevice x) => x.State == DeviceState.Disabled;

    public readonly static Func<TMMDevice, bool> IsDuplex =
      (TMMDevice x) => x.DataFlow == DataFlow.All;

    public readonly static Func<TMMDevice, bool> IsEnabled =
      (TMMDevice x) => x.State != DeviceState.Disabled;

    public readonly static Func<TMMDevice, bool> IsPresent =
      (TMMDevice x) => x.State == PresentDeviceState;

    public readonly static Func<TMMDevice, bool> IsRender =
      (TMMDevice x) => x.DataFlow == DataFlow.Render;

    #endregion

    #region Logic

    /// <summary>
    /// Match a <typeparamref name="TMMDevice"/> item ID.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The function</returns>
    public static Func<TMMDevice, bool> ContainsId(string id)
    {
      return (TMMDevice x) => x.ID == id;
    }

    /// <summary>
    /// Match an enumerable of <typeparamref name="TMMDevice"/> item ID(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    /// <returns>The function</returns>
    public static Func<TMMDevice, bool> ContainsIdEnumerable
    (IEnumerable<string> idEnumerable)
    {
      return (TMMDevice x) => idEnumerable.Contains(x.ID);
    }

    #endregion
  }
}