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
    MMDevice Get(string id);
    void Disable(string id);
    void Enable(string id);
    void Update();

    #endregion
  }
}