using NAudio.CoreAudioApi;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace VACARM.NET4.Models
{
    public class DeviceList
    {

        /*
         * TODO: evaluate the purpose of available, disabled, and selected lists.
         * are they separate? 
         * or does selected include available? 
         * or should I rename them?
         * 
         */


        #region Parameters

        private MMDeviceEnumerator mMDeviceEnumerator;
        private DeviceState available = DeviceState.Active | DeviceState.Unplugged;

        public List<MMDevice> AvailableWaveInMMDeviceList { get; private set; }
        public List<MMDevice> AvailableWaveOutMMDeviceList { get; private set; }
        public List<MMDevice> DisabledWaveInMMDeviceList { get; private set; }
        public List<MMDevice> DisabledWaveOutMMDeviceList { get; private set; }
        public List<MMDevice> SelectedWaveInMMDeviceList { get; private set; }
        public List<MMDevice> SelectedWaveOutMMDeviceList { get; private set; }
        public List<string> AvailableWaveInNameList { get; private set; }
        public List<string> AvailableWaveOutNameList { get; private set; }
        public List<string> DisabledWaveInNameList { get; private set; }
        public List<string> DisabledWaveOutNameList { get; private set; }
        public List<string> SelectedWaveInNameList { get; private set; }
        public List<string> SelectedWaveOutNameList { get; private set; }

        #endregion

        #region Logic

        /// <summary>
        /// Constructor
        /// </summary>
        [ExcludeFromCodeCoverage]
        public DeviceList()
        {
            GetDeviceLists();
            SelectedWaveInMMDeviceList = new List<MMDevice>();
            SelectedWaveInNameList = new List<string>();
            SelectedWaveOutMMDeviceList = new List<MMDevice>();
            SelectedWaveOutNameList = new List<string>();
        }

        /// <summary>
        /// Get device lists.
        /// </summary>
        internal void GetDeviceLists()
        {
            mMDeviceEnumerator = new MMDeviceEnumerator();

            AvailableWaveInMMDeviceList = mMDeviceEnumerator.EnumerateAudioEndPoints
                (DataFlow.Capture, available).ToList();

            AvailableWaveInNameList = AvailableWaveInMMDeviceList.Select
                (x => x.FriendlyName).ToList();

            AvailableWaveOutMMDeviceList = mMDeviceEnumerator.EnumerateAudioEndPoints
                (DataFlow.Render, available).ToList();

            AvailableWaveOutNameList = AvailableWaveOutMMDeviceList.Select
                (x => x.FriendlyName).ToList();

            DisabledWaveInMMDeviceList = mMDeviceEnumerator.EnumerateAudioEndPoints
                (DataFlow.Capture, DeviceState.Disabled).ToList();

            DisabledWaveInNameList = AvailableWaveInMMDeviceList.Select
                (x => x.FriendlyName).ToList();

            DisabledWaveOutMMDeviceList = mMDeviceEnumerator.EnumerateAudioEndPoints
                (DataFlow.Render, DeviceState.Disabled).ToList();

            DisabledWaveOutNameList = AvailableWaveOutMMDeviceList.Select
                (x => x.FriendlyName).ToList();
        }

        /// <summary>
        /// If device state changes, move between lists.
        /// If device is null or device state is not present, remove from list.
        /// </summary>
        internal void UpdateWaveInListsGivenNewDeviceState()
        {
            AvailableWaveInMMDeviceList.ForEach(mMDevice =>
            {
                MMDevice newMMDevice = mMDeviceEnumerator.GetDevice(mMDevice.ID);

                if (newMMDevice.State == DeviceState.Disabled)
                {
                    DisabledWaveInMMDeviceList.Remove(mMDevice);
                }
                else if
                    (newMMDevice is null || newMMDevice.State == DeviceState.NotPresent)
                {
                    AvailableWaveInMMDeviceList.Remove(mMDevice);
                }
                else
                {
                    mMDevice = newMMDevice;
                }
            });

            DisabledWaveInMMDeviceList.ForEach(mMDevice =>
            {
                MMDevice newMMDevice = mMDeviceEnumerator.GetDevice(mMDevice.ID);

                if (newMMDevice.State == available)
                {
                    AvailableWaveInMMDeviceList.Remove(mMDevice);
                }
                else if
                    (newMMDevice is null || newMMDevice.State == DeviceState.NotPresent)
                {
                    DisabledWaveInMMDeviceList.Remove(mMDevice);
                }
                else
                {
                    mMDevice = newMMDevice;
                }
            });
        }

        /// <summary>
        /// If device state changes, move between lists.
        /// If device is null or device state is not present, remove from list.
        /// </summary>
        internal void UpdateWaveOutListsGivenNewDeviceState()
        {
            AvailableWaveOutMMDeviceList.ForEach(mMDevice =>
            {
                MMDevice newMMDevice = mMDeviceEnumerator.GetDevice(mMDevice.ID);

                if (newMMDevice.State == DeviceState.Disabled)
                {
                    DisabledWaveOutMMDeviceList.Remove(mMDevice);
                }
                else if
                    (newMMDevice is null || newMMDevice.State == DeviceState.NotPresent)
                {
                    AvailableWaveOutMMDeviceList.Remove(mMDevice);
                }
                else
                {
                    mMDevice = newMMDevice;
                }
            });

            DisabledWaveOutMMDeviceList.ForEach(mMDevice =>
            {
                MMDevice newMMDevice = mMDeviceEnumerator.GetDevice(mMDevice.ID);

                if (newMMDevice.State == available)
                {
                    AvailableWaveOutMMDeviceList.Remove(mMDevice);
                }
                else if
                    (newMMDevice is null || newMMDevice.State == DeviceState.NotPresent)
                {
                    DisabledWaveOutMMDeviceList.Remove(mMDevice);
                }
                else
                {
                    mMDevice = newMMDevice;
                }
            });
        }

        /// <summary>
        /// If device is null, remove from list.
        /// If device has changed, update entry (and preserve device in list;
        /// repeater execution logic will ignore Disabled and Not Present repeater 
        /// devices).
        /// </summary>
        internal void UpdateSelectedListsGivenNewDeviceState()
        {
            SelectedWaveInMMDeviceList.ForEach(mMDevice =>
            {
                MMDevice newMMDevice = mMDeviceEnumerator.GetDevice(mMDevice.ID);

                if (newMMDevice is null)
                {
                    SelectedWaveOutMMDeviceList.Remove(mMDevice);
                }
                else if (newMMDevice != mMDevice)
                {
                    mMDevice = newMMDevice;
                }
            });

            SelectedWaveOutMMDeviceList.ForEach(mMDevice =>
            {
                MMDevice newMMDevice = mMDeviceEnumerator.GetDevice(mMDevice.ID);

                if (newMMDevice is null)
                {
                    SelectedWaveOutMMDeviceList.Remove(mMDevice);
                }
                else if (newMMDevice != mMDevice)
                {
                    mMDevice = newMMDevice;
                }
            });
        }

        /// <summary>
        /// Get the device state of a device given its name.
        /// </summary>
        /// <param name="deviceName">The device name</param>
        /// <returns>The device state</returns>
        public DeviceState GetDeviceState(string deviceName)
        {
            if (AvailableWaveInNameList.Contains(deviceName))
            {
                return AvailableWaveInMMDeviceList
                    [AvailableWaveInNameList.IndexOf(deviceName)].State;
            }
            else if (AvailableWaveOutNameList.Contains(deviceName))
            {
                return AvailableWaveOutMMDeviceList
                    [AvailableWaveOutNameList.IndexOf(deviceName)].State;
            }
            else if (DisabledWaveInNameList.Contains(deviceName))
            {
                return DisabledWaveInMMDeviceList
                    [DisabledWaveInNameList.IndexOf(deviceName)].State;
            }
            else if (DisabledWaveOutNameList.Contains(deviceName))
            {
                return DisabledWaveOutMMDeviceList
                    [DisabledWaveOutNameList.IndexOf(deviceName)].State;
            }

            return DeviceState.NotPresent;
        }

        /// <summary>
        /// Move device from selected Wave In or Wave Out lists, to
        /// available or disabled lists.
        /// </summary>
        /// <param name="deviceName">The device name</param>
        public void MoveDeviceFromSelectedList(string deviceName)
        {
            MMDevice mMDevice = null;

            if (SelectedWaveInNameList.Contains(deviceName))
            {
                mMDevice = SelectedWaveInMMDeviceList
                    [SelectedWaveInNameList.IndexOf(deviceName)];
                SelectedWaveInMMDeviceList.Remove(mMDevice);
                SelectedWaveInNameList.Remove(deviceName);
            }
            else if (SelectedWaveOutNameList.Contains(deviceName))
            {
                mMDevice = SelectedWaveOutMMDeviceList
                    [SelectedWaveOutNameList.IndexOf(deviceName)];
                SelectedWaveOutMMDeviceList.Remove(mMDevice);
                SelectedWaveOutNameList.Remove(deviceName);
            }
            else
            {
                return;
            }

            if (mMDevice.State == available)
            {
                if (mMDevice.DataFlow == DataFlow.Capture)
                {
                    AvailableWaveInMMDeviceList.Add(mMDevice);
                    AvailableWaveInNameList.Add(deviceName);
                }
                else
                {
                    AvailableWaveOutMMDeviceList.Add(mMDevice);
                    AvailableWaveOutNameList.Add(deviceName);
                }
            }
            else if (mMDevice.State == DeviceState.Disabled)
            {
                if (mMDevice.DataFlow == DataFlow.Capture)
                {
                    DisabledWaveInMMDeviceList.Add(mMDevice);
                    DisabledWaveInNameList.Add(deviceName);
                }
                else
                {
                    DisabledWaveOutMMDeviceList.Add(mMDevice);
                    DisabledWaveOutNameList.Add(deviceName);
                }
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// Move device from available or disabled, Wave In or Wave Out lists, to
        /// selected list.
        /// </summary>
        /// <param name="deviceName">The device name</param>
        public void MoveDeviceToSelectedList(string deviceName)
        {
            MMDevice mMDevice = null;

            if (AvailableWaveInNameList.Contains(deviceName))
            {
                mMDevice = AvailableWaveInMMDeviceList
                    [AvailableWaveInNameList.IndexOf(deviceName)];
                AvailableWaveInMMDeviceList.Remove(mMDevice);
                AvailableWaveInNameList.Remove(deviceName);
            }
            else if (AvailableWaveOutNameList.Contains(deviceName))
            {
                mMDevice = AvailableWaveOutMMDeviceList
                    [AvailableWaveOutNameList.IndexOf(deviceName)];
                AvailableWaveOutMMDeviceList.Remove(mMDevice);
                AvailableWaveOutNameList.Remove(deviceName);
            }
            else if (DisabledWaveInNameList.Contains(deviceName))
            {
                mMDevice = DisabledWaveInMMDeviceList
                    [DisabledWaveInNameList.IndexOf(deviceName)];
                DisabledWaveInMMDeviceList.Remove(mMDevice);
                DisabledWaveInNameList.Remove(deviceName);
            }
            else if (DisabledWaveOutNameList.Contains(deviceName))
            {
                mMDevice = DisabledWaveOutMMDeviceList
                    [DisabledWaveOutNameList.IndexOf(deviceName)];
                DisabledWaveOutMMDeviceList.Remove(mMDevice);
                DisabledWaveOutNameList.Remove(deviceName);
            }
            else
            {
                return;
            }

            if (mMDevice.DataFlow == DataFlow.Capture)
            {
                SelectedWaveInMMDeviceList.Add(mMDevice);
                SelectedWaveInNameList.Add(deviceName);
            }
            else
            {
                SelectedWaveOutMMDeviceList.Add(mMDevice);
                SelectedWaveOutNameList.Add(deviceName);
            }
        }

        /// <summary>
        /// Query system for changes, and update lists as needed.
        /// </summary>
        public void SetDeviceLists()
        {
            GetDeviceLists();
            //UpdateWaveInListsGivenNewDeviceState();                                   //TODO: currently redundant. May need to rethink use or delete.
            //UpdateWaveOutListsGivenNewDeviceState();                                  //TODO: currently redundant. May need to rethink use or delete.
            UpdateSelectedListsGivenNewDeviceState();
        }

        #endregion
    }
}