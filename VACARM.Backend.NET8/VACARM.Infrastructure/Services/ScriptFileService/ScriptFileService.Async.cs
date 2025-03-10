using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VACARM.Domain.Models;
using VACARM.Extensions;

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
      if (StringExtension.IsNullOrEmptyOrWhitespace(output))
      {
        return;
      }

      if (StringExtension.IsNullOrEmptyOrWhitespace(filePathName))
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

    /// <summary>
    /// Write enumerable of <typeparamref name="TRepeaterModel"/>(s) to a
    /// start script file.
    /// </summary>
    /// <param name="enumerable">The enumerable of item(s)</param>
    /// <param name="filePathName">The file path name</param>
    /// <returns>True/false result.</returns>
    public async static Task WriteStartScriptFileAsync
    (
      IEnumerable<TRepeaterModel> enumerable,
      string filePathName
    )
    {
      if (IEnumerableExtension<TRepeaterModel>.IsNullOrEmpty(enumerable))
      {
        return;
      }

      var func = (TRepeaterModel x) => x.StartArguments;

      var arguments = enumerable
        .Select(func)
        .ToString();

      await WriteScriptFileAsync
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
    /// <returns>True/false result.</returns>
    public async static Task WriteStopScriptFileAsync
    (
      IEnumerable<TRepeaterModel> enumerable,
      string filePathName
    )
    {
      if (IEnumerableExtension<TRepeaterModel>.IsNullOrEmpty(enumerable))
      {
        return;
      }

      var func = (TRepeaterModel x) => x.StopArguments;

      var arguments = enumerable
        .Select(func)
        .ToString();

      await WriteScriptFileAsync
        (
          arguments,
          filePathName
        );
    }

    #endregion
  }
}