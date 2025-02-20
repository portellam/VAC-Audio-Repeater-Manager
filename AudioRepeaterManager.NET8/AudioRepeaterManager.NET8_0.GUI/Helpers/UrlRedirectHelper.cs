using System.Diagnostics;
using System.Runtime.InteropServices;

namespace AudioRepeaterManager.NET8_0.GUI.Helpers
{
  public class UrlRedirectHelper
  {
    /// <summary>
    /// Opens a URL in the default browser of the system.
    /// </summary>
    /// <param name="url">The URL</param>
    public static void GoToSite(string url)
    {
      if (string.IsNullOrWhiteSpace(url))
      {
        Debug.WriteLine
        (
          "Failed open URL in browser. " +
          "URL is null or whitespace."
        );

        return;
      }

      try
      {
        Process.Start(url);

        Debug.WriteLine
        (
          string.Format
          (
            "Trying to open URL in browser\t=> URL: '{0}'",
            url
          )
        );
      }

      catch
      {
        /*
         * The following implementation is a hack because of this: 
         * https://github.com/dotnet/corefx/issues/10361.
         */

        string newValue;
        string oldValue;

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
          newValue = "^&";
          oldValue = "&";
        }

        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
          newValue = url;
          oldValue = "xdg-open";
        }

        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
          newValue = url;
          oldValue = "open";
        }

        else
        {
          Debug.WriteLine
          (
            "Failed open URL in browser. " +
            "Failed to detect valid OS platform."
          );

          throw;
        }

        url = url.Replace
        (
          newValue,
          oldValue
        );

        Debug.WriteLine
        (
          string.Format
          (
            "Detected valid OS platform and updated URL\t=> " +
            "OS Description: {0}, URL: '{1}'",
            RuntimeInformation.OSDescription,
            url
          )
        );

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
          Process.Start
          (
            new ProcessStartInfo(url)
            {
              UseShellExecute = true
            }
          );
        }

        else
        {
          Process.Start(new ProcessStartInfo(url));
        }

        Debug.WriteLine("Opened URL in browser.");
      }
    }
  }
}