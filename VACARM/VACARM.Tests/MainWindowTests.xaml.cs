using NUnit.Framework;
using static VACARM.MainWindow;

namespace VACARM.Tests
{
    [TestFixture]
    public class MainWindowTests
    {
        // RunCommand()
        /*
         * _CommandIsEmpty_ReturnVoid
         * _CommandIsNull_ReturnVoid
         * _CommandIsValid_StartHiddenTerminalProcess
         */

        // ResetActiveRepeaters
        /*
         * _ResetActiveRepeaterList_UndoList
         */

        // SavedEditedGraph()
        /*
         * 
         */

        // SaveGraph_Click()
        /*
         * 
         * 
         */

        // StartStop()
        /*
         * _ToggleIsRunning
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

        // Window_KeyUp()
        /*
         * _KeyEventArgsIsNull_ReturnVoid
         * _KeyEventArgsContainsNoKey_ReturnVoid
         * _KeyEventArgsContainsKeyT_AddDevice
         * _KeyEventArgsContainsKeyDelete_RemoveDevice
         * _KeyEventArgsContainsKeyH_DoSomething
         * _KeyEventArgsContainsKeyL_DoSomething
         * _KeyEventArgsContainsKeyR_Restart
         * _KeyEventArgsContainsKeyP_StartStop
         */

        // Window_Closing()
        /*
         * _Window_Closing_CanCallStopEngine
         */
    }
}