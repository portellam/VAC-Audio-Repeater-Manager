using NAudio.CoreAudioApi;

namespace AudioRepeaterManager.NET8_0.Infrastructure.Repositories
{
  public interface IMMDeviceRepository
  {
    #region Logic

    List<MMDevice> GetAll();
    List<MMDevice> GetAllStarted();
    List<MMDevice> GetAllStopped();
    List<MMDevice> GetRange(List<string> idList);
    MMDevice? Get(string id);
    void StartAll();
    void StartRange(List<string> idList);
    void Start(string id);
    void Stop(string id);
    void StopAll();
    void StopRange(List<string> idList);
    void Update(string id);
    void UpdateAll();
    void UpdateRange(List<string> idList);
    
    #endregion
  }
}