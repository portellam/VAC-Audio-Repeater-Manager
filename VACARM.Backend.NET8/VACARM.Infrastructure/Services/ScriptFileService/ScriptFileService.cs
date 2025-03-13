using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VACARM.Domain.Models;

namespace VACARM.Infrastructure.Services
{
  /// <summary>
  /// Write script file(s) for <typeparamref name="TRepeaterModel"/>(s).
  /// </summary>
  public partial class ScriptFileService<TRepeaterModel>
    where TRepeaterModel :
    RepeaterModel
  {
    #region Parameters

    private const string Extension = ".bat";

    #endregion

    #region Logic

    /// <summary>
    /// Write output to a script file.
    /// </summary>
    /// <param name="output">The output</param>
    /// <param name="filePathName">The file path name</param>
    private static void WriteScriptFile
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

      Array.Empty<int>();

      filePathName = GetModifiedFilePathName(filePathName);

      File.WriteAllText
        (
          filePathName,
          output
        );
    }

    /// <summary>
    /// Get the file path name with the extension.
    /// </summary>
    /// <param name="filePathName">The file path name</param>
    /// <returns>The modified file path name</returns>
    private static string GetModifiedFilePathName(string filePathName)
    {
      var diff = filePathName.Length - Extension.Length;

      var result = filePathName
        .Substring
        (
          diff
        ) == Extension;

      if (!result)
      {
        filePathName += Extension;
      }

      return filePathName;
    }

    /// <summary>
    /// Write enumerable of <typeparamref name="TRepeaterModel"/>(s) to a
    /// start script file.
    /// </summary>
    /// <param name="enumerable">The enumerable of item(s)</param>
    /// <param name="filePathName">The file path name</param>
    public static void WriteStartScriptFile
    (
      IEnumerable<TRepeaterModel> enumerable,
      string filePathName
    )
    {
      if (enumerable.IsNullOrEmpty())
      {
        return;
      }

      Func<TRepeaterModel, string> func = (TRepeaterModel x) => x.StartArguments;

      var arguments = enumerable
        .Select(func)
        .ToString();

      WriteScriptFile
        (
          arguments,
          filePathName
        );
    }

    /// <summary>
    /// Write enumerable of <typeparamref name="TRepeaterModel"/>(s) to a
    /// stop script file.
    /// </summary>
    /// <param name="enumerable">The enumerable of item(s)</param>
    /// <param name="filePathName">The file path name</param>
    public static void WriteStopScriptFile
    (
      IEnumerable<TRepeaterModel> enumerable,
      string filePathName
    )
    {
      if (enumerable.IsNullOrEmpty())
      {
        return;
      }

      Func<TRepeaterModel, string> func = (TRepeaterModel x) => x.StopArguments;

      var arguments = enumerable
        .Select(func)
        .ToString();

      WriteScriptFile
        (
          arguments,
          filePathName
        );
    }

    #endregion
  }
}