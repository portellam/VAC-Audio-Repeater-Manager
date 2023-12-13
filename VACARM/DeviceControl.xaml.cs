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

        public static DeviceControl SelectedControl
        {
            get
            {
                return selectedControl;
            }
            set
            {
                if (selectedControl != null) selectedControl.deviceBackground.Background = (selectedControl.Device.DataFlow == DataFlow.Capture) ? Brushes.LightGreen : Brushes.PaleVioletRed;
                if (value != null) value.deviceBackground.Background = Brushes.AliceBlue;
                selectedControl = value;
            }
        }
        public static DeviceControl InitialLink;

        public BipartiteDeviceGraph Graph { get; }

        public MMDevice Device;

        public DataFlow DataFlow { 
            get
            {
                return Device.DataFlow;
            } 
        }

        public DeviceState State
        {
            get
            {
                return Device.State;
            }
        }

        public string ID
        {
            get
            {
                return Device.ID;
            }
        }

        public string DeviceName
        {
            get
            {
                return Device.FriendlyName;
            }
        }

        private double left;

        public double Left
        {
            get
            {
                return left;
            }
            set
            {
                left = value;
                OnPropertyChanged("Left");
                X = value + Width / 2;
                OnPropertyChanged("X");
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
                OnPropertyChanged("Top");
                Y = value + Height / 2;
                OnPropertyChanged("Y");
            }
        }

        public double X { get; set; }
        public double Y { get; set; }

        private Point start;

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public DeviceControl(MMDevice device, BipartiteDeviceGraph graph)
        {
            InitializeComponent();

            
            Device = device;
            Graph = graph;
            Panel.SetZIndex(this, 1);
            deviceBackground.Background = (device.DataFlow == DataFlow.Capture) ? Brushes.LightGreen : Brushes.PaleVioletRed;
            txtDeviceName.Text = device.FriendlyName;

            ContextMenu = new ContextMenu();
        }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SelectedControl = this;
            if (MainWindow.SelectedTool == "Hand")
            {
                start = Mouse.GetPosition(sender as UIElement);
                Panel.SetZIndex(this, 2);
            }
            else
            {
                if (InitialLink == null) InitialLink = this;
                else
                {
                    Graph.AddEdge(InitialLink, this);
                    InitialLink = null;
                }
            }
        }

        private void UserControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (MainWindow.SelectedTool == "Hand") Panel.SetZIndex(this, 1);
            SelectedControl = this;
        }

        private void UserControl_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (MainWindow.SelectedTool == "Hand")
            {
                var draggableControl = sender as UserControl;
                var parentControl = Parent as Canvas;

                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    var currPos = Mouse.GetPosition(draggableControl);

                    double nx = currPos.X - start.X + Left;
                    if (nx < 0) nx = 0;
                    if (nx + Width > parentControl.ActualWidth) nx = parentControl.ActualWidth - Width;
                    double ny = currPos.Y - start.Y + Top;
                    if (ny < 0) ny = 0;
                    if (ny + Height > parentControl.ActualHeight) ny = parentControl.ActualHeight - Height;
                    Left = nx;
                    Top = ny;
                }
            }
        }
    }
}
