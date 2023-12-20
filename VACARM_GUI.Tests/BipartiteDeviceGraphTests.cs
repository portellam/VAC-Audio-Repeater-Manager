using static VACARM_GUI.BipartiteDeviceGraph;

namespace VACARM_GUI.Tests
{
    [TestFixture]
    public class BipartiteDeviceGraphTests
    {
        // Constructor
        /*
         * _SetMetadata
         */

        // AddEdge()
        /*
         * 
         * 
         */

        // AddVertex()
        /*
         * 
         * 
         */

        // LoadGraph()
        /*
         * _FileDoesExist_FileCanBeParsed_ReturnGraphFromFile
         * _FileDoesExist_FileCannotBeParsed_ReturnEmptyGraph
         * _FileDoesNotExist_ReturnEmptyGraph
         */

        // GetAdjacent()
        /*
         * 
         * 
         */

        // GetEdges()
        /*
         * 
         * 
         */

        // GetGraphOfVertices()
        /*
         * _VertexCountIsNotPositive_ReturnGraph
         * _VertexCountIsPositive_CaptureAndRenderDevicesAreNotNull_DoWork
         * _VertexCountIsPositive_CaptureDeviceIsNull_DoNoWork
         * _VertexCountIsPositive_RenderDeviceIsNull_DoNoWork
         */

        // GetListOfVertices()
        /*
         * _VertexCountIsNotPositive_ReturnEmptyDeviceControlArray
         * _VertexCountIsPositive_CatchExceptionAndAddNullToList_ReturnList
         * _VertexCountIsPositive_ReadLineAndModifyDeviceControlMetadataAndAddToList_ReturnList
         */

        // RemoveEdge()
        /*
         * _DeviceControl1And2AreNotNull_RemoveLinkOnWindowAndInEdge
         * _DeviceControl1IsNull_ReturnVoid
         * _DeviceControl2IsNull_ReturnVoid
         */

        // RemoveVertex()
        /*
         * _DeviceControlIsNull_ReturnVoid
         * _EdgeDoesNotContainKeyOfDeviceControl_ReturnVoid
         * _EdgeIsNull_ReturnVoid
         * _EdgeKeyDeviceControlValueIsNull_ReturnVoid
         * _InputsAreNotNull_RemoveLinksOfAdjacentDevicesAndRemoveDevice
         */

        // SaveEdge()
        /*
         * 
         */

        // WriteEdgeRepeaterInfoToFile()
        /*
         * _HashSetIsNotNull_WriteEachEdgeToFile
         * _HashSetIsNull_ReturnVoid
         */

        // WriteHalfOfEdgesCountToFile()
        /*
         * _EdgesCountIsNegative_ReturnVoid
         * _StreamWriterIsNull_ReturnVoid
         * _WriteLine
         */
    }
}