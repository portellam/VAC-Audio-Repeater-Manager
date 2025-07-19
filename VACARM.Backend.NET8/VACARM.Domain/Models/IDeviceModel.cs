using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VACARM.Domain.Models
{
  public interface IDeviceModel
  {
    #region Parameters

    /// <summary>
    /// Foreign keys
    /// </summary>
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    ICollection<uint> LinkIdCollection { get; set; }

    /// <summary>
    /// Foreign key
    /// </summary>
    [Required]
    string ActualId { get; set; }

    [Required]
    ICollection<RepeaterDeviceLinkModel> LinkModelCollection
    { get; set; }

    [Required]
    bool IsCapture { get; set; }
    bool IsDefault { get; set; }
    bool IsEnabled { get; set; }
    bool IsMuted { get; set; }
    bool IsRender { get; }
    bool IsPresent { get; set; }
    string Availability { get; }
    string Name { get; set; }
    string Role { get; set; }

    #endregion

    #region Logic

    /// <summary>
    /// Deconstructor
    /// </summary>
    /// <param name="id">The ID</param>
    /// <param name="linkIdCollection">
    /// The repeater device link ID collection
    /// </param>
    /// <param name="actualId">The actual ID</param>
    /// <param name="createdDateTime">The construct date-time</param>
    /// <param name="modifiedDateTime">The last date-time a change occurred</param>
    /// <param name="linkModelCollection">
    /// The repeater device link model collection
    /// </param>
    /// <param name="isCapture">True/false is a capture device</param>
    /// <param name="isDefault">True/false is the device default</param>
    /// <param name="isEnabled">True/false is the device enabled</param>
    /// <param name="isMuted">True/false is the device muted</param>
    /// <param name="isPresent">True/false is the device present</param>
    /// <param name="name">The name</param>
    /// <param name="role">The role</param>
    void Deconstruct
    (
      out uint id,
      out ICollection<uint> linkIdCollection,
      out string actualId,
      out DateTime createdDateTime,
      out DateTime modifiedDateTime,
      out ICollection<RepeaterDeviceLinkModel> linkModelCollection,
      out string name,
      out bool isCapture,
      out bool? isDefault,
      out bool? isEnabled,
      out bool? isMuted,
      out bool? isPresent,
      out string role
    );

    #endregion
  }
}