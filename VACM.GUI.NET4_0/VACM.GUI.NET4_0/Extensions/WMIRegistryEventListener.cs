﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace VACM.NET4.Extensions
{
    public class WMIRegistryEventListener
    {
        #region Parameters

        private Dictionary<string, Dictionary<string, ManagementEventWatcher>>
            registryKeyPathAndValueNameAndManagementEventWatcherDictionary;

        private Dictionary<RegistryHive, Dictionary<string, List<string>>>
            registryHiveAndKeyPathAndValueNameListDictionary;

        /// <summary>
        /// Valid hive objects and values for RegistryEvent class and derivatives.
        /// URL: https://learn.microsoft.com/en-us/previous-versions/windows/desktop/regprov/registrykeychangeevent
        /// </summary>
        private static Dictionary<RegistryHive, string>
            validRegistryHiveObjectAndValueDictionary =
            new Dictionary<RegistryHive, string>
            {
                {
                    RegistryHive.LocalMachine,      "HKEY_LOCAL_MACHINE"
                },
                {
                    RegistryHive.Users,             "HKEY_USERS"
                },
            };

        #endregion

        #region Constructor Logic

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="registryHive">The registry hive</param>
        /// <param name="registryKeyPath">The registry key path</param>
        /// <param name="registryValueNameList">The registry value name list</param>
        public WMIRegistryEventListener(RegistryHive registryHive,
            string registryKeyPath, List<string> registryValueNameList)
        {
            this.registryHiveAndKeyPathAndValueNameListDictionary =
                new Dictionary<RegistryHive, Dictionary<string, List<string>>>()
                {
                    {
                        registryHive, new Dictionary<string, List<string>>
                        {
                            { registryKeyPath, registryValueNameList }
                        }
                    },
                };

            //TODO: add constuctor helper here.
        }

        //TODO: add multiple constructors for ease of use.

        //TODO: add start and stop watcher methods.

        //TODO: add dispose.

        // TODO: add on value change event.

        /// <summary>
        /// Get the registry key from the valid hive.
        /// </summary>
        /// <param name="registryHive">The registry hive</param>
        /// <returns>The registry key</returns>
        internal RegistryKey GetRegistryKeyFromHive(RegistryHive registryHive)
        {
            if (!validRegistryHiveObjectAndValueDictionary.ContainsKey(registryHive))
            {
                return null;
            }

            switch (registryHive)
            {
                case RegistryHive.LocalMachine:
                    return Registry.LocalMachine;

                case RegistryHive.Users:
                    return Registry.CurrentUser;

                default:
                    return null;
            }
        }

        #endregion

        #region Logic

        /// <summary>
        /// Get sub key value of the registry key.
        /// </summary>
        /// <param name="registryHive">The registry hive</param>
        /// <param name="registryKeyPath">The registry key path</param>
        /// <param name="registryValueName">The registry value name</param>
        public string GetSubKeyValueOfRegistryKey(RegistryHive registryHive,
            string registryKeyPath, string registryValueName)
        {
            RegistryKey registryKey = GetRegistryKeyFromHive(registryHive);

            if (registryKey is null)
            {
                return null;
            }

            registryKey.OpenSubKey(registryKeyPath);
            var subKeyValue = registryKey?.GetValue(registryValueName);

            if (subKeyValue is null)
            {
                return null;
            }

            return subKeyValue.ToString();
        }

        /// <summary>
        /// Dispose the constructor object.
        /// </summary>
        public void Dispose()
        {
            //TODO: stop all watchers on exit.
        }

        #endregion

    }
}