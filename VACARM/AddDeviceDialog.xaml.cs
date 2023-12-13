using NAudio.CoreAudioApi;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace VACARM
{
	/// <summary>
	/// Interaction logic for AddDeviceDialog.xaml
	/// </summary>
	public partial class AddDeviceDialog : Window
    {
        public MMDevice mMDevice;

        /// <summary>
        /// Constructor
        /// </summary>
        public AddDeviceDialog()
        {
            InitializeComponent();
            DataContext = new DeviceList();
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectDeviceType.SelectedIndex == -1 || selectDevice.SelectedIndex == -1) return;

            List<MMDevice> devices = (selectDeviceType.SelectedIndex == 0) ? (DataContext as DeviceList).WaveIn : (DataContext as DeviceList).WaveOut;
            mMDevice = devices[selectDevice.SelectedIndex];

            Close();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void selectDeviceType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectDevice.SelectedIndex = -1;
            if (selectDeviceType.SelectedIndex == 0) selectDevice.ItemsSource = (DataContext as DeviceList).WaveInName;
            else selectDevice.ItemsSource = (DataContext as DeviceList).WaveOutName;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }
    }
}
