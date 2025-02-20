using NAudio.CoreAudioApi;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using AudioRepeaterManager.NET2_0.Backend.Extensions;

namespace AudioRepeaterManager.NET2_0.Backend.Repositories
{
  public class MMDeviceRepository :
    IMMDeviceRepository,
    INotifyPropertyChanged
  {
    #region Parameters

    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// The list of actual devices.
    /// </summary>
    private List<MMDevice> MMDeviceList;

    /// <summary>
    /// The enumerator of actual devices.
    /// </summary>
    private MMDeviceEnumerator mMDeviceEnumerator;

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    public MMDeviceRepository()
    {
      mMDeviceEnumerator = new MMDeviceEnumerator();
      Update();
    }

    /// <summary>
    /// Disable an actual device.
    /// </summary>
    /// <param name="mMDevice">the actual device</param>
    private void Disable(MMDevice mMDevice)
    {
      if (mMDevice is null)
      {
        Debug.WriteLine("Failed to get audio device. Audio device is null.");
        return;
      }

      if (mMDevice.State == DeviceState.Disabled)
      {
        Debug
          .WriteLine
          (
            string
            .Format
            (
              "Audio '{1}' device is already disabled.",
              mMDevice.FriendlyName
            )
          );

        return;
      }

      mMDevice
        .AudioClient
        .Stop();

      Debug
        .WriteLine
        (
          string
          .Format
          (
            "Audio '{1}' device disabled.",
            mMDevice.FriendlyName
          )
        );


      mMDevice
        .AudioClient
        .Reset();

      Debug
        .WriteLine
        ("Reset audio devices.");

      //TODO: add logic to refresh audio devices?
    }  

    /// <summary>
    /// Enable an actual device.
    /// </summary>
    /// <param name="mMDevice">the actual device</param>
    private void Enable(MMDevice mMDevice)
    {
      if (mMDevice is null)
      {
        Debug.WriteLine("Failed to get audio device. Audio device is null.");
        return;
      }

      if (mMDevice.State != DeviceState.Disabled)
      {
        Debug
          .WriteLine
          (
            string
            .Format
            (
              "Audio '{1}' device is already enabled.",
              mMDevice.FriendlyName
            )
          );

        return;
      }

      mMDevice
        .AudioClient
        .Start();

      Debug
        .WriteLine
        (
          string
          .Format
          (
            "Audio '{1}' device enabled.",
            mMDevice.FriendlyName
          )
        );

      //TODO: add logic to refresh audio devices?
    }

    /// <summary>
    /// Logs event when property has changed.
    /// </summary>
    /// <param name="propertyName">The property name</param>
    private void OnPropertyChanged(string propertyName)
    {
      PropertyChanged?.Invoke
      (
        this,
        new PropertyChangedEventArgs(propertyName)
      );

      Debug.WriteLine
      (
        string.Format
        (
          "PropertyChanged: '{1}'" +
          propertyName
        )
      );
    }

    /// <summary>
    /// Get an actual device.
    /// </summary>
    /// <param name="id">the actual device ID</param>
    /// <returns>The actual device.</returns>
    public MMDevice Get(string id)
    {
      if (StringExtension.IsNullOrWhiteSpace(id))
      {
        Debug.WriteLine
        (
          "Failed to get audio device. " +
          "Actual device ID is either null or whitespace."
        );

        return null;
      }

      MMDevice mMDevice = MMDeviceList
        .FirstOrDefault(x => x.ID == id);

      if (mMDevice is null)
      {
        Debug.WriteLine("Audio device is null.");
      }

      else
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Got audio device\t=> ID: '{1}'",
            mMDevice.ID
          )
        );
      }

      return mMDevice;
    }

    /// <summary>
    /// Get the list of actual devices.
    /// </summary>
    /// <returns>The list of actual devices.</returns>
    public List<MMDevice> GetAll()
    {
      if
      (
        MMDeviceList is null
        || MMDeviceList.Count == 0
      )
      {
        Debug.WriteLine
        (
          "Failed to get audio device(s). " +
          "Audio device list is null or empty."
        );

        return new List<MMDevice>();
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Got audio device(s) => Count: {1}",
          MMDeviceList.Count()
        )
      );

      return MMDeviceList;
    }

    /// <summary>
    /// Get the list of disabled actual devices.
    /// </summary>
    /// <returns>The list of disabled actual devices.</returns>
    public List<MMDevice> GetAllDisabled()
    {
      if
      (
        MMDeviceList is null
        || MMDeviceList.Count == 0
      )
      {
        Debug.WriteLine
        (
          "Failed to get disabled audio device(s). " +
          "Audio device list is null or empty."
        );

        return new List<MMDevice>();
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Got audio disabled device(s) => Count: {1}",
          MMDeviceList
            .Where
            (
              x =>
              x.State == DeviceState.Disabled
            ).Count()
        )
      );

      return MMDeviceList;
    }

    /// <summary>
    /// Get the list of enabled actual devices.
    /// </summary>
    /// <returns>The list of enabled actual devices.</returns>
    public List<MMDevice> GetAllEnabled()
    {
      if
      (
        MMDeviceList is null
        || MMDeviceList.Count == 0
      )
      {
        Debug.WriteLine
        (
          "Failed to get enabled audio device(s). " +
          "Audio device list is null or empty."
        );

        return new List<MMDevice>();
      }

      Debug.WriteLine
      (
        string.Format
        (
          "Got audio enabled device(s) => Count: {1}",
          MMDeviceList
            .Where
            (
              x =>
              x.State != DeviceState.Disabled
            ).Count()
        )
      );

      return MMDeviceList;
    }

    /// <summary>
    /// Get a list of actual devices.
    /// </summary>
    /// <param name="idList">the actual device ID list</param>
    /// <returns>A list of actual devices.</returns>
    public List<MMDevice> GetRange(List<string> idList)
    {
      if
      (
        idList is null
        || idList.Count == 0
        || MMDeviceList is null
        || MMDeviceList.Count == 0
      )
      {
        Debug.WriteLine
        (
          "Failed to get audio device(s). " +
          "Either actual ID list is null or empty, " +
          "or audio device list is null or empty."
        );

        return new List<MMDevice>();
      }

      List<MMDevice> mMDeviceList = new List<MMDevice>();

      idList
        .ForEach
        (
          id =>
          mMDeviceList
            .Add(Get(id))
        );

      Debug.WriteLine
      (
        string.Format
        (
          "Got audio device(s) => Count: {1}",
          mMDeviceList.Count()
        )
      );

      return mMDeviceList;
    }

    /// <summary>
    /// Disable an actual device.
    /// </summary>
    /// <param name="id">the actual device ID</param>
    public void Disable(string id)
    {
      MMDevice mMDevice = Get(id);
      Disable(mMDevice);
    }

    /// <summary>
    /// Enable an actual device.
    /// </summary>
    /// <param name="id">the actual device ID</param>
    public void Enable(string id)
    {
      MMDevice mMDevice = Get(id);
      Enable(mMDevice);
    }

    /// <summary>
    /// Set the actual device list.
    /// </summary>
    public void Update()
    {
      MMDeviceList = mMDeviceEnumerator
        .EnumerateAudioEndPoints
        (
          DataFlow.All,
          DeviceState.All
        )
        .Distinct()
        .OrderBy(x => x.ID)
        .ToList();

      var item = MMDeviceList.Select(x => x.ID);

      Debug
        .WriteLine("Updated audio devices.");
    }

    #endregion
  }
}