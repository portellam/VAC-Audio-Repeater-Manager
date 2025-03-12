using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Newtonsoft.Json
{
  public static class JsonSerializerExtension
  {
    #region Logic

    public static async Task SerializeAsync<T>
    (
      Stream stream,
      T value,
      JsonSerializer serializer = null
    )
    {
      if (serializer == null)
      {
        serializer = new JsonSerializer();
      }

      using
      (
        var streamWriter = new StreamWriter
          (
            stream, 
            new UTF8Encoding(false),
            bufferSize: 1024, 
            leaveOpen: true
          )
      )

      using 
      (
        var jsonWriter = new JsonTextWriter(streamWriter)
        { 
          CloseOutput = false, 
          AutoCompleteOnClose = false 
        }
      )
      {
        await Task.Run
          (
            () => serializer.Serialize
            (
              jsonWriter, value
            )
          );

        await jsonWriter.FlushAsync();
      }
    }

    public static async Task<T> DeserializeAsync<T>
    (
      Stream stream, 
      JsonSerializer serializer = null
    )
    {
      if (serializer == null)
      {
        serializer = new JsonSerializer();
      }

      using
      (
        var streamReader = new StreamReader
          (
            stream,
            new UTF8Encoding(false),
            detectEncodingFromByteOrderMarks: true,
            bufferSize: 1024,
            leaveOpen: true
          )
      )
      
      using
      (
        var jsonReader = new JsonTextReader(streamReader)
        {
          CloseInput = false
        }
      )
      {
        return await Task.Run
          (
            () => 
            serializer.Deserialize<T>(jsonReader)
          );
      }
    }


    #endregion
  }
}