using NAudio.CoreAudioApi;
using NAudio.CoreAudioApi.Interfaces;

namespace VACARM.Infrastructure.Watchers
{
  /// <summary>
  /// A watcher for the system audio devices.
  /// </summary>
  internal class NAudioMMNotificationClient :
    IMMNotificationClient
  {
    #region Parameters

    internal Action? AnyChanged { get; private set; } = null;

    internal Action<string, DataFlow, Role>? OnDefaultDeviceChanged
    { get; private set; } = null;

    internal Action<string>? OnDeviceAdded { get; private set; } = null;
    internal Action<string>? OnDeviceRemoved { get; private set; } = null;

    internal Action<string, DeviceState>? OnDeviceStateChanged
    { get; private set; } = null;

    internal Action<string, PropertyKey>? OnPropertyValueChanged
    { get; private set; } = null;

    private bool UseDefaultBehavior
    {
      get
      {
        return this.AnyChanged != null;
      }
    }

    internal MMDeviceEnumerator MMDeviceEnumerator { get; private set; }

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="anyChanged">The action</param>
    internal NAudioMMNotificationClient(Action anyChanged)
    {
      this.MMDeviceEnumerator = new MMDeviceEnumerator();
      this.MMDeviceEnumerator.RegisterEndpointNotificationCallback(this);
      this.AnyChanged = anyChanged;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="onDefaultDeviceChanged">The action</param>
    /// <param name="onDeviceAdded">The action</param>
    /// <param name="onDeviceRemoved">The action</param>
    /// <param name="onDeviceStateChanged">The action</param>
    /// <param name="onPropertyValueChanged">The action</param>
    internal NAudioMMNotificationClient
    (
      Action<string, DataFlow, Role> onDefaultDeviceChanged,
      Action<string> onDeviceAdded,
      Action<string> onDeviceRemoved,
      Action<string, DeviceState> onDeviceStateChanged,
      Action<string, PropertyKey> onPropertyValueChanged
    )
    {
      this.MMDeviceEnumerator = new MMDeviceEnumerator();
      this.MMDeviceEnumerator.RegisterEndpointNotificationCallback(this);
      this.OnDefaultDeviceChanged = onDefaultDeviceChanged;
      this.OnDeviceAdded = onDeviceAdded;
      this.OnDeviceRemoved = onDeviceRemoved;
      this.OnPropertyValueChanged = onPropertyValueChanged;
    }

    void IMMNotificationClient.OnDeviceStateChanged
    (
      string id,
      DeviceState deviceState
    )
    {
      if (this.UseDefaultBehavior)
      {
        this.AnyChanged
          .Invoke();

        return;
      }

      this.OnDeviceStateChanged
        .Invoke
        (
          id,
          deviceState
        );
    }

    void IMMNotificationClient.OnDeviceAdded(string id)
    {
      if (this.UseDefaultBehavior)
      {
        this.AnyChanged
          .Invoke();

        return;
      }

      this.OnDeviceAdded
        .Invoke(id);
    }

    void IMMNotificationClient.OnDeviceRemoved(string id)
    {
      if (this.UseDefaultBehavior)
      {
        this.AnyChanged
          .Invoke();

        return;
      }

      this.OnDeviceRemoved
        .Invoke(id);
    }

    void IMMNotificationClient.OnDefaultDeviceChanged
    (
      DataFlow dataFlow,
      Role role,
      string id
    )
    {
      if (this.UseDefaultBehavior)
      {
        this.AnyChanged
          .Invoke();

        return;
      }

      this.OnDefaultDeviceChanged
        .Invoke
        (
          id,
          dataFlow,
          role
        );
    }

    void IMMNotificationClient.OnPropertyValueChanged
    (
      string id,
      PropertyKey propertyKey
    )
    {
      if (this.UseDefaultBehavior)
      {
        this.AnyChanged
          .Invoke();

        return;
      }

      this.OnPropertyValueChanged
        .Invoke
        (
          id,
          propertyKey
        );
    }

    #endregion
  }
}