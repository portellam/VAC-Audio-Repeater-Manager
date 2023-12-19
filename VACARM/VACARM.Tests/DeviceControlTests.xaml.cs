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
         * _IfSelectedToolIsNotHand_ReturnVoid

         * _LeftIsLessThanZero_LeftIsGreaterThanActualWidth_SetTopToZero_TopIsGreaterThanActualHeight
         * _LeftIsLessThanZero_LeftIsGreaterThanActualWidth_SetTopToZero_TopIsLessThanOrEqualToActualHeight
         * _LeftIsLessThanZero_LeftIsGreaterThanActualWidth_TopIsLessThanZero_TopIsGreaterThanActualHeight
         * _LeftIsLessThanZero_LeftIsGreaterThanActualWidth_TopIsLessThanZero_TopIsLessThanOrEqualToActualHeight

         * _LeftIsLessThanZero_LeftIsLessThanOrEqualToActualWidth_SetTopToZero_TopIsGreaterThanActualHeight
         * _LeftIsLessThanZero_LeftIsLessThanOrEqualToActualWidth_SetTopToZero_TopIsLessThanOrEqualToActualHeight
         * _LeftIsLessThanZero_LeftIsLessThanOrEqualToActualWidth_TopIsLessThanZero_TopIsGreaterThanActualHeight
         * _LeftIsLessThanZero_LeftIsLessThanOrEqualToActualWidth_TopIsLessThanZero_TopIsLessThanOrEqualToActualHeight
         * 
         * _SetLeftToZero_LeftIsGreaterThanActualWidth_SetTopToZero_TopIsGreaterThanActualHeight
         * _SetLeftToZero_LeftIsGreaterThanActualWidth_SetTopToZero_TopIsLessThanOrEqualToActualHeight
         * _SetLeftToZero_LeftIsGreaterThanActualWidth_TopIsLessThanZero_TopIsGreaterThanActualHeight
         * _SetLeftToZero_LeftIsGreaterThanActualWidth_TopIsLessThanZero_TopIsLessThanOrEqualToActualHeight

         * _SetLeftToZero_LeftIsLessThanOrEqualToActualWidth_SetTopToZero_TopIsGreaterThanActualHeight
         * _SetLeftToZero_LeftIsLessThanOrEqualToActualWidth_SetTopToZero_TopIsLessThanOrEqualToActualHeight
         * _SetLeftToZero_LeftIsLessThanOrEqualToActualWidth_TopIsLessThanZero_TopIsGreaterThanActualHeight
         * _SetLeftToZero_LeftIsLessThanOrEqualToActualWidth_TopIsLessThanZero_TopIsLessThanOrEqualToActualHeight
         */
    }
}