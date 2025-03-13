﻿#warning Differs from projects of later NET revisions (above Framework 4.6).

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using VACARM.Domain.Models;
using JsonSerializer = Newtonsoft.Json.Extensions.JsonSerializer;
using String = System.Extensions.String;

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
      if (enumerable.IsNullOrEmpty())
      {
        return;
      }

      if (String.IsNullOrEmptyOrWhitespace(filePathName))
      {
        return;
      }

      filePathName = GetModifiedFilePathName(filePathName);

      var fileStream = await Task.Run
        (
          () => File.Create(filePathName)
        );

      await JsonSerializer.SerializeAsync<IEnumerable<TBaseModel>>
        (
          fileStream,
          enumerable
        );

      fileStream.Dispose();
    }

    /// <summary>
    /// Get an enumerable of <typeparamref name="TBaseModel"/>(s) from a JSON file.
    /// </summary>
    /// <param name="filePathName">The file path name</param>
    /// <returns>The enumerable of item(s)</returns>
    public async static Task<IEnumerable<TBaseModel>> ReadJsonFileAsync
    (string filePathName)
    {
      IEnumerable<TBaseModel> enumerable = Array.Empty<TBaseModel>();

      if (String.IsNullOrEmptyOrWhitespace(filePathName))
      {
        return enumerable;
      }

      filePathName = GetModifiedFilePathName(filePathName);
      var fileStream = File.OpenRead(filePathName);

      try
      {
        enumerable = await JsonSerializer
          .DeserializeAsync<IEnumerable<TBaseModel>>(fileStream)
          .ConfigureAwait(false);
      }

      catch
      {
        enumerable = Array.Empty<TBaseModel>();
      }

      fileStream.Dispose();
      return enumerable;
    }


    #endregion
  }
}