using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace VACARM.Domain.Models
{
  /// <summary>
  /// The link between <typeparamref name="DeviceModel"/> and
  /// <typeparamref name="RepeaterModel"/>.
  /// </summary>
  public class RepeaterDeviceLinkModel :
    BaseModel,
    IRepeaterDeviceLinkModel
  {
    #region Parameters

    public virtual DeviceModel InputDeviceModel { get; set; }
    public virtual DeviceModel OutputDeviceModel { get; set; }
    public virtual RepeaterModel RepeaterModel { get; set; }

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="id">The ID</param>
    /// <param name="inputDeviceModel">
    /// The input<typeparamref name="DeviceModel"/>
    /// </param>
    /// <param name="outputDeviceModel">
    /// The output <typeparamref name="DeviceModel"/>
    /// </param>
    /// <param name="repeaterModel">The repeater</param>
    [ExcludeFromCodeCoverage]
    public RepeaterDeviceLinkModel
    (
      uint id,
      DeviceModel inputDeviceModel,
      DeviceModel outputDeviceModel,
      RepeaterModel repeaterModel
    ) :
      base(id)
    {
      this.Id = id;
      this.InputDeviceModel = inputDeviceModel;
      this.OutputDeviceModel = outputDeviceModel;
      this.RepeaterModel = repeaterModel;
    }

    [ExcludeFromCodeCoverage]
    public void Deconstruct
    (
      out uint id,
      out DeviceModel inputDeviceModel,
      out DeviceModel outputDeviceModel,
      out RepeaterModel repeaterModel
    )
    {

    }

    #endregion
  }
}