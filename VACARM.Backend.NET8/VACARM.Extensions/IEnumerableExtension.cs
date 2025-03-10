using System.Collections.Generic;
using System.Linq;

namespace VACARM.Extensions
{
  public class IEnumerableExtension<T>
  {
    #region Logic

    /// <summary>
    /// Is enumerable null or empty.
    /// </summary>
    /// <param name="enumerable">The enumerable</param>
    /// <returns>True/false</returns>
    public static bool IsNullOrEmpty(IEnumerable<T> enumerable)
    {
      if (enumerable == null)
      {
        return true;
      }

      if (enumerable.Count() == 0)
      {
        return true;
      }

      return false;
    }

    #endregion
  }
}