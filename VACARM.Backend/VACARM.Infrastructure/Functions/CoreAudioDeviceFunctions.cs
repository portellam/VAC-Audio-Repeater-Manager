using AudioSwitcher.AudioApi;

namespace VACARM.Infrastructure.Functions
{
  internal static class CoreAudioDeviceFunctions<TDevice> where TDevice :
    Device
  {
    #region Parameters

    private const DeviceState PresentDeviceState =
      DeviceState.Active
      | DeviceState.Disabled
      | DeviceState.Unplugged;

    internal readonly static Func<TDevice, bool> IsAbsent =
      (TDevice x) => x.State != PresentDeviceState;

    internal readonly static Func<TDevice, bool> IsCapture =
      (TDevice x) => x.IsCaptureDevice;

    internal readonly static Func<TDevice, bool> IsDefault =
      (TDevice x) => x.IsDefaultDevice;

    internal readonly static Func<TDevice, bool> IsDefaultCommunications =
      (TDevice x) => x.IsDefaultCommunicationsDevice;

    internal readonly static Func<TDevice, bool> IsDisabled =
      (TDevice x) => x.State == DeviceState.Disabled;

    internal readonly static Func<TDevice, bool> IsDuplex =
      (TDevice x) => x.IsCaptureDevice == x.IsPlaybackDevice;

    internal readonly static Func<TDevice, bool> IsEnabled =
      (TDevice x) => x.State != DeviceState.Disabled;

    internal readonly static Func<TDevice, bool> IsMuted =
      (TDevice x) => x.IsMuted;

    internal readonly static Func<TDevice, bool> IsPresent =
      (TDevice x) => x.State == PresentDeviceState;

    internal readonly static Func<TDevice, bool> IsPlayback =
      (TDevice x) => x.IsPlaybackDevice;

    internal readonly static Func<TDevice, bool> IsUnmuted =
      (TDevice x) => !x.IsMuted;

    #endregion

    #region Logic

    private static Guid ToGuid(string id)
    {
      return new Guid(id);
    }

    /// <summary>
    /// Match a <typeparamref name="TDevice"/> ID.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The function</returns>
    internal static Func<TDevice, bool> ContainsId(string id)
    {
      return (TDevice item) => item.Id == ToGuid(id);
    }

    /// <summary>
    /// Match an enumerable of <typeparamref name="TDevice"/> ID(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    /// <returns>The function</returns>
    internal static Func<TDevice, bool> ContainsIdEnumerable
    (IEnumerable<string> idEnumerable)
    {
      return (TDevice item) =>
      {
        string id = item.Id.ToString();
        return idEnumerable.Contains(id);
      };
    }

    #endregion
  }
}