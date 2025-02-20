using NAudio.CoreAudioApi;
using System.Windows.Forms;

namespace AudioRepeaterManager.NET4_0.Backend.ViewModels
{
    public partial class DeviceControl : UserControl
    {
        #region Parameters

        public MMDevice MMDevice { get; private set; }

        #endregion

        #region Logic

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mMDevice">The MMDevice</param>
        public DeviceControl(MMDevice mMDevice)
        {
            MMDevice = mMDevice;
            InitializeComponent();
        }

        #endregion
    }
}
