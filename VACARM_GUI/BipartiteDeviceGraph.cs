using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Shapes;

namespace VACARM_GUI
{
    public class BipartiteDeviceGraph
    {
        private Dictionary<DeviceControl, Dictionary<DeviceControl, RepeaterInfo>> Edge;

        /// <summary>
        /// Constructor
        /// </summary>
        public BipartiteDeviceGraph()
        {
            Edge = new Dictionary<DeviceControl, Dictionary<DeviceControl, RepeaterInfo>>();
        }

        /// <summary>
        /// Adds one vertex between two devices.
        /// </summary>
        /// <param name="deviceControl1">The first device</param>
        /// <param name="deviceControl2">The second device</param>
        public void AddEdge(DeviceControl deviceControl1, DeviceControl deviceControl2)
        {
            if (Edge[deviceControl1].ContainsKey(deviceControl2) || deviceControl1.DataFlow == deviceControl2.DataFlow)
            {
                return;
            }

            DeviceControl captureDeviceControl, renderDeviceControl;

            if (deviceControl1.DataFlow == DataFlow.Capture)
            {
                captureDeviceControl = deviceControl1;
                renderDeviceControl = deviceControl2;
            }
            else
            {
                captureDeviceControl = deviceControl2;
                renderDeviceControl = deviceControl1;
            }

            RepeaterInfo repeaterInfo = new RepeaterInfo(captureDeviceControl, renderDeviceControl, this);
            AddEdge(deviceControl1, deviceControl2, repeaterInfo);
        }

        /// <summary>
        /// Adds device to graph.
        /// </summary>
        /// <param name="deviceControl">The device</param>
        public void AddVertex(DeviceControl deviceControl)
        {
            if (deviceControl is null || Edge.ContainsKey(deviceControl))
            {
                return;
            }

            Edge[deviceControl] = new Dictionary<DeviceControl, RepeaterInfo>();
        }

        /// <summary>
        /// Load graph from given file. If it does not exist, load an empty graph.
        /// </summary>
        /// <param name="file">The file</param>
        /// <returns>The graph.</returns>
        public static BipartiteDeviceGraph LoadGraph(string file)
        {
            BipartiteDeviceGraph bipartiteDeviceGraph = new BipartiteDeviceGraph();

            if (!File.Exists(file))
            {
                return bipartiteDeviceGraph;
            }

            StreamReader streamReader = new StreamReader(file);

            if (!int.TryParse(streamReader.ReadLine(), out int vertexCount))
            {
                return bipartiteDeviceGraph;
            }

            DeviceControl[] deviceControlList = GetListOfVertices(bipartiteDeviceGraph, streamReader);
            bipartiteDeviceGraph = GetGraphOfVertices(bipartiteDeviceGraph, deviceControlList, streamReader);
            streamReader.Close();
            return bipartiteDeviceGraph;
        }

        /// <summary>
        /// Gets adjacent vertices.
        /// </summary>
        /// <param name="deviceControl">The device</param>
        /// <returns>The vertices.</returns>
        public Dictionary<DeviceControl, RepeaterInfo> GetAdjacent(DeviceControl deviceControl)
        {
            return Edge[deviceControl];
        }

        /// <summary>
        /// Get edges from vertices and adjacent devices.
        /// </summary>
        /// <returns>The edges.</returns>
        public HashSet<RepeaterInfo> GetEdges()
        {
            HashSet<RepeaterInfo> edgeRepeaterInfoHashSet = new HashSet<RepeaterInfo>();

            foreach (DeviceControl vertexDeviceControl in Edge.Keys)
            {
                foreach (DeviceControl adjacentDeviceControl in Edge[vertexDeviceControl].Keys)
                {
                    edgeRepeaterInfoHashSet.Add(Edge[vertexDeviceControl][adjacentDeviceControl]);
                }
            }

            return edgeRepeaterInfoHashSet;
        }

