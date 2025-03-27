namespace VACARM.Infrastructure.Extensions
{
  public class uintExtension
  {
    #region Logic

    /// <summary>
    /// Converts the string representation of a number to its 32-bit signed
    /// integer equivalent.
    /// A return value indicates whether the operation succeeded.
    /// </summary>
    /// <param name="enumerable">the enumerable of string(s)</param>
    /// <returns><see langword="true"/> if enumerable was successfully parsed;
    /// otherwise, <see langword="false"/>.</returns>
    public static IEnumerable<uint> TryParse
    (IEnumerable<string> enumerable)
    {
      if (enumerable == null)
      {
        yield break;
      }

      foreach(var item in enumerable)
      {
        uint i;

        bool result = uint.TryParse
          (
            item,
            out i
          );

        if (!result)
        {
          continue;
        }

        yield return i;
      }
    }

    #endregion
  }
}