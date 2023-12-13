using Microsoft.Win32;
using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace VACARM
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
    {
        private const int HOTKEY_ID = 9000;
        private const uint MOD_NONE = 0x0;
        private const uint VK_SCROLL = 0x91;
        private const string libraryToImport = "user32.dll";
        private const string terminalExecutable = "cmd.exe";

        private bool isRunning;
        private HwndSource _source;
        private IntPtr _windowHandle;
        private List<string> activeRepeaters = new List<string>();

        [DllImport(libraryToImport)]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport(libraryToImport)]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        public bool IsRunning
        {
            get
            {
                return isRunning;
            }
            set
            {
                if (isRunning == value)
                {
                    return;
                }

                if (value)
                {
                    StartEngine();
                }
                else
                {
                    StopEngine();
                }

                const string imageFileExtension = ".png";
                const string imagesPath = "/icons/";
                const string pause = "pause";
                const string play = "play";

                startStopTool.Content = new BitmapImage(new Uri(imagesPath + (value ? pause : play) + imageFileExtension, UriKind.RelativeOrAbsolute));

                isRunning = value;
            }
        }

        public string CurrentDirectoryPath
        {
            get
            {
                return Environment.CurrentDirectory;
            }
        }

        public static BipartiteDeviceGraph Graph;
        public static Canvas GraphMap;
        public static string SelectedTool;

        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            GraphMap = graphCanvas;
            DefaultData.CheckFile();

            SelectedTool = "Hand";

            if (DefaultData.DefaultGraph == null) Graph = new BipartiteDeviceGraph();
            else Graph = BipartiteDeviceGraph.LoadGraph($@"{DefaultData.SavePath}\{DefaultData.DefaultGraph}");

            IsRunning = true;
        }

        /// <summary>
        /// Adds new device to existing graph.
        /// </summary>
        private void AddDevice()
        {
            AddDeviceDialog dialog = new AddDeviceDialog();
            dialog.Owner = this;
            dialog.ShowDialog();

            if (dialog.Device == null)
            {
                return;
            }

            DeviceControl control = new DeviceControl(dialog.Device, Graph);
            Graph.AddVertex(control);
            graphCanvas.Children.Add(control);
            Canvas.SetLeft(control, 0);
            Canvas.SetTop(control, 0);
        }

        /// <summary>
        /// Click event for AddDevice.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="routedEventArgs">The routed event arguments</param>
        private void addDevice_Click(object sender, RoutedEventArgs routedEventArgs)
        {
            AddDevice();
        }

        /// <summary>
        /// Loads existing graph on click event.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="routedEventArgs">The routed event arguments</param>
        private void loadGraph_Click(object sender, RoutedEventArgs routedEventArgs)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = DefaultData.SavePath;
            bool? result = fileDialog.ShowDialog();

            if (result == false || result == null)
            {
                return;
            }

            string file = fileDialog.FileName;
            string fileName = file.Replace($@"{DefaultData.SavePath}\", "");

            if (fileName.Contains("\\") || !fileName.EndsWith(DefaultData.FileExtension))
            {
                return;
            }

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

        /// <summary>
        /// Click event: Unselects previously selected device on existing graph.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="mouseButtonEventArgs">The mouse button event arguments</param>
        private void graphCanvas_MouseLeftButtonClick(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            DeviceControl.SelectedControl = null;
        }

        private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)   //TODO: explain operation of this method.
        {
            const int WM_HOTKEY = 0x0312;

            switch (msg)
            {
                case WM_HOTKEY:
                    HwndHookIsMatchForWParam(wParam, lParam, ref handled);
                    break;
            }

            return IntPtr.Zero;
        }

        private void HwndHookIsMatchForWParam(IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (wParam.ToInt32())
            {
                case HOTKEY_ID:
                    HwndHookIsWParamEqualToHotkeyId(wParam, lParam, ref handled);
                    return;
            }
        }

        private void HwndHookIsWParamEqualToHotkeyId(IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            int vkey = (((int)lParam >> 16) & 0xFFFF);

            if (vkey == VK_SCROLL)
            {
                IsRunning = false;
                IsRunning = true;
            }

            handled = true;
        }
        
        /// <summary>
        /// Registers hotkey(s) to Window hook.
        /// </summary>
        /// <param name="eventArgs">The event arguments</param>
        protected override void OnSourceInitialized(EventArgs eventArgs)
        {
            base.OnSourceInitialized(eventArgs);

            _windowHandle = new WindowInteropHelper(this).Handle;
            _source = HwndSource.FromHwnd(_windowHandle);
            _source.AddHook(HwndHook);

            RegisterHotKey(_windowHandle, HOTKEY_ID, MOD_NONE, VK_SCROLL);
        }

        /// <summary>
        /// Removes device (vertex) if it is not null..
        /// </summary>
        private void RemoveDevice()
        {
            if (DeviceControl.SelectedControl == null)
            {
                return;
            }

            Graph.RemoveVertex(DeviceControl.SelectedControl);
            DeviceControl.SelectedControl = null;
        }

        /// <summary>
        /// Click event for RemoveDevice.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="routedEventArgs">The routed event arguments</param>
        private void removeDevice_Click(object sender, RoutedEventArgs routedEventArgs)
        {
            RemoveDevice();
        }

        /// <summary>
        /// Clears list of active repeaters.
        /// </summary>
        private void ResetActiveRepeaters()
        {
            activeRepeaters = new List<string>();
        }

        /// <summary>
        /// Sets IsRunning to false, then true.
        /// </summary>
        private void Restart()
        {
            IsRunning = false;
            IsRunning = true;
        }

        /// <summary>
        /// Click event for Restart.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="routedEventArgs">The routed event arguments</param>
        private void restart_Click(object sender, RoutedEventArgs routedEventArgs)
        {
            Restart();
        }

        /// <summary>
        /// Run given command in Windows terminal.
        /// </summary>
        /// <param name="command">The command</param>
        private void RunCommand(string command)
        {
            Process process = new Process();
            ProcessStartInfo info = new ProcessStartInfo();
            info.WindowStyle = ProcessWindowStyle.Hidden;
            info.FileName = terminalExecutable;
            info.Arguments = "/C " + command;
            process.StartInfo = info;
            process.Start();
        }

        /// <summary>
        /// Save graph to file if graph is edited or not default.
        /// </summary>
        /// <param name="saveFileDialog">The save file dialog</param>
        private void SaveEditedGraph(SaveFileDialog saveFileDialog)
        {
            string file = saveFileDialog.FileName.Replace($@"{DefaultData.SavePath}\", "");

            if (file.Contains("\\"))
            {
                return;
            }

            Graph.SaveGraph(file);
            DefaultData.DefaultGraph = file;
        }

        /// <summary>
        /// Click event for SaveGraph.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="routedEventArgs">The routed event arguments</param>
        private void saveGraph_Click(object sender, RoutedEventArgs routedEventArgs)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = DefaultData.SavePath;
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.ValidateNames = true;

            if (saveFileDialog.ShowDialog() == true)
            {
                SaveEditedGraph(saveFileDialog);
            }
        }

        /// <summary>
        /// Click event for StartStop.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="routedEventArgs">The routed event arguments</param>
        private void startStop_Click(object sender, RoutedEventArgs routedEventArgs)
        {
            StartStop();
        }

        /// <summary>
        /// Toggles IsRunning value.
        /// </summary>
        private void StartStop()
        {
            IsRunning = !IsRunning;
        }

        /// <summary>
        /// Starts engine of all active repeaters.
        /// </summary>
        private void StartEngine()
        {
            foreach (RepeaterInfo repeaterInfo in Graph.GetEdges())
            {
                StartEngineOfActiveRepeater(repeaterInfo);
            }
        }

        /// <summary>
        /// If repeater is active, start engine. False, do nothing.
        /// </summary>
        /// <param name="repeaterInfo">The repeater info</param>
        private void StartEngineOfActiveRepeater(RepeaterInfo repeaterInfo)
        {
            if (repeaterInfo.Capture.State != DeviceState.Active || repeaterInfo.Render.State != DeviceState.Active)
            {
                return;
            }

            RunCommand(repeaterInfo.ToCommand());
            activeRepeaters.Add(repeaterInfo.WindowName);
        }

        /// <summary>
        /// Stops all active repeaters, and start default repeater.
        /// </summary>
        private void StopEngine()
        {
            string commandToStopActiveRepeaterAndStartDefaultRepeater = $"start \"audiorepeater\" \"{DefaultData.RepeaterPath}\" /CloseInstance:";

            foreach (string activeRepeater in activeRepeaters)
            {
                string activeRepeaterCommand = $"{commandToStopActiveRepeaterAndStartDefaultRepeater}\"{activeRepeater}\"";
                RunCommand(activeRepeaterCommand);
            }

            ResetActiveRepeaters();
        }

        /// <summary>
        /// Click event for toolbar selection.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="routedEventArgs">The routed event arguments</param>
        private void toolBarSelect_Click(object sender, RoutedEventArgs routedEventArgs)
        {
            SelectedTool = ((RadioButton)sender).Tag.ToString();
        }

        /// <summary>
        /// Window shortcuts given marco input (Windows Key + key).
        ///
        /// Examples:
        /// T = add device
        /// Delete = remove device
        ///  H = hand tool
        /// L = link tool
        /// R = restart engine
        /// P = start/stop engine
        /// 
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="keyEventArgs">The key event arguments</param>
        private void window_KeyUp(object sender, KeyEventArgs keyEventArgs)
        {
            switch (keyEventArgs.Key)
            {
                case Key.T:
                    AddDevice();
                    break;

                case Key.Delete:
                    RemoveDevice();
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
                    Restart();
                    break;

                case Key.P:
                    StartStop();
                    break;
            }
        }

        /// <summary>
        /// Stops engine when window closes.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="cancelEventArgs">The cancel event arguments</param>
        private void window_Closing(object sender, System.ComponentModel.CancelEventArgs cancelEventArgs)
        {
            StopEngine();
        }
    }
}