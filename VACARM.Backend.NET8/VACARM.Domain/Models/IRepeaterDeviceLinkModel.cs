using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VACARM.Domain.Models
{
  public interface IRepeaterDeviceLinkModel
  {
    #region Parameters

    /// <summary>
    /// Foreign key
    /// </summary>
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    int InputDeviceId { get; set; }

    /// <summary>
    /// Foreign key
    /// </summary>
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    int OutputDeviceId { get; set; }

    /// <summary>
    /// Foreign key
    /// </summary>
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    int RepeaterId { get; set; }

    DeviceModel InputDeviceModel { get; set; }
    DeviceModel OutputDeviceModel { get; set; }
    RepeaterModel RepeaterModel { get; set; }

    #endregion

    #region Logic

    /// <summary>
    /// Deconstructor
    /// </summary>
    /// <param name="id">The ID</param>
    /// <param name="repeaterId">The repeater ID</param>
    /// <param name="inputDeviceId">The input device ID</param>
    /// <param name="outputDeviceId">The output device ID</param>
    /// <param name="createdDateTime">The construct date-time</param>
    /// <param name="modifiedDateTime">The last date-time a change occurred</param>
    /// <param name="inputDeviceModel">
    /// The input<typeparamref name="DeviceModel"/>
    /// </param>
    /// <param name="outputDeviceModel">
    /// The output <typeparamref name="DeviceModel"/>
    /// </param>
    /// <param name="repeaterModel">The repeater</param>
    void Deconstruct
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
    );

    #endregion
  }
}