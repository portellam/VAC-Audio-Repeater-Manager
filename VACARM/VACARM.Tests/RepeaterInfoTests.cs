using NUnit.Framework;
using static VACARM.RepeaterInfo;

namespace VACARM.Tests
{
	[TestFixture]
	public class RepeaterInfoTests
	{
		// Constructor
		/*
         * _CaptureDeviceIsNull_ThrowArgumentNullException
         * _RenderDeviceIsNull_ThrowArgumentNullException
         * _GraphIsNull_ThrowArgumentNullException
         * _InputIsValid_AddDevicesToContextMenuAndAddBindings
         */

		// ContextClick()
		/*
         * _ClickAndShowDialog_DialogIsShown
         */

		// OnPropertyChanged()
		/*
         * _PropertyNameIsEmpty_DoNothing
         * _PropertyNameIsNull_DoNothing
         * _PropertyNameIsValid_PropertyIsChanged
         */

		// SetData()
		/*
         * _InfoListIsNull_PropertiesAreUnchanged_ReturnVoid
         * _InfoListIsValid_PropertiesAreChanged_ReturnVoid
         */

		// ToCommand()
		/*
         * _FormatString_ReturnString
         */

		// ToSaveData()
		/*
         * _FormatString_ReturnString
         */
	}
}