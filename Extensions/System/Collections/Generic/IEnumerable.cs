using System.Linq;

namespace System.Collections.Generic
{
  public static class IEnumerable
  {
    #region Logic

    /// <summary>
    /// Is enumerable null or empty.
    /// </summary>
    /// <param name="enumerable">The enumerable</param>
    /// <returns>True/false</returns>
    public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
    {
      if (enumerable == null)
      {
        return true;
      }

      return enumerable.Count() == 0;
    }

    #endregion
  }
}
