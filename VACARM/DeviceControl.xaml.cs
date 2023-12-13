using NAudio.CoreAudioApi;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace VACARM
{
	/// <summary>
	/// Interaction logic for DeviceControl.xaml
	/// </summary>
	public partial class DeviceControl : UserControl, INotifyPropertyChanged
    {
        private static DeviceControl selectedControl;
        private double left;
        private Point start;
        public static DeviceControl InitialLink;
        
        public static DeviceControl SelectedControl
        {
            get
            {
                return selectedControl;
            }
            set
            {
                if (selectedControl != null)
                {
                    selectedControl.deviceBackground.Background = (selectedControl.mMDevice.DataFlow == DataFlow.Capture) ? Brushes.LightGreen : Brushes.PaleVioletRed;
                }

                if (value != null)
                {
                    value.deviceBackground.Background = Brushes.AliceBlue;
                }

                selectedControl = value;
            }
        }

        public BipartiteDeviceGraph Graph { get; }

        public DataFlow DataFlow
        {
            get
            {
                return mMDevice.DataFlow;
            }
        }

        public DeviceState State
        {
            get
            {
                return mMDevice.State;
            }
        }

        public MMDevice mMDevice;

        public string ID
        {
            get
            {
                return mMDevice.ID;
            }
        }

        public string DeviceName
        {
            get
            {
                return mMDevice.FriendlyName;
            }
        }

        public double Left
        {
            get
            {
                return left;
            }
            set
            {
                left = value;
                OnPropertyChanged(nameof(Left));
                X = value + Width / 2;
                OnPropertyChanged(nameof(X));
            }
        }

        private double top;
        
        public double Top
        {
            get
            {
                return top;
            }
            set
            {
                top = value;
                OnPropertyChanged(nameof(Top));
                Y = value + Height / 2;
                OnPropertyChanged(nameof(Y));
            }
        }

        public double X { get; set; }
        public double Y { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mMDevice">The device</param>
        /// <param name="bipartiteDeviceGraph">The graph</param>
        public DeviceControl(MMDevice mMDevice, BipartiteDeviceGraph bipartiteDeviceGraph)
        {
            InitializeComponent();
            this.mMDevice = mMDevice;
            Graph = bipartiteDeviceGraph;
            Panel.SetZIndex(this, 1);
            deviceBackground.Background = (mMDevice.DataFlow == DataFlow.Capture) ? Brushes.LightGreen : Brushes.PaleVioletRed;
            txtDeviceName.Text = mMDevice.FriendlyName;
            ContextMenu = new ContextMenu();
        }

        /// <summary>
		/// Logs event when DeviceControl property has changed.
		/// </summary>
		/// <param name="propertyName">The property name</param>
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Event logic for Mouse left button up.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="mouseButtonEventArgs">The mouse button event arguments</param>
        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            SelectedControl = this;

            if (MainWindow.SelectedTool == "Hand")
            {
                start = Mouse.GetPosition(sender as UIElement);
                Panel.SetZIndex(this, 2);
                return;
            }

            if (InitialLink == null)
            {
                InitialLink = this;
                return;
            }

            Graph.AddEdge(InitialLink, this);
            InitialLink = null;
        }

        /// <summary>
        /// Event logic for Mouse left button up.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="mouseButtonEventArgs">The mouse button event arguments</param>
        private void UserControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            if (MainWindow.SelectedTool == "Hand")
            {
                Panel.SetZIndex(this, 1);
            }

            SelectedControl = this;
        }

        /// <summary>
        /// Preview mouse movement if selected tool is not a hand and left button is not pressed.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="mouseEventArgs">The mouse event arguments</param>
        private void UserControl_PreviewMouseMove(object sender, MouseEventArgs mouseEventArgs)
        {
            if (MainWindow.SelectedTool != "Hand" || mouseEventArgs.LeftButton != MouseButtonState.Pressed)
            {
                return;
            }

            var draggableControl = sender as UserControl;
            var parentControl = Parent as Canvas;
            var currentPosition = Mouse.GetPosition(draggableControl);
            double left = currentPosition.X - start.X + Left;

            if (left < 0)
            {
                left = 0;
            }

            if (left + Width > parentControl.ActualWidth)
            {
                left = parentControl.ActualWidth - Width;
            }

            double top = currentPosition.Y - start.Y + Top;

            if (top < 0)
            {
                top = 0;
            }

            if (top + Height > parentControl.ActualHeight)
            {
                top = parentControl.ActualHeight - Height;
            }

            Left = left;
            Top = top;
        }
    }
}
