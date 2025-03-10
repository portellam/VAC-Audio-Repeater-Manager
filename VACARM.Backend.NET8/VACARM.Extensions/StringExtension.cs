namespace VACARM.Extensions
{
  public class StringExtension
  {
    #region Logic

    /// <summary>
    /// Is string null, empty, or whitespace.
    /// </summary>
    /// <param name="value">The string</param>
    /// <returns>True/false</returns>
    public static bool IsNullOrEmptyOrWhitespace(string value)
    {
      if (string.IsNullOrWhiteSpace(value))
      {
        return true;
      }

      if (value.Length == 0)
      {
        return true;
      }

      return false;
    }

    #endregion
  }
}