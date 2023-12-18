﻿using Microsoft.Win32;
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
        private List<string> activeRepeaterList = new List<string>();

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

        public static BipartiteDeviceGraph BipartiteDeviceGraph;
        public static Canvas GraphMapCanvas;
        public static string SelectedTool;

        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            GraphMapCanvas = graphCanvas;
            DefaultData.CheckFile();

            SelectedTool = "Hand";

            if (DefaultData.DefaultGraph is null) BipartiteDeviceGraph = new BipartiteDeviceGraph();
            else BipartiteDeviceGraph = BipartiteDeviceGraph.LoadGraph($@"{DefaultData.SavePath}\{DefaultData.DefaultGraph}");

            IsRunning = true;
        }

        /// <summary>
        /// Adds new device to existing graph.
        /// </summary>
        protected internal virtual void AddDevice()
        {
            AddDeviceDialog dialog = new AddDeviceDialog();
            dialog.Owner = this;
            dialog.ShowDialog();

            if (dialog.mMDevice is null)
            {
                return;
            }

            DeviceControl deviceControl = new DeviceControl(dialog.mMDevice, BipartiteDeviceGraph);
            BipartiteDeviceGraph.AddVertex(deviceControl
                );
            graphCanvas.Children.Add(deviceControl);
            Canvas.SetLeft(deviceControl, 0);
            Canvas.SetTop(deviceControl, 0);
        }

        /// <summary>
        /// Click event for AddDevice.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="routedEventArgs">The routed event</param>
        protected internal virtual void AddDevice_Click(object sender, RoutedEventArgs routedEventArgs)
        {
            AddDevice();
        }

        /// <summary>
        /// Loads existing graph on click event.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="routedEventArgs">The routed event</param>
        protected internal virtual void LoadGraph_Click(object sender, RoutedEventArgs routedEventArgs)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = DefaultData.SavePath;
            bool? result = fileDialog.ShowDialog();

            if (result == false || result is null)
            {
                return;
            }

            string file = fileDialog.FileName;
            string fileName = file.Replace($@"{DefaultData.SavePath}\", "");

            if (fileName.Contains("\\") || !fileName.EndsWith(DefaultData.FileExtension))
            {
                return;
            }

            GraphMapCanvas.Children.Clear();

            try
            {
                BipartiteDeviceGraph = BipartiteDeviceGraph.LoadGraph(file);
                DefaultData.DefaultGraph = fileName;
            }
            catch
            {
                GraphMapCanvas.Children.Clear();
                BipartiteDeviceGraph = new BipartiteDeviceGraph();
            }

            GC.Collect();
        }

        /// <summary>
        /// Click event: Unselects previously selected device on existing graph.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="mouseButtonEventArgs">The mouse button event</param>
        protected internal virtual void GraphCanvas_MouseLeftButtonClick(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            DeviceControl.SelectedDeviceControl = null;
        }

        /// <summary>
        /// Return window handle hook. Return int pointer given match for hotkey.
        /// </summary>
        /// <param name="hwnd">Window handle pointer</param>
        /// <param name="message">The message</param>
        /// <param name="widthParam">Width parameter</param>
        /// <param name="lengthParam">Length parameter</param>
        /// <param name="isHandled">Is window handled</param>
        /// <returns>Int pointer</returns>
        protected internal virtual IntPtr HwndHook(IntPtr hwnd, int message, IntPtr widthParam, IntPtr lengthParam, ref bool isHandled)   //TODO: explain operation of this method.
        {
            const int WM_HOTKEY = 0x0312;

            switch (message)
            {
                case WM_HOTKEY:
                    HwndHookIsMatchForWParam(widthParam, lengthParam, ref isHandled);
                    break;
            }

            return IntPtr.Zero;
        }

        /// <summary>
        /// Match for hotkey. If true, set handled to true.
        /// </summary>
        /// <param name="widthParam">Width parameter</param>
        /// <param name="lengthParam">Length parameter</param>
        /// <param name="isHandled">Is window handled</param>
        protected internal virtual void HwndHookIsMatchForWParam(IntPtr widthParam, IntPtr lengthParam, ref bool isHandled)
        {
            switch (widthParam.ToInt32())
            {
                case HOTKEY_ID:
                    SetRefOfIsHandledToTrueAndRestartIfScrollKeyId(widthParam, lengthParam, ref isHandled);
                    return;
            }
        }

        /// <summary>
        /// If given key is pressed, Restart. Always set handled to true.
        /// </summary>
        /// <param name="widthParam">Width parameter</param>
        /// <param name="lengthParam">Length parameter</param>
        /// <param name="isHandled">Is window handled</param>
        protected internal virtual void SetRefOfIsHandledToTrueAndRestartIfScrollKeyId(IntPtr widthParam, IntPtr lengthParam, ref bool isHandled)
        {
            int vkey = (((int)lengthParam >> 16) & 0xFFFF);

            if (vkey == VK_SCROLL)
            {
                Restart();
            }

            isHandled = true;
        }
        
        /// <summary>
        /// Registers hotkey(s) to Window hook.
        /// </summary>
        /// <param name="eventArgs">The event</param>
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
        protected internal virtual void RemoveDevice()
        {
            if (DeviceControl.SelectedDeviceControl is null)
            {
                return;
            }

            BipartiteDeviceGraph.RemoveVertex(DeviceControl.SelectedDeviceControl);
            DeviceControl.SelectedDeviceControl = null;
        }

        /// <summary>
        /// Click event for RemoveDevice.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="routedEventArgs">The routed event</param>
        protected internal virtual void RemoveDevice_Click(object sender, RoutedEventArgs routedEventArgs)
        {
            RemoveDevice();
        }

        /// <summary>
        /// Clears list of active repeaters.
        /// </summary>
        protected internal virtual void ResetActiveRepeaters()
        {
            activeRepeaterList = new List<string>();
        }

        /// <summary>
        /// Sets IsRunning to false, then true.
        /// </summary>
        protected internal virtual void Restart()
        {
            IsRunning = false;
            IsRunning = true;
        }

        /// <summary>
        /// Click event for Restart.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="routedEventArgs">The routed event</param>
        protected internal virtual void Restart_Click(object sender, RoutedEventArgs routedEventArgs)
        {
            Restart();
        }

        /// <summary>
        /// Run given command in Windows terminal.
        /// </summary>
        /// <param name="command">The command</param>
        protected internal virtual void RunCommand(string command)
        {
            if (command is null || command == String.Empty)
            {
                return;
            }

            Process process = new Process();
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            processStartInfo.FileName = terminalExecutable;
            processStartInfo.Arguments = "/C " + command;
            process.StartInfo = processStartInfo;
            process.Start();
        }
        /// <summary>
        /// Save graph to file if graph is edited or not default.
        /// </summary>
        /// <param name="saveFileDialog">The save file dialog</param>
        protected internal virtual void SaveEditedGraph(SaveFileDialog saveFileDialog)
        {
			string file = saveFileDialog.FileName.Replace($@"{DefaultData.SavePath}\", "");

            if (file.Contains("\\"))
            {
                return;
            }

            BipartiteDeviceGraph.SaveGraph(file);
            DefaultData.DefaultGraph = file;
        }

        /// <summary>
        /// Click event for SaveGraph.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="routedEventArgs">The routed event</param>
        protected internal virtual void SaveGraph_Click(object sender, RoutedEventArgs routedEventArgs)
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
        /// <param name="sender">The sender</param>
        /// <param name="routedEventArgs">The routed event</param>
        protected internal virtual void StartStop_Click(object sender, RoutedEventArgs routedEventArgs)
        {
            StartStop();
        }

        /// <summary>
        /// Toggles IsRunning value.
        /// </summary>
        protected internal virtual void StartStop()
        {
            IsRunning = !IsRunning;
        }

        /// <summary>
        /// Starts engine of all active repeaters.
        /// </summary>
        protected internal virtual void StartEngine()
        {
            foreach (RepeaterInfo repeaterInfo in BipartiteDeviceGraph.GetEdges())
            {
                StartEngineOfActiveRepeater(repeaterInfo);
            }
        }
        /// <summary>
        /// If repeater is active, start engine. False, do nothing.
        /// </summary>
        /// <param name="repeaterInfo">The repeater info</param>
        protected internal virtual void StartEngineOfActiveRepeater(RepeaterInfo repeaterInfo)
        {
            if (repeaterInfo.CaptureDeviceControl.DeviceState != DeviceState.Active || repeaterInfo.RenderDeviceControl.DeviceState != DeviceState.Active)
            {
                return;
            }

            RunCommand(repeaterInfo.ToCommand());
            activeRepeaterList.Add(repeaterInfo.WindowName);
        }
 /// <summary>
        /// Stops all active repeaters, and start default repeater.
        /// </summary>
        protected internal virtual void StopEngine()
        {
            if (activeRepeaterList is null || activeRepeaterList.Count == 0)
            {
                return;
            }

            string commandToStopActiveRepeaterAndStartDefaultRepeater = $"start \"audiorepeater\" \"{DefaultData.RepeaterPath}\" /CloseInstance:";

            foreach (string activeRepeater in activeRepeaterList)
            {
                string activeRepeaterCommand = $"{commandToStopActiveRepeaterAndStartDefaultRepeater}\"{activeRepeater}\"";
                RunCommand(activeRepeaterCommand);
            }

            ResetActiveRepeaters();
        }

        /// <summary>
        /// Click event for toolbar selection.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="routedEventArgs">The routed event</param>
        protected internal virtual void ToolBarSelect_Click(object sender, RoutedEventArgs routedEventArgs)
        {
            if (sender is null || (string)sender == String.Empty)
            {
                return;
            }

            SelectedTool = ((RadioButton)sender).Tag.ToString();
        }
        /// <summary>
        /// Window shortcuts given marco input (Windows Key + key).
        ///
        /// Examples:
        /// T       = add device
        /// Delete  = remove device
        /// H       = hand tool
        /// L       = link tool
        /// R       = restart engine
        /// P       = start/stop engine
        /// 
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="keyEventArgs">The key event</param>
        protected internal virtual void Window_KeyUp(object sender, KeyEventArgs keyEventArgs)
        {
            if (keyEventArgs is null)
            {
                return;
            }

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
        /// <param name="sender">The sender</param>
        /// <param name="cancelEventArgs">The cancel event</param>
        protected internal virtual void Window_Closing(object sender, System.ComponentModel.CancelEventArgs cancelEventArgs)
        {
            StopEngine();
        }
    }
}