#warning Differs from projects of earlier NET revisions (below Framework 4.8).

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text.Json;
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

      var fileStream = await Task.Run
        (
          () => File.Create(filePathName)
        );

      try
      {
        await JsonSerializer.SerializeAsync<IEnumerable<TBaseModel>>
        (
          fileStream,
          enumerable
        );

        Debug.WriteLine
           (
             string.Format
             (
               "Successfully wrote JSON file\t=> File name: '{0}', Count: {1}",
               filePathName,
               enumerable.Count()
             )
           );
      }

      catch (Exception exception)
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Failed to write JSON file\t=> File name: '{0}', Count: {1}",
            filePathName,
            enumerable.Count()
          )
        );
      }

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

      if (string.IsNullOrWhiteSpace(filePathName))
      {
        return enumerable;
      }

      filePathName = GetModifiedFilePathName(filePathName);
      var fileStream = File.OpenRead(filePathName);

      try
      {
        enumerable = await
          JsonSerializer.DeserializeAsync<IEnumerable<TBaseModel>>(fileStream)
          .ConfigureAwait(false);

        Debug.WriteLine
        (
          string.Format
          (
            "Successfully read JSON file\t=> File name: '{0}', Count: {1}",
            filePathName,
            enumerable.Count()
          )
        );
      }

      catch (Exception exception)
      {
        Debug.WriteLine
        (
          string.Format
          (
            "Failed to read JSON file\t=> File name: '{0}'",
            filePathName
          )
        );

        enumerable = Array.Empty<TBaseModel>();
      }

      fileStream.Dispose();
      return enumerable;
    }

    #endregion
  }
}