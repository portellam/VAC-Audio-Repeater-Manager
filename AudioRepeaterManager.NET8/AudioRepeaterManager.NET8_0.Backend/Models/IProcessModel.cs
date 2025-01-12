using System.ComponentModel;

namespace AudioRepeaterManager.NET8_0.Backend.Models
{
  public interface IProcessModel
  {
    #region Parameters

    /// <summary>
    /// Primary Key
    /// </summary>
    int Id { get; set; }

    bool IsRunning { get; }
    event PropertyChangedEventHandler? PropertyChanged;
    string Arguments { get; set; }
    string FileName { get; set; }
    string Priority { get; }

    #endregion

    #region Logic

    Task<int> Restart();
    Task<int> Start();
    Task<int> Stop();
    void Update();

    #endregion
  }
}
