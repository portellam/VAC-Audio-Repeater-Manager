using Microsoft.Win32;
using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VACARM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private const int HOTKEY_ID = 9000;
        private const uint MOD_NONE = 0x0;
        private const uint VK_SCROLL = 0x91;

        public static string SelectedTool;
        public static Canvas GraphMap;

        private bool isrunning;

        public bool isRunning
        {
            get
            {
                return isrunning;
            }
            set
            {
                if (isrunning == value) return;

                if (value) StartEngine();
                else StopEngine();

                startStopTool.Content = new BitmapImage(new Uri($"/icons/" + (value ? "pause" : "play") + ".png", UriKind.RelativeOrAbsolute));

                isrunning = value;
            }
        }

        public static BipartiteDeviceGraph Graph;

        List<string> activeRepeaters = new List<string>();

        public string CurrentDirectoryPath
        {
            get
            {
                return Environment.CurrentDirectory;
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            GraphMap = graphCanvas;
            DefaultData.CheckFile();

            SelectedTool = "Hand";

            if (DefaultData.DefaultGraph == null) Graph = new BipartiteDeviceGraph();
            else Graph = BipartiteDeviceGraph.LoadGraph($@"{DefaultData.SavePath}\{DefaultData.DefaultGraph}");

            isRunning = true;
        }

        private IntPtr _windowHandle;
        private HwndSource _source;
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            _windowHandle = new WindowInteropHelper(this).Handle;
            _source = HwndSource.FromHwnd(_windowHandle);
            _source.AddHook(HwndHook);

            RegisterHotKey(_windowHandle, HOTKEY_ID, MOD_NONE, VK_SCROLL);
        }

        private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            const int WM_HOTKEY = 0x0312;
            switch (msg)
            {
                case WM_HOTKEY:
                    switch (wParam.ToInt32())
                    {
                        case HOTKEY_ID:
                            int vkey = (((int)lParam >> 16) & 0xFFFF);
                            if (vkey == VK_SCROLL)
                            {
                                isRunning = false;
                                isRunning = true;
                            }
                            handled = true;
                            break;
                    }
                    break;
            }
            return IntPtr.Zero;
        }

        private void StartEngine()
        {
            foreach (RepeaterInfo info in Graph.GetEdges())
            {
                if (info.Capture.State != DeviceState.Active || info.Render.State != DeviceState.Active) continue;

                RunCommand(info.ToCommand());
                activeRepeaters.Add(info.WindowName);
            }
        }

        private void StopEngine()
        {
            foreach(string repeater in activeRepeaters)
            {
                RunCommand($"start \"audiorepeater\" \"{DefaultData.RepeaterPath}\" /CloseInstance:\"{repeater}\"");
            }

            activeRepeaters = new List<string>();
        }

        private void toolBarSelect_Click(object sender, RoutedEventArgs e)
        {
            SelectedTool = ((RadioButton)sender).Tag.ToString();
        }

        private void addDevice_Click(object sender, RoutedEventArgs e)
        {
            addDevice();
        }

        private void addDevice()
        {
            AddDeviceDialog dialog = new AddDeviceDialog();
            dialog.Owner = this;

            dialog.ShowDialog();

            if (dialog.Device == null) return;

            DeviceControl control = new DeviceControl(dialog.Device, Graph);
            Graph.AddVertex(control);
            graphCanvas.Children.Add(control);
            Canvas.SetLeft(control, 0);
            Canvas.SetTop(control, 0);
        }

        private void removeDevice_Click(object sender, RoutedEventArgs e)
        {
            removeDevice();
        }

        public void removeDevice()
        {
            if (DeviceControl.SelectedControl == null) return;

            Graph.RemoveVertex(DeviceControl.SelectedControl);
            DeviceControl.SelectedControl = null;
        }

        private void loadGraph_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = DefaultData.SavePath;
            bool? result = fileDialog.ShowDialog();

            if (result == false || result == null) return;

            string file = fileDialog.FileName;
            string fileName = file.Replace($@"{DefaultData.SavePath}\", "");

            if (fileName.Contains("\\") || !fileName.EndsWith(".vac")) return;
            
            GraphMap.Children.Clear();
            try
            {
                Graph = BipartiteDeviceGraph.LoadGraph(file);
                DefaultData.DefaultGraph = fileName;
            }
            catch
            {
                GraphMap.Children.Clear();
                Graph = new BipartiteDeviceGraph();
            }
            GC.Collect();
        }

        private void saveGraph_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.InitialDirectory = DefaultData.SavePath;
            fileDialog.OverwritePrompt = true;
            fileDialog.ValidateNames = true;

            if (fileDialog.ShowDialog() == true)
            {
                string file = fileDialog.FileName.Replace($@"{DefaultData.SavePath}\", "");

                if (file.Contains("\\")) return;

                Graph.SaveGraph(file);
                DefaultData.DefaultGraph = file;
            }
        }

        private void restart_Click(object sender, RoutedEventArgs e)
        {
            isRunning = false;
            isRunning = true;
        }

        private void startStop_Click(object sender, RoutedEventArgs e)
        {
            isRunning = !isRunning;
        }

        private void graphCanvas_MouseLeftButtonClick(object sender, MouseButtonEventArgs e)
        {
            DeviceControl.SelectedControl = null;
        }

        public void RunCommand(string cmd)
        {
            Process process = new Process();
            ProcessStartInfo info = new ProcessStartInfo();
            info.WindowStyle = ProcessWindowStyle.Hidden;
            info.FileName = "cmd.exe";
            info.Arguments = "/C " + cmd;
            process.StartInfo = info;
            process.Start();
        }

        /* 
         T = add device
         Delete = remove device
         H = hand tool
         L = link tool
         R = restart engine
         P = start/stop engine
        */
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.T:
                    addDevice();
                    break;
                case Key.Delete:
                    removeDevice();
                    break;
                case Key.H:
                    handTool.IsChecked = true;
                    SelectedTool = handTool.Tag.ToString();
                    break;
                case Key.L:
                    linkTool.IsChecked = true;
                    SelectedTool = linkTool.Tag.ToString();
                    break;
                case Key.R:
                    isRunning = false;
                    isRunning = true;
                    break;
                case Key.P:
                    isRunning = !isRunning;
                    break;
            }

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            StopEngine();
        }


    }
}
