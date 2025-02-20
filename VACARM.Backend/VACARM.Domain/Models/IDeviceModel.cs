using System.ComponentModel.DataAnnotations;

namespace VACARM.Domain.Models
{
  public interface IDeviceModel :
    IBaseModel
  {
    #region Parameters

    /// <summary>
    /// Foreign key
    /// </summary>
    [Required]
    string ActualId { get; set; }
    bool IsCapture { get; set; }
    bool IsDefault { get; set; }

    /// <summary>
    /// Is both Capture and Render.
    /// </summary>
    bool IsDuplex { get; }

    bool IsEnabled { get; set; }
    bool IsMuted { get; set; }
    bool IsRender { get; set; }
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
    /// <param name="isCapture">True/false is the device default</param>
    /// <param name="isEnabled">True/false is the device enabled</param>
    /// <param name="isMuted">True/false is the device muted</param>
    /// <param name="isPresent">True/false is the device present</param>
    /// <param name="isRender">True/false is a render device</param>
    /// <param name="name">The name</param>
    /// <param name="role">The role</param>
    void Deconstruct
    (
      out uint id,
      out string actualId,
      out string name,
      out bool? isCapture,
      out bool? isDefault,
      out bool? isEnabled,
      out bool? isMuted,
      out bool? isPresent,
      out bool? isRender,
      out string role
    );

    #endregion
  }
}