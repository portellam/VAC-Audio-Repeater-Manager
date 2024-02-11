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
        public List<MMDevice> SelectedWaveInMMDeviceList { get; private set; }
        public List<MMDevice> SelectedWaveOutMMDeviceList { get; private set; }
        public List<MMDevice> UnselectedWaveInMMDeviceList { get; private set; }
        public List<MMDevice> UnselectedWaveOutMMDeviceList { get; private set; }
        public List<string> SelectedWaveInNameList { get; private set; }
        public List<string> SelectedWaveOutNameList { get; private set; }
        public List<string> UnselectedWaveInNameList { get; private set; }
        public List<string> UnselectedWaveOutNameList { get; private set; }
        public static DeviceState Present =
            DeviceState.Active | DeviceState.Unplugged | DeviceState.Disabled;

        #endregion

        #region Logic

        /// <summary>
        /// Constructor
        /// </summary>
        [ExcludeFromCodeCoverage]
        public DeviceList()
        {
            GetAllDeviceLists();
            GetUnselectedWaveInDeviceLists();
            GetUnselectedWaveOutDeviceLists();
            SelectedWaveInMMDeviceList = new List<MMDevice>();
            SelectedWaveInNameList = new List<string>();
            SelectedWaveOutMMDeviceList = new List<MMDevice>();
            SelectedWaveOutNameList = new List<string>();
        }

        /// <summary>
        /// Does device list contain device.
        /// </summary>
        /// <param name="deviceList">The device list</param>
        /// <param name="mMDevice">The device</param>
        /// <returns></returns>
        internal bool DoesListContainDevice
            (List<MMDevice> mMDeviceList, MMDevice mMDevice)
        {
            return mMDeviceList is null || mMDeviceList.Count == 0
                || !mMDeviceList.Contains(mMDevice);
        }

        /// <summary>
        /// Get all device lists.
        /// </summary>
        internal void GetAllDeviceLists()
        {
            mMDeviceEnumerator = new MMDeviceEnumerator();
            AllWaveInDeviceList = mMDeviceEnumerator
                .EnumerateAudioEndPoints(DataFlow.Capture, Present).Distinct()
                .ToList();
            AllWaveInDeviceList = AllWaveInDeviceList
                .OrderBy(mMDevice => mMDevice.FriendlyName).ToList()
                .OrderBy(mMDevice => mMDevice.DeviceFriendlyName).ToList();
            AllWaveOutDeviceList = mMDeviceEnumerator
                .EnumerateAudioEndPoints(DataFlow.Capture, Present).Distinct()
                .ToList();
            AllWaveOutDeviceList = AllWaveOutDeviceList
                .OrderBy(mMDevice => mMDevice.FriendlyName).ToList()
                .OrderBy(mMDevice => mMDevice.DeviceFriendlyName).ToList();
        }

        /// <summary>
        /// Get unselected wave in device lists.
        /// </summary>
        internal void GetUnselectedWaveInDeviceLists()
        {
            if (SelectedWaveInMMDeviceList is null ||
                SelectedWaveInMMDeviceList.Count == 0)
            {
                UnselectedWaveInMMDeviceList = AllWaveInDeviceList;
            }

            UnselectedWaveInMMDeviceList = AllWaveInDeviceList
                .Where(mMDevice =>
                    !DoesListContainDevice(SelectedWaveInMMDeviceList, mMDevice))
                .ToList();
            UnselectedWaveInNameList = UnselectedWaveInMMDeviceList
                .Select(mMDevice => mMDevice.FriendlyName).ToList();
        }

        /// <summary>
        /// Get unselected wave out device lists.
        /// </summary>
        internal void GetUnselectedWaveOutDeviceLists()
        {
            if (SelectedWaveOutMMDeviceList is null ||
                SelectedWaveOutMMDeviceList.Count == 0)
            {
                SelectedWaveOutMMDeviceList = AllWaveOutDeviceList;
            }

            UnselectedWaveOutMMDeviceList = AllWaveOutDeviceList
                .Where(mMDevice =>
                    !DoesListContainDevice(SelectedWaveOutMMDeviceList, mMDevice))
                .ToList();
            UnselectedWaveOutNameList = UnselectedWaveOutMMDeviceList
                .Select(mMDevice => mMDevice.FriendlyName).ToList();
        }

        /// <summary>
        /// Remove device(s) from selected lists if device does not currently exist or
        /// it is not present.
        /// </summary>
        internal void SetSelectedListsGivenNewDeviceState()
        {
            SelectedWaveInMMDeviceList.ForEach(mMDevice =>
            {
                if (!AllWaveInDeviceList.Contains(mMDevice)
                    || mMDevice.State != Present)
                {
                    SelectedWaveInMMDeviceList.Remove(mMDevice);
                }
            });
            SelectedWaveOutMMDeviceList.ForEach(mMDevice =>
            {
                if (!AllWaveOutDeviceList.Contains(mMDevice)
                    || mMDevice.State != Present)
                {
                    SelectedWaveOutMMDeviceList.Remove(mMDevice);
                }
            });
        }

        /// <summary>
        /// Move device from selected list to related unselected list.
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

            if (mMDevice.State == Present && mMDevice.DataFlow == DataFlow.Capture)
            {
                UnselectedWaveInMMDeviceList.Add(mMDevice);
                UnselectedWaveInNameList.Add(deviceName);
            }
            else if (mMDevice.State == Present && mMDevice.DataFlow == DataFlow.Render)
            {
                UnselectedWaveOutMMDeviceList.Add(mMDevice);
                UnselectedWaveOutNameList.Add(deviceName);
            }
            else if (mMDevice.State != Present && mMDevice.DataFlow == DataFlow.Render)
            {
                UnselectedWaveInMMDeviceList.Remove(mMDevice);
                UnselectedWaveInNameList.Remove(deviceName);
            }
            else
            {
                UnselectedWaveOutMMDeviceList.Remove(mMDevice);
                UnselectedWaveOutNameList.Remove(deviceName);
            }
        }

        /// <summary>
        /// Move device from unselected list to related selected list.
        /// </summary>
        /// <param name="deviceName">The device name</param>
        public void MoveDeviceToSelectedList(string deviceName)
        {
            if (deviceName is null || deviceName == string.Empty)
            {
                return;
            }

            MMDevice mMDevice = null;

            if (UnselectedWaveInNameList.Contains(deviceName))
            {
                mMDevice = UnselectedWaveInMMDeviceList
                    [UnselectedWaveInNameList.IndexOf(deviceName)];
                UnselectedWaveInMMDeviceList.Remove(mMDevice);
                UnselectedWaveInNameList.Remove(deviceName);
            }
            else if (UnselectedWaveOutNameList.Contains(deviceName))
            {
                mMDevice = UnselectedWaveOutMMDeviceList
                    [UnselectedWaveOutNameList.IndexOf(deviceName)];
                UnselectedWaveOutMMDeviceList.Remove(mMDevice);
                UnselectedWaveOutNameList.Remove(deviceName);
            }
            else
            {
                return;
            }

            if (mMDevice.State == Present && mMDevice.DataFlow == DataFlow.Capture)
            {
                SelectedWaveInMMDeviceList.Add(mMDevice);
                SelectedWaveInNameList.Add(deviceName);
            }
            else if (mMDevice.State == Present && mMDevice.DataFlow == DataFlow.Render)
            {
                SelectedWaveOutMMDeviceList.Add(mMDevice);
                SelectedWaveOutNameList.Add(deviceName);
            }
            else if (mMDevice.State != Present && mMDevice.DataFlow == DataFlow.Render)
            {
                SelectedWaveInMMDeviceList.Remove(mMDevice);
                SelectedWaveInNameList.Remove(deviceName);
            }
            else
            {
                SelectedWaveOutMMDeviceList.Remove(mMDevice);
                SelectedWaveOutNameList.Remove(deviceName);
            }
        }

        /// <summary>
        /// Query system for changes, and update lists as needed.
        /// </summary>
        public void SetDeviceLists()
        {
            GetAllDeviceLists();
            GetUnselectedWaveInDeviceLists();
            GetUnselectedWaveOutDeviceLists();
            SetSelectedListsGivenNewDeviceState();
        }

        #endregion
    }
}