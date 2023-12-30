using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace VACARM_GUI_NET_4
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
            InitializeComponentDifferently();
            DataContext = new DeviceList();
        }

        /// <summary>
        /// Attempt to generate window using a unit-testable method, before calling the assembly method.
        /// </summary>
        protected internal virtual void InitializeComponentDifferently()
        {
            string namespaceString = typeof(AddDeviceDialog).Namespace.ToLower();
            string xamlName = $"{typeof(AddDeviceDialog).Name}.xaml".ToLower();
            string uri = $"/{namespaceString};component/{xamlName}";

            try
            {
                Extension.LoadViewFromUri(uri);
            }
            catch
            {
                InitializeComponent();    //TODO: remove if "LoadViewFromUri" works as intended and is unit-testable.
            }
        }

        [ExcludeFromCodeCoverage]
        /// <summary>
        /// Calls DragMove for current Window object.
        /// </summary>
        protected internal virtual void CallClose()
        {
            Close();
        }

        [ExcludeFromCodeCoverage]
        /// <summary>
        /// Calls DragMove for current Window object.
        /// </summary>
        protected internal virtual void CallDragMove()
        {
            DragMove();
        }

        /// <summary>
        /// Cancel event if button is clicked.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="routedEventArgs">The routed event</param>
        protected internal virtual void CancelButton_Click(object sender, RoutedEventArgs routedEventArgs)
        {
            if (!(sender as Button).IsCancel)
            {
                return;
            }

            CallClose();
        }

        /// <summary>
        /// Closes window for given device when Ok button is clicked.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="routedEventArgs">The routed event</param>
        protected internal virtual void OkButton_Click(object sender, RoutedEventArgs routedEventArgs)
        {
            if ((sender as Button).IsCancel)
            {
                return;
            }

            bool isNeitherWaveInOrWaveOutDevice = selectDeviceType.SelectedIndex == -1 || selectDevice.SelectedIndex == -1;

            if (isNeitherWaveInOrWaveOutDevice)
            {
                return;
            }

            bool isWaveInDevice = selectDeviceType.SelectedIndex == 0;
            List<MMDevice> mMDeviceList = new List<MMDevice>();

            if (isWaveInDevice)
            {
                mMDeviceList = (DataContext as DeviceList).WaveInMMDeviceList;
            }
            else
            {
                mMDeviceList = (DataContext as DeviceList).WaveOutMMDeviceList;
            }

            mMDevice = mMDeviceList[selectDevice.SelectedIndex];
            CallClose();
        }

        /// <summary>
        /// Select device type (Wave In or Wave Out) if selection has changed.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="selectionChangedEventArgs">The selection changed event</param>
        /// <exception cref="ArgumentNullException"></exception>
        protected internal virtual void SelectDeviceType_SelectionChanged(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            selectDevice.SelectedIndex = -1;
            bool isWaveInDevice = selectDeviceType.SelectedIndex == 0;

            if (isWaveInDevice)
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
            if (mouseButtonEventArgs.ChangedButton != MouseButton.Left)
            {
                return;
            }

            CallDragMove();
        }
    }
}
