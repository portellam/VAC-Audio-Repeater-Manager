using NAudio.CoreAudioApi;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace VACARM.NET4.Models
{
    public class DeviceList
    {
        #region Parameters

        public List<MMDevice> AvailableWaveInMMDeviceList { get; private set; }
        public List<MMDevice> AvailableWaveOutMMDeviceList { get; private set; }

        public List<MMDevice> DisabledWaveInMMDeviceList { get; private set; }
        public List<MMDevice> DisabledWaveOutMMDeviceList { get; private set; }
        public List<string> AvailableWaveInNameList { get; private set; }
        public List<string> AvailableWaveOutNameList { get; private set; }
        public List<string> DisabledWaveInNameList { get; private set; }
        public List<string> DisabledWaveOutNameList { get; private set; }

        private DeviceState available = DeviceState.Active | DeviceState.Unplugged;

        #endregion

        #region Logic

        /// <summary>
        /// Constructor
        /// </summary>
        [ExcludeFromCodeCoverage]
        public DeviceList()
        {
            MMDeviceEnumerator mMDeviceEnumerator = new MMDeviceEnumerator();

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

        #endregion
    }
}