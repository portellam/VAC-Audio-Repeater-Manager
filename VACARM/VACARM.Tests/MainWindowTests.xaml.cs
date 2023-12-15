using NUnit.Framework;
using static VACARM.MainWindow;

namespace VACARM.Tests
{
    [TestFixture]
    public class MainWindowTests
    {
        // RunCommand()
        /*
         * CommandIsEmpty_ReturnVoid
         * CommandIsNull_ReturnVoid
         * CommandIsValid_StartHiddenTerminalProcess
         */

        // ResetActiveRepeaters
        /*
         * ResetActiveRepeaterList_UndoList
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
         * ToggleIsRunning
         */

        // StartEngine()
        /*
         * StartEngineOfEveryActiveRepeater
         */

        // StartEngineOfActiveRepeater()
        /*
         * CaptureDeviceControlDeviceStateIsNotActive_ReturnVoid
         * RenderDeviceControlDeviceStateIsNotActive_ReturnVoid
         * DeviceStatesAreActive_RunCommandAndAddToList
         */

        // StopEngine
        /*
         * ActiveRepeaterListIsNull_ReturnVoid
         * ActiveRepeaterListIsEmpty_ReturnVoid
         * ActiveRepeaterListIsValid_RunCommandsAndUndoList
         */


        // ToolBarSelect_Click()
        /*
         * SenderIsEmpty_ReturnVoid
         * SenderIsNull_ReturnVoid
         * SenderIsValid_UpdateSelectedToolAsRadioButton
         */

        // Window_KeyUp()
        /*
         * KeyEventArgsIsNull_ReturnVoid
         * KeyEventArgsContainsNoKey_ReturnVoid
         * KeyEventArgsContainsKeyT_AddDevice
         * KeyEventArgsContainsKeyDelete_RemoveDevice
         * KeyEventArgsContainsKeyH_DoSomething
         * KeyEventArgsContainsKeyL_DoSomething
         * KeyEventArgsContainsKeyR_Restart
         * KeyEventArgsContainsKeyP_StartStop
         */

        // Window_Closing()
        /*
         * Window_Closing_CanCallStopEngine
         */
    }
}