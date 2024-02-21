using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace VACARM.NET4.Extensions
{
    public class WmiRegistryEventListener : IDisposable
    {
        #region Parameters

        private ManagementEventWatcher managementEventWatcher;

        private Dictionary<RegistryKey, ManagementEventWatcher>
            registryKeyManagementEventWatcherDictionary;

        private Dictionary<RegistryKey, string> registryKeyQueryDictionary;
        private Dictionary<RegistryKey, string> registryKeyValueDictionary;

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
        public WmiRegistryEventListener()
        {
            managementEventWatcher = new ManagementEventWatcher(databaseQuery);

            managementEventWatcher.EventArrived +=
                new EventArrivedEventHandler(RegistryEventHandler);

            managementEventWatcher.Start();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="registryKey">The registry key</param>
        public WmiRegistryEventListener(RegistryKey registryKey)
        {
            SetDictionaries(registryKey);
            StartAllManagementEventWatchers();
        }

        ///TODO: add logic to watch for given subkey and its values. Track the values changed, or if it does change (boolean).

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="registryKeyList">The registry key list</param>
        public WmiRegistryEventListener(List<RegistryKey> registryKeyList)
        {
            IterateRegistryKeyListAndSetDictionaries(registryKeyList);
            StartAllManagementEventWatchers();
        }

        /// <summary>
        /// Get the database query as a string.
        /// </summary>
        /// <param name="registryKey">The registry key</param>
        /// <returns>The database query</returns>
        internal string GetQuery(RegistryKey registryKey)
        {
            if (registryKey is null)
            {
                return string.Empty;
            }

            string hive = registryKey.Name;
            string rootPath = registryKey.ToString();                                   //TODO: get the rootpath as a string here.

            return null;

            if (hive is null || hive == string.Empty || rootPath is null
                || rootPath == string.Empty)
            {
                return string.Empty;
            }

            return $" WHERE Hive='{hive} AND RootPath='{rootPath}'";
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
        /// Iterate registry key list and set dictionaries.
        /// </summary>
        /// <param name="registryKeyList">The registry key list</param>
        internal void IterateRegistryKeyListAndSetDictionaries
            (List<RegistryKey> registryKeyList)
        {
            if (registryKeyList is null || registryKeyList.Count == 0)
            {
                return;
            }

            foreach (RegistryKey registryKey in registryKeyList)
            {
                SetDictionaries(registryKey);
            }
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
            var theSender = sender;
            Console.WriteLine($"{propertyData.Name}:{propertyData.Value.ToString()}");
        }

        /// <summary>
        /// Set dictionaries given registry key.
        /// </summary>
        /// <param name="registryKey">The registry key</param>
        internal void SetDictionaries
            (RegistryKey registryKey)
        {
            if (registryKey is null || IsRegistryKeyNameValid(registryKey.Name)
                || registryKey.SubKeyCount == 0)
            {
                return;
            }

            string query = GetQuery(registryKey);

            if (string.IsNullOrEmpty(query))
            {
                return;
            }

            registryKeyQueryDictionary.Add(registryKey, query);
            registryKeyValueDictionary.Add(registryKey, null);

            ManagementEventWatcher managementEventWatcher =
                new ManagementEventWatcher(databaseQuery);

            managementEventWatcher.EventArrived +=
                new EventArrivedEventHandler(RegistryEventHandler);

            registryKeyManagementEventWatcherDictionary.Add
                (registryKey, managementEventWatcher);
        }

        /// <summary>
        /// Start all management event watchers.
        /// </summary>
        internal void StartAllManagementEventWatchers()
        {
            if (registryKeyManagementEventWatcherDictionary is null
                || registryKeyManagementEventWatcherDictionary.Count == 0)
            {
                return;
            }

            registryKeyManagementEventWatcherDictionary.Values.ToList().ForEach
                (managementEventWatcher =>
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
            if (registryKeyManagementEventWatcherDictionary is null
                || registryKeyManagementEventWatcherDictionary.Count == 0)
            {
                return;
            }

            registryKeyManagementEventWatcherDictionary.Values.ToList().ForEach
                (managementEventWatcher =>
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
            this.managementEventWatcher?.Dispose();
        }

        #endregion
    }
}