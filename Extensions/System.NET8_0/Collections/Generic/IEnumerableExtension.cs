using System.Linq;

namespace System.Collections.Generic
{
  public static class IEnumerableExtension
  {
    #region Logic

    /// <summary>
    /// Is the sequence null or empty.
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source">The sequence to test.</param>
    /// <returns><see langword="true"/> if the
    /// <typeparamref name="enumerable"/> parameter is <see langword="null"/> or
    /// empty; otherwise, <see langword="false"/>.</returns>
    public static bool IsNullOrEmpty<TSource>(this IEnumerable<TSource> source)
    {
      if (source == null)
      {
        return true;
      }

      return source.Count() == 0;
    }

    #endregion
  }
}