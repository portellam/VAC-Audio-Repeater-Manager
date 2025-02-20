using System.Threading;
using AudioRepeaterManager.NET4_0.Backend.Consoles;

namespace AudioRepeaterManager.NET4_0.Backend
{
    public class ConsoleWindow
    {
        #region Logic

        /// <summary>
        /// Constructor
        /// </summary>
        public ConsoleWindow()
        {
            var thread = new Thread(() =>
            {
                var mainConsole = new MainConsole();
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        #endregion
    }
}
