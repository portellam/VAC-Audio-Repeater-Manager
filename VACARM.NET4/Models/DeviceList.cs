using NAudio.CoreAudioApi;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace VACARM.NET4.Models
{
    public class DeviceList : INotifyPropertyChanged
    {
        ///////////////////////////////// Properties ///////////////////////////////////
        public List<MMDevice> WaveInMMDeviceList
        {
            get
            {
                return WaveInMMDeviceList;
            }
            set
            {
                WaveInMMDeviceList = value;
                OnPropertyChanged(nameof(WaveInMMDeviceList));
            }
        }

        public List<MMDevice> WaveOutMMDeviceList
        {
            get
            {
                return WaveOutMMDeviceList;
            }
            set
            {
                WaveOutMMDeviceList = value;
                OnPropertyChanged(nameof(WaveOutMMDeviceList));
            }
        }

        public List<string> WaveInNameList
        {
            get
            {
                return WaveInNameList;
            }
            set
            {
                WaveInNameList = value;
                OnPropertyChanged(nameof(WaveInNameList));
            }
        }

        public List<string> WaveOutNameList
        {
            get
            {
                return WaveOutNameList;
            }
            set
            {
                WaveOutNameList = value;
                OnPropertyChanged(nameof(WaveOutNameList));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /////////////////////////////////// Methods ////////////////////////////////////
        /// <summary>
        /// Constructor
        /// </summary>
        [ExcludeFromCodeCoverage]
        public DeviceList()
        {
            MMDeviceEnumerator mMDeviceEnumerator = new MMDeviceEnumerator();

            WaveInMMDeviceList = mMDeviceEnumerator.EnumerateAudioEndPoints
                (DataFlow.Capture, DeviceState.Active).ToList();

            WaveInNameList = WaveInMMDeviceList.Select(x => x.FriendlyName).ToList();

            WaveOutMMDeviceList = mMDeviceEnumerator.EnumerateAudioEndPoints
                (DataFlow.Render, DeviceState.Active).ToList();

            WaveOutNameList = WaveOutMMDeviceList.Select(x => x.FriendlyName).ToList();
        }

        /// <summary>
		/// Logs event when property has changed.
		/// </summary>
		/// <param name="propertyName">The property name</param>
        internal void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Return True if Wave In list contains the given device.
        /// </summary>
        /// <param name="deviceName">The device name</param>
        /// <returns>True/False</returns>
        public bool DoesWaveInListContainsDevice(string deviceName)
        {
            bool listContainsDevice = false;

            if (deviceName is null
                || deviceName == string.Empty)
            {
                return listContainsDevice;
            }

            WaveInNameList.Where
                (FriendlyName => FriendlyName == deviceName
                    && !listContainsDevice)
                    .ToList()
                    .ForEach(x => listContainsDevice = true);

            return listContainsDevice;
        }

        /// <summary>
        /// Return True if Wave Out list contains the given device.
        /// </summary>
        /// <param name="deviceName">The device name</param>
        /// <returns>True/False</returns>
        public bool DoesWaveOutListContainsDevice(string deviceName)
        {
            bool listContainsDevice = false;

            if (deviceName is null
                || deviceName == string.Empty)
            {
                return listContainsDevice;
            }

            WaveOutNameList.Where
                (FriendlyName => FriendlyName == deviceName
                    && !listContainsDevice)
                    .ToList()
                    .ForEach(x => listContainsDevice = true);

            return listContainsDevice;
        }
    }
}