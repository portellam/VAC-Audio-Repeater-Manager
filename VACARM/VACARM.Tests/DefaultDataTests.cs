using NUnit.Framework;
using static VACARM.DefaultData;

namespace VACARM.Tests
{
	[TestFixture]
	public class DefaultDataTests
	{
		// CheckDefaultSavePath()
		/*
         * 
         * 
         * 
         */

		// CheckFile()
		/*
         * _PathIsNull_WriteDefaultRepeaterToFile__
         * _PathDoesNotExist_WriteDefaultRepeaterToFile__
         * _PathDoesExist_DoNotWriteDefaultRepeaterToFile__ 
         * _DefaultGraphIsNotNullAndNetworkCountIsGreaterThanOneAndSavePathExists_DefaultGraphIsUnchanged
         * _DefaultGraphIsNull_DefaultGraphIsTwoBackSlashes
         * _NetworkCountIsOne_DefaultGraphIsChanged
         * _NetworkCountIsZero_DefaultGraphIsTwoBackSlashes
         * _SavePathDoesNotExist_DefaultGraphIsTwoBackSlashes
         * 
         * _FileIsNotNull_ReadAllLinesFails_ThrowIOException
         * _FileIsNotNull_ReadAllLinesPasses_DoesSavePathExistReturnsFalse_ReturnVoid
         * _FileIsNotNull_ReadAllLinesPasses_DoesSavePathExistReturnsTrue_NetworkArrayLengthIsOne_SetDefaultGraphToEmpty
         * _FileIsNotNull_ReadAllLinesPasses_DoesSavePathExistReturnsTrue_NetworkArrayLengthIsZero_SetDefaultGraphToDefault
         * _FileIsNotNull_ReadAllLinesPasses_DoesSavePathExistReturnsTrue_NetworkArrayLengthIs
         */

		// DoesSavePathExist()
		/*
         * _SavePathExists_ReturnTrue
         * _SavePathDoesNotExist_CreateDirectoryFails_CatchIOExceptionAndReturnFalse
         * _SavePathDoesNotExist_CreateDirectoryPasses_ReturnTrue
         */

		// Refresh()
		/*
         * _CheckFile_FileIsNotNull_ReadAllLinesFromPath
         * _CheckFile_FileIsNull_CatchIOException
         */

		// Save()
		/*
         * _FileIsNotNullAndDataIsNotNull_DataWriteAllLinesToPath
         * _FileIsNotNullAndDataIsNull_DataWriteAllLinesToPath
         * _FileIsNull_CatchIOException
         */
	}
}