using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace VACARM_GUI_NET_4.Tests
{
    [TestFixture, Apartment(ApartmentState.STA)]
    public class AddDeviceDialogTests
    {
        private const int selectDeviceSelectedIndex = -1;
        private const int waveInSelectedIndex = 0;
        private const int waveOutSelectedIndex = 1;
        private const string comboBoxName = "selectDeviceType";
        private const string waveInContentName = "Wave In";
        private const string waveOutContentName = "Wave Out";
        private AddDeviceDialog addDeviceDialog;
        private ComboBox comboBox;
        private ComboBoxItem waveInComboBoxItem, waveOutComboBoxItem;
        private DeviceList deviceList;
        private SelectionChangedEventArgs selectionChangedEventArgs;
        private Mock<AddDeviceDialog> addDeviceDialogMock;

        [SetUp]
        public void Setup()
        {
            addDeviceDialog = new AddDeviceDialog();
            addDeviceDialogMock = new Mock<AddDeviceDialog>()
            {
                CallBase = true
            };

            deviceList = new DeviceList();

            waveInComboBoxItem = new ComboBoxItem()
            {
                Name = comboBoxName,
                Content = waveInContentName,
                DataContext = deviceList.WaveInNameList
            };

            waveOutComboBoxItem = new ComboBoxItem()
            {
                Name = comboBoxName,
                Content = waveOutContentName,
                DataContext = deviceList.WaveOutNameList
            };

            comboBox = new ComboBox()
            {
                Name = comboBoxName,
                ItemsSource = new[]
                {
                    waveInComboBoxItem,
                    waveOutComboBoxItem
                }
            };

            selectionChangedEventArgs = new SelectionChangedEventArgs(
                Selector.SelectionChangedEvent,
                new List<string> { },
                comboBox.Items
            );
        }

        // Constructor
        /*
         * _SetMetadata
         */

        [Test]
        public void CancelButton_Click_IsCancelButton_Close()
        {
            // Arrange
            Button button = new Button()
            {
                IsCancel = true
            };

            RoutedEventArgs routedEventArgs = new RoutedEventArgs();
            addDeviceDialogMock.Setup(x => x.CallClose()).Verifiable();

            // Act
            addDeviceDialogMock.Object.CancelButton_Click(button, routedEventArgs);

            // Assert
            addDeviceDialogMock.Verify(x => x.CallClose(), Times.Once());
        }

        [Test]
        public void CancelButton_Click_IsNotCancelButton_DoNotClose()
        {
            // Arrange
            Button button = new Button()
            {
                IsCancel = false
            };

            RoutedEventArgs routedEventArgs = new RoutedEventArgs();
            addDeviceDialogMock.Setup(x => x.CallClose()).Verifiable();

            // Act
            addDeviceDialogMock.Object.CancelButton_Click(button, routedEventArgs);

            // Assert
            addDeviceDialogMock.Verify(x => x.CallClose(), Times.Never());
        }

        // OkButton_Click()
        /*
         * _SelectedDeviceSelectedIndexIsNegativeOne_ReturnVoid
         * _SelectedDeviceTypeSelectedIndexIsNegativeOne_ReturnVoid
         * _SelectedIndexIsNotNegativeOne_SelectedDeviceTypeSelectedIndexIsNonZero_DevicesAreWaveOut
         * _SelectedIndexIsNotNegativeOne_SelectedDeviceTypeSelectedIndexIsZero_DevicesAreWaveIn
         */

        [Test]
        public void SelectDeviceType_SelectionChanged_SelectedIndexIsZero_ItemsSourceIsWaveInNameList()
        {
            // Arrange
            addDeviceDialog.selectDevice = new ComboBox()
            {
                Name = comboBoxName
            };

            addDeviceDialog.selectDeviceType.SelectedIndex = waveInSelectedIndex;
            comboBox.SelectedItem = comboBox.Items.Contains(waveInComboBoxItem);

            selectionChangedEventArgs = new SelectionChangedEventArgs(
                Selector.SelectionChangedEvent,
                new List<string> { },
                comboBox.Items
            )
            {
                Source = comboBox
            };

            // Act
            addDeviceDialog.SelectDeviceType_SelectionChanged(comboBox, selectionChangedEventArgs);
            var result1 = addDeviceDialog.selectDevice.Name;
            var result2 = addDeviceDialog.selectDevice.SelectedIndex;
            var result3 = addDeviceDialog.selectDevice.ItemsSource;

            Assert.Multiple(() =>
            {
                Assert.That(result1, Is.EqualTo(comboBox.Name));
                Assert.That(result2, Is.EqualTo(selectDeviceSelectedIndex));
                Assert.That(result3, Is.Not.EqualTo(waveOutComboBoxItem.DataContext));
                Assert.That(result3, Is.EqualTo(waveInComboBoxItem.DataContext));
            });
        }

        [Test]
        public void SelectDeviceType_SelectionChanged_SelectedIndexIsNonZero_ItemsSourceIsWaveOutNameList()
        {
            // Arrange
            addDeviceDialog.selectDevice = new ComboBox()
            {
                Name = comboBoxName
            };

            addDeviceDialog.selectDeviceType.SelectedIndex = waveOutSelectedIndex;
            comboBox.SelectedItem = comboBox.Items.Contains(waveOutComboBoxItem);

            selectionChangedEventArgs = new SelectionChangedEventArgs(
                Selector.SelectionChangedEvent,
                new List<string> { },
                comboBox.Items
            )
            {
                Source = comboBox
            };

            // Act
            addDeviceDialog.SelectDeviceType_SelectionChanged(comboBox, selectionChangedEventArgs);
            var result1 = addDeviceDialog.selectDevice.Name;
            var result2 = addDeviceDialog.selectDevice.SelectedIndex;
            var result3 = addDeviceDialog.selectDevice.ItemsSource;

            Assert.Multiple(() =>
            {
                Assert.That(result1, Is.EqualTo(comboBox.Name));
                Assert.That(result2, Is.EqualTo(selectDeviceSelectedIndex));
                Assert.That(result3, Is.Not.EqualTo(waveInComboBoxItem.DataContext));
                Assert.That(result3, Is.EqualTo(waveOutComboBoxItem.DataContext));
            });
        }

        //[Test]
        //public void Window_MouseDown_MouseButtonChangedButtonIsLeftButton_DoNotDragMove()       //NOTE: TypeMock isolator install on Windows machine is likely necessary. TODO: install and test this unit test.
        //{
        //    // Arrange
        //    Isolate.WhenCalled(() => mouseButtonEventArgsFake.ChangedButton).WillReturn(MouseButton.Left);

        //    // Act
        //    addDeviceDialogMock.Setup(x => x.Window_MouseDown(mouseButtonEventArgsFake.Device, mouseButtonEventArgsFake));

        //    // Assert
        //    addDeviceDialogMock.Verify(x => x.CallDragMove(), Times.Once);
        //}

        //[Test]
        //public void Window_MouseDown_MouseButtonChangedButtonIsNotLeftButton_ReturnVoid()       //NOTE: TypeMock isolator install on Windows machine is likely necessary. TODO: install and test this unit test.
        //{
        //    // Arrange
        //    Isolate.WhenCalled(() => mouseButtonEventArgsFake.ChangedButton).WillReturn(MouseButton.Right);

        //    // Act
        //    addDeviceDialogMock.Setup(x => x.Window_MouseDown(mouseButtonEventArgsFake.Device, mouseButtonEventArgsFake));

        //    // Assert
        //    addDeviceDialogMock.Verify(x => x.CallDragMove(), Times.Never);
        //}
    }
}