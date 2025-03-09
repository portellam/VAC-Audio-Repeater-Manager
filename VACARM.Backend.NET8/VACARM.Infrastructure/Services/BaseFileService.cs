using System.Collections.Generic;
using System.Text.Json;
using VACARM.Domain.Models;
using VACARM.Infrastructure.Extensions;

namespace VACARM.Infrastructure.Services
{
  /// <summary>
  /// Read/write <typeparamref name="TBaseModel"/>(s) to/from a file.
  /// </summary>
  public class BaseFileService<TBaseModel>
    where TBaseModel :
    BaseModel
  {
    #region Parameters

    private const string Extension = ".json";

    #endregion

    #region Logic

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
    /// Write enumerable of <typeparamref name="TBaseModel"/>(s) to a JSON file.
    /// </summary>
    /// <param name="enumerable">The enumerable of item(s)</param>
    /// <param name="filePathName">The file path name</param>
    /// <returns>True/false result.</returns>
    public async static Task WriteJsonFileAsync
    (
      IEnumerable<TBaseModel> enumerable,
      string filePathName
    )
    {
      if (IEnumerableExtension<TBaseModel>.IsNullOrEmpty(enumerable))
      {
        return;
      }

      if (StringExtension.IsNullOrEmptyOrWhitespace(filePathName))
      {
        return;
      }

      filePathName = GetModifiedFilePathName(filePathName);
      await using var fileStream = File.Create(filePathName);

      await JsonSerializer.SerializeAsync
        (
          fileStream,
          enumerable
        );
    }

    /// <summary>
    /// Get an enumerable of <typeparamref name="TBaseModel"/>(s) from a JSON file.
    /// </summary>
    /// <param name="filePathName">The file path name</param>
    /// <returns>The enumerable of item(s)</returns>
    public async static IAsyncEnumerable<TBaseModel> ReadJsonFileAsync
    (string filePathName)
    {
      if (StringExtension.IsNullOrEmptyOrWhitespace(filePathName))
      {
        yield break;
      }

      filePathName = GetModifiedFilePathName(filePathName);
      using var fileStream = File.OpenRead(filePathName);
      IEnumerable<TBaseModel>? enumerable;

      try
      {
        enumerable = await 
          JsonSerializer.DeserializeAsync<IEnumerable<TBaseModel>>(fileStream)
          .ConfigureAwait(false);
      }

      catch
      {
        yield break;
      }

      if (IEnumerableExtension<TBaseModel>.IsNullOrEmpty(enumerable))
      {
        yield break;
      }

      foreach (var item in enumerable)
      {
        yield return item;
      }
    }

    #endregion
  }
}