namespace VACARM.GUI.Views
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
        new System.ComponentModel
          .ComponentResourceManager(typeof(AboutForm));

      this.labelCompanyName = new Label();
      this.labelCopyright = new Label();
      this.labelProductName = new Label();
      this.labelVersion = new Label();
      this.tableLayoutPanel = new TableLayoutPanel();
      this.logoPictureBox = new PictureBox();
      this.textBoxDescription = new TextBox();
      this.okButton = new Button();
      this.tableLayoutPanel.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)logoPictureBox).BeginInit();
      this.SuspendLayout();
      // 
      // labelCompanyName
      // 
      this.labelCompanyName
        .Dock = DockStyle.Fill;

      this.labelCompanyName
        .Location = new Point
        (
          96, 
          78
        );

      this.labelCompanyName
        .Margin = new Padding
        (
          6, 
          0, 
          3, 
          0
        );

      this.labelCompanyName
        .MaximumSize = new Size
        (
          0, 
          17
        );

      this.labelCompanyName
        .Name = "labelCompanyName";

      this.labelCompanyName
        .Size = new Size
        (
          318, 
          17
        );

      this.labelCompanyName
        .TabIndex = 22;

      this.labelCompanyName
        .Text = "Company Name";

      this.labelCompanyName
        .TextAlign = ContentAlignment.MiddleLeft;
      // 
      // labelCopyright
      // 
      this.labelCopyright
        .Dock = DockStyle.Fill;

      this.labelCopyright
        .Location = new Point
        (
          96, 
          52
        );

      this.labelCopyright
        .Margin = new Padding
        (
          6, 
          0, 
          3, 
          0
        );

      this.labelCopyright
        .MaximumSize = new Size
        (
          0, 
          17
        );

      this.labelCopyright
        .Name = "labelCopyright";

      this.labelCopyright
        .Size = new Size
        (
          318, 
          17
        );

      this.labelCopyright
        .TabIndex = 21;

      this.labelCopyright
        .Text = "Copyright";

      this.labelCopyright
        .TextAlign = ContentAlignment.MiddleLeft;
      // 
      // labelProductName
      // 
      this.labelProductName
        .Dock = DockStyle.Fill;

      this.labelProductName
        .Location = new Point
        (
          96,
          0
        );

      this.labelProductName
        .Margin = new Padding
        (
          6, 
          0, 
          3, 
          0
        );

      this.labelProductName
        .MaximumSize = new Size
        (
          0, 
          17
        );

      this.labelProductName
        .Name = "labelProductName";

      this.labelProductName
        .Size = new Size
        (
          318, 
          17
        );

      this.labelProductName
        .TabIndex = 19;

      this.labelProductName
        .Text = "Product Name";

      this.labelProductName
        .TextAlign = ContentAlignment.MiddleLeft;
      // 
      // labelVersion
      // 
      this.labelVersion
        .Dock = DockStyle.Fill;

      this.labelVersion
        .Location = new Point
        (
          96, 
          26
        );

      this.labelVersion
        .Margin = new Padding
        (
          6, 
          0, 
          3, 
          0
        );

      this.labelVersion
        .MaximumSize = new Size
        (
          0, 
          17
        );

      this.labelVersion
        .Name = "labelVersion";

      this.labelVersion
        .Size = new Size
        (
          318, 
          17
        );

      this.labelVersion
        .TabIndex = 0;

      this.labelVersion
        .Text = "Version {0}";

      this.labelVersion
        .TextAlign = ContentAlignment.MiddleLeft;
      // 
      // this.tableLayoutPanel
      // 
      this.tableLayoutPanel
        .ColumnCount = 2;

      this.tableLayoutPanel
        .ColumnStyles
        .Add
        (
          new ColumnStyle
          (
            SizeType.Absolute, 
            90F
          )
        );

      this.tableLayoutPanel
        .ColumnStyles
        .Add
        (
          new ColumnStyle
          (
            SizeType.Percent,
            33F
          )
        );

      this.tableLayoutPanel
        .ColumnStyles
        .Add
        (
          new ColumnStyle
          (
            SizeType.Percent,
            67F
          )
        );

      this.tableLayoutPanel
        .Controls
        .Add
        (
          this.logoPictureBox, 
          0, 
          0
        );

      this.tableLayoutPanel
        .Controls
        .Add
        (
          labelProductName, 
          2, 
          0
        );

      this.tableLayoutPanel
        .Controls
        .Add
        (
          labelVersion, 
          2, 
          1
        );

      this.tableLayoutPanel
        .Controls
        .Add
        (
          labelCopyright,
          2, 
          2
        );

      this.tableLayoutPanel
        .Controls
        .Add
        (
          labelCompanyName, 
          2, 
          3
        );

      this.tableLayoutPanel
        .Controls
        .Add
        (
          textBoxDescription, 
          2, 
          4
        );

      this.tableLayoutPanel
        .Controls
        .Add
        (
          okButton,
          2, 
          5
        );

      this.tableLayoutPanel
        .Dock = DockStyle.Fill;

      this.tableLayoutPanel
        .Location = new Point
        (
          9, 
          9
        );

      this.tableLayoutPanel
        .Name = "this.tableLayoutPanel";

      this.tableLayoutPanel
        .RowCount = 6;

      this.tableLayoutPanel
        .RowStyles.Add
        (
          new RowStyle
          (
            SizeType.Percent,
            10F
          )
        );

      this.tableLayoutPanel
        .RowStyles.Add
        (
          new RowStyle
          (
            SizeType.Percent,
            10F
          )
        );

      this.tableLayoutPanel
        .RowStyles.Add
        (
          new RowStyle
          (
            SizeType.Percent,
            10F
          )
        );

      this.tableLayoutPanel
        .RowStyles.Add
        (
          new RowStyle
          (
            SizeType.Percent,
            10F
          )
        );

      this.tableLayoutPanel
       .RowStyles.Add
        (
          new RowStyle
          (
            SizeType.Percent,
            50F
          )
        );

      this.tableLayoutPanel
        .RowStyles.Add
        (
          new RowStyle
          (
            SizeType.Percent, 
            10F
          )
        );

      this.tableLayoutPanel
        .Size = new Size
        (
          417, 
          265
        );

      this.tableLayoutPanel
        .TabIndex = 0;
      // 
      // this.logoPictureBox
      // 
      this.logoPictureBox
        .Anchor = AnchorStyles.Top 
        | AnchorStyles.Left 
        | AnchorStyles.Right;

      this.logoPictureBox
        .Image = (Image)resources.GetObject("this.logoPictureBox.Image");

      this.logoPictureBox
        .Location = new Point
        (
          3, 
          3
        );

      this.logoPictureBox
        .Name = "this.logoPictureBox";

      this.tableLayoutPanel
        .SetRowSpan
        (
          this.logoPictureBox, 6
        );

      this.logoPictureBox
        .Size = new Size
        (
          84, 
          102
        );

      this.logoPictureBox
        .SizeMode = PictureBoxSizeMode.Zoom;

      this.logoPictureBox
        .TabIndex = 25;

      this.logoPictureBox
        .TabStop = false;
      // 
      // textBoxDescription
      // 
      this.textBoxDescription
        .Dock = DockStyle.Fill;

      this.textBoxDescription
        .Location = new Point
        (
          96, 
          107
        );

      this.textBoxDescription
        .Margin = new Padding
        (
          6, 
          3, 
          3, 
          3
        );

      this.textBoxDescription
        .Multiline = true;

      this.textBoxDescription
        .Name = "textBoxDescription";

      this.textBoxDescription
        .ReadOnly = true;

      this.textBoxDescription
        .ScrollBars = ScrollBars.Both;

      this.textBoxDescription
        .Size = new Size
        (318, 126
        );

      this.textBoxDescription
        .TabIndex = 23;

      this.textBoxDescription
        .TabStop = false;

      this.textBoxDescription
        .Text = "Description";
      // 
      // okButton
      // 
      this.okButton
        .Anchor = AnchorStyles.Bottom 
        | AnchorStyles.Right;

      this.okButton
        .DialogResult = DialogResult.Cancel;

      this.okButton
        .Location = new Point
        (
          339, 
          239
        );

      this.okButton
        .Name = "okButton";

      this.okButton
        .Size = new Size
        (
          75, 
          23
        );

      this.okButton
        .TabIndex = 24;

      this.okButton
        .Text = "&OK";

      this.okButton
        .Click += okButton_Click;
      // 
      // AboutForm
      // 
      this.AcceptButton = okButton;

      this.AutoScaleDimensions = new SizeF
        (
          6F, 
          13F
        );

      this.AutoScaleMode = AutoScaleMode.Font;

      this.ClientSize = new Size
        (
          435, 
          283
        );
      
      this.Controls
        .Add(this.tableLayoutPanel);
      
      this.DoubleBuffered = true;
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "AboutForm";
      this.Padding = new Padding(9);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "About {0}";
      
      this.tableLayoutPanel
        .ResumeLayout(false);
      
      this.tableLayoutPanel
        .PerformLayout();
      
      ((System.ComponentModel.ISupportInitialize)this.logoPictureBox)
        .EndInit();
      
      this.ResumeLayout(false);
    }

    #endregion

    private Button okButton;
    private Label labelProductName;
    private Label labelVersion;
    private Label labelCopyright;
    private Label labelCompanyName;
    private PictureBox logoPictureBox;
    private TableLayoutPanel tableLayoutPanel;
    private TextBox textBoxDescription;
  }
}