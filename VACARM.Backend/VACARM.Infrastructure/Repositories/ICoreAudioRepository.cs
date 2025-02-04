﻿using AudioSwitcher.AudioApi;

namespace VACARM.Infrastructure.Repositories
{
  public partial interface ICoreAudioRepository<T> :
    IGenericRepository<T> where T :
    Device
  {
    #region Logic

    /// <summary>
    /// Is a default <typeparamref name="Device"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The true/false result.</returns>
    bool IsDefault(string id);

    /// <summary>
    /// Is a default communications<typeparamref name="Device"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The true/false result.</returns>
    bool IsDefaultCommunications(string id);

    /// <summary>
    /// Is a muted <typeparamref name="Device"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The true/false result.</returns>
    bool IsMuted(string id);

    /// <summary>
    /// Set the <typeparamref name="Device"/> item volume.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <param name="volume">The audio volume</param>
    /// <returns>The true/false result.</returns>
    bool SetVolume
    (
      string id,
      double volume
    );

    /// <summary>
    /// Get the volume of the <typeparamref name="Device"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The audio volume.</returns>
    double GetVolume(string id);

    /// <summary>
    /// Get a <typeparamref name="Device"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The item.</returns>
    Device? Get(string id);

    /// <summary>
    /// Get a default communications <typeparamref name="Device"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The item.</returns>
    Device? GetDefaultCommunications(string id);

    /// <summary>
    /// Get a default console <typeparamref name="Device"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The item.</returns>
    Device? GetDefaultConsole(string id);

    /// <summary>
    /// Get a default multimedia <typeparamref name="Device"/> item.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The item.</returns>
    Device? GetDefaultMultimedia(string id);

    /// <summary>
    /// Get an enumerable of all muted <typeparamref name="Device"/> 
    /// item(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<Device> GetAllMuted();

    /// <summary>
    /// Get an enumerable of all not muted <typeparamref name="Device"/> 
    /// item(s).
    /// </summary>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<Device> GetAllNotMuted();

    /// <summary>
    /// Get an enumerable of some <typeparamref name="Device"/> item(s).
    /// </summary>
    /// <param name="func">The function</param>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<Device> GetRange(Func<Device, bool> func);

    /// <summary>
    /// Get an enumerable of some <typeparamref name="Device"/> item(s).
    /// </summary>
    /// <param name="idList">The enumerable of ID(s)</param>
    /// <returns>The enumerable of item(s).</returns>
    IEnumerable<Device> GetRange(IEnumerable<string> idList);

    #endregion
  }
}