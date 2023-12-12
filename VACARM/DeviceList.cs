using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VACARM
{
    class DeviceList
    {
        public List<MMDevice> WaveIn { get; set; }
        public List<MMDevice> WaveOut { get; set; }
        public List<string> WaveInName { get; set; }
        public List<string> WaveOutName { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public DeviceList()
        {
            MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
            WaveIn = enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active).ToList();
            WaveInName = WaveIn.Select(x => x.FriendlyName).ToList();
            WaveOut = enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active).ToList();
            WaveOutName = WaveOut.Select(x => x.FriendlyName).ToList();
        }
    }
}
