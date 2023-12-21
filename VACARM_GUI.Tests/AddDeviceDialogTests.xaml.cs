using System.Windows;

namespace VACARM_GUI.Tests
{
    [TestFixture]
    public class AddDeviceDialogTests
    {
        private AddDeviceDialog addDeviceDialog;

        [SetUp]
        public void Setup()
        {
            addDeviceDialog = new AddDeviceDialog();
        }

        // Constructor
        /*
         * _SetMetadata
         */

        // CancelButton_Click()
        /*
         * _Close_ConfirmClose
         */

        [Test]
        public void CancelButton_Click_ConfirmClose()
        {
            // Arrange
            object sender = new object();
            RoutedEventArgs routedEventArgs = new RoutedEventArgs();

            // Act
            try
            {
                addDeviceDialog.CancelButton_Click(sender, routedEventArgs);
            }
            catch (Exception exception)
            {
                Assert.Fail(exception.ToString());
            }

            // Assert
            Assert.Pass();
        }

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
    }
}