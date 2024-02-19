using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace VACARM.NET4
{
    public class WMIRegistryEventListener : IDisposable
    {
        #region Parameters

        private ManagementEventWatcher managementEventWatcher;
        private List<RegistryKey> registryKeyList;

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

        private List<ManagementEventWatcher> managementEventWatcherList =
            new List<ManagementEventWatcher>();

        #endregion

        #region Logic

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="registryKeyPathDictionary">The registry key path and key value</param>
        public WMIRegistryEventListener
            (List<RegistryKey> registryKeyList)
        {
            SetRegistryKeyList(registryKeyList);
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

            if (hive is null || hive == string.Empty || rootPath is null
                || rootPath == string.Empty)
            {
                return string.Empty;
            }

            return $" WHERE Hive='{hive} AND RootPath='{rootPath}'";
        }

        /// <summary>
        /// Add management event watcher.
        /// </summary>
        /// <param name="registryKey"></param>
        internal void AddManagementEventWatcher(RegistryKey registryKey)
        {
            string query = GetQuery(registryKey);

            if (string.IsNullOrEmpty(query))
            {
                return;
            }

            managementEventWatcher = new ManagementEventWatcher(query);
            managementEventWatcherList.Add(managementEventWatcher);

            managementEventWatcher.EventArrived +=
                new EventArrivedEventHandler(RegistryEventHandler);
        }

        /// <summary>
        /// Event handler for Windows Registry.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArrivedEventArgs">The event arrived event arguments
        /// </param>
        internal void RegistryEventHandler
            (object sender, EventArrivedEventArgs eventArrivedEventArgs)
        {
            if (sender is null || eventArrivedEventArgs is null
                || eventArrivedEventArgs.NewEvent.Properties.Count == 0)
            {
                return;
            }

            Console.WriteLine("Received an event:");

            foreach (var property in eventArrivedEventArgs.NewEvent.Properties)
            {
                Console.WriteLine($"{property.Name}:{property.Value.ToString()}");
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Stop and remove management event watcher.
        /// </summary>
        /// <param name="registryKey">The registry key</param>
        internal void RemoveManagementEventWatcher(RegistryKey registryKey)
        {
            string query = GetQuery(registryKey);

            if (string.IsNullOrEmpty(query))
            {
                return;
            }

            managementEventWatcher = new ManagementEventWatcher(query);

            if (!managementEventWatcherList.Contains(managementEventWatcher))
            {
                return;
            }

            managementEventWatcher.Stop();
            //TODO: add logger here. Check status (if stopped or not).
            managementEventWatcherList.Remove(managementEventWatcher);
        }

        /// <summary>
        /// Set the registry key list.
        /// </summary>
        /// <param name="registryKeyList">The registry key list</param>
        internal void SetRegistryKeyList(List<RegistryKey> registryKeyList)
        {
            if (registryKeyList is null || registryKeyList.Count == 0)
            {
                return;
            }

            registryKeyList.ForEach(registryKey =>
            {
                AddRegistryKey(registryKey);
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
        /// Add registry key.
        /// </summary>
        /// <param name="registryKey">The registry key</param>
        public void AddRegistryKey(RegistryKey registryKey)
        {
            if (registryKey is null || registryKeyList.Contains(registryKey))
            {
                return;
            }

            if (!registryBaseKeyHiveValueDictionary.ContainsKey(registryKey.Name))
            {
                throw new ArgumentNullException(nameof(registryKey));
            }

            registryKeyList.Add(registryKey);
            AddManagementEventWatcher(registryKey);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        public void Dispose()
        {
            this.managementEventWatcher?.Dispose();
        }

        /// <summary>
        /// Start all management event watchers.
        /// </summary>
        public void StartAllManagementEventWatchers()
        {
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
        /// Stop all management event watchers.
        /// </summary>
        public void StopAllManagementEventWatchers()
        {
            if (managementEventWatcherList is null
                || managementEventWatcherList.Count == 0)
            {
                return;
            }

            managementEventWatcherList.ForEach(managementEventWatcher =>
            {
                StopManagementEventWatcher(managementEventWatcher);
            });

            Dispose();
        }

        /// <summary>
        /// Remove registry key.
        /// </summary>
        /// <param name="registryKey"></param>
        public void RemoveRegistryKey(RegistryKey registryKey)
        {
            if (registryKey is null || !registryKeyList.Contains(registryKey))
            {
                return;
            }

            registryKeyList.Remove(registryKey);
            RemoveManagementEventWatcher(registryKey);
        }

        /// <summary>
        /// Get the valid registry hive from the registry key.
        /// </summary>
        /// <param name="registryKey">The registry key</param>
        /// <returns>The valid registry hive</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static RegistryHive GetRegistryHive(RegistryKey registryKey)
        {
            if (registryKey is null)
            {
                throw new ArgumentNullException(nameof(registryKey));
            }

            int index = registryKey.Name.IndexOf('\\');
            string baseKeyName;

            if (index == -1)
            {
                baseKeyName = registryKey.Name.Substring(0, index);
            }
            else
            {
                baseKeyName = registryKey.Name;
            }

            if (!registryBaseKeyHiveValueDictionary.ContainsKey(baseKeyName))
            {
                throw new ArgumentException(nameof(baseKeyName));
            }

            return registryBaseKeyHiveValueDictionary[baseKeyName];
        }

        /// <summary>
        /// Get the opened registry key.
        /// </summary>
        /// <param name="registryKey">The registry key</param>
        /// <returns>The opened registry key</returns>
        public static RegistryKey OpenBaseKey(RegistryKey registryKey)
        {
            return RegistryKey.OpenBaseKey
                (GetRegistryHive(registryKey), registryKey.View);
        }

        #endregion
    }
}