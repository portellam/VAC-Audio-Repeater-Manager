using System.Collections.Generic;
using System.ComponentModel;
using VACARM.NET4.ViewModels;

namespace VACARM.NET4.Models
{
    public class RepeaterDataModel
    {
        #region Parameters

        public Dictionary<DeviceControl, Dictionary<DeviceControl, RepeaterModel>>
            RepeaterData
        { get; private set; }

        #endregion

        #region Logic

        /// <summary>
        /// Constructor
        /// </summary>
        public RepeaterDataModel()
        {
            RepeaterData = new Dictionary<DeviceControl,
                Dictionary<DeviceControl, RepeaterModel>>();
        }

        /// <summary>
        /// Adds a dictionary to the repeater data model, if it does not already exist.
        /// </summary>
        /// <param name="firstDeviceControl">The first device control</param>
        /// <param name="secondDeviceControl">The second device control</param>
        public void AddDictionary
            (DeviceControl inputDeviceControl, DeviceControl outputDeviceControl)
        {
            if (RepeaterData.ContainsKey(inputDeviceControl)
                || RepeaterData[inputDeviceControl].ContainsKey(outputDeviceControl)
                || RepeaterData.ContainsKey(outputDeviceControl)
                || RepeaterData[outputDeviceControl].ContainsKey(inputDeviceControl))
            {
                return;
            }

            RepeaterData.Add(inputDeviceControl, 
                GetDictionary(inputDeviceControl, outputDeviceControl));
            RepeaterData.Add(outputDeviceControl,
                GetDictionary(outputDeviceControl, inputDeviceControl));
        }

        /// <summary>
        /// Return a valid dictionary object.
        /// </summary>
        /// <param name="firstDeviceControl">The first device control</param>
        /// <param name="secondDeviceControl">The second device control</param>
        /// <returns>The dictionary object</returns>
        internal Dictionary<DeviceControl, RepeaterModel> GetDictionary
            (DeviceControl firstDeviceControl, DeviceControl secondDeviceControl)
        {
            return new Dictionary<DeviceControl, RepeaterModel>
            {
                {
                    secondDeviceControl,
                    new RepeaterModel(firstDeviceControl, secondDeviceControl)
                },
            };
        }

        #endregion
    }
}
