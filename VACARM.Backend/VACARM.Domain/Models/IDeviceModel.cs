namespace VACARM.Domain.Models
{
  public interface IDeviceModel : IBaseModel
  {
    #region Parameters

    /// <summary>
    /// Foreign key
    /// </summary>
    string ActualId { get; set; }

    bool IsDuplex { get; }
    bool IsInput { get; set; }
    bool IsOutput { get; set; }
    bool IsPresent { get; set; }
    string Name { get; set; }
    string Availability { get; }

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