        /// <summary>
        /// Remove one adjacent vertex between two devices.
        /// </summary>
        /// <param name="deviceControl1">The first device</param>
        /// <param name="deviceControl2">The second device</param>
        public void RemoveEdge(DeviceControl deviceControl1, DeviceControl deviceControl2)
        {
            if (deviceControl1 is null || deviceControl2 is null)
            {
                return;
            }

            MainWindow.GraphMapCanvas.Children.Remove(Edge[deviceControl1][deviceControl2].Link);
            Edge[deviceControl1].Remove(deviceControl2);
            Edge[deviceControl2].Remove(deviceControl1);
        }

        /// <summary>
        /// Removes vertex and relationship between device and adjacent devices on graph.
        /// </summary>
        /// <param name="deviceControl">The device</param>
        public void RemoveVertex(DeviceControl deviceControl)
        {
            if (deviceControl is null || Edge is null || !Edge.ContainsKey(deviceControl) || Edge[deviceControl] is null)
            {
                return;
            }

            List<DeviceControl> adjacentDeviceControlList = Edge[deviceControl].Keys.ToList();

            foreach (DeviceControl adjacentDeviceControl in adjacentDeviceControlList)
            {
                Edge[adjacentDeviceControl].Remove(deviceControl);
                Line link = Edge[deviceControl][adjacentDeviceControl].Link;
                MainWindow.GraphMapCanvas.Children.Remove(link);
            }

            Edge.Remove(deviceControl);
            MainWindow.GraphMapCanvas.Children.Remove(deviceControl);
        }

        /// <summary>
        /// Save graph to given file.
        /// </summary>
        /// <param name="file">The file</param>
        public void SaveGraph(string file)
        {
            if (file is null)
            {
                //TODO: add logger, output message to user, then return
                return;
            }

            if (!file.EndsWith(DefaultData.FileExtension))
            {
                file += DefaultData.FileExtension;
            }

            string path = $@"{DefaultData.SavePath}\{file}";
            StreamWriter streamWriter = new StreamWriter(path);
            List<DeviceControl> vertexDeviceControlList = Edge.Keys.ToList();
            string vertexCount = vertexDeviceControlList.Count.ToString();
            streamWriter.WriteLine(vertexCount);

            Dictionary<DeviceControl, int> deviceControlIdDictionary = new Dictionary<DeviceControl, int>();
            int edgesCount = 0;
            int secondIndex = 1;

            foreach (DeviceControl vertexDeviceControl in vertexDeviceControlList)
            {
                streamWriter.WriteLine(vertexDeviceControl.ID);
                streamWriter.WriteLine($"{vertexDeviceControl.Left} {vertexDeviceControl.Top}");
                deviceControlIdDictionary[vertexDeviceControl] = secondIndex;
                edgesCount += Edge[vertexDeviceControl].Count;
            }

            WriteHalfOfEdgesCountToFile(edgesCount, streamWriter);
            WriteEdgeRepeaterInfoToFile(deviceControlIdDictionary, streamWriter);
            streamWriter.Close();
        }

        /// <summary>
        /// Helper method for AddEdge.
        /// </summary>
        /// <param name="deviceControl1">The first device</param>
        /// <param name="deviceControl2">The second device</param>
        /// <param name="repeaterInfo">The repeater info</param>
        protected internal virtual void AddEdge(DeviceControl deviceControl1, DeviceControl deviceControl2, RepeaterInfo repeaterInfo)
        {
            Edge[deviceControl1].Add(deviceControl2, repeaterInfo);
            Edge[deviceControl2].Add(deviceControl1, repeaterInfo);
            MainWindow.GraphMapCanvas.Children.Add(repeaterInfo.Link);
        }

