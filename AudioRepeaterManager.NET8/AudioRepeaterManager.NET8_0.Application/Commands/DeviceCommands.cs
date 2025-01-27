using System.Diagnostics;
using NAudio.CoreAudioApi;

namespace AudioRepeaterManager.NET8_0.Application.Commands
{
  public static class DeviceCommands
  {
    #region Logic

    /// <summary>
    /// Is the audio device started.
    /// </summary>
    /// <param name="model">The audio device</param>
    /// <returns>True/false is the audio device started.</returns>
    private static bool IsStarted(MMDevice model)
    {
      return model.State != DeviceState.Disabled;
    }

    /// <summary>
    /// Reset the audio device.
    /// </summary>
    /// <param name="model">the audio device</param>
    public static void Reset(MMDevice? model)
    {
      if (model is null)
      {
        Debug.WriteLine
          (
            "Failed to reset audio device. " +
            "Audio device is null."
          );

        return;
      }

      if (IsStarted(model))
      {
        Debug
          .WriteLine
          (
            string
            .Format
            (
              "Failed to reset audio device. " +
              "Audio device is already started\t=> Name: {0}.",
              model.FriendlyName
            )
          );

        return;
      }

      try
      {
        model.AudioClient
          .Reset();
      }
      catch
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Failed to reset audio device\t=> Name: {0}.",
            model.FriendlyName
          )
        );

        return;
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Reset audio device\t=> Name: {0}.",
          model.FriendlyName
        )
      );
    }

    /// <summary>
    /// Start the audio device.
    /// </summary>
    /// <param name="model">the audio device</param>
    public static void Start(MMDevice? model)
    {
      if (model is null)
      {
        Debug.WriteLine
          (
            "Failed to start the audio device. " +
            "The audio device is null."
          );

        return;
      }

      if (IsStarted(model))
      {
        Debug
          .WriteLine
          (
            string
            .Format
            (
              "The audio device is already started\t=> Name: {0}.",
              model.FriendlyName
            )
          );

        return;
      }

      try
      {
        model.AudioClient
          .Start();
      }
      catch
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Failed to start the audio device\t=> Name: {0}.",
            model.FriendlyName
          )
        );

        return;
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Start the audio device\t=> Name: {0}.",
          model.FriendlyName
        )
      );

      Update(model);
    }

    /// <summary>
    /// Stop the audio device.
    /// </summary>
    /// <param name="model">the audio device</param>
    public static void Stop(MMDevice? model)
    {
      if (model is null)
      {
        Debug.WriteLine
          (
            "Failed to stop the audio device. " +
            "The audio device is null."
          );

        return;
      }

      if (!IsStarted(model))
      {
        Debug
          .WriteLine
          (
            string
            .Format
            (
              "The audio device is already stopped\t=> Name: {0}.",
              model.FriendlyName
            )
          );

        return;
      }

      try
      {
        model.AudioClient
          .Stop();
      }
      catch
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Failed to stop the audio device\t=> Name: {0}.",
            model.FriendlyName
          )
        );

        return;
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Stopped the audio device\t=> Name: {0}.",
          model.FriendlyName
        )
      );

      Update(model);
    }

    /// <summary>
    /// Update the audio device.
    /// </summary>
    /// <param name="model">The audio device</param>
    public static void Update(MMDevice? model)
    {
      if (model is null)
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
        model.AudioSessionManager
          .RefreshSessions();
      }
      catch
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Failed to update the audio device\t=> Name: {0}.",
            model.FriendlyName
          )
        );

        return;
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Updated the audio device\t=> Name: {0}.",
          model.FriendlyName
        )
      );
    }

    #endregion
  }
}