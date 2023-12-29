using Moq;
using NUnit.Framework;
using TypeMock;
using VACARM_GUI_NET_8;

namespace VACARM_GUI_NET_8.Tests
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
         * _KeyValuePairOfFirstDeviceContainsSecondDevice_ReturnVoid
         * _KeyValuePairOfSecondDeviceContainsFirstDevice_ReturnVoid
         * _NeitherDeviceIsPairedTogether_DataFlowIsCapture_FirstDeviceIsCaptureAndSecondIsRender_UpdateRepeaterInfoMetadataAndAddEdge
         * _NeitherDeviceIsPairedTogether_DataFlowIsCapture_SecondDeviceIsCaptureAndFirstIsRender_UpdateRepeaterInfoMetadataAndAddEdge
         */

        // AddVertex()
        /*
         * _DeviceIsNull_ReturnVoid
         * _EdgeContainsKeyOfDevice_ReturnVoid
         * _DeviceIsNotNullAndEdgeDoesNotContainKeyOfDevice_UpdateEdgeValueWithEmptyDictionary
         */

        // LoadGraph()
        /*
         * _FileDoesExist_FileCanBeParsed_ReturnGraphFromFile
         * _FileDoesExist_FileCannotBeParsed_ReturnEmptyGraph
         * _FileDoesNotExist_ShowMessageAndReturnEmptyGraph
         * _FileNameIsNull_ShowMessageAndReturnEmptyGraph
         */

        // GetAdjacent()
        /*
         * _ReturnDictionary
         */

        // GetEdges()
        /*
         * _EdgeKeyListIsEmpty_ReturnEmptyHashSet
         * _EdgeKeyListIsNotEmpty_ParseAdjacentKeysOfEdgeKeyVertex_AddEachEdgeKeyValuePairToHashSet_ReturnHashSet
         */

        // GetGraphOfVertices()
        /*
         * _VertexCountIsNotPositive_ReturnGraph
         * _VertexCountIsPositive_CaptureAndRenderDevicesAreNotNull_SetRepeaterInfoDataAndAddEdge_ReturnGraph
         * _VertexCountIsPositive_CaptureDeviceIsNull_DoNotSetRepeaterInfoDataAndDoNotAddEdge_ReturnGraph
         * _VertexCountIsPositive_RenderDeviceIsNull_DoNotSetRepeaterInfoDataAndDoNotAddEdge_ReturnGraph
         */

        // GetListOfVertices()
        /*
         * _VertexCountIsNotPositive_ReturnEmptyDeviceArray
         * _VertexCountIsPositive_CatchExceptionAndAddNullToList_ReturnList
         * _VertexCountIsPositive_ReadLineAndModifyDeviceMetadataAndAddToList_ReturnList
         */

        // RemoveEdge()
        /*
         * _FirstDeviceAnd2AreNotNull_RemoveLinkOnWindowAndInEdge
         * _FirstDeviceIsNull_ReturnVoid
         * _SecondDeviceIsNull_ReturnVoid
         */

        // RemoveVertex()
        /*
         * _DeviceIsNull_ReturnVoid
         * _EdgeDoesNotContainKeyOfDevice_ReturnVoid
         * _EdgeIsNull_ReturnVoid
         * _EdgeKeyDeviceValueIsNull_ReturnVoid
         * _InputsAreNotNull_RemoveLinksOfAdjacentDevicesAndRemoveDevice
         */

        // SaveEdge()
        /*
         * _FileDoesExist_FileNameSuffixDoesNotHaveFileExtension_AppendFileExtension_WriteMetadataToFileAndCloseFile
         * _FileDoesExist_FileNameSuffixDoesHaveFileExtension_WriteMetadataToFileAndCloseFile
         * _FileDoesNotExist_ShowMessageAndReturnEmptyGraph
         * _FileNameIsNull_ShowMessageAndReturnEmptyGraph
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