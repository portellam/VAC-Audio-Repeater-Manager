using NUnit.Framework;
using static VACARM.DefaultData;

namespace VACARM.Tests
{
    [TestFixture]
    public class DefaultDataTests
    {

        // CheckFile()
        /*
         * PathIsNull_WriteDefaultRepeaterToFile__
         * PathDoesNotExist_WriteDefaultRepeaterToFile__
         * PathDoesExist_DoNotWriteDefaultRepeaterToFile__ 
         * _DefaultGraphIsNotNullAndNetworkCountIsGreaterThanOneAndSavePathExists_DefaultGraphIsUnchanged
         * _DefaultGraphIsNull_DefaultGraphIsTwoBackSlashes
         * _NetworkCountIsOne_DefaultGraphIsChanged
         * _NetworkCountIsZero_DefaultGraphIsTwoBackSlashes
         * _SavePathDoesNotExist_DefaultGraphIsTwoBackSlashes
         */

        // Refresh()
        /*
         * NOTE: this borrows one line from CheckFile.
         * TODO: refactor?
         */

        // Save()
        /*
         * DataWriteAllLinesToPath_ConfirmDataIsSame ?
         * TODO: try to test for every exception? NOTE: refactor?
         */
    }
}