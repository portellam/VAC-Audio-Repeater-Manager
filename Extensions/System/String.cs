﻿namespace System
{
  public static class String
  {
    #region Logic

    /// <summary>
    /// Is string null, empty, or whitespace.
    /// </summary>
    /// <param name="value">The string</param>
    /// <returns>True/false</returns>
    public static bool IsNullOrEmptyOrWhitespace(this string value)
    {
      return string.IsNullOrWhiteSpace(value)
        || value.Length == 0;
    }

    #endregion
  }
}