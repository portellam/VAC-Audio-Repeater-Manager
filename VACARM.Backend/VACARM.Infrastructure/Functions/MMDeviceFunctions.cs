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

    public readonly static Func<TMMDevice, bool> IsAbsentFunc =
      (TMMDevice x) => x.State != PresentDeviceState;

    public readonly static Func<TMMDevice, bool> IsCaptureFunc =
      (TMMDevice x) => x.DataFlow == DataFlow.Capture;

    public readonly static Func<TMMDevice, bool> IsDisabledFunc =
      (TMMDevice x) => x.State == DeviceState.Disabled;

    public readonly static Func<TMMDevice, bool> IsDuplexFunc =
      (TMMDevice x) => x.DataFlow == DataFlow.All;

    public readonly static Func<TMMDevice, bool> IsEnabledFunc =
      (TMMDevice x) => x.State != DeviceState.Disabled;

    public readonly static Func<TMMDevice, bool> IsPresentFunc =
      (TMMDevice x) => x.State == PresentDeviceState;

    public readonly static Func<TMMDevice, bool> IsRenderFunc =
      (TMMDevice x) => x.DataFlow == DataFlow.Render;

    #endregion

    #region Logic

    public static Func<TMMDevice, bool> ContainsIdFunc(string id)
    {
      return (TMMDevice x) => x.ID == id;
    }

    public static Func<TMMDevice, bool> ContainsIdEnumerableFunc
    (IEnumerable<string> idEnumerable)
    {
      return (TMMDevice x) => idEnumerable.Contains(x.ID);
    }

    #endregion
  }
}