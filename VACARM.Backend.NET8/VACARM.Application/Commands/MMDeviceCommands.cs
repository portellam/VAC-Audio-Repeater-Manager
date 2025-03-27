using System.Diagnostics;
using NAudio.CoreAudioApi;

namespace VACARM.Application.Commands
{
  /// <summary>
  /// Start, stop, and reset a <typeparamref name="MMDevice"/>
  /// </summary>
  public static class MMDeviceCommands
  {
    #region Logic

    /// <summary>
    /// Is the audio device started.
    /// </summary>
    /// <param name="item">The audio device</param>
    /// <returns>True/false is the audio device started.</returns>
    private static bool IsStarted(MMDevice item)
    {
      return item.State != DeviceState.Disabled;
    }

    /// <summary>
    /// Reset the audio device.
    /// </summary>
    /// <param name="item">the audio device</param>
    public static void Reset(MMDevice? item)
    {
      if (item == null)
      {
        Debug.WriteLine
          (
            "Failed to reset audio device. " +
            "Audio device is null."
          );

        return;
      }

      if (IsStarted(item))
      {
        Debug
          .WriteLine
          (
            string
            .Format
            (
              "Failed to reset audio device. " +
              "Audio device is already started\t=> Name: {0}.",
              item.FriendlyName
            )
          );

        return;
      }

      try
      {
        item.AudioClient
          .Reset();
      }

      catch
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Failed to reset audio device\t=> Name: {0}.",
            item.FriendlyName
          )
        );

        return;
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Reset audio device\t=> Name: {0}.",
          item.FriendlyName
        )
      );
    }

    /// <summary>
    /// Start the audio device.
    /// </summary>
    /// <param name="item">the audio device</param>
    public static void Start(MMDevice? item)
    {
      if (item == null)
      {
        Debug.WriteLine
          (
            "Failed to start the audio device. " +
            "The audio device is null."
          );

        return;
      }

      if (IsStarted(item))
      {
        Debug
          .WriteLine
          (
            string
            .Format
            (
              "The audio device is already started\t=> Name: {0}.",
              item.FriendlyName
            )
          );

        return;
      }

      try
      {
        item.AudioClient
          .Start();
      }

      catch
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Failed to start the audio device\t=> Name: {0}.",
            item.FriendlyName
          )
        );

        return;
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Start the audio device\t=> Name: {0}.",
          item.FriendlyName
        )
      );

      Update(item);
    }

    /// <summary>
    /// Stop the audio device.
    /// </summary>
    /// <param name="item">the audio device</param>
    public static void Stop(MMDevice? item)
    {
      if (item == null)
      {
        Debug.WriteLine
          (
            "Failed to stop the audio device. " +
            "The audio device is null."
          );

        return;
      }

      if (!IsStarted(item))
      {
        Debug
          .WriteLine
          (
            string
            .Format
            (
              "The audio device is already stopped\t=> Name: {0}.",
              item.FriendlyName
            )
          );

        return;
      }

      try
      {
        item.AudioClient
          .Stop();
      }

      catch
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Failed to stop the audio device\t=> Name: {0}.",
            item.FriendlyName
          )
        );

        return;
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Stopped the audio device\t=> Name: {0}.",
          item.FriendlyName
        )
      );

      Update(item);
    }

    /// <summary>
    /// Update the audio device.
    /// </summary>
    /// <param name="item">The audio device</param>
    public static void Update(MMDevice? item)
    {
      if (item == null)
      {
        Debug.WriteLine
          (
            "Failed to update the audio device. " +
            "The audio device is null."
          );

        return;
      }

      try
      {
        item.AudioSessionManager
          .RefreshSessions();
      }

      catch
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Failed to update the audio device\t=> Name: {0}.",
            item.FriendlyName
          )
        );

        return;
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Updated the audio device\t=> Name: {0}.",
          item.FriendlyName
        )
      );
    }

    #endregion
  }
}