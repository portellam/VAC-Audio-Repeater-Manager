using NUnit.Framework;
using static VACARM.MainWindow;

namespace VACARM.Tests
{
    [TestFixture]
    public class MainWindowTests
    {
        // Constructor
        /*
         * _SetMetadata_CheckFile_IfDefaultGraphIsNotNull_LoadGraph_SetIsRunningToTrue
         * _SetMetadata_CheckFile_IfDefaultGraphIsNull_CreateNewGraph_SetIsRunningToTrue
         */

        // AddDevice()
        /*
         * _AddDeviceDialogMMDeviceIsNull_ReturnVoid
         * _AddDeviceDialogMMDeviceIsNotNull_AddNewDeviceToDefaultPosition
         */

        // AddDevice_Click()
        /*
         * _AddDevice_ConfirmAddDevice
         */

        // LoadGraph_Click()
        /*
         * _ClearGraphCanvas_TryLoadGraphFromFile_CatchException_ClearGraphAndCanvas
         * _ClearGraphCanvas_TryLoadGraphFromFile_UpdateDefaultGraph
         * _FileIsPath_ReturnVoid
         * _FileIsNotRecognizedFileType_ReturnVoid
         * _ShowOpenFileDialogIsFalse_ReturnVoid
         * _ShowOpenFileDialogIsNull_ReturnVoid
         */

        // GraphCanvas_MouseLeftButtonClick()
        /*
         * _UnselectDevice_ConfirmUnselectDevice
         */

        // HwndHook()
        /*
         * _MessageIsWMHOTKEY_HwndHookIsMatchForWParam_ReturnIntPtrZero
         * _MessageIsNotWMHOTKEY_ReturnIntPtrZero
         */

        // HwndHookIsMatchForWParam()
        /*
         * _widthParamIsHOTKEYID_SetRefOfIsHandledToTrueAndRestartIfScrollKeyId
         * _widthParamIsNotHOTKEYID_ReturnVoid
         */

        // SetRefOfIsHandledToTrueAndRestartIfScrollKeyId()
        /*
         * _vkeyIsVKSCROLL_Restart
         * _vkeyIsNotVKSCROLL_DoNotRestart
         */

        // OnSourceInitialized()
        /*
         * _SetProperties_AddHookAndRegisterHotKey
         */

        // RemoveDevice()
        /*
         * _DeviceIsNotSelected_ReturnVoid
         * _DeviceIsSelected_RemoveDeviceAndUnselectDevice
         */

        // RemoveDevice_Click()
        /*
         * _RemoveDevice_ConfrmUnselectDevice
         */

        // ResetActiveRepeaters()
        /*
         * _UpdatePropertyAsEmptyStringList
         */

        // Restart()
        /*
         * _SetIsRunningToFalseThenTrue
         */

        // Restart_Click()
        /*
         * Restart_ConfirmRestart
         */

        // ResetActiveRepeaters()
        /*
         * _ResetActiveRepeaterList_UndoList
         */

        // RunCommand()
        /*
         * _CommandIsEmpty_ReturnVoid
         * _CommandIsNull_ReturnVoid
         * _CommandIsValid_StartHiddenTerminalProcess
         */

        // SavedEditedGraph()
        /*
         * _IfFileIsEdited_DoUpdateDefaultGraph
         * _IfFileIsNotEdited_DoNotUpdateDefaultGraph
         */

        // SaveGraph_Click()
        /*
         * _IfShowDialogIsTrue_SaveEditedGraph
         * _IfShowDialogIsFalse_DoNotSaveEditedGraph
         */

        // StartStop()
        /*
         * _ToggleIsRunning
         */

        // StartStop_Click()
        /*
         * _StartStop_ConfirmStartStop
         */

        // StartEngine()
        /*
         * _StartEngineOfEveryActiveRepeater
         */

        // StartEngineOfActiveRepeater()
        /*
         * _CaptureDeviceControlDeviceStateIsNotActive_ReturnVoid
         * _RenderDeviceControlDeviceStateIsNotActive_ReturnVoid
         * _DeviceStatesAreActive_RunCommandAndAddToList
         */

        // StopEngine
        /*
         * _ActiveRepeaterListIsNull_ReturnVoid
         * _ActiveRepeaterListIsEmpty_ReturnVoid
         * _ActiveRepeaterListIsValid_RunCommandsAndUndoList
         */


        // ToolBarSelect_Click()
        /*
         * _SenderIsEmpty_ReturnVoid
         * _SenderIsNull_ReturnVoid
         * _SenderIsValid_UpdateSelectedToolAsRadioButton
         */

        // UnselectDevice()
        /*
         * _SetSelectedDeviceAsNull
         */

        // Window_KeyUp()
        /*
         * _KeyEventArgsIsNull_ReturnVoid
         * _KeyEventArgsContainsNoKey_ReturnVoid
         * _KeyEventArgsContainsKeyT_AddDevice
         * _KeyEventArgsContainsKeyDelete_RemoveDevice
         * _KeyEventArgsContainsKeyH_HandToolIsSelected
         * _KeyEventArgsContainsKeyL_LinkToolIsCSelected
         * _KeyEventArgsContainsKeyR_Restart
         * _KeyEventArgsContainsKeyP_StartStop
         */

        // Window_Closing()
        /*
         * StopEngine_ConfirmEngineStop
         */
    }
}