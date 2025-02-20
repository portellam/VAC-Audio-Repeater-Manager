using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Security.Principal;
using AudioRepeaterManager.NET4_8.GUI.Extensions.RegistrySubKeyChanged;

namespace AudioRepeaterManager.NET4_8.GUI.Extensions
{
  public class WMIRegistryEventListener : IDisposable
  {
    #region Parameters

    private WindowsIdentity currentWindowsIdentity;

    private List<ManagementEventWatcher> managementEventWatcherList
    {
      get
      {
        return keyPathAndValueNameAndManagementEventWatcherDict
          .SelectMany
          (
            valueNameAndManagementEventWatcherDictionary =>
              valueNameAndManagementEventWatcherDictionary
              .Value
              .Select(managementEventWatcher => managementEventWatcher.Value))
              .ToList();
      }
    }

    private Dictionary<string, Dictionary<string, ManagementEventWatcher>>
      keyPathAndValueNameAndManagementEventWatcherDict;

    private Dictionary<RegistryHive, Dictionary<string, List<string>>>
      registryHiveAndKeyPathAndValueNameListDictionary;

    /// <summary>
    /// Targeted, valid hive objects and values for RegistryEvent class and
    /// derivatives.
    /// URL: https://learn.microsoft.com/en-us/previous-versions/windows/desktop/regprov/registrykeychangeevent
    /// </summary>
    private static Dictionary<RegistryHive, string>
      validRegistryHiveObjectAndValueDictionary =
        new Dictionary<RegistryHive, string>
        {
          {
            RegistryHive.LocalMachine,
            "HKEY_LOCAL_MACHINE"
          },
          {
            RegistryHive.Users,
            "HKEY_USERS"
          },
        };

    public event RegistrySubKeyChangedDelegate RegistrySubKeyChangedDelegate;

    #endregion

