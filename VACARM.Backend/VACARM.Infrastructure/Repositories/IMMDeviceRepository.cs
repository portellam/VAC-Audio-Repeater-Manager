using NAudio.CoreAudioApi;

namespace VACARM.Infrastructure.Repositories
{
  public interface IMMDeviceRepository
  {
    #region Logic

    List<MMDevice> GetAll();
    List<MMDevice> GetAllStarted();
    List<MMDevice> GetAllStopped();
    List<MMDevice> GetRange(List<string> idList);
    MMDevice? Get(string id);
    void Update(string id);
    void UpdateAll();
    void UpdateRange(List<string> idList);
    
    #endregion
  }
}