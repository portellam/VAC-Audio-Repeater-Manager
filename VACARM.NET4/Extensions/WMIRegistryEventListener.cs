using System;
using System.Management;

namespace VACARM.NET4.Extensions
{
    public class WmiRegistryEventListener : IDisposable
    {
        #region Parameters

        private const string databaseQuery = @"SELECT * FROM RegistryTreeChangeEvent " +
            "WHERE Hive='HKEY_LOCAL_MACHINE' " +
            "AND RootPath='SOFTWARE'";

        private ManagementEventWatcher managementEventWatcher;

        #endregion

        #region Logic

        /// <summary>
        /// Constructor
        /// </summary>
        public WmiRegistryEventListener()
        {
            managementEventWatcher = new ManagementEventWatcher(databaseQuery);

            managementEventWatcher.EventArrived +=
                new EventArrivedEventHandler(registryEventHandler);

            managementEventWatcher.Start();
        }

        /// <summary>
        /// Whenever an event is received, write the event to console.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="eventArrivedEventArgs">The event arrived event arguments
        /// </param>
        internal void registryEventHandler
            (object sender, EventArrivedEventArgs eventArrivedEventArgs)
        {
            Console.WriteLine("Received an event:");

            foreach (var prop in eventArrivedEventArgs.NewEvent.Properties)
            {
                Console.WriteLine($"{prop.Name}:{prop.Value.ToString()}");
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Dispose the constructor object.
        /// </summary>
        public void Dispose()
        {
            this.managementEventWatcher?.Dispose();
        }

        #endregion
    }
}