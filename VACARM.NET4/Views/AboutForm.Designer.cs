using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace VACARM.NET4.Views
{
    partial class AboutForm
    {
        private Button okButton;
        private Label labelCompanyName;
        private Label labelCopyright;
        private Label labelProductName;
        private Label labelVersion;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private TextBox textBoxDescription;
        private List<Control> controlList = new List<Control>();

        private readonly Color darkBackColor = Color.FromArgb(60, 63, 65);
        private readonly Color lightBackColor = Color.White;
        private readonly Color darkTextColor = Color.White;
        private readonly Color lightTextColor = Color.Black;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.labelProductName = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.labelCopyright = new System.Windows.Forms.Label();
            this.labelCompanyName = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle
                (System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle
                (System.Windows.Forms.SizeType.Percent, 67F));
            this.tableLayoutPanel.Controls.Add(this.labelProductName, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.labelVersion, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.labelCopyright, 1, 2);
            this.tableLayoutPanel.Controls.Add(this.labelCompanyName, 1, 3);
            this.tableLayoutPanel.Controls.Add(this.textBoxDescription, 1, 4);
            this.tableLayoutPanel.Controls.Add(this.okButton, 1, 5);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(12, 11);
            this.tableLayoutPanel.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 6;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle
                (System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle
                (System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle
                (System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle
                (System.Windows.Forms.SizeType.Percent, 7.97546F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle
                (System.Windows.Forms.SizeType.Percent, 51.84049F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle
                (System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle
                (System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle
                (System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle
                (System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle
                (System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle
                (System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle
                (System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(556, 326);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // labelProductName
            // 
            this.labelProductName.AutoSize = true;
            this.labelProductName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelProductName.Location = new System.Drawing.Point(8, 0);
            this.labelProductName.Margin = new System.Windows.Forms.Padding(8, 0, 4, 0);
            this.labelProductName.MaximumSize = new System.Drawing.Size(0, 21);
            this.labelProductName.Name = "labelProductName";
            this.labelProductName.Size = new System.Drawing.Size(544, 21);
            this.labelProductName.TabIndex = 19;
            this.labelProductName.Text = "Product Name";
            this.labelProductName.TextAlign =
                 System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelVersion
            // 
            this.labelVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelVersion.Location = new System.Drawing.Point(8, 32);
            this.labelVersion.Margin = new System.Windows.Forms.Padding(8, 0, 4, 0);
            this.labelVersion.MaximumSize = new System.Drawing.Size(0, 21);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(544, 21);
            this.labelVersion.TabIndex = 0;
            this.labelVersion.Text = "Version";
            this.labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelCopyright
            // 
            this.labelCopyright.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCopyright.Location = new System.Drawing.Point(8, 64);
            this.labelCopyright.Margin = new System.Windows.Forms.Padding(8, 0, 4, 0);
            this.labelCopyright.MaximumSize = new System.Drawing.Size(0, 42);
            this.labelCopyright.MinimumSize = new System.Drawing.Size(0, 42);
            this.labelCopyright.Name = "labelCopyright";
            this.labelCopyright.Size = new System.Drawing.Size(544, 42);
            this.labelCopyright.TabIndex = 21;
            this.labelCopyright.Text = "Copyright";
            // 
            // labelCompanyName
            // 
            this.labelCompanyName.Anchor = ((System.Windows.Forms.AnchorStyles)
                ((System.Windows.Forms.AnchorStyles.Bottom
                 | System.Windows.Forms.AnchorStyles.Left)));
            this.labelCompanyName.Location = new System.Drawing.Point(8, 101);
            this.labelCompanyName.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.labelCompanyName.MaximumSize = new System.Drawing.Size(0, 21);
            this.labelCompanyName.Name = "labelCompanyName";
            this.labelCompanyName.Size = new System.Drawing.Size(0, 21);
            this.labelCompanyName.TabIndex = 22;
            this.labelCompanyName.Text = "Company Name";
            this.labelCompanyName.TextAlign =
                 System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBoxDescription.Location = new System.Drawing.Point(8, 126);
            this.textBoxDescription.Margin =
                 new System.Windows.Forms.Padding(8, 4, 4, 4);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.ReadOnly = true;
            this.textBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxDescription.Size = new System.Drawing.Size(544, 161);
            this.textBoxDescription.TabIndex = 23;
            this.textBoxDescription.TabStop = false;
            this.textBoxDescription.Text = "Description";
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)
                ((System.Windows.Forms.AnchorStyles.Bottom
                 | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.okButton.Location = new System.Drawing.Point(452, 295);
            this.okButton.Margin = new System.Windows.Forms.Padding(4);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(100, 27);
            this.okButton.TabIndex = 24;
            this.okButton.Text = "&OK";
            this.okButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // AboutForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 348);
            this.Controls.Add(this.tableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.Padding = new System.Windows.Forms.Padding(12, 11, 12, 11);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AboutBox1";
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        /// <summary>
        /// Add all controls to list.
        /// </summary>
        internal void AddControlsToList()
        {
            controlList.Add(labelCompanyName);
            controlList.Add(labelCopyright);
            controlList.Add(labelProductName);
            controlList.Add(labelVersion);
            controlList.Add(okButton);
            controlList.Add(tableLayoutPanel);
            controlList.Add(textBoxDescription);
        }

        /// <summary>
        /// Code to run after generated code.
        /// </summary>
        internal void PostInitializeComponent()
        {
            SetAssemblyInformation();
            AddControlsToList();
            SetInitialChanges();
            SetColorTheme();
        }

        /// <summary>
        /// Set about page information.
        /// </summary>
        internal void SetAssemblyInformation()
        {
            this.labelCompanyName.Text = AssemblyCompany;
            this.labelCopyright.Text = AssemblyCopyright;
            this.labelProductName.Text = AssemblyProduct;
            this.labelVersion.Text = String.Format("Version {0}", AssemblyVersion);
            this.Text = String.Format("About {0}", AssemblyTitle);
            this.textBoxDescription.Text = AssemblyDescription;
        }

        /// <summary>
        /// Set color theme given dark mode is enabled or not.
        /// </summary>
        internal void SetColorTheme()
        {
            SetConstructorBackColor();
            SetBackAndForeColorOfEveryControl(this.Controls);
            this.Invalidate();
        }

        /// <summary>
        /// Set initial changes to form.
        /// </summary>
        internal void SetInitialChanges()
        {
            this.Text = AssemblyProduct;
        }

        /// <summary>
        /// Set the backcolor and forecolor of every control, 
        /// given dark mode is enabled or not.
        /// </summary>
        /// <param name="controlCollection">The control collection</param>
        internal void SetBackAndForeColorOfEveryControl
            (Control.ControlCollection controlCollection)
        {
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
                if (control is Control.ControlCollection)
                {
                    SetBackAndForeColorOfEveryControl
                        (control as Control.ControlCollection);
                }

                (control as Control).BackColor = backColor;
                (control as Control).ForeColor = foreColor;
            }

            controlList.ForEach(control =>
            {
                control.BackColor = backColor;
                control.ForeColor = foreColor;
            });
        }

        /// <summary>
        /// Set the backcolor of the constructor, given dark mode is enabled or not.
        /// </summary>
        internal void SetConstructorBackColor()
        {
            if (Program.IsDarkModeEnabledDuringRunTime)
            {
                this.BackColor = darkBackColor;
                this.ForeColor = darkTextColor;
            }
            else
            {
                this.BackColor = lightBackColor;
                this.ForeColor = lightTextColor;
            }
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}