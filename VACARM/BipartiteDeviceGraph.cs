using NAudio.CoreAudioApi;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VACARM
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
            if (deviceControl1 is null)
            {
                throw new System.ArgumentNullException(nameof(deviceControl1));
            }

            if (deviceControl2 is null)
            {
                throw new System.ArgumentNullException(nameof(deviceControl2));
            }

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
        /// Helper method for AddEdge.
        /// </summary>
        /// <param name="deviceControl1">The first device</param>
        /// <param name="deviceControl2">The second device</param>
        /// <param name="repeaterInfo">The repeater info</param>
        protected internal virtual void AddEdge(DeviceControl deviceControl1, DeviceControl deviceControl2, RepeaterInfo repeaterInfo)
        {
            if (deviceControl1 is null)
            {
                throw new System.ArgumentNullException(nameof(deviceControl1));
            }

            if (deviceControl2 is null)
            {
                throw new System.ArgumentNullException(nameof(deviceControl2));
            }

            if (deviceControl1 is null)
            {
                throw new System.ArgumentNullException(nameof(repeaterInfo));
            }

            Edge[deviceControl1].Add(deviceControl2, repeaterInfo);
            Edge[deviceControl2].Add(deviceControl1, repeaterInfo);
            MainWindow.GraphMapCanvas.Children.Add(repeaterInfo.Link);
        }

        /// <summary>
        /// Remove one adjacent vertex between two devices.
        /// </summary>
        /// <param name="deviceControl1">The first device</param>
        /// <param name="deviceControl2">The second device</param>
        public void RemoveEdge(DeviceControl deviceControl1, DeviceControl deviceControl2)
        {
            MainWindow.GraphMapCanvas.Children.Remove(Edge[deviceControl1][deviceControl2].Link);
            Edge[deviceControl1].Remove(deviceControl2);
            Edge[deviceControl2].Remove(deviceControl1);
        }

        /// <summary>
        /// Gets adjacent vertices.
        /// </summary>
        /// <param name="deviceControl">The device</param>
        /// <returns>The vertices.</returns>
        public Dictionary<DeviceControl, RepeaterInfo> GetAdjacent(DeviceControl deviceControl)
        {
            if (deviceControl is null)
            {
                throw new System.ArgumentNullException(nameof(deviceControl));
            }

            if (!Edge.ContainsKey(deviceControl))
            {
                throw new System.ArgumentNullException($"{Edge} does not contain key '{deviceControl}'.");
            }

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
        /// Load graph from given file.
        /// </summary>
        /// <param name="fileName">The file</param>
        /// <returns>The graph.</returns>
        public static BipartiteDeviceGraph LoadGraph(string fileName)
        {
            BipartiteDeviceGraph bipartiteDeviceGraph = new BipartiteDeviceGraph();
            StreamReader streamReader = new StreamReader(fileName);

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
        /// Get list of vertices.
        /// </summary>
        /// <param name="bipartiteDeviceGraph">The graph</param>
        /// <param name="streamReader">The text stream</param>
        /// <returns>The graph.</returns>
        private static DeviceControl[] GetListOfVertices(BipartiteDeviceGraph bipartiteDeviceGraph, StreamReader streamReader)
        {
            if (bipartiteDeviceGraph is null)
            {
                throw new System.ArgumentNullException(nameof(bipartiteDeviceGraph));
            }

            if (streamReader is null)
            {
                throw new System.ArgumentNullException(nameof(streamReader));
            }

            int vertexCount = int.Parse(streamReader.ToString());
            DeviceControl[] deviceControlList = new DeviceControl[vertexCount];
            Dictionary<string, MMDevice> mMDeviceById = new Dictionary<string, MMDevice>();

            foreach (MMDevice mMDevice in new MMDeviceEnumerator().EnumerateAudioEndPoints(DataFlow.All, DeviceState.All))
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
        private static BipartiteDeviceGraph GetGraphOfVertices(BipartiteDeviceGraph bipartiteDeviceGraph, DeviceControl[] deviceControlList, StreamReader streamReader)
        {
            if (bipartiteDeviceGraph is null)
            {
                throw new System.ArgumentNullException(nameof(bipartiteDeviceGraph));
            }

            if (deviceControlList is null)
            {
                throw new System.ArgumentNullException(nameof(deviceControlList));
            }

            if (streamReader is null)
            {
                throw new System.ArgumentNullException(nameof(streamReader));
            }

            int vertexCount = int.Parse(streamReader.ToString());
            int bufferCount = 8;

            for (int i = 0; i < vertexCount; i++)
            {
                int[] adjacentList = streamReader.ReadLine().Split().Select(x => int.Parse(x)).ToArray();
                DeviceControl captureDeviceControl = deviceControlList[adjacentList[0]];
                DeviceControl renderDeviceControl = deviceControlList[adjacentList[1]];

                if (captureDeviceControl == null || renderDeviceControl == null)
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
        /// Save graph to given file.
        /// </summary>
        /// <param name="fileName">The file</param>
        public void SaveGraph(string fileName)
        {
            if (fileName is null)
            {
                //TODO: add logger, output message to user, then return
                return;
            }

            if (!fileName.EndsWith(DefaultData.FileExtension))
            {
                fileName += DefaultData.FileExtension;
            }

            string pathName = $@"{Directory.GetCurrentDirectory()}{DefaultData.SavePartialPath}\{fileName}";
            StreamWriter streamWriter = new StreamWriter(pathName);

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
        /// Writes edges to file.
        /// </summary>
        /// <param name="deviceControlIdDictionary"></param>
        /// <param name="streamWriter">The stream writer</param>
        protected internal virtual void WriteEdgeRepeaterInfoToFile(Dictionary<DeviceControl, int> deviceControlIdDictionary, StreamWriter streamWriter)
        {
            if (deviceControlIdDictionary is null)
            {
                throw new System.ArgumentNullException(nameof(deviceControlIdDictionary));
            }

            if (streamWriter is null)
            {
                throw new System.ArgumentNullException(nameof(streamWriter));
            }

            foreach (RepeaterInfo edgeRepeaterInfo in GetEdges())
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

            streamWriter.WriteLine(edgesCount / 2);
        }

        /// <summary>
        /// Add vertices to edge.
        /// </summary>
        /// <param name="deviceControl">The device's vertices</param>
        public void AddVertex(DeviceControl deviceControl)
        {
            if (deviceControl is null || Edge.ContainsKey(deviceControl))
            {
                return;
            }

            Edge[deviceControl] = new Dictionary<DeviceControl, RepeaterInfo>();
        }

        /// <summary>
        /// Remove vertex along with any edges the given vertex has an endpoint towards.
        /// </summary>
        /// <param name="deviceControl">The device</param>
        public void RemoveVertex(DeviceControl deviceControl)
        {
            if (deviceControl is null)
            {
                return;
            }

            foreach (DeviceControl adjacentDeviceControl in Edge[deviceControl].Keys)
            {
                Edge[adjacentDeviceControl].Remove(deviceControl);
                MainWindow.GraphMapCanvas.Children.Remove(Edge[deviceControl][adjacentDeviceControl].Link);
            }

            Edge.Remove(deviceControl);
            MainWindow.GraphMapCanvas.Children.Remove(deviceControl);
        }
    }
}