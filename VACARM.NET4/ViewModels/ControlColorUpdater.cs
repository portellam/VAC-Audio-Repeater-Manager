using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace VACARM.NET4.ViewModels
{
    public class ControlColorUpdater
    {
        private readonly static Color darkBackColor = Color.FromArgb(60, 63, 65);
        private readonly static Color lightBackColor = Color.White;
        private readonly static Color darkTextColor = Color.White;
        private readonly static Color lightTextColor = Color.Black;

        /// <summary>
        /// Set the colors of the constructor, given dark mode is enabled or not.
        /// </summary>
        public static void SetColorsOfConstructor(object parentObject)
        {
            if (!(parentObject is Form))
            {
                return;
            }

            if (Program.IsDarkModeEnabledDuringRunTime)
            {
                (parentObject as Form).BackColor = darkBackColor;
                (parentObject as Form).ForeColor = darkTextColor;
            }
            else
            {
                (parentObject as Form).BackColor = lightBackColor;
                (parentObject as Form).ForeColor = lightTextColor;
            }
        }

        /// <summary>
        /// Set the colors of every control, given dark mode is enabled or not.
        /// </summary>
        /// <param name="controlCollection">The control collection</param>
        public static void SetColorsOfControlCollection
            (Control.ControlCollection controlCollection)
        {
            if (controlCollection.Count == 0)
            {
                return;
            }

            Color backColor, foreColor;

            if (Program.IsDarkModeEnabledDuringRunTime)
            {
                backColor = darkBackColor;
                foreColor = lightBackColor;
            }
            else
            {
                backColor = lightBackColor;
                foreColor = darkBackColor;
            }

            foreach (var control in controlCollection)
            {
                (control as Control).BackColor = backColor;
                (control as Control).ForeColor = foreColor;

                if (control is Control.ControlCollection)
                {
                    SetColorsOfControlCollection
                        (control as Control.ControlCollection);
                }

                if ((control as Control).Controls.Count == 0)
                {
                    continue;
                }

                SetColorsOfControlCollection((control as Control).Controls);
            }
        }

        /// <summary>
        /// Set the colors of every control in list, given dark mode is enabled or not.
        /// </summary>
        public static void SetColorsOfControlList(List<Control> controlList)
        {
            if (controlList.Count == 0)
            {
                return;
            }

            Color backColor, foreColor;

            if (Program.IsDarkModeEnabledDuringRunTime)
            {
                backColor = darkBackColor;
                foreColor = lightBackColor;
            }
            else
            {
                backColor = lightBackColor;
                foreColor = darkBackColor;
            }

            foreach (Control control in controlList)
            {
                control.BackColor = backColor;
                control.ForeColor = foreColor;

                if (control.Controls.Count == 0)
                {
                    continue;
                }

                SetColorsOfControlCollection(control.Controls);
            };
        }
    }
}
