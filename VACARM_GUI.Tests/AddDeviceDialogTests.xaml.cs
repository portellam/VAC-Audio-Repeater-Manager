using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace VACARM_GUI.Tests
{
    [TestFixture, Apartment(ApartmentState.STA)]
    public class AddDeviceDialogTests
    {
        private AddDeviceDialog addDeviceDialog, waveInAddDeviceDialog, waveOutAddDeviceDialog;
        private Mock<AddDeviceDialog> addDeviceDialogMock;
        private ComboBox WaveInDeviceType, WaveOutDeviceType;
        private Mock<MouseDevice> mouseDeviceMock;

        [SetUp]
        public void Setup()
        {
            mouseDeviceMock = new Mock<MouseDevice>();

            WaveInDeviceType = new ComboBox()
            {
                SelectedIndex = 0
            };

            WaveOutDeviceType = new ComboBox()
            {
                SelectedIndex = 1
            };

            waveInAddDeviceDialog = new AddDeviceDialog()
            {
                selectDevice = WaveInDeviceType,
                selectDeviceType = WaveInDeviceType
            };

            waveOutAddDeviceDialog = new AddDeviceDialog()
            {
                selectDevice = WaveOutDeviceType
            };
        }

        // Constructor
        /*
         * _SetMetadata
         */

        // CancelButton_Click()
        /*
         * _Close_ConfirmClose
         */

        // [Test]
        // public void CancelButton_Click_ConfirmClose()
        // {
        //     // Arrange
        //     object sender = new object();
        //     RoutedEventArgs routedEventArgs = new RoutedEventArgs();

        //     // Act
        //     try
        //     {
        //         addDeviceDialog.CancelButton_Click(sender, routedEventArgs);
        //     }
        //     catch (Exception exception)
        //     {
        //         Assert.Fail(exception.ToString());
        //     }

        //     // Assert
        //     Assert.Pass();
        // }

        // OkButton_Click()
        /*
         * _SelectedDeviceSelectedIndexIsNegativeOne_ReturnVoid
         * _SelectedDeviceTypeSelectedIndexIsNegativeOne_ReturnVoid
         * _SelectedIndexIsNotNegativeOne_SelectedDeviceTypeSelectedIndexIsNonZero_DevicesAreWaveOut
         * _SelectedIndexIsNotNegativeOne_SelectedDeviceTypeSelectedIndexIsZero_DevicesAreWaveIn
         */

        // SelectDeviceType_SelectionChanged()
        /*
         * _SelectedIndexIsZero_ItemsSourceIsWaveInNameList
         * _SelectedIndexIsNonZero_ItemsSourceIsWaveOutNameList
         */

        // Window_MouseDown()
        /*
         * _MouseButtonChangedButtonIsLeftButton_DragMove
         * _MouseButtonChangedButtonIsNotLeftButton_ReturnVoid
         */

        // [Test]
        // public void Window_MouseDown_MouseButtonChangedButtonIsLeftButton_DragMove()
        // {
        //     // Arrange
        //     mouseDeviceMock.Setup(x => x.LeftButton).Returns(MouseButtonState.Pressed;

        //     MouseButtonEventArgs mouseButtonEventArgs = new MouseButtonEventArgs(mouseDeviceMock.Object, 0, MouseButton.Left);
        //     //addDeviceDialogMock.Setup(x => x.selectDevice).Equals(WaveInDeviceType);
        //     //addDeviceDialogMock.CallBase = true;

        //     // Act
        //     var result = addDeviceDialogMock.Setup(x => x.Window_MouseDown(WaveInDeviceType, mouseButtonEventArgs));

        //     // Assert
        //     Assert.Equals(mouseButtonEventArgs.MouseDevice, mouseDeviceMock.Object);
        //     Assert.Equals(addDeviceDialogMock.Object.selectDeviceType, WaveInDeviceType);
        //     Mock.Get(addDeviceDialogMock).Verify(x => x.Object.DragMove(), Times.Exactly(1));
        // }

        //[Test]
        //public void Window_MouseDown_MouseButtonChangedButtonIsNotLeftButton_ReturnVoid()
        //{
        //}
    }
}