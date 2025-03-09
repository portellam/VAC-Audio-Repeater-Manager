using AudioSwitcher.AudioApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace VACARM.Infrastructure.Functions
{
  internal static class CoreAudioDeviceFunctions<TDevice>
    where TDevice :
    Device
  {
    #region Parameters

    private const DeviceState PresentDeviceState =
      DeviceState.Active
      | DeviceState.Disabled
      | DeviceState.Unplugged;

    internal readonly static Func<TDevice, bool> IsAbsent =
      (TDevice x) =>
      {
        if (x == null)
        {
          return false;
        }

        return x.State != PresentDeviceState;
      };

    internal readonly static Func<TDevice, bool> IsCapture =
      (TDevice x) =>
      {
        if (x == null)
        {
          return false;
        }

        return x.IsCaptureDevice;
      };

    internal readonly static Func<TDevice, bool> IsDefault =
      (TDevice x) =>
      {
        if (x == null)
        {
          return false;
        }

        return x.IsDefaultDevice;
      };

    internal readonly static Func<TDevice, bool> IsDefaultCommunications =
      (TDevice x) =>
      {
        if (x == null)
        {
          return false;
        }

        return x.IsDefaultCommunicationsDevice;
      };

    internal readonly static Func<TDevice, bool> IsDisabled =
      (TDevice x) =>
      {
        if (x == null)
        {
          return false;
        }

        return x.State == DeviceState.Disabled;
      };

    internal readonly static Func<TDevice, bool> IsDuplex =
      (TDevice x) =>
      {
        if (x == null)
        {
          return false;
        }

        return x.IsCaptureDevice == x.IsPlaybackDevice;
      };

    internal readonly static Func<TDevice, bool> IsEnabled =
      (TDevice x) =>
      {
        if (x == null)
        {
          return false;
        }

        return x.State != DeviceState.Disabled;
      };

    internal readonly static Func<TDevice, bool> IsMuted =
      (TDevice x) =>
      {
        if (x == null)
        {
          return false;
        }

        return x.IsMuted;
      };

    internal readonly static Func<TDevice, bool> IsPresent =
      (TDevice x) =>
      {
        if (x == null)
        {
          return false;
        }

        return x.State == PresentDeviceState;
      };

    internal readonly static Func<TDevice, bool> IsPlayback =
      (TDevice x) =>
      {
        if (x == null)
        {
          return false;
        }

        return x.IsPlaybackDevice;
      };

    internal readonly static Func<TDevice, bool> IsUnmuted =
      (TDevice x) =>
      {
        if (x == null)
        {
          return false;
        }

        return !x.IsMuted;
      };

    #endregion

    #region Logic

    /// <summary>
    /// Prepares string <typeparamref name="guid"/> by removing prefix.
    /// The prefix determines the <typeparamref name="DataFlow"/> of the device.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The ID</returns>
    private static string PrepareStringGuid(string id)
    {
      string pattern = @"^\{\d+\.\d+\.\d+\.\d{8}\}\.";

      var isMatch = Regex.IsMatch
        (
          id,
          pattern
        );


      if (isMatch)
      {
        id = Regex.Replace
          (
            id, 
            pattern, 
            string.Empty
          );
      }

      return id;
    }

    private static Guid ToGuid(string id)
    {
      id = PrepareStringGuid(id);
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
        string id = item.Id
          .ToString();

        return idEnumerable.Contains(id);
      };
    }

    #endregion
  }
}