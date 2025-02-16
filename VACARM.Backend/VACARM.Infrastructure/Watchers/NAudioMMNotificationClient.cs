using NAudio.CoreAudioApi;
using NAudio.CoreAudioApi.Interfaces;

/*
 * TODO:
 * - determine location.
 *  - Repository v. Service
 *    - should a Repository only deal with CRUD?
 *    - moved complex/extraneous CRUD logic outside of Repository to Service.
 *    - business logic is in Service. This includes async logic.
 *    - minimum CRUD logic is in Repository.
 *  
 *    - Q: Why Service? B/c this logic is async, and we should keep any complex
 *    logic outside of the Repository.
 *  
 *  - MMDevice Service v. DeviceModel Service
 *    - MMDevice business logic will reside in MMDeviceService.
 *    - DeviceService may reference MMDeviceService. 
 *    
 *  - UI Controllers v. Services
 *    - A Controller may call the business logic of one or more Service(s).
 *    - A Service will care for the Repository/ies it handles.
 *    - A Controller likely only needs Readonly access to the Repository, no complete CRUD.
 *  
 * - determine use case
 *   - 1. update entire enumerable(s)
 *      - if done individually (foreach item in an enumerable), this is slow.
 *      - if done wholesale (entire enumerable), this is fast?
 *   
 *   - 2. update entire repository/ies
 *      -  if done this way, the Repository should be 
 *        - kept readonly, no complete CRUD.
 *        - the enumerable changed wholesale on events.
 *            
 *  - conclusion:
 *    - 1. Place this Watcher in MMDeviceService.
 *    
 *    - 2. Create an object of MMDeviceRepository. For the enumerable, use the 
 *    collection from the property MMDeviceEnumerator.
 *    
 *    - 3. Determine what to do for changes: 
 *      - a. Update the MMDeviceRepository wholesale.
 *        - NOTE: For one Repository, this is fastest.
 *        - WARNING: For two or more Services watching, this may cause slowdown.
 *        - IDEA: 
 *          - i. Prioritize MMDeviceService for UI presentation and logic. 
 *          - ii. Forcedly have other Services/Repositories update slower.
 *          - iii. For DeviceService, do its logic when it's appropriate. 
 *          When updating, do so at file save or less periodically 
 *          (every minute instead of every second).
 *          - iv. For RepeaterService, when trying to start a repeater
 *            - 1. Validate by MMDeviceService. If device ID is valid, and 
 *            device is input or output.
 *            - 2. Start a repeater. Do not enable or disable an audio device.
 *      
 *      - b. Optionally: run business logic in MMDeviceService.
 *    
 *    - 4. Determine how to watch for property changes to the 
 *    MMDeviceService/MMDeviceRepository from other classes.
 */

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