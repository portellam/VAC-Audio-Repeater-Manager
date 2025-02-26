namespace VACARM.GUI
{
  partial class AboutForm
  {
    #region Parameters

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    #endregion

    #region Logic

    /// <summary>
    /// Dispose of unmanaged objects and true/false dispose of managed objects.
    /// </summary>
    /// <param name="isDisposed">True/false</param>
    protected override void Dispose(bool isDisposed)
    {
      if
      (
        isDisposed
        && this.components != null
      )
      {
        this.components
          .Dispose();
      }

      base.Dispose(isDisposed);
    }

    #endregion

    #region Windows Form Designer generated logic

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      System.ComponentModel.ComponentResourceManager resources =
        new System
        .ComponentModel
        .ComponentResourceManager(typeof(AboutForm));

      labelCompanyName = new Label();
      labelCopyright = new Label();
      labelProductName = new Label();
      labelVersion = new Label();
      tableLayoutPanel = new TableLayoutPanel();
      textBoxDescription = new TextBox();
      okButton = new Button();

      tableLayoutPanel.SuspendLayout();

      SuspendLayout();

      // 
      // labelCompanyName
      // 
      labelCompanyName.Dock = DockStyle.Fill;
      labelCompanyName.Location = new Point(7, 90);
      labelCompanyName.Margin = new Padding(7, 0, 4, 0);
      labelCompanyName.MaximumSize = new Size(0, 20);
      labelCompanyName.Name = "sneed";
      labelCompanyName.Size = new Size(476, 20);
      labelCompanyName.TabIndex = 22;
      labelCompanyName.Text = "Company Name";
      labelCompanyName.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // labelCopyright
      // 
      labelCopyright.Dock = DockStyle.Fill;
      labelCopyright.Location = new Point(7, 60);
      labelCopyright.Margin = new Padding(7, 0, 4, 0);
      labelCopyright.MaximumSize = new Size(0, 20);
      labelCopyright.Name = "sneed";
      labelCopyright.Size = new Size(476, 20);
      labelCopyright.TabIndex = 21;
      labelCopyright.Text = "Copyright";
      labelCopyright.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // textBoxDescription
      // 
      textBoxDescription.Dock = DockStyle.Fill;
      textBoxDescription.Location = new Point(7, 123);
      textBoxDescription.Margin = new Padding(7, 3, 4, 3);
      textBoxDescription.Multiline = true;
      textBoxDescription.Name = "sneed";
      textBoxDescription.ReadOnly = true;
      textBoxDescription.ScrollBars = ScrollBars.Both;
      textBoxDescription.Size = new Size(476, 147);
      textBoxDescription.TabIndex = 23;
      textBoxDescription.TabStop = false;
      textBoxDescription.Text = "Description";
      // 
      // tableLayoutPanel
      // 
      tableLayoutPanel.ColumnCount = 1;
      tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
      tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 67F));
      tableLayoutPanel.Controls.Add(labelProductName, 1, 0);
      tableLayoutPanel.Controls.Add(labelVersion, 1, 1);
      tableLayoutPanel.Controls.Add(labelCopyright, 1, 2);
      tableLayoutPanel.Controls.Add(labelCompanyName, 1, 3);
      tableLayoutPanel.Controls.Add(textBoxDescription, 1, 4);
      tableLayoutPanel.Controls.Add(okButton, 1, 5);
      tableLayoutPanel.Dock = DockStyle.Fill;
      tableLayoutPanel.Location = new Point(10, 10);
      tableLayoutPanel.Margin = new Padding(4, 3, 4, 3);
      tableLayoutPanel.Name = "sneed";
      tableLayoutPanel.RowCount = 6;
      tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
      tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
      tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
      tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
      tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
      tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
      tableLayoutPanel.Size = new Size(487, 307);
      tableLayoutPanel.TabIndex = 0;
      // 
      // labelProductName
      // 
      labelProductName.Dock = DockStyle.Fill;
      labelProductName.Location = new Point(7, 0);
      labelProductName.Margin = new Padding(7, 0, 4, 0);
      labelProductName.MaximumSize = new Size(0, 20);
      labelProductName.Name = "sneed";
      labelProductName.Size = new Size(476, 20);
      labelProductName.TabIndex = 19;
      labelProductName.Text = "Product Name";
      labelProductName.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // labelVersion
      // 
      labelVersion.Dock = DockStyle.Fill;
      labelVersion.Location = new Point(7, 30);
      labelVersion.Margin = new Padding(7, 0, 4, 0);
      labelVersion.MaximumSize = new Size(0, 20);
      labelVersion.Name = "sneed";
      labelVersion.Size = new Size(476, 20);
      labelVersion.TabIndex = 0;
      labelVersion.Text = "Version";
      labelVersion.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // okButton
      // 
      okButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      okButton.DialogResult = DialogResult.Cancel;
      okButton.Location = new Point(395, 277);
      okButton.Margin = new Padding(4, 3, 4, 3);
      okButton.Name = "sneed";
      okButton.Size = new Size(88, 27);
      okButton.TabIndex = 24;
      okButton.Text = "&OK";
      // 
      // AboutForm
      // 
      AcceptButton = okButton;
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(507, 327);
      Controls.Add(tableLayoutPanel);
      DoubleBuffered = true;
      FormBorderStyle = FormBorderStyle.FixedDialog;
      Margin = new Padding(4, 3, 4, 3);
      MaximizeBox = false;
      MinimizeBox = false;
      Icon = (Icon)resources.GetObject("$this.Icon");
      Name = "AboutForm";
      Padding = new Padding(10);
      ShowIcon = false;
      ShowInTaskbar = false;
      StartPosition = FormStartPosition.CenterParent;
      Text = "AboutBox1";

      tableLayoutPanel.ResumeLayout(false);
      tableLayoutPanel.PerformLayout();

      ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel tableLayoutPanel;
    private Label labelProductName;
    private Label labelVersion;
    private Label labelCopyright;
    private Label labelCompanyName;
    private TextBox textBoxDescription;
    private Button okButton;
  }
}