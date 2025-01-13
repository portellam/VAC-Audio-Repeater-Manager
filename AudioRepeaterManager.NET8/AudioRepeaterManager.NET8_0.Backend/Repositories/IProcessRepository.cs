using System.ComponentModel;
using System.Diagnostics;

namespace AudioRepeaterManager.NET8_0.Backend.Repositories
{
  public interface IProcessRepository
  {
    #region Parameters

    event PropertyChangedEventHandler PropertyChanged;

    #endregion

    #region Logic

    List<Process> GetAll();
    List<Process> GetAllStarted();
    List<Process> GetAllStopped();
    List<Process> GetRange(List<int> idList);
    Process Get(int id);
    Task<int> Restart(int id);
    Task<int> Start(int id);
    Task<int> Stop(int id);
    void Update();

    #endregion
  }
}