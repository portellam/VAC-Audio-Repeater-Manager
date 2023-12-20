using NAudio.CoreAudioApi;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace VACARM_GUI
{
    /// <summary>
    /// Interaction logic for DeviceControl.xaml
    /// </summary>
    public partial class DeviceControl : UserControl, INotifyPropertyChanged
    {
        private static DeviceControl selectedDeviceControl;
        private double left;
        private Point startPoint;
        private static readonly SolidColorBrush defaultDeviceColor = Brushes.AliceBlue;
        private static readonly SolidColorBrush inputDeviceColor = Brushes.LightGreen;
        private static readonly SolidColorBrush outputDeviceColor = Brushes.PaleVioletRed;
        public static DeviceControl InitialDeviceControl;

        public static DeviceControl SelectedDeviceControl
        {
            get
            {
                return selectedDeviceControl;
            }
            set
            {
                if (selectedDeviceControl != null)
                {
                    selectedDeviceControl.deviceBackground.Background = SetBackgroundColor(selectedDeviceControl.mMDevice.DataFlow);
                }

                if (value != null)
                {
                    value.deviceBackground.Background = defaultDeviceColor;
                }

                selectedDeviceControl = value;
            }
        }

        public BipartiteDeviceGraph BipartiteDeviceGraph { get; }

        public DataFlow DataFlow
        {
            get
            {
                return mMDevice.DataFlow;
            }
        }

        public DeviceState DeviceState
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
            BipartiteDeviceGraph = bipartiteDeviceGraph;
            Panel.SetZIndex(this, 1);
            deviceBackground.Background = SetBackgroundColor(mMDevice.DataFlow);
            txtDeviceName.Text = mMDevice.FriendlyName;
            ContextMenu = new ContextMenu();
        }

        /// <summary>
		/// Logs event when DeviceControl property has changed.
		/// </summary>
		/// <param name="propertyName">The property name</param>
        public void OnPropertyChanged(string propertyName)
        {
            if (propertyName is null || propertyName == string.Empty)
            {
                return;
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Match for capture device (input device). True/false set color accordingly (input/output).
        /// </summary>
        /// <param name="dataFlow">The dataflow</param>
        /// <returns>Color</returns>
        protected internal static SolidColorBrush SetBackgroundColor(DataFlow dataFlow)
        {
            bool isCaptureDevice = dataFlow == DataFlow.Capture;

            if (isCaptureDevice)
            {
                return inputDeviceColor;
            }

            return outputDeviceColor;
        }

        /// <summary>
        /// Event logic for Mouse left button up.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="mouseButtonEventArgs">The mouse button event</param>
        protected internal virtual void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            SelectedDeviceControl = this;

            if (MainWindow.SelectedTool == MainWindow.HandSelectedTool)
            {
                startPoint = Mouse.GetPosition(sender as UIElement);
                Panel.SetZIndex(this, 2);
                return;
            }

            if (InitialDeviceControl is null)
            {
                InitialDeviceControl = this;
                return;
            }

            BipartiteDeviceGraph.AddEdge(InitialDeviceControl, this);
            InitialDeviceControl = null;
        }

        /// <summary>
        /// Event logic for Mouse left button up.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="mouseButtonEventArgs">The mouse button event</param>
        protected internal virtual void UserControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            if (MainWindow.SelectedTool == MainWindow.HandSelectedTool)
            {
                Panel.SetZIndex(this, 1);
            }

            SelectedDeviceControl = this;
        }

        /// <summary>
        /// Preview mouse movement if selected tool is not a hand and left button is not pressed.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="mouseEventArgs">The mouse event</param>
        protected internal virtual void UserControl_PreviewMouseMove(object sender, MouseEventArgs mouseEventArgs)
        {
            if (MainWindow.SelectedTool != MainWindow.HandSelectedTool || mouseEventArgs.LeftButton != MouseButtonState.Pressed)
            {
                return;
            }

            var draggableControl = sender as UserControl;
            var parentControl = Parent as Canvas;
            var currentPosition = Mouse.GetPosition(draggableControl);
            double left = currentPosition.X - startPoint.X + Left;

            if (left < 0)
            {
                left = 0;
            }

            double leftAndWidthSum = left + Width;

            if (leftAndWidthSum > parentControl.ActualWidth)
            {
                left = parentControl.ActualWidth - Width;
            }

            double top = currentPosition.Y - startPoint.Y + Top;

            if (top < 0)
            {
                top = 0;
            }

            double topAndHeightSum = top + Height;

            if (topAndHeightSum > parentControl.ActualHeight)
            {
                top = parentControl.ActualHeight - Height;
            }

            Left = left;
            Top = top;
        }
    }
}