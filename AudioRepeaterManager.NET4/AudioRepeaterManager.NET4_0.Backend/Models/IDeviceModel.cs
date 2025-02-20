using System.ComponentModel;

namespace AudioRepeaterManager.NET4_0.Backend.Models
{
  public interface IDeviceModel
  {
    #region Parameters

    /// <summary>
    /// Primary Key
    /// </summary>
    uint Id { get; set; }

    /// <summary>
    /// Foreign key
    /// </summary>
    string ActualId { get; set; }

    bool IsDuplex { get; }
    bool IsInput { get; set; }
    bool IsOutput { get; set; }
    bool IsPresent { get; set; }
    event PropertyChangedEventHandler PropertyChanged;
    string Name { get; set; }

    #endregion

    #region Logic

    void Deconstruct
    (
      out uint id,
      out string actualId,
      out string name,
      out bool? isInput,
      out bool? isOutput,
      out bool? isPresent
    );

    #endregion
  }
}
