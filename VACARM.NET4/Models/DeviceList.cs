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

        public List<MMDevice> AllDeviceList { get; private set; }
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
            AllDeviceList = new MMDeviceEnumerator()
                .EnumerateAudioEndPoints(DataFlow.All, Present).Distinct().ToList();

            SetAndSortAllMMDeviceLists();
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
        /// <param name="allDeviceList">the MMDevice list</param>
        public DeviceList(List<MMDevice> allDeviceList)
        {
            AllDeviceList = allDeviceList.Where
                (mMDevice => mMDevice.State == Present).Distinct().ToList();

            SetAndSortAllMMDeviceLists();
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
        /// <returns>The MMDevice</returns>
        public MMDevice GetMMDevice(string mMDeviceFriendlyName)
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
        /// Get MMDevice by friendly name and data flow.
        /// </summary>
        /// <param name="mMDeviceFriendlyName">The MMDevice friendly name</param>
        /// <param name="dataFlow">The data flow</param>
        /// <returns>The MMDevice</returns>
        public MMDevice GetMMDevice(string mMDeviceFriendlyName, DataFlow dataFlow)
        {
            MMDevice mMDevice = null;

            if (dataFlow == DataFlow.Capture)
            {
                mMDevice = AllWaveInDeviceList
                    .Where(thisMMDevice =>
                        thisMMDevice.FriendlyName == mMDeviceFriendlyName)
                    .FirstOrDefault();
            }
            else
            {
                mMDevice = AllWaveOutDeviceList
                    .Where(thisMMDevice =>
                        thisMMDevice.FriendlyName == mMDeviceFriendlyName)
                    .FirstOrDefault();
            }

            return mMDevice;
        }

        /// <summary>
        /// Get index of MMDevice by friendly name.
        /// </summary>
        /// <param name="mMDeviceFriendlyName">The MMDevice friendly name</param>
        /// <param name="dataFlow">The data flow</param>
        /// <returns>The index</returns>
        public int GetIndexOfMMDevice(string mMDeviceFriendlyName, DataFlow dataFlow)
        {
            if (mMDeviceFriendlyName is null)
            {
                return -1;
            }

            if (dataFlow == DataFlow.Capture)
            {
                return AllWaveInNameList.IndexOf(mMDeviceFriendlyName);
            }
            else if (AllWaveOutNameList.Contains(mMDeviceFriendlyName))
            {
                return AllWaveOutNameList.IndexOf(mMDeviceFriendlyName);
            }

            return -1;
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
        /// Set and sort all MMDevice lists.
        /// </summary>
        internal void SetAndSortAllMMDeviceLists()
        {
            if (AllDeviceList is null)
            {
                return;
            }

            AllWaveInDeviceList = AllDeviceList.Where(mMDevice =>
                mMDevice.DataFlow == DataFlow.Capture).Distinct().ToList();

            AllWaveInDeviceList = AllWaveInDeviceList
                .OrderBy(mMDevice => mMDevice.FriendlyName).ToList()
                .OrderBy(mMDevice => mMDevice.DeviceFriendlyName).ToList();

            AllWaveInNameList = GetNameListGivenMMDeviceList(AllWaveInDeviceList);

            AllWaveOutDeviceList = AllDeviceList.Where(mMDevice =>
                mMDevice.DataFlow == DataFlow.Render).Distinct().ToList();

            AllWaveOutDeviceList = AllWaveOutDeviceList
                .OrderBy(mMDevice => mMDevice.FriendlyName).ToList()
                .OrderBy(mMDevice => mMDevice.DeviceFriendlyName).ToList();

            AllWaveOutNameList = GetNameListGivenMMDeviceList(AllWaveOutDeviceList);
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
        /// Sort selected MMDevice lists.
        /// </summary>
        internal void SortSelectedMMDeviceLists()
        {
            SelectedWaveInMMDeviceList = SelectedWaveInMMDeviceList
                .OrderBy(mMDevice => mMDevice.FriendlyName).ToList()
                .OrderBy(mMDevice => mMDevice.DeviceFriendlyName).ToList();

            SelectedWaveInNameList =
                GetNameListGivenMMDeviceList(SelectedWaveInMMDeviceList);

            SelectedWaveOutMMDeviceList = SelectedWaveOutMMDeviceList
                .OrderBy(mMDevice => mMDevice.FriendlyName).ToList()
                .OrderBy(mMDevice => mMDevice.DeviceFriendlyName).ToList();

            SelectedWaveOutNameList =
                GetNameListGivenMMDeviceList(SelectedWaveOutMMDeviceList);
        }

        /// <summary>
        /// Sort unselected MMDevice lists.
        /// </summary>
        internal void SortUnselectedMMDeviceLists()
        {
            UnselectedWaveInMMDeviceList = UnselectedWaveInMMDeviceList
                .OrderBy(mMDevice => mMDevice.FriendlyName).ToList()
                .OrderBy(mMDevice => mMDevice.DeviceFriendlyName).ToList();

            UnselectedWaveInNameList =
                GetNameListGivenMMDeviceList(UnselectedWaveInMMDeviceList);

            UnselectedWaveOutMMDeviceList = UnselectedWaveOutMMDeviceList
                .OrderBy(mMDevice => mMDevice.FriendlyName).ToList()
                .OrderBy(mMDevice => mMDevice.DeviceFriendlyName).ToList();

            UnselectedWaveOutNameList =
                GetNameListGivenMMDeviceList(UnselectedWaveOutMMDeviceList);
        }

        /// <summary>
        /// Move all unselected MMDevices to selected lists.
        /// </summary>
        public void MoveAllMMDevicesToSelectedLists()
        {
            SelectedWaveInMMDeviceList.AddRange(UnselectedWaveInMMDeviceList);
            UnselectedWaveInMMDeviceList = new List<MMDevice>();
            UnselectedWaveInNameList =
                GetNameListGivenMMDeviceList(UnselectedWaveInMMDeviceList);

            SelectedWaveOutMMDeviceList.AddRange(UnselectedWaveOutMMDeviceList);
            UnselectedWaveOutMMDeviceList = new List<MMDevice>();
            UnselectedWaveOutNameList =
                GetNameListGivenMMDeviceList(UnselectedWaveOutMMDeviceList);

            SortSelectedMMDeviceLists();
        }

        /// <summary>
        /// Move MMDevice from selected list to related unselected list.
        /// </summary>
        /// <param name="mMDeviceName">The MMDevice name</param>
        /// <param name="dataFlow">The data flow</param>
        public void MoveMMDeviceFromSelectedList(string mMDeviceName, DataFlow dataFlow)
        {
            if (mMDeviceName is null || mMDeviceName == string.Empty
                || dataFlow == DataFlow.All)
            {
                return;
            }

            MMDevice mMDevice = GetMMDevice(mMDeviceName, dataFlow);

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
            }
            else
            {
                SelectedWaveOutMMDeviceList.Remove(mMDevice);
                SelectedWaveOutNameList =
                    GetNameListGivenMMDeviceList(SelectedWaveOutMMDeviceList);
                UnselectedWaveOutMMDeviceList.Add(mMDevice);
                UnselectedWaveOutNameList =
                    GetNameListGivenMMDeviceList(UnselectedWaveOutMMDeviceList);
            }

            SortUnselectedMMDeviceLists();
        }

        /// <summary>
        /// Move MMDevice from unselected list to related selected list.
        /// </summary>
        /// <param name="mMDeviceName">The MMDevice name</param>
        /// <param name="dataFlow">The data flow</param>
        public void MoveMMDeviceToSelectedList(string mMDeviceName, DataFlow dataFlow)
        {
            if (mMDeviceName is null || mMDeviceName == string.Empty
                || dataFlow == DataFlow.All)
            {
                return;
            }

            MMDevice mMDevice = GetMMDevice(mMDeviceName, dataFlow);

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
            }
            else
            {
                UnselectedWaveOutMMDeviceList.Remove(mMDevice);
                UnselectedWaveOutNameList =
                    GetNameListGivenMMDeviceList(UnselectedWaveOutMMDeviceList);
                SelectedWaveOutMMDeviceList.Add(mMDevice);
                SelectedWaveOutNameList =
                    GetNameListGivenMMDeviceList(SelectedWaveOutMMDeviceList);
            }

            SortSelectedMMDeviceLists();
        }

        #endregion
    }
}