using System.Reflection;

namespace System.Windows.Forms
{
  internal static class ToolStripMenuItemExtension
  {
    #region Logic

    /// <summary>
    /// Get a clone <typeparamref name="ToolStripMenuItem"/>.
    /// Required for when updating a <typeparamref name="ToolStripMenuItem"/>
    /// at run-time.
    /// </summary>
    /// <param name="original">
    /// The original <typeparamref name="ToolStripMenuItem"/>
    /// </param>
    /// <returns>The clone <typeparamref name="ToolStripMenuItem"/></returns>
    internal static ToolStripMenuItem GetClone(this ToolStripMenuItem original)
    {
      ToolStripMenuItem clone = new ToolStripMenuItem();

      PropertyInfo[] propertyInfoArray = typeof(ToolStripMenuItem).GetProperties
        (
          BindingFlags.Public
          | BindingFlags.Instance
        );

      foreach (PropertyInfo property in propertyInfoArray)
      {
        if (!property.CanWrite)
        {
          continue;
        }

        object value = property.GetValue(original);

        property.SetValue
          (
            clone,
            value
          );
      }

      return clone;
    }

    #endregion
  }
}