using NUnit.Framework;
using static VACARM.DeviceControl;

namespace VACARM.Tests
{
    [TestFixture]
    public class DeviceControlTests
    {
        // Constructor
        /*
         * InitalizeComponentAndSetMetadata_IfDataFlowIsCapture_SetColorToInputDeviceColor
         * InitalizeComponentAndSetMetadata_IfDataFlowIsNotCapture_SetColorToOutputDeviceColor
         */

        // OnPropertyChanged()
        /*
         * _PropertyNameIsEmpty_DoNothing
         * _PropertyNameIsNull_DoNothing
         * _PropertyNameIsValid_PropertyIsChanged
         */

        // SetBackgrounColor()
        /*
         * _IsCaptureDevice_ReturnInputDeviceColor
         * _IsNotCaptureDevice_ReturnOutputDeviceColor
         */

        // UserControl_MouseLeftButtonDown()
        /*
         * _SetSelectedDevice_IfInitialDeviceIsNull_SetInitialDevice
         * _SetSelectedDevice_IfSelectedToolIsHand_SetStartPointAndZIndex
         * _SetSelectedDevice_IfSelectedToolIsNotHandAndIfInitialDeviceIsNotNull_AddEdgeAndSetInitialDeviceToNull
         */

        // UserControl_MouseLeftButtonUp()
        /*
         * _IfSelectedToolIsHand_SetZIndex_SetSelectedDevice
         * _IfSelectedToolIsNotHand_DoNotSetZIndex_SetSelectedDevice
         */

        // UserControl_PreviewMouseMove()
        /*
         * _IfLeftButtonIsNotPressed_ReturnVoid
  
         * _IfLeftIsGreaterThanOrEqualAsZero_LeaveLeftAsIs_IfLeftIsGreaterThanActualWidth_IfTopIsGreaterThanOrEqualAsZero_LeaveTopAsIs_IfTopIsGreaterThanActualHeight
         * _IfLeftIsGreaterThanOrEqualAsZero_LeaveLeftAsIs_IfLeftIsGreaterThanActualWidth_IfTopIsGreaterThanOrEqualAsZero_LeaveTopAsIs_IfTopIsLessThanOrEqualToActualHeight
         * _IfLeftIsGreaterThanOrEqualAsZero_LeaveLeftAsIs_IfLeftIsGreaterThanActualWidth_IfTopIsLessThanZero_SetTopAsZero_IfTopIsGreaterThanActualHeight
         * _IfLeftIsGreaterThanOrEqualAsZero_LeaveLeftAsIs_IfLeftIsGreaterThanActualWidth_IfTopIsLessThanZero_SetTopAsZero_IfTopIsLessThanOrEqualToActualHeight

         * _IfLeftIsGreaterThanOrEqualAsZero_LeaveLeftAsIs_IfLeftIsLessThanOrEqualToActualWidth_IfTopIsGreaterThanOrEqualAsZero_LeaveTopAsIs_IfTopIsGreaterThanActualHeight
         * _IfLeftIsGreaterThanOrEqualAsZero_LeaveLeftAsIs_IfLeftIsLessThanOrEqualToActualWidth_IfTopIsGreaterThanOrEqualAsZero_LeaveTopAsIs_IfTopIsLessThanOrEqualToActualHeight
         * _IfLeftIsGreaterThanOrEqualAsZero_LeaveLeftAsIs_IfLeftIsLessThanOrEqualToActualWidth_IfTopIsLessThanZero_SetTopAsZero_IfTopIsGreaterThanActualHeight
         * _IfLeftIsGreaterThanOrEqualAsZero_LeaveLeftAsIs_IfLeftIsLessThanOrEqualToActualWidth_IfTopIsLessThanZero_SetTopAsZero_IfTopIsLessThanOrEqualToActualHeight
        
         * _IfLeftIsLessThanZero_SetLeftAsZero_IfLeftIsGreaterThanActualWidth_IfTopIsGreaterThanOrEqualAsZero_LeaveTopAsIs_IfTopIsGreaterThanActualHeight
         * _IfLeftIsLessThanZero_SetLeftAsZero_IfLeftIsGreaterThanActualWidth_IfTopIsGreaterThanOrEqualAsZero_LeaveTopAsIs_IfTopIsLessThanOrEqualToActualHeight
         * _IfLeftIsLessThanZero_SetLeftAsZero_IfLeftIsGreaterThanActualWidth_IfTopIsLessThanZero_SetTopAsZero_IfTopIsGreaterThanActualHeight
         * _IfLeftIsLessThanZero_SetLeftAsZero_IfLeftIsGreaterThanActualWidth_IfTopIsLessThanZero_SetTopAsZero_IfTopIsLessThanOrEqualToActualHeight

         * _IfLeftIsLessThanZero_SetLeftAsZero_IfLeftIsLessThanOrEqualToActualWidth_IfTopIsGreaterThanOrEqualAsZero_LeaveTopAsIs_IfTopIsGreaterThanActualHeight
         * _IfLeftIsLessThanZero_SetLeftAsZero_IfLeftIsLessThanOrEqualToActualWidth_IfTopIsGreaterThanOrEqualAsZero_LeaveTopAsIs_IfTopIsLessThanOrEqualToActualHeight
         * _IfLeftIsLessThanZero_SetLeftAsZero_IfLeftIsLessThanOrEqualToActualWidth_IfTopIsLessThanZero_SetTopAsZero_IfTopIsGreaterThanActualHeight
         * _IfLeftIsLessThanZero_SetLeftAsZero_IfLeftIsLessThanOrEqualToActualWidth_IfTopIsLessThanZero_SetTopAsZero_IfTopIsLessThanOrEqualToActualHeight
  
         * _IfSelectedToolIsNotHand_ReturnVoid 
         */
    }
}