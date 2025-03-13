using Newtonsoft.Json;
using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Newtonsoft.Json
{
  public static class JsonSerializerExtension
  {
    #region Parameters

    private static bool LeaveOpen = true;
    private static readonly int BufferSize = 1024;
    private static readonly UTF8Encoding UTF8Encoding = new UTF8Encoding(false);

    #endregion

    #region Logic

    /// <summary>
    /// Converts the provided value to UTF-8 encoded JSON text and writes it to
    /// the System.IO.Stream.
    /// </summary>
    /// <typeparam name="TValue">The target type of the JSON value.</typeparam>
    /// <param name="utf8Json">The UTF-8 System.IO.Stream to write to.</param>
    /// <param name="value">The value to convert</param>
    /// <returns>
    /// A task that represents the asynchronous write operation.
    /// </returns>
    /// <exception cref="ArgumentNullException">utf8Json is null.</exception>
    public async static Task SerializeAsync<TValue>
    (
      Stream utf8Json,
      TValue value
    )
    {
      if (utf8Json == null)
      {
        throw new ArgumentNullException(nameof(utf8Json));
      }

      var jsonSerializer = new JsonSerializer();

      using
      (
        var streamWriter = new StreamWriter
          (
            utf8Json,
            UTF8Encoding,
            bufferSize: BufferSize,
            leaveOpen: LeaveOpen
          )
      )

      using
      (
        var jsonTextWriter = new JsonTextWriter(streamWriter)
        {
          CloseOutput = false,
          AutoCompleteOnClose = false
        }
      )
      {
        await Task.Run
          (
            () => 
            jsonSerializer.Serialize
              (
                jsonTextWriter,
                value
              )
          );

        await jsonTextWriter.FlushAsync();
      }
    }

    /// <summary>
    /// Asynchronously reads the UTF-8 encoded text representing a single JSON
    /// value into an instance of a type specified by a generic type parameter.
    /// The stream will be read to completion.
    /// </summary>
    /// <typeparam name="TValue">The target type of the JSON value.</typeparam>
    /// <param name="utf8Json">The JSON data to parse.</param>
    /// <returns>
    /// A <typeparamref name="TValue"/> representation of the JSON value.
    /// </returns>
    /// <exception cref="ArgumentNullException">utf8Json is null.</exception>
    public async static Task<TValue> DeserializeAsync<TValue>(Stream utf8Json)
    {
      if (utf8Json == null)
      {
        throw new ArgumentNullException(nameof(utf8Json));
      }

      var jsonSerializer = new JsonSerializer();

      using
      (
        var streamReader = new StreamReader
          (
            utf8Json,
            UTF8Encoding,
            detectEncodingFromByteOrderMarks: true,
            bufferSize: BufferSize,
            leaveOpen: LeaveOpen
          )
      )

      using
      (
        var jsonTextReader = new JsonTextReader(streamReader)
        {
          CloseInput = false
        }
      )
      {
        return await Task.Run
          (
            () =>
            jsonSerializer.Deserialize<TValue>(jsonTextReader)
          );
      }
    }

    #endregion
  }
}