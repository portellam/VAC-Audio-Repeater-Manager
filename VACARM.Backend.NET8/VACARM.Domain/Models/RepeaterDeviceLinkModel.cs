using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int InputDeviceId { get; set; }

    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int OutputDeviceId { get; set; }

    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int RepeaterId { get; set; }

    public DeviceModel InputDeviceModel { get; set; }
    public DeviceModel OutputDeviceModel { get; set; }
    public RepeaterModel RepeaterModel { get; set; }

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    [ExcludeFromCodeCoverage]
    public RepeaterDeviceLinkModel()
    {
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="id">The ID</param>
    /// <param name="inputDeviceModel">The input device</param>
    /// <param name="outputDeviceModel">The output device</param>
    /// <param name="repeaterModel">The repeater</param>
    [ExcludeFromCodeCoverage]
    public RepeaterDeviceLinkModel
    (
      int id,
      DeviceModel inputDeviceModel,
      DeviceModel outputDeviceModel,
      RepeaterModel repeaterModel
    ) :
      base(id)
    {
      this.InputDeviceModel = inputDeviceModel;
      this.OutputDeviceModel = outputDeviceModel;
      this.RepeaterModel = repeaterModel;
    }

    [ExcludeFromCodeCoverage]
    public void Deconstruct
    (
      out int id,
      out int repeaterId,
      out int inputDeviceId,
      out int outputDeviceId,
      out DateTime createdDateTime,
      out DateTime modifiedDateTime,
      out DeviceModel inputDeviceModel,
      out DeviceModel outputDeviceModel,
      out RepeaterModel repeaterModel
    )
    {
      base.Deconstruct
      (
        out id,
        out createdDateTime,
        out modifiedDateTime
      );

      inputDeviceId = this.InputDeviceId;
      outputDeviceId = this.OutputDeviceId;
      repeaterId = this.RepeaterId;
      inputDeviceModel = this.InputDeviceModel;
      outputDeviceModel = this.OutputDeviceModel;
      repeaterModel = this.RepeaterModel;
    }

    #endregion
  }
}