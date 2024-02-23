using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace VACARM.NET4.Extensions
{
    public class WMIRegistryEventListener : IDisposable
    {
        //TODO: create new thread with async tasks, monitor for new event on registry
        //  key, and check for new value on event, and retrieve value.

        //TODO: add logic to watch for given sub key and its values.
        //  Track the values changed, or if it does change (boolean).

        #region Parameters

        private Thread thread;

        private Dictionary<RegistryKey, List<string>>
            registryKeyAndSubKeyListDictionary;

        /// <summary>
        /// Nested dictionary of registry key, registry sub key, management event
        /// watcher, and query result.
        /// </summary>
        private Dictionary<RegistryKey, Dictionary<string, Dictionary
            <ManagementEventWatcher, string>>>
            registryKey_SubKeyPath_ManagementEventWatcherDictionary_AndResultDictionary;

        private List<Task> taskList;

        private const string databaseQuery = @"SELECT * FROM RegistryTreeChangeEvent " +
            "WHERE Hive='HKEY_LOCAL_MACHINE' " +
            "AND RootPath='SOFTWARE'";

        private static Dictionary<string, RegistryHive>
            registryBaseKeyHiveValueDictionary = new Dictionary<string, RegistryHive>
            {
                {
                    "HKEY_CLASSES_ROOT", RegistryHive.ClassesRoot
                },
                {
                    "HKEY_CURRENT_USER", RegistryHive.CurrentUser
                },
                {
                    "HKEY_LOCAL_MACHINE", RegistryHive.LocalMachine
                },
                {
                    "HKEY_USERS", RegistryHive.Users
                },
                {
                    "HKEY_PERFORMANCE_DATA", RegistryHive.LocalMachine
                },
                {
                    "HKEY_CURRENT_CONFIG", RegistryHive.CurrentConfig
                },
                {
                    "HKEY_DYN_DATA", RegistryHive.DynData
                },
            };

        #endregion

        #region Logic

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="registryKey">The registry key</param>
        /// <param name="registrySubKeyList">The registry sub key list</param>
        public WMIRegistryEventListener
            (RegistryKey registryKey, List<string> registrySubKeyList)
        {
            if (registryKey.SubKeyCount == 0)
            {
                return;
            }

            this.registryKeyAndSubKeyListDictionary =
                new Dictionary<RegistryKey, List<string>>()
                {
                    {
                        registryKey, registrySubKeyList
                    },
                };


            InitializeDictionary();
            ParseRegistryKeyAndSubKeyListAndSetDictionary();
            ClearListDictionary();
            StartAllManagementEventWatchers();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="registryKeyAndSubKeyPathListDictionary">The dictionary of
        /// registry key and sub key list</param>
        public WMIRegistryEventListener(Dictionary<RegistryKey, List<string>>
            registryKeyAndSubKeyPathListDictionary)
        {
            this.registryKeyAndSubKeyListDictionary =
                registryKeyAndSubKeyPathListDictionary;

            InitializeDictionary();
            ParseRegistryKeyAndSubKeyListAndSetDictionary();
            ClearListDictionary();
            StartAllManagementEventWatchers();
        }

        /// <summary>
        /// Return True if registry key name is found in dictionary.
        /// </summary>
        /// <param name="registryKeyName">The registry key name</param>
        /// <returns>True/False</returns>
        internal bool IsRegistryKeyNameValid(string registryKeyName)
        {
            if (string.IsNullOrEmpty(registryKeyName))
            {
                return false;
            }

            return registryBaseKeyHiveValueDictionary.ContainsKey
                (registryKeyName.ToUpper());
        }

        /// <summary>
        /// Get the database query as a string.
        /// </summary>
        /// <param name="registryKey">The registry key</param>
        /// <param name="registrySubKey">The registry sub key</param>
        internal string GetQuery(RegistryKey registryKey, string registrySubKey)
        {
            if (registryKey is null)
            {
                return string.Empty;
            }

            List<string> registryKeyPathSplitList =
                registryKey.Name.Split('\\').ToList();

            if (string.IsNullOrEmpty(registrySubKey) || registryKeyPathSplitList is null
                || registryKeyPathSplitList.Count == 0)
            {
                return string.Empty;
            }

            string hive = registryKeyPathSplitList.FirstOrDefault();

            if (!registryBaseKeyHiveValueDictionary.ContainsKey(hive)
                || string.IsNullOrEmpty(hive))
            {
                return string.Empty;
            }

            registryKeyPathSplitList.RemoveAt(0);

            if (registryKeyPathSplitList.Count == 0)
            {
                return string.Empty;
            }

            string registrySubKeyPathExceptHive =
                string.Join("\\", registryKeyPathSplitList.ToArray() + registrySubKey);

            if (string.IsNullOrEmpty(registrySubKeyPathExceptHive))
            {
                return string.Empty;
            }

            return $" WHERE Hive='{hive} AND RootPath='{registrySubKeyPathExceptHive}'";
        }

        /// <summary>
        /// Get the management event watcher.
        /// </summary>
        /// <param name="query">The query</param>
        /// <returns>The management event watcher</returns>
        internal ManagementEventWatcher GetManagementEventWatcher(string query)
        {
            ManagementEventWatcher managementEventWatcher =
                new ManagementEventWatcher(databaseQuery);

            managementEventWatcher.EventArrived +=
                new EventArrivedEventHandler(RegistryEventHandler);

            return managementEventWatcher;
        }

        /// <summary>
        /// Get the management event watcher list from the dictionary.
        /// </summary>
        /// <returns>The management event watcher list</returns>
        internal List<ManagementEventWatcher> GetManagementEventWatcherList()
        {
            return
                registryKey_SubKeyPath_ManagementEventWatcherDictionary_AndResultDictionary
                .Values.SelectMany
                    (x => x.Values.SelectMany
                        (y => y.Keys)).ToList();
        }

        /// <summary>
        /// Clear list dictionary.
        /// </summary>
        internal void ClearListDictionary()
        {
            this.registryKeyAndSubKeyListDictionary = null;
        }

        /// <summary>
        /// Initialize dictionary.
        /// </summary>
        internal void InitializeDictionary()
        {
            registryKey_SubKeyPath_ManagementEventWatcherDictionary_AndResultDictionary
                    = new Dictionary<RegistryKey, Dictionary<string,
                        Dictionary<ManagementEventWatcher, string>>>();
        }

        /// <summary>
        /// Parse registry key and sub key list dictionary and set dictionary.
        /// </summary>
        internal void ParseRegistryKeyAndSubKeyListAndSetDictionary()
        {
            if (registryKeyAndSubKeyListDictionary is null
                || registryKeyAndSubKeyListDictionary.Count == 0)
            {
                return;
            }

            registryKeyAndSubKeyListDictionary.Keys.ToList().ForEach(registryKey =>
            {
                ParseRegistryKeyToSetDictionary(registryKey);
            });
        }

        /// <summary>
        /// Parse registry key for sub key path list and set dictionary.
        /// </summary>
        /// <param name="registryKey">The registry key</param>
        internal void ParseRegistryKeyToSetDictionary(RegistryKey registryKey)
        {
            List<string> registrySubKeyPathList =
                registryKeyAndSubKeyListDictionary[registryKey];

            if (registrySubKeyPathList is null || registrySubKeyPathList.Count == 0)
            {
                return;
            }

            registrySubKeyPathList.ForEach(registrySubKeyPath =>
            {
                ParseRegistryKeyAndSubKeyPathToSetDictionary
                    (registryKey, registrySubKeyPath);
            });
        }

        /// <summary>
        /// Set dictionary if registry key, registry sub key, and query are valid.
        /// </summary>
        /// <param name="registryKey">The registry key</param>
        /// <param name="registrySubKey">The registry sub key</param>
        internal void ParseRegistryKeyAndSubKeyPathToSetDictionary
            (RegistryKey registryKey, string registrySubKey)
        {
            if (registryKey is null || string.IsNullOrEmpty(registrySubKey))
            {
                return;
            }

            var subKeyValue = registryKey?.GetValue(registrySubKey).ToString();

            if (subKeyValue is null)
            {
                return;
            }

            string query = GetQuery(registryKey, registrySubKey);

            if (string.IsNullOrEmpty(query))
            {
                return;
            }

            ManagementEventWatcher managementEventWatcher =
                GetManagementEventWatcher(query);

            Dictionary<ManagementEventWatcher, string>
                managementEventWatcherAndResultDictionary =
                new Dictionary<ManagementEventWatcher, string>()
                {
                    {
                        managementEventWatcher, subKeyValue
                    }
                };

            Dictionary<string, Dictionary<ManagementEventWatcher, string>>
                registrySubKeyPathAndNestedDictionary =
                new Dictionary<string, Dictionary<ManagementEventWatcher, string>>()
                {
                    {
                        registrySubKey, managementEventWatcherAndResultDictionary
                    }
                };

            if (registryKey_SubKeyPath_ManagementEventWatcherDictionary_AndResultDictionary
                .ContainsKey(registryKey))
            {
                registryKey_SubKeyPath_ManagementEventWatcherDictionary_AndResultDictionary
                    [registryKey].Concat(registrySubKeyPathAndNestedDictionary);

                return;
            }

            registryKey_SubKeyPath_ManagementEventWatcherDictionary_AndResultDictionary
                .Add(registryKey, registrySubKeyPathAndNestedDictionary);
        }

        /// <summary>
        /// Whenever an event is received, write the event to console.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArrivedEventArgs">The event arrived event arguments
        /// </param>
        internal void RegistryEventHandler
            (object sender, EventArrivedEventArgs eventArrivedEventArgs)
        {
            Console.WriteLine("Received an event:");

            foreach (var propertyData in eventArrivedEventArgs.NewEvent.Properties)
            {
                if (!(propertyData is PropertyData))
                {
                    return;
                }

                RegistryEventHandlerGetNewEventProperties(sender, propertyData);
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Get the new event properties and write to console.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="propertyData">The property data</param>
        internal void RegistryEventHandlerGetNewEventProperties
            (object sender, PropertyData propertyData)
        {
            Console.WriteLine($"{propertyData.Name}:{propertyData.Value.ToString()}");
        }

        /// <summary>
        /// Start all management event watchers.
        /// </summary>
        internal void StartAllManagementEventWatchers()
        {
            if (registryKey_SubKeyPath_ManagementEventWatcherDictionary_AndResultDictionary
                is null)
            {
                return;
            }

            List<ManagementEventWatcher> managementEventWatcherList =
                GetManagementEventWatcherList();

            if (managementEventWatcherList is null
                || managementEventWatcherList.Count == 0)
            {
                return;
            }

            managementEventWatcherList.ForEach(managementEventWatcher =>
            {
                StartManagementEventWatcher(managementEventWatcher);
            });
        }

        /// <summary>
        /// Start management event watcher.
        /// </summary>
        /// <param name="managementEventWatcher">The management event watcher</param>
        internal void StartManagementEventWatcher
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
        internal void StopAllManagementEventWatchers()
        {
            if (registryKey_SubKeyPath_ManagementEventWatcherDictionary_AndResultDictionary
                is null)
            {
                return;
            }

            List<ManagementEventWatcher> managementEventWatcherList =
                GetManagementEventWatcherList();

            if (managementEventWatcherList is null
                || managementEventWatcherList.Count == 0)
            {
                return;
            }

            managementEventWatcherList.ForEach(managementEventWatcher =>
                {
                    StopManagementEventWatcher(managementEventWatcher);
                });
        }

        /// <summary>
        /// Stop management event watcher.
        /// </summary>
        /// <param name="managementEventWatcher">The management event watcher</param>
        internal void StopManagementEventWatcher
            (ManagementEventWatcher managementEventWatcher)
        {
            if (managementEventWatcher is null)
            {
                return;
            }

            managementEventWatcher.Stop();
            //TODO: add logger here. Check status (if stopped or not).
        }

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