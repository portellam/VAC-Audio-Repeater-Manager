namespace AudioRepeaterManager.NET2_0.Backend.Extensions
{
  public static class StringExtension
  {
    /// <summary>
    /// Is string null, empty, and/or whitespace, or neither.
    /// </summary>
    /// <param name="value">The string</param>
    /// <returns>True/false is the string null, empty, and/or whitespace.</returns>
    public static bool IsNullOrWhiteSpace(string value)
    {
      if (string.IsNullOrEmpty(value))
      {
        return true;
      }

      foreach (char _char in value.ToCharArray())
      {
        if (!char.IsWhiteSpace(_char))
        {
          return true;
        }
      }

      return false;
    }
  }
}
