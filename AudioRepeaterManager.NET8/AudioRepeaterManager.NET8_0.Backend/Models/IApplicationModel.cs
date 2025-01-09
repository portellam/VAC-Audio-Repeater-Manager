using System.ComponentModel;

namespace AudioRepeaterManager.NET8_0.Backend.Models
{
  public interface IApplicationModel
  {
    #region Parameters

    /// <summary>
    /// Primary Key
    /// </summary>
    uint Id { get; set; }

    /// <summary>
    /// Foreign Key
    /// </summary>
    uint RepeaterId { get; set; }

    bool IsRunning { get; set; }
    event PropertyChangedEventHandler PropertyChanged;
    string Priority { get; set; }
    string StartArguments { get; }
    string StopArguments { get; }
    string WindowName { get; }
    uint ProcessId { get; set; }

    #endregion

    #region Logic

    Task<int> Restart();
    Task<int> Start();
    Task<int> Stop();
    void Update();

    #endregion
  }
}
