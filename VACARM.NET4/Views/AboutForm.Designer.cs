using System.Windows.Forms;

namespace VACARM.NET4.Views
{
    partial class AboutForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private Button okButton;
        private Label labelCompanyName;
        private Label labelCopyright;
        private Label labelProductName;
        private Label labelVersion;
        private TextBox textBoxDescription;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;

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
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.labelCompanyName = new System.Windows.Forms.Label();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.
                ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.
                ColumnStyle(System.Windows.Forms.SizeType.Percent, 67F));
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
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
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
        /// Code to run after generated code.
        /// </summary>
        internal void PostInitializeComponent()
        {
            this.labelProductName.Text = AssemblyTitle;
            this.labelVersion.Text = AssemblyVersion;
            this.labelCopyright.Text = AssemblyCopyright;
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
