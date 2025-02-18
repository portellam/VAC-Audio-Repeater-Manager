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

    internal Action? DefaultAction { get; private set; } = null;

    internal Action<string, DataFlow, Role>? OnDefaultDeviceChangedAction
    { get; private set; } = null;

    internal Action<string>? OnDeviceAddedAction { get; private set; } = null;

    internal Action<string>? OnDeviceRemovedAction { get; private set; } = null;

    internal Action<string, DeviceState>? OnDeviceStateChangedAction
    { get; private set; } = null;

    internal Action<string, PropertyKey>? OnPropertyValueChangedAction
    { get; private set; } = null;

    private bool HasDisposed { get; set; }

    private bool UseDefaultAction
    {
      get
      {
        return this.DefaultAction != null;
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
      this.DefaultAction = anyChanged;
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
      this.OnDefaultDeviceChangedAction = onDefaultDeviceChanged;
      this.OnDeviceAddedAction = onDeviceAdded;
      this.OnDeviceRemovedAction = onDeviceRemoved;
      this.OnPropertyValueChangedAction = onPropertyValueChanged;
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
      if (this.UseDefaultAction)
      {
        this.DefaultAction
          .Invoke();

        return;
      }

      this.OnDeviceStateChangedAction
        .Invoke
        (
          id,
          deviceState
        );
    }

    void IMMNotificationClient.OnDeviceAdded(string id)
    {
      if (this.UseDefaultAction)
      {
        this.DefaultAction
          .Invoke();

        return;
      }

      this.OnDeviceAddedAction
        .Invoke(id);
    }

    void IMMNotificationClient.OnDeviceRemoved(string id)
    {
      if (this.UseDefaultAction)
      {
        this.DefaultAction
          .Invoke();

        return;
      }

      this.OnDeviceRemovedAction
        .Invoke(id);
    }

    void IMMNotificationClient.OnDefaultDeviceChanged
    (
      DataFlow dataFlow,
      Role role,
      string id
    )
    {
      if (this.UseDefaultAction)
      {
        this.DefaultAction
          .Invoke();

        return;
      }

      this.OnDefaultDeviceChangedAction
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
      if (this.UseDefaultAction)
      {
        this.DefaultAction
          .Invoke();

        return;
      }

      this.OnPropertyValueChangedAction
        .Invoke
        (
          id,
          propertyKey
        );
    }

    #endregion
  }
}