using System.ComponentModel;
using System.Diagnostics;

namespace AudioRepeaterManager.NET8_0.Backend.Repositories
{
  public interface IProcessRepository
  {
    #region Parameters

    event PropertyChangedEventHandler PropertyChanged;
    List<string> ExecutableNameList { get; set; }

    #endregion

    #region Logic

    List<Process> GetAll();
    List<Process> GetRange(List<int> idList);
    Process Get(int id);
    Task<int> Run(int id);
    Task<int> RunAll();
    Task<int> RunRange(int id);
    Task<int> Kill(int id);
    Task<int> KillAll();
    Task<int> KillRange(int id);
    void Update();

    #endregion
  }
}