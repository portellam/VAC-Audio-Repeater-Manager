namespace VACARM.GUI.Extensions
{
  public static class ColorExtension
  {
    #region Logic

    /// <summary>
    /// Convert Media Color (WPF) to Drawing Color (WinForm)
    /// </summary>
    /// <param name="mediaColor">the media color</param>
    /// <returns>the drawing color</returns>
    public static System.Drawing.Color ToDrawingColor
      (this System.Windows.Media.Color mediaColor)
    {
      return
        System.Drawing.Color.FromArgb
        (
          mediaColor.A,
          mediaColor.R,
          mediaColor.G,
          mediaColor.B
        );
    }

    /// <summary>
    /// Convert Drawing Color (WPF) to Media Color (WinForm)
    /// </summary>
    /// <param name="drawingColor">the drawing color</param>
    /// <returns>the media color</returns>
    public static System.Windows.Media.Color ToMediaColor
      (this System.Drawing.Color drawingColor)
    {
      return
        System.Windows.Media.Color.FromArgb
        (
          drawingColor.A,
          drawingColor.R,
          drawingColor.G,
          drawingColor.B
        );
    }

    #endregion
  }
}