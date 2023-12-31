using Moq;
using NUnit.Framework;

namespace VACARM.NET4.GUI.UnitTests
{
    [TestFixture]
    public class IComponentConnectorAltTests
    {
        // NOTES:
        /*
         * -for an external unit test (not of this class), should the new method fail and the original assembly method be reached, expect the following exception and warn the user.
         * "System.Exception : The component 'Castle.Proxies.AddDeviceDialogProxy' does not have a resource identified by the URI '/VACARM.NET4.GUI;component/adddevicedialog.xaml'."
         * 
         */

        // InitializeComponent_
        /*
         * _ObjectIsNotNull_InitializeComponentAltFails_InitializeComponentFails_ThrowException
         * _ObjectIsNotNull_InitializeComponentAltFails_InitializeComponentPasses_ReturnVoid
         * _ObjectIsNotNull_InitializeComponentAltPasses_ReturnVoid
         * _ObjectIsNull_ThrowArgumentNullException
         */

        // InitializeComponentAlt_
        /*
         * _BaseUriIsEmpty_ReturnVoid
         * _BaseUriIsNull_ReturnVoid
         * _BaseUriIsValid_LoadXAMLFails_ThrowException
         * _BaseUriIsValid_LoadXAMLPasses_ReturnVoid
         */

        // LoadXAML_
        /*
         * _GetFirstMethodFails_ThrowTargetInvocationException                      //NOTE: method's assembly may not be available.
         * _InvokeFirstMethodFails_ThrowException                                   //NOTE: method may not accept overloads.
         
         * _InvokeFirstMethodPasses_GetSecondConstructorFails_ThrowException        //NOTE: constructor's assembly may not be available OR constructor may not have property.
         * _InvokeFirstMethodPasses_GetSecondConstructorValueFails_ThrowException   //NOTE: if property exists, then it should have a getter. Therefore, this test may not be necessary.    //TODO: do not add?

         * _InvokeFirstMethodPasses_GetSecondConstructorValuePasses_GetThirdMethodFails_ThrowTargetInvocationException      //NOTE: method's assembly may not be available.
         * _InvokeFirstMethodPasses_GetSecondConstructorValuePasses_InvokeThirdMethodFails_ThrowException                   //NOTE: method may not accept overloads.

         * _InvokeFirstMethodPasses_GetSecondConstructorValuePasses_InvokeThirdMethodPasses_ReturnVoid
         */
    }
}