#warning Differs from projects of later NET revisions (above v4.6).

using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace VACARM.Extensions
{
  public class JsonSerializerExtension<T>
  {
    #region Logic

    public async static Task<T> DeserializeAsync<T>(Stream stream)
    {
      if (stream == null)
      {
        throw new ArgumentNullException(nameof(stream));
      }

      var streamReader = new StreamReader(stream);
      var jsonTextReader = new JsonTextReader(streamReader);
      var jsonSerializer = new JsonSerializer();

      return await Task.Run
        (
          () =>
          jsonSerializer.Deserialize<T>(jsonTextReader)
        );
    }

    public async static Task SerializeAsync<T>
    (
      Stream stream,
      T t
    )
    {
      if (stream == null)
      {
        throw new ArgumentNullException(nameof(stream));
      }

      var streamWriter = new StreamWriter(stream);
      var jsonSerializer = new JsonSerializer();

      await Task.Run
        (
          () =>
          jsonSerializer.Serialize
          (
            streamWriter,
            t
          )
        );
    }

    #endregion
  }
}