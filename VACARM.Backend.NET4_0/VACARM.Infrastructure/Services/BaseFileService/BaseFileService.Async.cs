#warning Differs from projects of later NET revisions (above Framework 4.0).

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace VACARM.Infrastructure.Services
{
  public partial class BaseFileService<TBaseModel>
  {
    #region Logic

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

      if (string.IsNullOrWhiteSpace(filePathName))
      {
        return;
      }

      filePathName = GetModifiedFilePathName(filePathName);

      var fileStream = await Task.Factory
        .StartNew
        (
          () => File.Create(filePathName)
        );

      await JsonSerializerExtension.SerializeAsync<IEnumerable<TBaseModel>>
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
      IEnumerable<TBaseModel> enumerable = ArrayExtension.Empty<TBaseModel>();

      if (string.IsNullOrWhiteSpace(filePathName))
      {
        return enumerable;
      }

      filePathName = GetModifiedFilePathName(filePathName);
      var fileStream = File.OpenRead(filePathName);

      try
      {
        enumerable = await
          JsonSerializerExtension
          .DeserializeAsync<IEnumerable<TBaseModel>>(fileStream)
          .ConfigureAwait(false);
      }

      catch
      {
        enumerable = ArrayExtension.Empty<TBaseModel>();
      }

      fileStream.Dispose();
      return enumerable;
    }

    #endregion
  }
}