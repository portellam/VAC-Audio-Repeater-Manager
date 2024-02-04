using System.Windows.Forms;
using VACARM.NET4.Views;

namespace VACARM.NET4
{
    public class Program
    {
        public static string[] Arguments { get; private set; }

        public static void Main(string[] arguments)
        {
            Arguments = arguments;
            Application.Run(new MainForm());
        }
    }
}
