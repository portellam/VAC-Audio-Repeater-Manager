using AudioSwitcher.AudioApi.CoreAudio;
using System.Threading.Tasks;

namespace VACARM.Infrastructure.Services
{
  public partial interface ICoreAudioService
    <
      TRepository,
      TEnumerable,
      TDevice
    >
  {
    #region Logic

    /// <summary>
    /// Update the enumerable of all <typeparamref name="TDevice"/>
    /// items.
    /// </summary>
    /// <returns>True/false result.</returns>
    Task<bool> UpdateServiceAsync();

    /// <summary>
    /// Get the <typeparamref name="TDevice"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The item.</returns>
    Task<CoreAudioDevice> GetAsync(string id);

    /// <summary>
    /// Get the default communications <typeparamref name="TDevice"/>.
    /// </summary>
    /// <param name="isInput">True/false is an input</param>
    /// <param name="isOutput">True/false is an output</param>
    /// <returns>The item.</returns>
    Task<CoreAudioDevice> GetDefaultCommunicationsAsync
    (
      bool isInput,
      bool isOutput
    );

    /// <summary>
    /// Get the default console <typeparamref name="TDevice"/>.
    /// </summary>
    /// <param name="isInput">True/false is an input</param>
    /// <param name="isOutput">True/false is an output</param>
    /// <returns>The item.</returns>
    Task<CoreAudioDevice> GetDefaultConsoleAsync
    (
      bool isInput,
      bool isOutput
    );

    /// <summary>
    /// Get the default multimedia <typeparamref name="TDevice"/>.
    /// </summary>
    /// <param name="isInput">True/false is an input</param>
    /// <param name="isOutput">True/false is an output</param>
    /// <returns>The item.</returns>
    Task<CoreAudioDevice> GetDefaultMultimediaAsync
    (
      bool isInput,
      bool isOutput
    );

    /// <summary>
    /// Get the volume of the <typeparamref name="TDevice"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The audio volume.</returns>
    Task<double> GetVolumeAsync(string id);

    #endregion 
  }
}