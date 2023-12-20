using NAudio.CoreAudioApi;
using System;
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

        /// <summary>
        /// Cancel event if button is clicked.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="routedEventArgs">The routed event</param>
        protected internal virtual void CancelButton_Click(object sender, RoutedEventArgs routedEventArgs)
        {
            Close();
        }

        /// <summary>
        /// Closes window for given device when Ok button is clicked.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="routedEventArgs">The routed event</param>
        protected internal virtual void OkButton_Click(object sender, RoutedEventArgs routedEventArgs)
        {
            if (selectDeviceType.SelectedIndex == -1 || selectDevice.SelectedIndex == -1)
            {
                return;
            }

            List<MMDevice> devices = (selectDeviceType.SelectedIndex == 0) ? (DataContext as DeviceList).WaveInMMDeviceList : (DataContext as DeviceList).WaveOutMMDeviceList;
            mMDevice = devices[selectDevice.SelectedIndex];
            Close();
        }

        /// <summary>
        /// Select device type (Wave In or Wave Out) if selection has changed.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="selectionChangedEventArgs">The selection changed event</param>
        /// <exception cref="ArgumentNullException"></exception>
        protected internal virtual void SelectDeviceType_SelectionChanged(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            if (sender is null)
            {
                throw new ArgumentNullException(nameof(sender));
            }

            if (selectionChangedEventArgs is null)
            {
                throw new ArgumentNullException(nameof(selectionChangedEventArgs));
            }

            selectDevice.SelectedIndex = -1;

            if (selectDeviceType.SelectedIndex == 0)
            {
                selectDevice.ItemsSource = (DataContext as DeviceList).WaveInNameList;
                return;
            }

            selectDevice.ItemsSource = (DataContext as DeviceList).WaveOutNameList;
        }

        /// <summary>
        /// Allow window drag if left mouse button is clicked.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="mouseButtonEventArgs">The mouse button event</param>
        protected internal virtual void Window_MouseDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            if (mouseButtonEventArgs.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }
    }
}
