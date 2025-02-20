using Microsoft.Win32;
using System;

namespace AudioRepeaterManager.NET4_0.GUI.Extensions
{
  public class RegistryKeyPropertyGetter
  {
    #region Logic

    /// <summary>
    /// Get the sub key value of a registry key.
    /// </summary>
    /// <param name="registryHive">The registry hive</param>
    /// <param name="registryKeyPath">The registry key path</param>
    /// <param name="registryValueName">The registry value name</param>
    public static object GetRegistrySubKeyValue
    (
      RegistryHive registryHive,
      string registryKeyPath,
      string registryValueName
    )
    {
      RegistryView registryView = Environment.Is64BitOperatingSystem ?
        RegistryView.Registry64
        : RegistryView.Registry32;

      RegistryKey baseRegistryKey = RegistryKey
        .OpenBaseKey
        (
          registryHive,
          registryView
        );

      if (baseRegistryKey is null)
      {
        return null;
      }

      RegistryKey registryKey = baseRegistryKey
        .OpenSubKey
        (
          registryKeyPath,
          false
        );

      return
        registryKey?
        .GetValue(registryValueName);
    }

    #endregion
  }
}