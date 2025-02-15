using AudioSwitcher.AudioApi;

namespace VACARM.Infrastructure.Functions
{
  internal static class CoreAudioDeviceFunctions<TDevice> where TDevice :
    Device
  {
    #region Logic

    private static Guid ToGuid(string id)
    {
      return new Guid(id);
    }

    /// <summary>
    /// Match a <typeparamref name="TDevice"/> item ID.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The function</returns>
    public static Func<TDevice, bool> ContainsId(string id)
    {
      return (TDevice item) => item.Id == ToGuid(id);
    }

    /// <summary>
    /// Match an enumerable of <typeparamref name="TDevice"/> item ID(s).
    /// </summary>
    /// <param name="startId">The first ID</param>
    /// <param name="endId">The last ID</param>
    /// <returns>The function</returns>
    public static Func<TDevice, bool> ContainsIdEnumerable
    (IEnumerable<string> idEnumerable)
    {
      return (TDevice item) =>
      {
        string id = item.Id.ToString();
        return idEnumerable.Contains(id);
      };
    }

    #endregion
  }
}
