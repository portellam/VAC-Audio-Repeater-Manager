using AudioSwitcher.AudioApi;
using AudioSwitcher.AudioApi.CoreAudio;
using NAudio.CoreAudioApi;
using VACARM.Application.Commands;
using VACARM.Infrastructure.Repositories;

namespace VACARM.Application.Controllers
{
  /// <summary>
  /// A controller for the <typeparamref name="CoreAudioRepository"/>.
  /// </summary>
  /// <typeparam name="T1">The repository</typeparam>
  /// <typeparam name="T2">The item</typeparam>
  public class CoreAudioControllerWrapper<T1, T2> :
    GenericListController<GenericListRepository<T2>, T2>,
    ICoreAudioController<CoreAudioRepository<T2>, T2> where T1 :
    CoreAudioRepository<T2> where T2 :
    Device
  {
    #region Parameters

    private CoreAudioController Controller { get; set; }

    private List<CoreAudioDevice> List { get; set; }

    #endregion

    #region Logic

    /// <summary>
    /// Get the default <typeparamref name="CoreAudio"/> item.
    /// </summary>
    /// <param name="role">The role</param>
    /// <param name="deviceType">The device type</param>
    /// <returns>The item.</returns>
    private async Task<CoreAudioDevice?> GetDefault
    (
      Role role,
      DeviceType deviceType
    )
    {
      return await Controller
        .GetDefaultDeviceAsync
        (
          deviceType,
          role
        )
        .ConfigureAwait(false);
    }

    /// <summary>
    /// Get the device type.
    /// </summary>
    /// <param name="isInput">True/false is an input</param>
    /// <param name="isOutput">True/false is an output</param>
    /// <returns>The device type.</returns>
    private static DeviceType GetDeviceType
    (
      bool isInput,
      bool isOutput
    )
    {
      if (isInput == isOutput)
      {
        return DeviceType.All;
      }

      if (isInput)
      {
        return DeviceType.Capture;
      }

      return DeviceType.Playback;
    }

    /// <summary>
    /// Convert an ID from a <typeparamref name="string"/> to a 
    /// <typeparamref name="GUID"/>.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <returns>The GUID</returns>
    private static Guid ToGuid(string id)
    {
      if (string.IsNullOrWhiteSpace(id))
      {
        id = string.Empty;
      }

      return new Guid(id);
    }

    /// <summary>
    /// Constructor
    /// </summary>
    public CoreAudioControllerWrapper()
    {
      Controller = new CoreAudioController();
    }

    public async Task<bool> IsDefault(string id)
    {
      CoreAudioDevice? model = await Get(id);

      if (model == null)
      {
        return false;
      }

      return model.IsDefaultDevice;
    }

    public async Task<bool> IsDefaultCommunications(string id)
    {
      CoreAudioDevice? model = await Get(id);

      if (model == null)
      {
        return false;
      }

      return model.IsDefaultCommunicationsDevice;
    }

    public async Task<bool> IsMuted(string id)
    {
      CoreAudioDevice? model = await Get(id);

      if (model == null)
      {
        return false;
      }

      return model.IsMuted;
    }

    public async Task<CoreAudioDevice?> Get(string id)
    {
      if (Controller == null)
      {
        return null;
      }

      Guid guid = ToGuid(id);

      return await Controller
        .GetDeviceAsync(guid)
        .ConfigureAwait(false);
    }

    public async Task<CoreAudioDevice?> GetDefaultCommunications
    (
      bool isInput,
      bool isOutput
    )
    {
      DeviceType deviceType = GetDeviceType
        (
          isInput,
          isOutput
        );

      return await GetDefault
        (
          Role.Communications,
          deviceType
        );
    }

    public async Task<CoreAudioDevice?> GetDefaultConsole
    (
      bool isInput,
      bool isOutput
    )
    {
      DeviceType deviceType = GetDeviceType
        (
          isInput,
          isOutput
        );

      return await GetDefault
        (
          Role.Console,
          deviceType
        );
    }

    public async Task<CoreAudioDevice?> GetDefaultMultimedia
    (
      bool isInput,
      bool isOutput
    )
    {
      DeviceType deviceType = GetDeviceType
        (
          isInput,
          isOutput
        );

      return await GetDefault
        (
          Role.Multimedia,
          deviceType
        );
    }

    public async Task<bool> DoMute(string id)
    {
      CoreAudioDevice? model = await Get(id);
      return await CoreAudioCommands.DoMute(model);
    }

    public async Task<bool> DoUnmute(string id)
    {
      CoreAudioDevice? model = await Get(id);
      return await CoreAudioCommands.DoUnmute(model);
    }

    public async Task<bool> SetAsDefault(string id)
    {
      CoreAudioDevice? model = await Get(id);
      return await CoreAudioCommands.SetAsDefault(model);
    }

    public async Task<bool> SetAsDefaultCommunications(string id)
    {
      CoreAudioDevice? model = await Get(id);
      return await CoreAudioCommands.SetAsDefaultCommunications(model);
    }

    public async Task<double> GetVolume(string id)
    {
      CoreAudioDevice? model = await Get(id);

      if (model == null)
      {
        return 0;
      }

      return model.Volume;
    }

    public async Task<bool> SetVolume
    (
      string id,
      double? volume
    )
    {
      CoreAudioDevice? model = await Get(id);

      return CoreAudioCommands.SetVolume
        (
          model,
          volume
        );
    }

    #endregion
  }
}