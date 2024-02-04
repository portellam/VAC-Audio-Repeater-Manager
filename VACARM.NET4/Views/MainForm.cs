using System;
using System.Windows.Forms;

namespace VACARM.NET4.Views
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            PostInitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
             * TODO:
             *  -add logic to...
             *      -check if runtime data is saved to file.
             *      -warn user to save changes.
             *      -warn user that audio repeaters may exit at app shutdown.
             */
            Application.Exit();
        }

        private void deviceToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void linkToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
