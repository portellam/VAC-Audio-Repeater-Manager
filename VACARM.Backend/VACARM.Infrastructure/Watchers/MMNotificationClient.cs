using NAudio.CoreAudioApi;
using NAudio.CoreAudioApi.Interfaces;

namespace VACARM.Infrastructure.Watchers
{
  /// <summary>
  /// A watcher for the system audio devices.
  /// </summary>
  internal class MMNotificationClient :
    IDisposable,
    IMMNotificationClient
  {
    #region Parameters

    /// <summary>
    /// The default action.
    /// </summary>
    internal Action? AnyChanged { get; private set; } = null;

    internal Action<string, DataFlow, Role>? OnDefaultDeviceChanged
    { get; private set; } = null;

    internal Action<string>? OnDeviceAdded { get; private set; } = null;

    internal Action<string>? OnDeviceRemoved { get; private set; } = null;

    internal Action<string, DeviceState>? OnDeviceStateChanged
    { get; private set; } = null;

    internal Action<string, PropertyKey>? OnPropertyValueChanged
    { get; private set; } = null;

    private bool HasDisposed;

    /// <summary>
    /// True/false use the default action.
    /// </summary>
    private bool UseDefaultBehavior
    {
      get
      {
        return this.AnyChanged != null;
      }
    }

    /// <summary>
    /// The collection of <typeparamref name="MMDevice"/>.
    /// </summary>
    internal MMDeviceCollection MMDeviceCollection
    {
      get
      {
        return this.MMDeviceEnumerator
          .EnumerateAudioEndPoints
          (
            DataFlow.All,
            DeviceState.All
          );
      }
    }

    private MMDeviceEnumerator MMDeviceEnumerator { get; set; }

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="anyChanged">The action</param>
    internal MMNotificationClient(Action anyChanged)
    {
      this.MMDeviceEnumerator = new MMDeviceEnumerator();
      this.Register();
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
    internal MMNotificationClient
    (
      Action<string, DataFlow, Role> onDefaultDeviceChanged,
      Action<string> onDeviceAdded,
      Action<string> onDeviceRemoved,
      Action<string, DeviceState> onDeviceStateChanged,
      Action<string, PropertyKey> onPropertyValueChanged
    )
    {
      this.MMDeviceEnumerator = new MMDeviceEnumerator();
      this.Register();
      this.OnDefaultDeviceChanged = onDefaultDeviceChanged;
      this.OnDeviceAdded = onDeviceAdded;
      this.OnDeviceRemoved = onDeviceRemoved;
      this.OnPropertyValueChanged = onPropertyValueChanged;
    }

    /// <summary>
    /// Get an enumerable of default <typeparamref name="MMDevice"/>(s).
    /// </summary>
    /// <param name="role">The role</param>
    /// <returns>The enumerable of item(s).</returns>
    internal IEnumerable<MMDevice> GetDefaultRange(Role role)
    {
      Array DataFlowEnumArray = Enum.GetValues(typeof(DataFlow));

      foreach (DataFlow dataFlow in DataFlowEnumArray)
      {
        var item = this.MMDeviceEnumerator
          .GetDefaultAudioEndpoint
          (
            dataFlow,
            role
          );

        yield return item;
      }
    }

    /// <summary>
    /// Register subscription to event watcher.
    /// </summary>
    internal void Register()
    {
      this.MMDeviceEnumerator
        .RegisterEndpointNotificationCallback(this);
    }

    /// <summary>
    /// Unregister subscription from event watcher.
    /// </summary>
    internal void UnRegister()
    {
      this.MMDeviceEnumerator
        .UnregisterEndpointNotificationCallback(this);
    }

    /// <summary>
    /// Dispose of unmanaged objects and true/false dispose of managed objects.
    /// </summary>
    /// <param name="isDisposed">True/false</param>
    protected virtual void Dispose(bool isDisposed)
    {
      if (this.HasDisposed)
      {
        return;
      }

      if (isDisposed)
      {
        this.MMDeviceEnumerator
          .Dispose();

        this.MMDeviceEnumerator = null;
      }

      this.HasDisposed = true;
    }

    /// <summary>
    /// Do not change this code. 
    /// Put cleanup code in Dispose(<paramref name="bool"/>
    ///  <typeparamref name="isDisposed"/>) method.
    /// </summary>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize(this);
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