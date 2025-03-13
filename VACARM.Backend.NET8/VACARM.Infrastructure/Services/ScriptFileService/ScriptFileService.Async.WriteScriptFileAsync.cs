using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Services
{
  public partial class ScriptFileService<TRepeaterModel>
  {
    #region Logic

    /// <summary>
    /// Write output to a script file.
    /// </summary>
    /// <param name="output">The output</param>
    /// <param name="filePathName">The file path name</param>
    /// <returns>True/false result.</returns>
    private async static Task WriteScriptFileAsync
    (
      string output,
      string filePathName
    )
    {
      if (string.IsNullOrWhiteSpace(output))
      {
        return;
      }

      if (string.IsNullOrWhiteSpace(filePathName))
      {
        return;
      }

      filePathName = GetModifiedFilePathName(filePathName);

      await File.WriteAllTextAsync
        (
          filePathName,
          output
        );
    }

    #endregion
  }
}