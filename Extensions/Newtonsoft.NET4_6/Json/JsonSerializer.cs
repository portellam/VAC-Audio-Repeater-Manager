using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Newtonsoft.Json.Extensions
{
  public static class JsonSerializer
  {
    #region Parameters

    private static readonly int BufferSize = 1024;
    private static readonly UTF8Encoding UTF8Encoding = new UTF8Encoding(false);

    #endregion

    #region Logic

    public async static Task SerializeAsync<T>
    (
      Stream stream,
      T value
    )
    {
      var jsonSerializer = new Newtonsoft.Json.JsonSerializer();

      using
      (
        var streamWriter = new StreamWriter
          (
            stream,
            UTF8Encoding,
            bufferSize: BufferSize,
            leaveOpen: true
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
            () => jsonSerializer.Serialize
            (
              jsonTextWriter,
              value
            )
          );

        await jsonTextWriter.FlushAsync();
      }
    }

    public async static Task<T> DeserializeAsync<T>(Stream stream)
    {
      var jsonSerializer = new Newtonsoft.Json.JsonSerializer();

      using
      (
        var streamReader = new StreamReader
          (
            stream,
            UTF8Encoding,
            detectEncodingFromByteOrderMarks: true,
            bufferSize: BufferSize,
            leaveOpen: true
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
            jsonSerializer.Deserialize<T>(jsonTextReader)
          );
      }
    }

    #endregion
  }
}