﻿using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace VACARM.NET4.ViewModels
{
    public class FormColorUpdater
    {
        #region Parameters

        private readonly static Color darkBackColor = Color.FromArgb(60, 63, 65);
        private readonly static Color darkTextColor = Color.White;
        private readonly static Color lightBackColor = Color.White;
        private readonly static Color lightTextColor = Color.Black;

        public static Color BackColor
        {
            get
            {
                if (Program.IsDarkModeEnabledDuringRunTime)
                {
                    return darkBackColor;
                }
                else
                {
                    return lightBackColor;
                }
            }
        }

        public static Color ForeColor
        {
            get
            {
                if (Program.IsDarkModeEnabledDuringRunTime)
                {
                    return darkTextColor;
                }
                else
                {
                    return lightTextColor;
                }
            }
        }

        #endregion

        #region Functions

        /// <summary>
        /// Set the colors of the constructor, given dark mode is enabled or not.
        /// </summary>
        public static void SetColorsOfConstructor(object parentObject)
        {
            if (!(parentObject is Form))
            {
                return;
            }

            (parentObject as Form).BackColor = BackColor;
            (parentObject as Form).ForeColor = ForeColor;
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

            foreach (var control in controlCollection)
            {
                (control as Control).BackColor = BackColor;
                (control as Control).ForeColor = ForeColor;

                if (control is Control.ControlCollection)
                {
                    SetColorsOfControlCollection
                        (control as Control.ControlCollection);
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

            foreach (Control control in controlList)
            {
                control.BackColor = BackColor;
                control.ForeColor = ForeColor;
                SetColorsOfControlCollection(control.Controls);
            };
        }

        /// <summary>
        /// Set the colors of every tool strip menu item in list, 
        /// given dark mode is enabled or not.
        /// </summary>
        public static void SetColorsOfToolStripMenuItemList
            (List<ToolStripMenuItem> toolStripMenuItemList)
        {
            toolStripMenuItemList.ForEach(toolStripMenuItem =>
            {
                toolStripMenuItem.ForeColor = ForeColor;
                SetColorsOfToolStripItemDropDownList(toolStripMenuItem.DropDownItems);
            });
        }

        /// <summary>
        /// Set the colors of every tool strip menu item in drop down list, 
        /// given dark mode is enabled or not.
        /// </summary>
        internal static void SetColorsOfToolStripItemDropDownList
            (ToolStripItemCollection toolStripItemCollection)
        {
            foreach(ToolStripItem toolStripItem in toolStripItemCollection)
            {
                toolStripItem.ForeColor = ForeColor;
            }
        }

        #endregion
    }
}