    #region Constructor Logic

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="registryHive">The registry hive</param>
    /// <param name="keyPath">The registry key path</param>
    /// <param name="valueName">The registry value name</param>
    public WMIRegistryEventListener(RegistryHive registryHive,
        string keyPath, string valueName)
    {
      this.registryHiveAndKeyPathAndValueNameListDictionary =
        new Dictionary<RegistryHive, Dictionary<string, List<string>>>()
        {
          {
            registryHive, new Dictionary<string, List<string>>
            {
              {
                keyPath,
                new List<string>()
                {
                  valueName
                }
              }
            }
          },
        };

      ConstructorHelper();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="registryHive">The registry hive
    /// <param name="keyPath">The registry key path</param>
    /// <param name="valueNameList">The registry value name list</param>
    public WMIRegistryEventListener
    (
      RegistryHive registryHive,
      string keyPath,
      List<string> valueNameList
    )
    {
      this.registryHiveAndKeyPathAndValueNameListDictionary =
        new Dictionary<RegistryHive, Dictionary<string, List<string>>>()
        {
          {
            registryHive, new Dictionary<string, List<string>>
            {
              {
                  keyPath,
                valueNameList
              }
            }
          },
        };

      ConstructorHelper();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="registryHive">The registry hive</param>
    /// <param name="keyPathAndValueNameListDictionary">The registry key
    /// path and value name list dictionary</param>
    public WMIRegistryEventListener
    (
      RegistryHive registryHive,
      Dictionary<string, List<string>> keyPathAndValueNameListDictionary
    )
    {
      this.registryHiveAndKeyPathAndValueNameListDictionary =
        new Dictionary<RegistryHive, Dictionary<string, List<string>>>()
        {
          {
            registryHive,
            keyPathAndValueNameListDictionary
          },
        };

      ConstructorHelper();
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="registryHiveAndKeyPathAndValueNameListDictionary">The registry
    /// hive, key path, and value name list dictionary</param>
    public WMIRegistryEventListener
    (
      Dictionary<RegistryHive, Dictionary<string, List<string>>>
        registryHiveAndKeyPathAndValueNameListDictionary
    )
    {
      this.registryHiveAndKeyPathAndValueNameListDictionary =
        registryHiveAndKeyPathAndValueNameListDictionary;

      ConstructorHelper();
    }

    /// <summary>
    /// Constructor helper logic
    /// </summary>
    private void ConstructorHelper()
    {
      if
      (
        registryHiveAndKeyPathAndValueNameListDictionary is null
        || registryHiveAndKeyPathAndValueNameListDictionary.Count == 0
      )
      {
        throw new ArgumentNullException();
      }

      keyPathAndValueNameAndManagementEventWatcherDict =
          new Dictionary<string, Dictionary<string, ManagementEventWatcher>>();

      currentWindowsIdentity = WindowsIdentity.GetCurrent();
      ParseConstructorDictionaryAndSetDictionaries();
      StartAllManagementEventWatchers();
    }

    /// <summary>
    /// Get the management event watcher.
    /// </summary>
    /// <param name="query">The query</param>
    /// <returns>The management event watcher</returns>
    private ManagementEventWatcher GetManagementEventWatcher(string query)
    {
      ManagementEventWatcher managementEventWatcher =
        new ManagementEventWatcher(new EventQuery(query));

      int timeoutInSeconds = 5;

      managementEventWatcher.Options.Timeout =
        TimeSpan.FromSeconds(timeoutInSeconds);

      managementEventWatcher.EventArrived +=
        new EventArrivedEventHandler
          (RegistryEventHandler_GetSubKeyValueOnRegistryValueChangeEvent);

      return managementEventWatcher;
    }

    /// <summary>
    /// Get a modified registry key path if the hive is users.
    /// </summary>
    /// <param name="registryHive">The registry hive</param>
    /// <param name="keyPath">The registry key path</param>
    /// <returns>The registry key path</returns>
    private string GetModifiedRegistryKeyPathIfHiveIsUsers
    (
      RegistryHive registryHive,
      string keyPath
    )
    {
      if (registryHive != RegistryHive.Users)
      {
        return keyPath;
      }

      keyPath = string.Format
        (
          "{0}\\{1}",
          currentWindowsIdentity
            .User
            .Value,
          keyPath
        );

      return keyPath.Replace
        (
          "\\",
          "\\\\"
        );
    }

    /// <summary>
    /// Get the WQL query as a string.
    /// </summary>
    /// <param name="registryHive">The registry hive</param>
    /// <param name="keyPath">The registry key path</param>
    /// <param name="valueName">The registry value name</param>
    /// <returns>The WQL query</returns>
    private string GetQuery
    (
      RegistryHive registryHive,
      string keyPath,
      string valueName
    )
    {
      if (string.IsNullOrWhiteSpace(keyPath)
          || string.IsNullOrWhiteSpace(valueName))
      {
        return string.Empty;
      }

      string registryHiveAsString =
          validRegistryHiveObjectAndValueDictionary[registryHive];

      keyPath = GetModifiedRegistryKeyPathIfHiveIsUsers
        (
          registryHive,
          keyPath
        );

      string table = "RegistryValueChangeEvent";

      return string.Format
        (
          "{0} {1} {2} {3} {4}",
          "SELECT *",
          $"FROM {table}",
          $"WHERE Hive = '{registryHiveAsString}'",
          $@"AND KeyPath = '{keyPath}'",
          $"AND ValueName = '{valueName}'"
        );
    }

    /// <summary>
    /// Parse constructor dictionary and set dictionaries.
    /// </summary>
    private void ParseConstructorDictionaryAndSetDictionaries()
    {
      registryHiveAndKeyPathAndValueNameListDictionary
        .Keys
        .ToList()
        .ForEach
        (
          registryHive =>
            ParseRegistryHiveAndSetDictionaries(registryHive)
        );
    }

    /// <summary>
    /// Parse registry hive and set dictionaries.
    /// </summary>
    /// <param name="registryHive">The registry hive</param>
    private void ParseRegistryHiveAndSetDictionaries(RegistryHive registryHive)
    {
      registryHiveAndKeyPathAndValueNameListDictionary
        [registryHive]
        .Keys
        .ToList()
        .ForEach
        (
          keyPath =>
            ParseRegistryHiveAndKeyPathAndSetDictionaries
            (
              registryHive,
              keyPath
            )
        );
    }

    /// <summary>
    /// Parse registry hive and registry 
    /// </summary>
    /// <param name="registryHive">The registry hive</param>
    /// <param name="keyPath">The registry key path</param>
    private void ParseRegistryHiveAndKeyPathAndSetDictionaries
    (
      RegistryHive registryHive,
      string keyPath
    )
    {
      registryHiveAndKeyPathAndValueNameListDictionary
        [registryHive]
        [keyPath]
        .ForEach
        (valueName =>
          ParseRegistryHiveAndKeyPathAndValueNameAndSetDictionaries
          (
            registryHive,
            keyPath,
            valueName)
          );
    }

    /// <summary>
    /// Parse registry key path and set dictionaries. Replace registry hive with
    /// users if current user.
    /// </summary>
    /// <param name="registryHive">The registry hive</param>
    /// <param name="keyPath">The registry key path</param>
    /// <param name="valueName">The registry value name</param>
    private void ParseRegistryHiveAndKeyPathAndValueNameAndSetDictionaries
    (
      RegistryHive registryHive,
      string keyPath,
      string valueName
    )
    {
      if (string.IsNullOrWhiteSpace(valueName))
      {
        return;
      }

      if (registryHive == RegistryHive.CurrentUser)
      {
        registryHive = RegistryHive.Users;
      }

      if
      (
        ! validRegistryHiveObjectAndValueDictionary
          .ContainsKey(registryHive)
      )
      {
        string message = $"Registry Hive '{registryHive.ToString()} is not" +
            "supported.'";

        throw new ArgumentException(message);
      }

      SetManagementEventWatcherInDictionary
        (
          registryHive,
          keyPath,
          valueName
        );
    }

    /// <summary>
    /// On RegistryValueChangeEvent, extract the WQL query fields related to the
    /// dictionary, and get the current sub key value.
    /// </summary>
    /// <param name="sender">The sender object</param>
    /// <param name="eventArrivedEventArgs">The event arrived event arguments
    /// </param>
    private void RegistryEventHandler_GetSubKeyValueOnRegistryValueChangeEvent
    (
      object sender,
      EventArrivedEventArgs eventArrivedEventArgs
    )
    {
      if (! (sender is ManagementEventWatcher))
      {
        return;
      }

      string registryHiveAsString = null;
      string keyPath = null;
      string valueName = null;

      foreach (var propertyData in eventArrivedEventArgs.NewEvent.Properties)
      {
        switch (propertyData.Name)
        {
          case "Hive":
            registryHiveAsString = propertyData
              .Value
              .ToString();

            continue;

          case "KeyPath":
            keyPath = propertyData
              .Value
              .ToString();

            continue;

          case "ValueName":
            valueName = propertyData
              .Value
              .ToString();

            continue;

          default:
            continue;
        }
      }

      if
      (
        string.IsNullOrWhiteSpace(registryHiveAsString)
        || string.IsNullOrWhiteSpace(keyPath)
        || string.IsNullOrWhiteSpace(valueName)
      )
      {
        return;
      }

      RegistryHive registryHive = validRegistryHiveObjectAndValueDictionary
        .FirstOrDefault(x => x.Value == registryHiveAsString)
        .Key;

      OnRegistrySubKeyChanged
        (
          registryHive,
          keyPath,
          valueName
        );
    }

    /// <summary>
    /// Set management event watcher in the dictionary.
    /// </summary>
    /// <param name="registryHive">The registry hive</param>
    /// <param name="keyPath">The registry key path</param>
    /// <param name="valueName">The registry value name</param>
    private void SetManagementEventWatcherInDictionary
    (
      RegistryHive registryHive,
      string keyPath,
      string valueName
    )
    {
      string query = GetQuery
        (
          registryHive,
          keyPath,
          valueName
        );

      ManagementEventWatcher managementEventWatcher =
        GetManagementEventWatcher(query);

      if
      (
        ! keyPathAndValueNameAndManagementEventWatcherDict
          .ContainsKey(keyPath)
      )
      {
        keyPathAndValueNameAndManagementEventWatcherDict
          .Add
          (
            keyPath,
            new Dictionary<string, ManagementEventWatcher>()
            {
              {
                valueName,
                managementEventWatcher
              },
            }
          );
      }
      else if
      (
        ! keyPathAndValueNameAndManagementEventWatcherDict[keyPath]
          .ContainsKey(valueName)
      )
      {
        keyPathAndValueNameAndManagementEventWatcherDict[keyPath]
          .Add
          (
            valueName,
            managementEventWatcher
          );
      }
      else
      {
        keyPathAndValueNameAndManagementEventWatcherDict[keyPath][valueName] =
          managementEventWatcher;
      }
    }

    /// <summary>
    /// Inform observers of a registry sub key that it has changed.
    /// </summary>
    /// <param name="registryHive">The registry key</param>
    /// <param name="keyPath">The registry key path</param>
    /// <param name="valueName">The registry value name</param>
    private void OnRegistrySubKeyChanged(RegistryHive registryHive,
        string keyPath, string valueName)
    {
      if
      (
        string.IsNullOrWhiteSpace(keyPath)
        || string.IsNullOrWhiteSpace(valueName)
      )
      {
        return;
      }

      RegistrySubKeyChangedEventArgs registrySubKeyChangedEventArgs =
        new RegistrySubKeyChangedEventArgs
        (
          registryHive,
          keyPath,
          valueName
        );

      RegistrySubKeyChangedDelegate?
        .Invoke
        (
          this,
          registrySubKeyChangedEventArgs
        );
    }

    /// <summary>
    /// Start all management event watchers.
    /// </summary>
    private void StartAllManagementEventWatchers()
    {
      if (managementEventWatcherList is null
          || managementEventWatcherList.Count == 0)
      {
        return;
      }

      managementEventWatcherList
        .ForEach
        (
          managementEventWatcher =>
            StartManagementEventWatcher(managementEventWatcher)
        );
    }

    /// <summary>
    /// Start management event watcher.
    /// </summary>
    /// <param name="managementEventWatcher">The management event watcher</param>        
    private void StartManagementEventWatcher
      (ManagementEventWatcher managementEventWatcher)
    {
      if (managementEventWatcher is null)
      {
        return;
      }

      managementEventWatcher.Start();
      //TODO: add logger here. Check status (if started or not).
    }

    /// <summary>
    /// Stop all management event watchers.
    /// </summary>
    private void StopAllManagementEventWatchers()
    {
      if (keyPathAndValueNameAndManagementEventWatcherDict
          is null)
      {
        return;
      }

      if (
        keyPathAndValueNameAndManagementEventWatcherDict is null
        || keyPathAndValueNameAndManagementEventWatcherDict.Count == 0
        )
      {
        return;
      }

      managementEventWatcherList
        .ForEach
        (
          managementEventWatcher =>
          StopManagementEventWatcher(managementEventWatcher)
        );
    }

    /// <summary>
    /// Stop management event watcher.
    /// </summary>
    /// <param name="managementEventWatcher">The management event watcher</param>
    private void StopManagementEventWatcher
      (ManagementEventWatcher managementEventWatcher)
    {
      if (managementEventWatcher is null)
      {
        return;
      }

      managementEventWatcher.Stop();
      //TODO: add logger here. Check status (if stopped or not).
    }

    #endregion

    #region Logic

    /// <summary>
    /// Dispose the constructor object.
    /// </summary>
    public void Dispose()
    {
      this.StopAllManagementEventWatchers();
    }

    #endregion
  }
}