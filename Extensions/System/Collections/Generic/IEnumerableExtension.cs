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
    /// <param name="source">The sequence.</param>
    /// <returns><see langword="true"/> if the
    /// <typeparamref name="enumerable"/> parameter is null or empty; otherwise,
    /// <see langword="false"/>.</returns>
    public static bool IsNullOrEmpty<TSource>(this IEnumerable<TSource> source)
    {
      if (source == null)
      {
        return true;
      }

      return source.Count() == 0;
    }

    /// <summary>
    /// Appends a value to the end of the sequence.
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source">The seqeuence</param>
    /// <param name="element">The value</param>
    /// <returns>A new sequence that ends with the element.</returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IEnumerable<TSource> Append<TSource>
    (
      this IEnumerable<TSource> source,
      TSource element
    )
    {
      if (source == null)
      {
        throw new ArgumentNullException(nameof(source));
      }

      return source.Concat(new TSource[] { element });
    }

    #endregion
  }
}