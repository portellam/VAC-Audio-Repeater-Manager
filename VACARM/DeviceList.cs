using NAudio.CoreAudioApi;
using System.Collections.Generic;
using System.Linq;

namespace VACARM
{
    class DeviceList
    {
        public List<MMDevice> WaveInMMDeviceList { get; set; }
        public List<MMDevice> WaveOutMMDeviceList { get; set; }
        public List<string> WaveInNameList { get; set; }
        public List<string> WaveOutNameList { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public DeviceList()
        {
            MMDeviceEnumerator mMDeviceEnumerator = new MMDeviceEnumerator();
            WaveInMMDeviceList = mMDeviceEnumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active).ToList();
            WaveInNameList = WaveInMMDeviceList.Select(x => x.FriendlyName).ToList();
            WaveOutMMDeviceList = mMDeviceEnumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active).ToList();
            WaveOutNameList = WaveOutMMDeviceList.Select(x => x.FriendlyName).ToList();
        }
    }
}