        /// <summary>
        /// Get list of vertices.
        /// </summary>
        /// <param name="bipartiteDeviceGraph">The graph</param>
        /// <param name="streamReader">The text stream</param>
        /// <returns>The graph.</returns>
        protected internal static DeviceControl[] GetListOfVertices(BipartiteDeviceGraph bipartiteDeviceGraph, StreamReader streamReader)
        {
            int vertexCount = int.Parse(streamReader.ToString());
            DeviceControl[] deviceControlList = new DeviceControl[vertexCount];

            if (vertexCount <= 1)
            {
                return deviceControlList;
            }

            Dictionary<string, MMDevice> mMDeviceById = new Dictionary<string, MMDevice>();
            MMDeviceCollection mMDeviceCollection = new MMDeviceEnumerator().EnumerateAudioEndPoints(DataFlow.All, DeviceState.All);

            foreach (MMDevice mMDevice in mMDeviceCollection)
            {
                mMDeviceById[mMDevice.ID] = mMDevice;
            }

            for (int i = 0; i < vertexCount; i++)
            {
                try
                {
                    MMDevice mMDevice = mMDeviceById[streamReader.ReadLine()];
                    double[] position = streamReader.ReadLine().Split().Select(x => double.Parse(x)).ToArray();
                    DeviceControl deviceControl = new DeviceControl(mMDevice, bipartiteDeviceGraph);
                    deviceControl.Left = position[0];
                    deviceControl.Top = position[1];
                    MainWindow.GraphMapCanvas.Children.Add(deviceControl);
                    bipartiteDeviceGraph.AddVertex(deviceControl);
                    deviceControlList[i] = deviceControl;
                }
                catch
                {
                    deviceControlList[i] = null;
                }
            }

            return deviceControlList;
        }

        /// <summary>
        /// Get graph filled with vertices.
        /// </summary>
        /// <param name="bipartiteDeviceGraph">The graph</param>
        /// <param name="deviceControlList">The devices</param>
        /// <param name="streamReader">The text stream</param>
        /// <returns>The graph.</returns>
        protected internal static BipartiteDeviceGraph GetGraphOfVertices(BipartiteDeviceGraph bipartiteDeviceGraph, DeviceControl[] deviceControlList, StreamReader streamReader)
        {
            int vertexCount = int.Parse(streamReader.ToString());

            if (vertexCount <= 1)
            {
                return bipartiteDeviceGraph;
            }

            int bufferCount = 8;

            for (int i = 0; i < vertexCount; i++)
            {
                int[] adjacentList = streamReader.ReadLine().Split().Select(x => int.Parse(x)).ToArray();
                DeviceControl captureDeviceControl = deviceControlList[adjacentList[0]];
                DeviceControl renderDeviceControl = deviceControlList[adjacentList[1]];

                if (captureDeviceControl is null || renderDeviceControl is null)
                {
                    continue;
                }

                List<string> data = new List<string>();

                for (int j = 0; j < bufferCount; j++)
                {
                    data.Add(streamReader.ReadLine());
                }

                RepeaterInfo repeaterInfo = new RepeaterInfo(captureDeviceControl, renderDeviceControl, bipartiteDeviceGraph);
                repeaterInfo.SetData(data);
                bipartiteDeviceGraph.AddEdge(captureDeviceControl, renderDeviceControl, repeaterInfo);
            }

            return bipartiteDeviceGraph;
        }

        /// <summary>
        /// Writes edges to file.
        /// </summary>
        /// <param name="deviceControlIdDictionary">The device dictionary</param>
        /// <param name="streamWriter">The stream writer</param>
        protected internal virtual void WriteEdgeRepeaterInfoToFile(Dictionary<DeviceControl, int> deviceControlIdDictionary, StreamWriter streamWriter)
        {
            HashSet<RepeaterInfo> edgeRepeaterInfoHashSet = GetEdges();

            if (edgeRepeaterInfoHashSet != null)
            {
                return;
            }

            foreach (RepeaterInfo edgeRepeaterInfo in edgeRepeaterInfoHashSet)
            {
                string indexOfDevices = $"{deviceControlIdDictionary[edgeRepeaterInfo.CaptureDeviceControl]} {deviceControlIdDictionary[edgeRepeaterInfo.RenderDeviceControl]}";
                string repeaterInfo = edgeRepeaterInfo.ToSaveData();
                streamWriter.WriteLine($"{indexOfDevices}\n{repeaterInfo}");
            }
        }

        /// <summary>
        /// Writes half of edges' count to file.
        /// </summary>
        /// <param name="edgesCount">The edges count</param>
        /// <param name="streamWriter">The stream writer</param>
        protected internal virtual void WriteHalfOfEdgesCountToFile(int edgesCount, StreamWriter streamWriter)
        {
            if (edgesCount < 0 || streamWriter is null)
            {
                return;
            }

            string halfOfEdgesCount = $"{edgesCount / 2}";
            streamWriter.WriteLine(halfOfEdgesCount);
        }
    }
}