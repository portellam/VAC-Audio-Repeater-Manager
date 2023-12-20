using static VACARM_GUI.DeviceControl;

namespace VACARM_GUI.Tests
{
    [TestFixture]
    public class DeviceControlTests
    {
        // Constructor
        /*
         * InitalizeComponentAndSetMetadata_DataFlowIsCapture_SetColorToInputDeviceColor
         * InitalizeComponentAndSetMetadata_DataFlowIsNotCapture_SetColorToOutputDeviceColor
         */

        // OnPropertyChanged()
        /*
         * _PropertyNameIsEmpty_DoNothing
         * _PropertyNameIsNull_DoNothing
         * _PropertyNameIsValid_PropertyIsChanged
         */

        // SetBackgroundColor()
        /*
         * _IsCaptureDevice_ReturnInputDeviceColor
         * _IsNotCaptureDevice_ReturnOutputDeviceColor
         */

        // UserControl_MouseLeftButtonDown()
        /*
         * _SetSelectedDevice_InitialDeviceIsNull_SetInitialDevice
         * _SetSelectedDevice_SelectedToolIsHand_SetStartPointAndZIndex
         * _SetSelectedDevice_SelectedToolIsNotHandAndIfInitialDeviceIsNotNull_AddEdgeAndSetInitialDeviceToNull
         */

        // UserControl_MouseLeftButtonUp()
        /*
         * _SelectedToolIsHand_SetZIndex_SetSelectedDevice
         * _SelectedToolIsNotHand_DoNotSetZIndex_SetSelectedDevice
         */

        // UserControl_PreviewMouseMove()
        /*
         * _LeftButtonIsNotPressed_ReturnVoid
  
         * _LeftIsGreaterThanOrEqualAsZero_LeaveLeftAsIs_LeftIsGreaterThanActualWidth_TopIsGreaterThanOrEqualAsZero_LeaveTopAsIs_TopIsGreaterThanActualHeight
         * _LeftIsGreaterThanOrEqualAsZero_LeaveLeftAsIs_LeftIsGreaterThanActualWidth_TopIsGreaterThanOrEqualAsZero_LeaveTopAsIs_TopIsLessThanOrEqualToActualHeight
         * _LeftIsGreaterThanOrEqualAsZero_LeaveLeftAsIs_LeftIsGreaterThanActualWidth_TopIsLessThanZero_SetTopAsZero_TopIsGreaterThanActualHeight
         * _LeftIsGreaterThanOrEqualAsZero_LeaveLeftAsIs_LeftIsGreaterThanActualWidth_TopIsLessThanZero_SetTopAsZero_TopIsLessThanOrEqualToActualHeight

         * _LeftIsGreaterThanOrEqualAsZero_LeaveLeftAsIs_LeftIsLessThanOrEqualToActualWidth_TopIsGreaterThanOrEqualAsZero_LeaveTopAsIs_TopIsGreaterThanActualHeight
         * _LeftIsGreaterThanOrEqualAsZero_LeaveLeftAsIs_LeftIsLessThanOrEqualToActualWidth_TopIsGreaterThanOrEqualAsZero_LeaveTopAsIs_TopIsLessThanOrEqualToActualHeight
         * _LeftIsGreaterThanOrEqualAsZero_LeaveLeftAsIs_LeftIsLessThanOrEqualToActualWidth_TopIsLessThanZero_SetTopAsZero_TopIsGreaterThanActualHeight
         * _LeftIsGreaterThanOrEqualAsZero_LeaveLeftAsIs_LeftIsLessThanOrEqualToActualWidth_TopIsLessThanZero_SetTopAsZero_TopIsLessThanOrEqualToActualHeight
        
         * _LeftIsLessThanZero_SetLeftAsZero_LeftIsGreaterThanActualWidth_TopIsGreaterThanOrEqualAsZero_LeaveTopAsIs_TopIsGreaterThanActualHeight
         * _LeftIsLessThanZero_SetLeftAsZero_LeftIsGreaterThanActualWidth_TopIsGreaterThanOrEqualAsZero_LeaveTopAsIs_TopIsLessThanOrEqualToActualHeight
         * _LeftIsLessThanZero_SetLeftAsZero_LeftIsGreaterThanActualWidth_TopIsLessThanZero_SetTopAsZero_TopIsGreaterThanActualHeight
         * _LeftIsLessThanZero_SetLeftAsZero_LeftIsGreaterThanActualWidth_TopIsLessThanZero_SetTopAsZero_TopIsLessThanOrEqualToActualHeight

         * _LeftIsLessThanZero_SetLeftAsZero_LeftIsLessThanOrEqualToActualWidth_TopIsGreaterThanOrEqualAsZero_LeaveTopAsIs_TopIsGreaterThanActualHeight
         * _LeftIsLessThanZero_SetLeftAsZero_LeftIsLessThanOrEqualToActualWidth_TopIsGreaterThanOrEqualAsZero_LeaveTopAsIs_TopIsLessThanOrEqualToActualHeight
         * _LeftIsLessThanZero_SetLeftAsZero_LeftIsLessThanOrEqualToActualWidth_TopIsLessThanZero_SetTopAsZero_TopIsGreaterThanActualHeight
         * _LeftIsLessThanZero_SetLeftAsZero_LeftIsLessThanOrEqualToActualWidth_TopIsLessThanZero_SetTopAsZero_TopIsLessThanOrEqualToActualHeight
  
         * _SelectedToolIsNotHand_ReturnVoid 
         */
    }
}