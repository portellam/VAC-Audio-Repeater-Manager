namespace AudioRepeaterManager.NET8_0.GUI.Extensions
{
  public class ComboBoxExtension
  {
    /// <summary>
    /// Auto adjust ComboBox drop down width.
    /// </summary>
    public static int DropDownWidth(ComboBox comboBox)
    {
      int maxWidth = 0;
      int temp = 0;
      Label label = new Label();

      foreach (var item in comboBox.Items)
      {
        label.Text = item.ToString();
        temp = label.PreferredWidth;

        if (temp > maxWidth)
        {
          maxWidth = temp;
        }
      }

      label.Dispose();
      return maxWidth;
    }

    /// <summary>
    /// Adjust ComboBox width to given longest Text string in Drop Down list.
    /// </summary>
    /// <param name="sender">The sender</param>
    /// <param name="eventArgs">The event args</param>
    public static ComboBox AdjustWidthComboBox_DropDown
    (
      object sender,
      EventArgs eventArgs
    )
    {
      var senderComboBox = (ComboBox)sender;
      int width = senderComboBox.DropDownWidth;
      Graphics graphics = senderComboBox.CreateGraphics();
      Font font = senderComboBox.Font;

      int vertScrollBarWidth =
        (senderComboBox.Items.Count > senderComboBox.MaxDropDownItems)
        ? SystemInformation.VerticalScrollBarWidth : 0;

      int newWidth;
      
      foreach (string text in senderComboBox.Items)
      {
        newWidth = (int)graphics
          .MeasureString
          (
            text,
            font
          )
          .Width
          + vertScrollBarWidth;

        if (width < newWidth)
        {
          width = newWidth;
        }
      }

      senderComboBox.DropDownWidth = width;
      return senderComboBox;
    }
  }
}
