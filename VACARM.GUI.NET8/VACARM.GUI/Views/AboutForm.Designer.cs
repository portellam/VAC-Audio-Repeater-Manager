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
      components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
      labelCompanyName = new Label();
      labelCopyright = new Label();
      labelProductName = new Label();
      labelVersion = new Label();
      tableLayoutPanel = new TableLayoutPanel();
      logoPictureBox = new PictureBox();
      textBoxDescription = new TextBox();
      okButton = new Button();
      tableLayoutPanel.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)logoPictureBox).BeginInit();
      SuspendLayout();
      // 
      // labelCompanyName
      // 
      labelCompanyName.Dock = DockStyle.Fill;
      labelCompanyName.Location = new Point(112, 90);
      labelCompanyName.Margin = new Padding(7, 0, 4, 0);
      labelCompanyName.MaximumSize = new Size(0, 20);
      labelCompanyName.Name = "labelCompanyName";
      labelCompanyName.Size = new Size(371, 20);
      labelCompanyName.TabIndex = 22;
      labelCompanyName.Text = "Company Name";
      labelCompanyName.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // labelCopyright
      // 
      labelCopyright.Dock = DockStyle.Fill;
      labelCopyright.Location = new Point(112, 60);
      labelCopyright.Margin = new Padding(7, 0, 4, 0);
      labelCopyright.MaximumSize = new Size(0, 20);
      labelCopyright.Name = "labelCopyright";
      labelCopyright.Size = new Size(371, 20);
      labelCopyright.TabIndex = 21;
      labelCopyright.Text = "Copyright";
      labelCopyright.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // labelProductName
      // 
      labelProductName.Dock = DockStyle.Fill;
      labelProductName.Location = new Point(112, 0);
      labelProductName.Margin = new Padding(7, 0, 4, 0);
      labelProductName.MaximumSize = new Size(0, 20);
      labelProductName.Name = "labelProductName";
      labelProductName.Size = new Size(371, 20);
      labelProductName.TabIndex = 19;
      labelProductName.Text = "Product Name";
      labelProductName.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // labelVersion
      // 
      labelVersion.Dock = DockStyle.Fill;
      labelVersion.Location = new Point(112, 30);
      labelVersion.Margin = new Padding(7, 0, 4, 0);
      labelVersion.MaximumSize = new Size(0, 20);
      labelVersion.Name = "labelVersion";
      labelVersion.Size = new Size(371, 20);
      labelVersion.TabIndex = 0;
      labelVersion.Text = "Version";
      labelVersion.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // tableLayoutPanel
      // 
      tableLayoutPanel.ColumnCount = 2;
      tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 105F));
      tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
      tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 67F));
      tableLayoutPanel.Controls.Add(logoPictureBox, 0, 0);
      tableLayoutPanel.Controls.Add(labelProductName, 2, 0);
      tableLayoutPanel.Controls.Add(labelVersion, 2, 1);
      tableLayoutPanel.Controls.Add(labelCopyright, 2, 2);
      tableLayoutPanel.Controls.Add(labelCompanyName, 2, 3);
      tableLayoutPanel.Controls.Add(textBoxDescription, 2, 4);
      tableLayoutPanel.Controls.Add(okButton, 2, 5);
      tableLayoutPanel.Dock = DockStyle.Fill;
      tableLayoutPanel.Location = new Point(10, 10);
      tableLayoutPanel.Margin = new Padding(4, 3, 4, 3);
      tableLayoutPanel.Name = "tableLayoutPanel";
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
      // logoPictureBox
      // 
      logoPictureBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
      logoPictureBox.Image = (Image)resources.GetObject("logoPictureBox.Image");
      logoPictureBox.Location = new Point(4, 3);
      logoPictureBox.Margin = new Padding(4, 3, 4, 3);
      logoPictureBox.Name = "logoPictureBox";
      tableLayoutPanel.SetRowSpan(logoPictureBox, 6);
      logoPictureBox.Size = new Size(96, 301);
      logoPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
      logoPictureBox.TabIndex = 25;
      logoPictureBox.TabStop = false;
      // 
      // textBoxDescription
      // 
      textBoxDescription.Dock = DockStyle.Fill;
      textBoxDescription.Location = new Point(112, 123);
      textBoxDescription.Margin = new Padding(7, 3, 4, 3);
      textBoxDescription.Multiline = true;
      textBoxDescription.Name = "textBoxDescription";
      textBoxDescription.ReadOnly = true;
      textBoxDescription.ScrollBars = ScrollBars.Both;
      textBoxDescription.Size = new Size(371, 147);
      textBoxDescription.TabIndex = 23;
      textBoxDescription.TabStop = false;
      textBoxDescription.Text = "Description";
      // 
      // okButton
      // 
      okButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      okButton.DialogResult = DialogResult.Cancel;
      okButton.Location = new Point(395, 277);
      okButton.Margin = new Padding(4, 3, 4, 3);
      okButton.Name = "okButton";
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
      Name = "AboutForm";
      Padding = new Padding(10);
      ShowIcon = false;
      ShowInTaskbar = false;
      StartPosition = FormStartPosition.CenterParent;
      Text = "AboutBox1";
      tableLayoutPanel.ResumeLayout(false);
      tableLayoutPanel.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)logoPictureBox).EndInit();
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
    private PictureBox logoPictureBox;
  }
}