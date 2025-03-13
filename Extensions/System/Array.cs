namespace System.Extensions
{
  public static class Array
  {
    #region Parameters

    /// <summary>
    /// Returns an empty array.
    /// </summary>
    /// <returns>An empty array.</returns>
    public static T[] Empty<T>()
    {
      return ArrayExtension<T>.EmptyArray;
    }

    #endregion
  }

  internal static class ArrayExtension<T>
  {
    #region Parameters

    internal readonly static T[] EmptyArray = new T[0];

    #endregion
  }
}