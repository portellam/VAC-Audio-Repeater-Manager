using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Legacy;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Animation;
using TypeMock.ArrangeActAssert.Suggest;

namespace VACARM_GUI.Tests
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

        [SetUp]
        public void Setup()
        {
            addDeviceDialog = new AddDeviceDialog();
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
                Items =
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

        //[Test]
        //public void SelectDeviceType_SelectionChanged_SelectedIndexIsZero_ItemsSourceIsWaveInNameList()
        //{
        //    // Arrange
        //    //deviceListMock.Setup(x => x.WaveInNameList).Returns(waveInNameList);
        //    //deviceListMock.Setup(x => x.WaveOutNameList).Returns(waveOutNameList);
        //    comboBox.SelectedIndex = waveInSelectedIndex;
            
        //    // Act
        //    addDeviceDialog.SelectDeviceType_SelectionChanged(comboBox, selectionChangedEventArgs);
        //    var result1 = addDeviceDialog.selectDevice.ItemsSource;
        //    var result2 = addDeviceDialog.selectDevice.SelectedIndex;

        //    // Assert
        //    Assert.That(result1, Is.EqualTo(waveOutNameList));           //NOTE: it is actually grabbing the devices on the system. Should I continue this? Or how do I put fake/test info and run that?
        //    Assert.That(result2, Is.EqualTo(waveOutSelectedIndex));
        //}

        [Test]
        public void SelectDeviceType_SelectionChanged_SelectedIndexIsNonZero_ItemsSourceIsWaveOutNameList()
        {
            // TODO: create ComboBox with at least Wave Out, then add to selectionChangedEvent. Do not add directly to addDeviceDialog, we want to test that it updates this metadata using the method.

            // Arrange
            ComboBox newComboBox = new ComboBox()
            {
                Name = comboBoxName,
                Items =
                {
                    //waveInComboBoxItem,
                    waveOutComboBoxItem
                }
            };

            newComboBox.SelectedItem = newComboBox.Items.Contains(waveOutComboBoxItem);

            selectionChangedEventArgs = new SelectionChangedEventArgs(
                Selector.SelectionChangedEvent,
                new List<string> { },
                newComboBox.Items
            );
            
            selectionChangedEventArgs.Source = newComboBox;

            addDeviceDialog.selectDevice = new ComboBox() {
                Name = comboBoxName
            };

            // Act
            addDeviceDialog.SelectDeviceType_SelectionChanged(comboBox, selectionChangedEventArgs);
            var result1 = addDeviceDialog.selectDevice.Name;
            var result2 = addDeviceDialog.selectDevice.SelectedIndex;
            var result3 = addDeviceDialog.selectDeviceType.SelectedIndex;
            var result4 = addDeviceDialog.selectDevice.ItemsSource;

            Assert.Multiple(() =>
            {
                Assert.That(result1, Is.EqualTo(newComboBox.Name));
                Assert.That(result2, Is.EqualTo(selectDeviceSelectedIndex));
                Assert.That(result3, Is.Not.EqualTo(waveInSelectedIndex));
                Assert.That(result3, Is.EqualTo(waveOutSelectedIndex));
                Assert.That(result4, Is.Not.EqualTo(waveInComboBoxItem.DataContext));
                Assert.That(result4, Is.EqualTo(waveOutComboBoxItem.DataContext));
            });
        }

        // Window_MouseDown()
        /*
         * _MouseButtonChangedButtonIsLeftButton_DragMove
         * _MouseButtonChangedButtonIsNotLeftButton_ReturnVoid
         */

        //[Test]
        //public void Window_MouseDown_MouseButtonChangedButtonIsLeftButton_DoNotDragMove()
        //{
        //    // Arrange
        //    MouseButton mouseButton = MouseButton.Left;
        //    MouseButtonState mouseButtonState = MouseButtonState.Pressed;

        //    windowMock.Setup(x => x.DragMove()).Verifiable();
        //    mouseDeviceMock.SetupGet(x => x.LeftButton).Returns(mouseButtonState);

        //    MouseButtonEventArgs mouseButtonEventArgs = new MouseButtonEventArgs(mouseDeviceMock.Object, 0, mouseButton);

        //    // Act
        //    addDeviceDialog.Window_MouseDown(mouseDeviceMock.Object, mouseButtonEventArgs);
        //    var result = addDeviceDialogMock.Setup(x => x.Window_MouseDown(WaveInDeviceType, mouseButtonEventArgs));
        //    windowMock.Setup(x => x.DragMove()).Verifiable();

        //    // Assert
        //    Assert.Equals(mouseButtonEventArgs.ChangedButton, mouseButton);
        //    windowMock.Verify(x => x.DragMove(), Times.Once);
        //    windowMock.VerifyNoOtherCalls();


        //    // Arrange
        //    //var fakeMouseDevice = Isolate.Fake.Instance<MouseDevice>();
        //    //Isolate.WhenCalled(() => fakeMouseDevice.LeftButton).WillReturn(MouseButtonState.Pressed);
        //    //MouseButtonEventArgs mouseButtonEventArgs = new MouseButtonEventArgs(fakeMouseDevice, 0, MouseButton.Left);

        //    //var fakeWindow = Isolate.Fake.Instance<Window>();

        //    //// Act
        //    ////var result = addDeviceDialogMock.Setup(x => x.Window_MouseDown(WaveInDeviceType, mouseButtonEventArgs));

        //    //addDeviceDialog.Window_MouseDown(WaveInDeviceType, mouseButtonEventArgs);

        //}

        //[Test]
        //public void Window_MouseDown_MouseButtonChangedButtonIsNotLeftButton_ReturnVoid()
        //{
        //}
    }
}