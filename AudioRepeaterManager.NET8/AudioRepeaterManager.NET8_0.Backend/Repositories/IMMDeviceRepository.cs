using NAudio.CoreAudioApi;

namespace AudioRepeaterManager.NET8_0.Backend.Repositories
{
  public interface IMMDeviceRepository
  {
    #region Logic

    List<MMDevice> GetAll();
    List<MMDevice> GetAllDisabled();
    List<MMDevice> GetAllEnabled();
    List<MMDevice> GetRange(List<string> idList);
    MMDevice? Get(string id);
    void Disable(string id);
    void DisableAll(List<string> idList);
    void DisableRange(List<string> idList);
    void EnableAll(List<string> idList);
    void EnableRange(List<string> idList);
    void Enable(string id);
    void UpdateAll();

    #endregion
  }
}