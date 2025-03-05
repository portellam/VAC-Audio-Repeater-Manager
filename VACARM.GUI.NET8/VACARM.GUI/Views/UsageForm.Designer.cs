namespace VACARM.GUI.Views
{
  partial class UsageForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

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

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      tableLayoutPanel = new TableLayoutPanel();
      okButton = new Button();
      textBoxDescription = new TextBox();
      tableLayoutPanel.SuspendLayout();
      SuspendLayout();
      // 
      // tableLayoutPanel
      // 
      tableLayoutPanel.ColumnCount = 1;
      tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
      tableLayoutPanel.Controls.Add(textBoxDescription, 0, 0);
      tableLayoutPanel.Controls.Add(okButton, 0, 1);
      tableLayoutPanel.Dock = DockStyle.Fill;
      tableLayoutPanel.Location = new Point(9, 9);
      tableLayoutPanel.Name = "tableLayoutPanel";
      tableLayoutPanel.RowCount = 2;
      tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 83.3333359F));
      tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
      tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
      tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
      tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
      tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
      tableLayoutPanel.Size = new Size(417, 265);
      tableLayoutPanel.TabIndex = 0;
      // 
      // okButton
      // 
      okButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      okButton.DialogResult = DialogResult.Cancel;
      okButton.Location = new Point(339, 239);
      okButton.Name = "okButton";
      okButton.Size = new Size(75, 23);
      okButton.TabIndex = 24;
      okButton.Text = "&OK";
      // 
      // textBoxDescription
      // 
      textBoxDescription.Dock = DockStyle.Fill;
      textBoxDescription.Location = new Point(6, 3);
      textBoxDescription.Margin = new Padding(6, 3, 3, 3);
      textBoxDescription.Multiline = true;
      textBoxDescription.Name = "textBoxDescription";
      textBoxDescription.ReadOnly = true;
      textBoxDescription.ScrollBars = ScrollBars.Both;
      textBoxDescription.Size = new Size(408, 214);
      textBoxDescription.TabIndex = 23;
      textBoxDescription.TabStop = false;
      textBoxDescription.Text = "Description";
      // 
      // UsageForm
      // 
      AcceptButton = okButton;
      AutoScaleDimensions = new SizeF(6F, 13F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(435, 283);
      Controls.Add(tableLayoutPanel);
      FormBorderStyle = FormBorderStyle.FixedDialog;
      MaximizeBox = false;
      MinimizeBox = false;
      Name = "UsageForm";
      Padding = new Padding(9);
      ShowIcon = false;
      ShowInTaskbar = false;
      StartPosition = FormStartPosition.CenterParent;
      Text = "UsageForm";
      tableLayoutPanel.ResumeLayout(false);
      tableLayoutPanel.PerformLayout();
      ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
    private TextBox textBoxDescription;
    private Button okButton;
  }
}
