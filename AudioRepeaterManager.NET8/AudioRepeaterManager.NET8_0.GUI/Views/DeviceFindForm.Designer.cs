namespace VACARM.GUI.Forms
{
  partial class DeviceFindForm
  {
    #region Parameters

    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    #endregion

    #region Logic

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="isDisposed">true if managed resources should be disposed;
    /// otherwise, false.</param>
    protected override void Dispose(bool isDisposed)
    {
      if
      (
        isDisposed
        && (components != null)
      )
      {
        components.Dispose();
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
      deviceFindInputCheckBox = new CheckBox();
      deviceFindPresentCheckBox = new CheckBox();
      deviceFindLabel = new Label();
      deviceFindInSelectionGroupBox = new GroupBox();
      deviceFindInSelectionCheckBox = new CheckBox();
      deviceFindNextButton = new Button();
      deviceFindCountButton = new Button();
      deviceFindCloseButton = new Button();
      deviceFindOutputCheckBox = new CheckBox();
      deviceFindEnabledCheckBox = new CheckBox();
      deviceFindDirectionArrowCheckBox = new CheckBox();
      deviceFindNextArrowButton = new Button();
      deviceFindPreviousArrowButton = new Button();
      deviceFindComboBox = new ComboBox();
      deviceFindDuplexCheckBox = new CheckBox();
      SuspendLayout();
      // 
      // deviceFindInputCheckBox
      // 
      deviceFindInputCheckBox.AutoSize = true;
      deviceFindInputCheckBox.Location = new Point(12, 49);
      deviceFindInputCheckBox.Name = "deviceFindInputCheckBox";
      deviceFindInputCheckBox.Size = new Size(54, 19);
      deviceFindInputCheckBox.TabIndex = 1;
      deviceFindInputCheckBox.Text = "Input";
      deviceFindInputCheckBox.UseVisualStyleBackColor = true;
      deviceFindInputCheckBox.CheckedChanged += deviceFindInputCheckBox_CheckedChanged;
      // 
      // deviceFindPresentCheckBox
      // 
      deviceFindPresentCheckBox.AutoSize = true;
      deviceFindPresentCheckBox.Location = new Point(12, 149);
      deviceFindPresentCheckBox.Name = "deviceFindPresentCheckBox";
      deviceFindPresentCheckBox.Size = new Size(65, 19);
      deviceFindPresentCheckBox.TabIndex = 2;
      deviceFindPresentCheckBox.Text = "Present";
      deviceFindPresentCheckBox.UseVisualStyleBackColor = true;
      deviceFindPresentCheckBox.CheckedChanged += deviceFindPresentCheckBox_CheckedChanged;
      // 
      // deviceFindLabel
      // 
      deviceFindLabel.AutoSize = true;
      deviceFindLabel.Location = new Point(25, 9);
      deviceFindLabel.Name = "deviceFindLabel";
      deviceFindLabel.Size = new Size(73, 15);
      deviceFindLabel.TabIndex = 3;
      deviceFindLabel.Text = "Find device: ";
      // 
      // deviceFindInSelectionGroupBox
      // 
      deviceFindInSelectionGroupBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      deviceFindInSelectionGroupBox.Location = new Point(237, 34);
      deviceFindInSelectionGroupBox.Name = "deviceFindInSelectionGroupBox";
      deviceFindInSelectionGroupBox.Size = new Size(232, 39);
      deviceFindInSelectionGroupBox.TabIndex = 4;
      deviceFindInSelectionGroupBox.TabStop = false;
      // 
      // deviceFindInSelectionCheckBox
      // 
      deviceFindInSelectionCheckBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      deviceFindInSelectionCheckBox.AutoSize = true;
      deviceFindInSelectionCheckBox.Location = new Point(239, 49);
      deviceFindInSelectionCheckBox.Name = "deviceFindInSelectionCheckBox";
      deviceFindInSelectionCheckBox.Size = new Size(86, 19);
      deviceFindInSelectionCheckBox.TabIndex = 8;
      deviceFindInSelectionCheckBox.Text = "In selection";
      deviceFindInSelectionCheckBox.UseVisualStyleBackColor = true;
      deviceFindInSelectionCheckBox.CheckedChanged += deviceFindInSelectionCheckBox_CheckedChanged;
      // 
      // deviceFindNextButton
      // 
      deviceFindNextButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      deviceFindNextButton.Location = new Point(331, 5);
      deviceFindNextButton.Name = "deviceFindNextButton";
      deviceFindNextButton.Size = new Size(136, 23);
      deviceFindNextButton.TabIndex = 5;
      deviceFindNextButton.Text = "Find Next";
      deviceFindNextButton.UseVisualStyleBackColor = true;
      deviceFindNextButton.Visible = false;
      deviceFindNextButton.Click += deviceFindNextButton_Click;
      // 
      // deviceFindCountButton
      // 
      deviceFindCountButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      deviceFindCountButton.Location = new Point(331, 45);
      deviceFindCountButton.Name = "deviceFindCountButton";
      deviceFindCountButton.Size = new Size(136, 23);
      deviceFindCountButton.TabIndex = 8;
      deviceFindCountButton.Text = "Count";
      deviceFindCountButton.UseVisualStyleBackColor = true;
      deviceFindCountButton.Click += deviceFindCountButton_Click;
      // 
      // deviceFindCloseButton
      // 
      deviceFindCloseButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      deviceFindCloseButton.Location = new Point(331, 79);
      deviceFindCloseButton.Name = "deviceFindCloseButton";
      deviceFindCloseButton.Size = new Size(136, 23);
      deviceFindCloseButton.TabIndex = 6;
      deviceFindCloseButton.Text = "Close";
      deviceFindCloseButton.UseVisualStyleBackColor = true;
      deviceFindCloseButton.Click += deviceFindCloseButton_Click;
      // 
      // deviceFindOutputCheckBox
      // 
      deviceFindOutputCheckBox.AutoSize = true;
      deviceFindOutputCheckBox.Location = new Point(12, 74);
      deviceFindOutputCheckBox.Name = "deviceFindOutputCheckBox";
      deviceFindOutputCheckBox.Size = new Size(64, 19);
      deviceFindOutputCheckBox.TabIndex = 8;
      deviceFindOutputCheckBox.Text = "Output";
      deviceFindOutputCheckBox.UseVisualStyleBackColor = true;
      deviceFindOutputCheckBox.CheckedChanged += deviceFindOutputCheckBox_CheckedChanged;
      // 
      // deviceFindEnabledCheckBox
      // 
      deviceFindEnabledCheckBox.AutoSize = true;
      deviceFindEnabledCheckBox.Location = new Point(12, 124);
      deviceFindEnabledCheckBox.Name = "deviceFindEnabledCheckBox";
      deviceFindEnabledCheckBox.Size = new Size(68, 19);
      deviceFindEnabledCheckBox.TabIndex = 9;
      deviceFindEnabledCheckBox.Text = "Enabled";
      deviceFindEnabledCheckBox.UseVisualStyleBackColor = true;
      deviceFindEnabledCheckBox.CheckedChanged += deviceFindEnabledCheckBox_CheckedChanged;
      // 
      // deviceFindDirectionArrowCheckBox
      // 
      deviceFindDirectionArrowCheckBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      deviceFindDirectionArrowCheckBox.AutoSize = true;
      deviceFindDirectionArrowCheckBox.Location = new Point(475, 9);
      deviceFindDirectionArrowCheckBox.Name = "deviceFindDirectionArrowCheckBox";
      deviceFindDirectionArrowCheckBox.Size = new Size(15, 14);
      deviceFindDirectionArrowCheckBox.TabIndex = 10;
      deviceFindDirectionArrowCheckBox.UseVisualStyleBackColor = true;
      deviceFindDirectionArrowCheckBox.CheckedChanged += deviceFindDirectionArrowCheckBox_CheckedChanged;
      // 
      // deviceFindNextArrowButton
      // 
      deviceFindNextArrowButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      deviceFindNextArrowButton.Location = new Point(361, 5);
      deviceFindNextArrowButton.Name = "deviceFindNextArrowButton";
      deviceFindNextArrowButton.Size = new Size(106, 23);
      deviceFindNextArrowButton.TabIndex = 11;
      deviceFindNextArrowButton.Text = "▼ Find Next";
      deviceFindNextArrowButton.UseVisualStyleBackColor = true;
      deviceFindNextArrowButton.Visible = false;
      deviceFindNextArrowButton.Click += deviceFindNextArrowButton_Click;
      // 
      // deviceFindPreviousArrowButton
      // 
      deviceFindPreviousArrowButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      deviceFindPreviousArrowButton.Location = new Point(331, 5);
      deviceFindPreviousArrowButton.Name = "deviceFindPreviousArrowButton";
      deviceFindPreviousArrowButton.Size = new Size(24, 23);
      deviceFindPreviousArrowButton.TabIndex = 12;
      deviceFindPreviousArrowButton.Text = "▲";
      deviceFindPreviousArrowButton.UseVisualStyleBackColor = true;
      deviceFindPreviousArrowButton.Visible = false;
      deviceFindPreviousArrowButton.Click += deviceFindPreviousArrowButton_Click;
      // 
      // deviceFindComboBox
      // 
      deviceFindComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      deviceFindComboBox.FormattingEnabled = true;
      deviceFindComboBox.Location = new Point(100, 6);
      deviceFindComboBox.Name = "deviceFindComboBox";
      deviceFindComboBox.Size = new Size(165, 23);
      deviceFindComboBox.TabIndex = 0;
      deviceFindComboBox.SelectedIndexChanged += deviceFindComboBox_SelectedIndexChanged;
      // 
      // deviceFindDuplexCheckBox
      // 
      deviceFindDuplexCheckBox.AutoSize = true;
      deviceFindDuplexCheckBox.Location = new Point(12, 99);
      deviceFindDuplexCheckBox.Name = "deviceFindDuplexCheckBox";
      deviceFindDuplexCheckBox.Size = new Size(63, 19);
      deviceFindDuplexCheckBox.TabIndex = 14;
      deviceFindDuplexCheckBox.Text = "Duplex";
      deviceFindDuplexCheckBox.UseVisualStyleBackColor = true;
      deviceFindDuplexCheckBox.CheckedChanged += deviceFindDuplexCheckBox_CheckedChanged;
      // 
      // DeviceFindForm
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(502, 173);
      Controls.Add(deviceFindDuplexCheckBox);
      Controls.Add(deviceFindInSelectionCheckBox);
      Controls.Add(deviceFindPreviousArrowButton);
      Controls.Add(deviceFindCountButton);
      Controls.Add(deviceFindNextArrowButton);
      Controls.Add(deviceFindDirectionArrowCheckBox);
      Controls.Add(deviceFindEnabledCheckBox);
      Controls.Add(deviceFindOutputCheckBox);
      Controls.Add(deviceFindCloseButton);
      Controls.Add(deviceFindNextButton);
      Controls.Add(deviceFindInSelectionGroupBox);
      Controls.Add(deviceFindLabel);
      Controls.Add(deviceFindPresentCheckBox);
      Controls.Add(deviceFindInputCheckBox);
      Controls.Add(deviceFindComboBox);
      DoubleBuffered = true;
      MinimumSize = new Size(518, 212);
      Name = "DeviceFindForm";
      StartPosition = FormStartPosition.CenterParent;
      Text = "Find";
      TopMost = true;
      Load += deviceFindForm_Load;
      ResumeLayout(false);
      PerformLayout();
    }

    #endregion

    #region Windows Form Designer generated parameters

    private Button deviceFindCloseButton;
    private Button deviceFindCountButton;
    private Button deviceFindNextArrowButton;
    private Button deviceFindNextButton;
    private Button deviceFindPreviousArrowButton;
    private CheckBox deviceFindDirectionArrowCheckBox;
    private CheckBox deviceFindEnabledCheckBox;
    private CheckBox deviceFindInputCheckBox;
    private CheckBox deviceFindInSelectionCheckBox;
    private CheckBox deviceFindOutputCheckBox;
    private CheckBox deviceFindPresentCheckBox;
    private ComboBox deviceFindComboBox;
    private GroupBox deviceFindInSelectionGroupBox;
    private Label deviceFindLabel;

    #endregion

    private CheckBox checkBox1;
    private CheckBox deviceFindDuplexCheckBox;
  }
}