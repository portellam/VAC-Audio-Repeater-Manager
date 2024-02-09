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
         * TODO: have available-unselected and disabled(unselected) be starter lists.
         * have enable... and enable all target disabled(unselected) and any 
         * disabled-selected devices.
         * and vicea vera for disable.. and disable all.
         * 
         * TODO: have repeater execution logic attempt to enable disabled selected
         * devices. warn those unplugged, and ignore not present.
         * TODO: have currently running repeaters' tasks be killed if any device in
         * repeater be unplugged or disabled, assuming issues may occur?
         * 
         * 
         * TODO!!! remove enabled and disabled from device menu.
         * Have device name text suggest device is enabled or not, 
         * or have menu tooltiptext give hint.
         *
         */

        #region Parameters

        private MMDeviceEnumerator mMDeviceEnumerator;

        public List<MMDevice> AllWaveInDeviceList { get; private set; }
        public List<MMDevice> AllWaveOutDeviceList { get; private set; }

        public List<MMDevice> AvailableUnselectedWaveInMMDeviceList { get; private set; }
        public List<MMDevice> AvailableUnselectedWaveOutMMDeviceList { get; private set; }
        public List<MMDevice> DisabledWaveInMMDeviceList { get; private set; }
        public List<MMDevice> DisabledWaveOutMMDeviceList { get; private set; }
        public List<MMDevice> SelectedWaveInMMDeviceList { get; private set; }
        public List<MMDevice> SelectedWaveOutMMDeviceList { get; private set; }
        public List<string> AvailableUnselectedWaveInNameList { get; private set; }
        public List<string> AvailableUnselectedWaveOutNameList { get; private set; }
        public List<string> DisabledWaveInNameList { get; private set; }
        public List<string> DisabledWaveOutNameList { get; private set; }
        public List<string> SelectedWaveInNameList { get; private set; }
        public List<string> SelectedWaveOutNameList { get; private set; }
        public static DeviceState available = DeviceState.Active | DeviceState.Unplugged;

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

            AllWaveInDeviceList = mMDeviceEnumerator
                .EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.All).Distinct()
                .ToList();

            AllWaveInDeviceList = AllWaveInDeviceList
                .OrderBy(mMDevice => mMDevice.FriendlyName).ToList()
                .OrderBy(mMDevice => mMDevice.DeviceFriendlyName).ToList();

            AllWaveOutDeviceList = mMDeviceEnumerator
                .EnumerateAudioEndPoints(DataFlow.Render, DeviceState.All).Distinct()
                .ToList();

            AllWaveOutDeviceList = AllWaveOutDeviceList
                .OrderBy(mMDevice => mMDevice.FriendlyName).ToList()
                .OrderBy(mMDevice => mMDevice.DeviceFriendlyName).ToList();

            AvailableUnselectedWaveInMMDeviceList = AllWaveInDeviceList
                .Where(mMDevice => mMDevice.State == available).ToList();

            AvailableUnselectedWaveInNameList = AvailableUnselectedWaveInMMDeviceList
                .Select(mMDevice => mMDevice.FriendlyName).ToList();

            AvailableUnselectedWaveOutMMDeviceList = AllWaveOutDeviceList
                .Where(mMDevice => mMDevice.State == available).ToList();

            AvailableUnselectedWaveOutNameList = AvailableUnselectedWaveOutMMDeviceList
                .Select(mMDevice => mMDevice.FriendlyName).ToList();

            DisabledWaveInMMDeviceList = AllWaveInDeviceList
                .Where(mMDevice => mMDevice.State == DeviceState.Disabled).ToList();

            DisabledWaveInNameList = DisabledWaveInMMDeviceList.Select
                (mMDevice => mMDevice.FriendlyName).ToList();

            DisabledWaveOutMMDeviceList = AllWaveOutDeviceList
                .Where(mMDevice => mMDevice.State == DeviceState.Disabled).ToList();

            DisabledWaveOutNameList = DisabledWaveOutMMDeviceList.Select
                (mMDevice => mMDevice.FriendlyName).ToList();
        }

        /// <summary>
        /// If device state changes, move between lists.
        /// If device is null or device state is not present, remove from list.
        /// </summary>
        internal void UpdateWaveInListsGivenNewDeviceState()
        {
            AvailableUnselectedWaveInMMDeviceList.ForEach(mMDevice =>
            {
                MMDevice newMMDevice = mMDeviceEnumerator.GetDevice(mMDevice.ID);

                if (newMMDevice.State == DeviceState.Disabled)
                {
                    DisabledWaveInMMDeviceList.Remove(mMDevice);
                }
                else if
                    (newMMDevice is null || newMMDevice.State == DeviceState.NotPresent)
                {
                    AvailableUnselectedWaveInMMDeviceList.Remove(mMDevice);
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
                    AvailableUnselectedWaveInMMDeviceList.Remove(mMDevice);
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
            AvailableUnselectedWaveOutMMDeviceList.ForEach(mMDevice =>
            {
                MMDevice newMMDevice = mMDeviceEnumerator.GetDevice(mMDevice.ID);

                if (newMMDevice.State == DeviceState.Disabled)
                {
                    DisabledWaveOutMMDeviceList.Remove(mMDevice);
                }
                else if
                    (newMMDevice is null || newMMDevice.State == DeviceState.NotPresent)
                {
                    AvailableUnselectedWaveOutMMDeviceList.Remove(mMDevice);
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
                    AvailableUnselectedWaveOutMMDeviceList.Remove(mMDevice);
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
            if (AvailableUnselectedWaveInNameList.Contains(deviceName))
            {
                return AvailableUnselectedWaveInMMDeviceList
                    [AvailableUnselectedWaveInNameList.IndexOf(deviceName)].State;
            }
            else if (AvailableUnselectedWaveOutNameList.Contains(deviceName))
            {
                return AvailableUnselectedWaveOutMMDeviceList
                    [AvailableUnselectedWaveOutNameList.IndexOf(deviceName)].State;
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
            if (deviceName is null || deviceName == string.Empty)
            {
                return;
            }

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
                    AvailableUnselectedWaveInMMDeviceList.Add(mMDevice);
                    AvailableUnselectedWaveInNameList.Add(deviceName);
                }
                else
                {
                    AvailableUnselectedWaveOutMMDeviceList.Add(mMDevice);
                    AvailableUnselectedWaveOutNameList.Add(deviceName);
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
            if (deviceName is null || deviceName == string.Empty)
            {
                return;
            }

            MMDevice mMDevice = null;

            if (AvailableUnselectedWaveInNameList.Contains(deviceName))
            {
                mMDevice = AvailableUnselectedWaveInMMDeviceList
                    [AvailableUnselectedWaveInNameList.IndexOf(deviceName)];
                AvailableUnselectedWaveInMMDeviceList.Remove(mMDevice);
                AvailableUnselectedWaveInNameList.Remove(deviceName);
            }
            else if (AvailableUnselectedWaveOutNameList.Contains(deviceName))
            {
                mMDevice = AvailableUnselectedWaveOutMMDeviceList
                    [AvailableUnselectedWaveOutNameList.IndexOf(deviceName)];
                AvailableUnselectedWaveOutMMDeviceList.Remove(mMDevice);
                AvailableUnselectedWaveOutNameList.Remove(deviceName);
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