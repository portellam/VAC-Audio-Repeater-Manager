using System;
using System.IO;
using System.Text;
using System.Threading;

namespace System.IO
{
  public static class FileExtension
  {
    #region Logic

    /// <summary>
    /// Asynchronously creates a new file, writes the specified string to the
    /// file, and then closes the file. If the target file already exists, it is
    /// overwritten.
    /// </summary>
    /// <param name="path">The file to write to.</param>
    /// <param name="contents">The string to write to the file.</param>
    /// <returns>
    /// A thread that represents the asynchronous write operation.
    /// </returns>
    /// <exception cref="ArgumentNullException"></exception>
    public async static void WriteAllTextAsync
    (
      string path,
      string contents
    )
    {
      if (StringExtension.IsNullOrWhiteSpace(path))
      {
        throw new ArgumentNullException(path);
      }

      if (contents == null)
      {
        contents = string.Empty;
      }

      using
      (
        var streamWriter = new StreamWriter
          (
            path,
            false,
            Encoding.UTF8
          )
      )
      {
        var action = new Action
          (
            streamWriter.Write(contents)
          );

        await TaskSimulator.RunAsync
          (
            action
          );
      }
    }

    #endregion
  }
}