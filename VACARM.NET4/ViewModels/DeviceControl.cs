using NAudio.CoreAudioApi;
using System.Windows.Forms;

namespace VACARM.NET4.ViewModels
{
    public partial class DeviceControl : UserControl
    {
        #region Parameters

        public MMDevice MMDevice { get; set; }

        #endregion

        #region Logic

        public DeviceControl()
        {
            InitializeComponent();
        }

        #endregion
    }
}
