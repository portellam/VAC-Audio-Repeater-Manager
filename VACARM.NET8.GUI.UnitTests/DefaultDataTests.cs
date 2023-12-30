using Moq;
using NUnit.Framework;
using VACARM.NET8.GUI;

namespace VACARM.NET8.GUI.UnitTests
{
    [TestFixture]
    public class DefaultDataTests
    {
        // CheckFile()
        /*
         * _DataPathDoesExistDoesThrowIOException

         * _DataPathDoesExistDoesNotThrowIOException_DoesFileExistDoesThrowIOException

         * _DataPathDoesExistDoesNotThrowIOException_DoesFileExistDoesNotThrowIOException_ReadFileFails_SavePathDoesExist_ReadAllLinesPasses_DoesSavePathExistReturnsFalse_ReturnVoid
         * _DataPathDoesExistDoesNotThrowIOException_DoesFileExistDoesNotThrowIOException_ReadFileFails_SavePathDoesExist_ReadAllLinesPasses_DoesSavePathExistReturnsTrue_SetDefaultGraph
         * _DataPathDoesExistDoesNotThrowIOException_DoesFileExistDoesNotThrowIOException_ReadFileFails_SavePathDoesNotExist_ReturnVoid
         * _DataPathDoesExistDoesNotThrowIOException_DoesFileExistDoesNotThrowIOException_ReadFilePasses_SavePathDoesExist_ReadAllLinesPasses_DoesSavePathExistReturnsFalse_ReturnVoid
         * _DataPathDoesExistDoesNotThrowIOException_DoesFileExistDoesNotThrowIOException_ReadFilePasses_SavePathDoesExist_ReadAllLinesPasses_DoesSavePathExistReturnsTrue_SetDefaultGraph
         * _DataPathDoesExistDoesNotThrowIOException_DoesFileExistDoesNotThrowIOException_ReadFilePasses_SavePathDoesNotExist_ReturnVoid 
         */

        // DoesDataPathExist()
        /*
         * _DataPathDoesExist_ReturnVoid
         * _DataPathDoesNotExist_CreateDirectoryPasses_DoesNotThrowIOException
         * _DataPathDoesNotExist_CreateDirectoryFails_ThrowIOException
         */

        // DoesFileExist()
        /*
         * _FileDoesExist_ReturnVoid
         * _FileDoesNotExist_WriteAllTextPasses_DoesNotThrowIOException
         * _FileDoesNotExist_WriteAllTextFails_DoesThrowIOException
         */

        // DoesSavePathExist()
        /*
         * _SavePathExists_ReturnTrue
         * _SavePathDoesNotExist_CreateDirectoryFails_CatchIOExceptionAndReturnFalse
         * _SavePathDoesNotExist_CreateDirectoryPasses_ReturnTrue
         */

        // ReadFile()
        /*
         * _FileIsNotNull_ReadAllLinesPasses
         * _FileIsNull_ReadAllLinesFails_CatchIOException
         */

        // Save()
        /*
         * _FileIsNotNullAndDataIsNotNull_WriteAllLinesPasses
         * _FileIsNotNullAndDataIsNull_WriteAllLinesPasses
         * _FileIsNull_WriteAllLinesFails_CatchIOException
         */

        // SetDefaultGraph()
        /*
         * _DefaultGraphIsNullAndDefaultGraphFileDoesNotExist_SetDefaultGraphToDefault
         * _GraphArrayLengthIsZero_SetDefaultGraphToDefault
         * _GraphArrayLengthIsOne_SetDefaultGraphToModifiedFirstIndexOfGraphArray
         */
    }
}