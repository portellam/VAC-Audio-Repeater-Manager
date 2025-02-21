using VACARM.Domain.Models;
using VACARM.Infrastructure.Extensions;

namespace VACARM.Infrastructure.Services
{
  /// <summary>
  /// Write script file(s) for <typeparamref name="TRepeaterModel"/>(s).
  /// </summary>
  public class ScriptFileService<TRepeaterModel>
    where TRepeaterModel :
    RepeaterModel
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