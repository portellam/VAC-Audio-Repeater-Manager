namespace VACARM.GUI.Views
{
  partial class UsageForm
  {
    #region Parameters

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    #endregion

    #region Logic

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="isDisposed">True/false</param>
    protected override void Dispose(bool isDisposed)
    {
      if
      (
        isDisposed 
        && (this.components != null)
      )
      {
        this.components.Dispose();
      }

      base.Dispose(isDisposed);
    }

    #endregion

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      System.ComponentModel.ComponentResourceManager resources = 
        new System.ComponentModel
          .ComponentResourceManager(typeof(UsageForm));

      this.tableLayoutPanel = new TableLayoutPanel();
      this.textBoxDescription = new TextBox();
      this.okButton = new Button();

      this.tableLayoutPanel
        .SuspendLayout();

      this.SuspendLayout();
      // 
      // tableLayoutPanel
      // 
      this.tableLayoutPanel
        .ColumnCount = 1;

      this.tableLayoutPanel
        .ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

      this.tableLayoutPanel
        .Controls.Add(textBoxDescription, 0, 0);

      this.tableLayoutPanel
        .Controls.Add(okButton, 0, 1);

      this.tableLayoutPanel
        .Dock = DockStyle.Fill;

      this.tableLayoutPanel
        .Location = new Point(9, 9);

      this.tableLayoutPanel
        .Name = "tableLayoutPanel";

      this.tableLayoutPanel
        .RowCount = 2;

      this.tableLayoutPanel
        .RowStyles
        .Add
        (
          new RowStyle
          (
            SizeType.Percent, 
            100F
          )
        );

      this.tableLayoutPanel
        .RowStyles
        .Add
        (
          new RowStyle
          (
            SizeType.Absolute, 
            29F
          )
        );

      this.tableLayoutPanel
        .Size = new Size
        (
          417, 
          470
        );
      this.tableLayoutPanel
        .TabIndex = 0;
      // 
      // textBoxDescription
      // 
      this.textBoxDescription
        .Dock = DockStyle.Fill;

      this.textBoxDescription
        .Location = new Point
        (
          6, 
          3
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
        (
          408, 
          435
        );

      this.textBoxDescription
        .TabIndex = 23;

      this.textBoxDescription
        .TabStop = false;

      this.textBoxDescription
        .Text = resources.GetString("textBoxDescription.Text");
      // 
      // okButton
      // 
      this.okButton
        .Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

      this.okButton
        .DialogResult = DialogResult.Cancel;

      this.okButton
        .Location = new Point
        (
          339, 
          444
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
      // UsageForm
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
          488
        );

      this.Controls
        .Add(tableLayoutPanel);

      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "UsageForm";
      this.Padding = new Padding(9);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "UsageForm";

      this.tableLayoutPanel
        .ResumeLayout(false);

      this.tableLayoutPanel
        .PerformLayout();

      this.ResumeLayout(false);
    }

    #endregion

    private Button okButton;
    private TableLayoutPanel tableLayoutPanel;
    private TextBox textBoxDescription;
  }
}