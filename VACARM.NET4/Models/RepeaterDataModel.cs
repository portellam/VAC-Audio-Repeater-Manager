using System.Collections.Generic;
using System.ComponentModel;
using VACARM.NET4.ViewModels;

namespace VACARM.NET4.Models
{
    public class RepeaterDataModel
    {
        #region Parameters

        public Dictionary<DeviceControl, Dictionary<DeviceControl, RepeaterModel>>
            RepeaterData { get; private set; }

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

        #endregion
    }
}
