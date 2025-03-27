using System.ComponentModel.DataAnnotations;

namespace VACARM.Domain.Models
{
  public interface IDeviceModel
  {
    #region Parameters

    /// <summary>
    /// Foreign key
    /// </summary>
    [Required]
    string ActualId { get; set; }

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
    /// <param name="actualId">The actual ID</param>
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
      out string actualId,
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