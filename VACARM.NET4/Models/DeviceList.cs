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
        public List<string> AllWaveInNameList { get; private set; }
        public List<string> AllWaveOutNameList { get; private set; }
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
            //TODO: be able to load device list from file. That way, we can edit configs for other systems, and not crash this app too.

            GetAllMMDeviceListsFromSystem();
            GetUnselectedWaveInMMDeviceLists();
            GetUnselectedWaveOutMMDeviceLists();
            SelectedWaveInMMDeviceList = new List<MMDevice>();
            SelectedWaveInNameList = new List<string>();
            SelectedWaveOutMMDeviceList = new List<MMDevice>();
            SelectedWaveOutNameList = new List<string>();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mMDeviceList">the MMDevice list</param>
        public DeviceList(List<MMDevice> mMDeviceList)
        {
            GetAllMMDeviceListsFromData(mMDeviceList);
            GetUnselectedWaveInMMDeviceLists();
            GetUnselectedWaveOutMMDeviceLists();
            SelectedWaveInMMDeviceList = new List<MMDevice>();
            SelectedWaveInNameList = new List<string>();
            SelectedWaveOutMMDeviceList = new List<MMDevice>();
            SelectedWaveOutNameList = new List<string>();
        }

        /// <summary>
        /// Does MMDevice list contain MMDevice.
        /// </summary>
        /// <param name="mMDeviceList">The MMDevice list</param>
        /// <param name="mMDevice">The MMDevice</param>
        /// <returns></returns>
        internal bool DoesListContainMMDevice
            (List<MMDevice> mMDeviceList, MMDevice mMDevice)
        {
            return mMDeviceList is null || mMDeviceList.Count == 0
                || !mMDeviceList.Contains(mMDevice);
        }

        /// <summary>
        /// Get list of MMDevice names, given MMDevice list.
        /// </summary>
        /// <param name="mMDeviceList">The MMDevice list</param>
        /// <returns></returns>
        internal List<string> GetNameListGivenMMDeviceList(List<MMDevice> mMDeviceList)
        {
            if (mMDeviceList is null || mMDeviceList.Count == 0)
            {
                return new List<string>();
            }

            return mMDeviceList.Select(mMDevice => mMDevice.FriendlyName).ToList();
        }

        /// <summary>
        /// Get MMDevice by friendly name.
        /// </summary>
        /// <param name="mMDeviceFriendlyName">The MMDevice friendly name</param>
        /// <returns></returns>
        internal MMDevice GetMMDevice(string mMDeviceFriendlyName)
        {
            MMDevice mMDevice = null;

            if (AllWaveInNameList.Contains(mMDeviceFriendlyName))
            {
                mMDevice = AllWaveInDeviceList
                    .Where(thisMMDevice =>
                        thisMMDevice.FriendlyName == mMDeviceFriendlyName)
                    .FirstOrDefault();
            }
            else if (AllWaveOutNameList.Contains(mMDeviceFriendlyName))
            {
                mMDevice = AllWaveOutDeviceList
                    .Where(thisMMDevice =>
                        thisMMDevice.FriendlyName == mMDeviceFriendlyName)
                    .FirstOrDefault();
            }

            return mMDevice;
        }

        /// <summary>
        /// Get all MMDevice lists from input data.
        /// </summary>
        internal void GetAllMMDeviceListsFromData(List<MMDevice> mMDeviceList)
        {
            mMDeviceEnumerator = null;

            AllWaveInDeviceList = mMDeviceList.Where(mMDevice =>
            {
                return mMDevice.DataFlow == DataFlow.Capture &&
                    mMDevice.State == Present;
            }).Distinct().ToList();

            AllWaveInDeviceList = AllWaveInDeviceList
                .OrderBy(mMDevice => mMDevice.FriendlyName).ToList()
                .OrderBy(mMDevice => mMDevice.DeviceFriendlyName).ToList();

            AllWaveOutDeviceList = mMDeviceList.Where(mMDevice =>
            {
                return mMDevice.DataFlow == DataFlow.Render &&
                    mMDevice.State == Present;
            }).Distinct().ToList();

            AllWaveOutDeviceList = AllWaveOutDeviceList
                .OrderBy(mMDevice => mMDevice.FriendlyName).ToList()
                .OrderBy(mMDevice => mMDevice.DeviceFriendlyName).ToList();
        }

        /// <summary>
        /// Get all MMDevice lists from system.
        /// </summary>
        internal void GetAllMMDeviceListsFromSystem()
        {
            mMDeviceEnumerator = new MMDeviceEnumerator();

            AllWaveInDeviceList = mMDeviceEnumerator
                .EnumerateAudioEndPoints(DataFlow.Capture, Present).Distinct().ToList();

            AllWaveInDeviceList = AllWaveInDeviceList
                .OrderBy(mMDevice => mMDevice.FriendlyName).ToList()
                .OrderBy(mMDevice => mMDevice.DeviceFriendlyName).ToList();

            AllWaveOutDeviceList = mMDeviceEnumerator
                .EnumerateAudioEndPoints(DataFlow.Render, Present).Distinct().ToList();

            AllWaveOutDeviceList = AllWaveOutDeviceList
                .OrderBy(mMDevice => mMDevice.FriendlyName).ToList()
                .OrderBy(mMDevice => mMDevice.DeviceFriendlyName).ToList();
        }

        /// <summary>
        /// Get unselected wave in MMDevice lists.
        /// </summary>
        internal void GetUnselectedWaveInMMDeviceLists()
        {
            if (SelectedWaveInMMDeviceList is null ||
                SelectedWaveInMMDeviceList.Count == 0)
            {
                UnselectedWaveInMMDeviceList = AllWaveInDeviceList;
            }
            else
            {
                UnselectedWaveInMMDeviceList = AllWaveInDeviceList
                    .Where(mMDevice =>
                        !DoesListContainMMDevice(SelectedWaveInMMDeviceList, mMDevice))
                    .ToList();
            }

            UnselectedWaveInNameList =
                GetNameListGivenMMDeviceList(UnselectedWaveInMMDeviceList);
        }

        /// <summary>
        /// Get unselected wave out MMDevice lists.
        /// </summary>
        internal void GetUnselectedWaveOutMMDeviceLists()
        {
            if (SelectedWaveOutMMDeviceList is null ||
                SelectedWaveOutMMDeviceList.Count == 0)
            {
                UnselectedWaveOutMMDeviceList = AllWaveOutDeviceList;
            }
            else
            {
                UnselectedWaveOutMMDeviceList = AllWaveOutDeviceList
                .Where(mMDevice =>
                    !DoesListContainMMDevice(SelectedWaveOutMMDeviceList, mMDevice))
                .ToList();
            }

            UnselectedWaveOutNameList =
                GetNameListGivenMMDeviceList(UnselectedWaveOutMMDeviceList);
        }

        /// <summary>
        /// Remove MMDevice(s) from selected lists if MMDevice does not currently exist
        /// or it is not present.
        /// </summary>
        internal void SetSelectedListsGivenNewMMDeviceState()
        {
            SelectedWaveInMMDeviceList.ForEach(mMDevice =>
            {
                if (!AllWaveInDeviceList.Contains(mMDevice)
                    || mMDevice.State != Present)
                {
                    SelectedWaveInMMDeviceList.Remove(mMDevice);
                }
            });

            SelectedWaveInNameList =
                GetNameListGivenMMDeviceList(SelectedWaveInMMDeviceList);

            SelectedWaveOutMMDeviceList.ForEach(mMDevice =>
            {
                if (!AllWaveOutDeviceList.Contains(mMDevice)
                    || mMDevice.State != Present)
                {
                    SelectedWaveOutMMDeviceList.Remove(mMDevice);
                }
            });

            SelectedWaveOutNameList =
                GetNameListGivenMMDeviceList(SelectedWaveOutMMDeviceList);
        }

        /// <summary>
        /// Move MMDevice from selected list to related unselected list.
        /// </summary>
        /// <param name="mMDeviceName">The MMDevice name</param>
        public void MoveMMDeviceFromSelectedList(string mMDeviceName)
        {
            if (mMDeviceName is null || mMDeviceName == string.Empty)
            {
                return;
            }

            MMDevice mMDevice = GetMMDevice(mMDeviceName);

            if (mMDevice is null)
            {
                return;
            }

            if (mMDevice.DataFlow == DataFlow.Capture)
            {
                SelectedWaveInMMDeviceList.Remove(mMDevice);
                SelectedWaveInNameList =
                    GetNameListGivenMMDeviceList(SelectedWaveInMMDeviceList);
                UnselectedWaveInMMDeviceList.Add(mMDevice);
                UnselectedWaveInNameList =
                    GetNameListGivenMMDeviceList(UnselectedWaveInMMDeviceList);
                return;
            }

            SelectedWaveOutMMDeviceList.Remove(mMDevice);
            SelectedWaveOutNameList =
                GetNameListGivenMMDeviceList(SelectedWaveOutMMDeviceList);
            UnselectedWaveOutMMDeviceList.Add(mMDevice);
            UnselectedWaveOutNameList =
                GetNameListGivenMMDeviceList(UnselectedWaveOutMMDeviceList);
        }

        /// <summary>
        /// Move all unselected MMDevices to selected lists.
        /// </summary>
        public void MoveAllMMDevicesToSelectedLists()
        {
            SelectedWaveInMMDeviceList = UnselectedWaveInMMDeviceList;
            UnselectedWaveInNameList =
                GetNameListGivenMMDeviceList(UnselectedWaveInMMDeviceList);
            SelectedWaveInNameList =
                GetNameListGivenMMDeviceList(SelectedWaveInMMDeviceList);

            SelectedWaveOutMMDeviceList = UnselectedWaveOutMMDeviceList;
            UnselectedWaveOutNameList =
                GetNameListGivenMMDeviceList(UnselectedWaveOutMMDeviceList);
            SelectedWaveOutNameList =
                GetNameListGivenMMDeviceList(SelectedWaveOutMMDeviceList);
        }

        /// <summary>
        /// Move MMDevice from unselected list to related selected list.
        /// </summary>
        /// <param name="mMDeviceName">The MMDevice name</param>
        public void MoveMMDeviceToSelectedList(string mMDeviceName)
        {
            if (mMDeviceName is null || mMDeviceName == string.Empty)
            {
                return;
            }

            MMDevice mMDevice = GetMMDevice(mMDeviceName);

            if (mMDevice is null)
            {
                return;
            }

            if (mMDevice.DataFlow == DataFlow.Capture)
            {
                UnselectedWaveInMMDeviceList.Remove(mMDevice);
                UnselectedWaveInNameList =
                    GetNameListGivenMMDeviceList(UnselectedWaveInMMDeviceList);
                SelectedWaveInMMDeviceList.Add(mMDevice);
                SelectedWaveInNameList =
                    GetNameListGivenMMDeviceList(SelectedWaveInMMDeviceList);
                return;
            }

            UnselectedWaveOutMMDeviceList.Remove(mMDevice);
            UnselectedWaveOutNameList =
                GetNameListGivenMMDeviceList(UnselectedWaveOutMMDeviceList);
            SelectedWaveOutMMDeviceList.Add(mMDevice);
            SelectedWaveOutNameList =
                GetNameListGivenMMDeviceList(SelectedWaveOutMMDeviceList);
        }

        /// <summary>
        /// Query system for changes, and update lists as needed.
        /// </summary>
        public void SetDeviceLists()
        {
            GetUnselectedWaveInMMDeviceLists();
            GetUnselectedWaveOutMMDeviceLists();
            GetAllMMDeviceListsFromSystem();
            SetSelectedListsGivenNewMMDeviceState();
        }

        #endregion
    }
}