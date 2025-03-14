namespace System
{
  public static class StringExtension
  {
    #region Logic

    /// <summary>
    /// Indicates whether a string is <see langword="null"/>, empty, or consists
    /// only of whitespace characters.
    /// </summary>
    /// <param name="value">The string to test.</param>
    /// <returns><see langword="true"/> if the
    /// <typeparamref name="value"/> parameter is <see langword="null"/> or
    /// <see langword="string.Empty"/>, or if value consists only of
    /// whitespace characters.
    /// <see langword="false"/>.</returns>
    public static bool IsNullOrWhiteSpace(string value)
    {
      if (string.IsNullOrEmpty(value))
      {
        return true;
      }

      foreach (char c in value)
      {
        if (c != ' ')
        {
          return false;
        }
      }

      return true;
    }

    #endregion
  }
}