using System.Diagnostics.CodeAnalysis;

namespace VACARM.Domain.Models
{
  /// <summary>
  /// A snapshot record of a system audio device.
  /// </summary>
  public class DeviceModel :
    BaseModel,
    IDeviceModel
  {
    #region Parameters

    private bool? isDefault { get; set; } = false;
    private bool? isEnabled { get; set; } = false;
    private bool? isMuted { get; set; } = false;
    private bool? isPresent { get; set; } = false;
    private string role { get; set; } = string.Empty;

    public override uint Id { get; set; }
    public bool IsCapture { get; set; }

    public bool IsDefault
    {
      get
      {
        if (isDefault == null)
        {
          return false;
        }

        return this.isDefault
          .Value;
      }
      set
      {
        isDefault = value;
      }
    }

    public bool IsEnabled
    {
      get
      {
        if (isEnabled == null)
        {
          return false;
        }

        return this.isEnabled
          .Value;
      }
      set
      {
        isEnabled = value;
      }
    }

    public bool IsMuted
    {
      get
      {
        if (isMuted == null)
        {
          return false;
        }

        return this.isMuted
          .Value;
      }
      set
      {
        this.isMuted = value;
      }
    }

    public bool IsPresent
    {
      get
      {
        if (this.isPresent == null)
        {
          return false;
        }

        return this.isPresent
          .Value;
      }
      set
      {
        this.isPresent = value;
      }
    }

    public bool IsRender
    {
      get
      {
        return !this.IsCapture;
      }
    }

    public string ActualId { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Availability
    {
      get
      {
        if
        (
          this.isPresent is null
          || !this.isPresent.Value
        )
        {
          return "Absent";
        }

        return "Present";
      }
    }

    public string Role
    {
      get
      {
        return this.role;
      }
      set
      {
        if (string.IsNullOrWhiteSpace(value))
        {
          value = string.Empty;
        }

        this.role = value;
      }
    }

    public virtual ICollection<RepeaterDeviceLinkModel> RepeaterDeviceLinkCollection
    { get; set; } = Array.Empty<RepeaterDeviceLinkModel>();

    #endregion

    #region Logic

    /// <summary>
    /// Abstract of the actual audio device.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <param name="actualId">The actual ID</param>
    /// <param name="name">The name</param>
    [ExcludeFromCodeCoverage]
    public DeviceModel
    (
      uint id,
      string actualId,
      string name,
      bool isCapture
    ) :
      base(id)
    {
      this.Id = id;
      this.ActualId = actualId;
      this.Name = name;
      this.IsCapture = isCapture;
    }

    /// <summary>
    /// Abstract of the actual audio device.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <param name="actualId">The actual ID</param>
    /// <param name="name">The name</param>
    /// <param name="isCapture">True/false is a capture device</param>
    /// <param name="isEnabled">True/false is the device enabled</param>
    /// <param name="isMuted">True/false is the device muted</param>
    /// <param name="isPresent">True/false is the device present</param>
    /// <param name="role">The role</param>
    [ExcludeFromCodeCoverage]
    public DeviceModel
    (
      uint id,
      string actualId,
      string name,
      bool isCapture,
      bool? isDefault,
      bool? isEnabled,
      bool? isMuted,
      bool? isPresent,
      bool? isRender,
      string? role
    ) :
      base(id)
    {
      this.Id = id;
      this.ActualId = actualId;
      this.Name = name;
      this.IsCapture = isCapture;
      this.IsDefault = (bool)isDefault;
      this.IsEnabled = (bool)isEnabled;
      this.IsMuted = (bool)isMuted;
      this.IsPresent = (bool)isPresent;
      this.Role = role;
    }

    /// <summary>
    /// Abstract of the actual audio device.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <param name="createdDateTime">The construct date-time</param>
    /// <param name="modifiedDateTime">The last date-time a change occurred</param>
    /// <param name="actualId">The actual ID</param>
    /// <param name="name">The name</param>
    /// <param name="isCapture">True/false is a capture device</param>
    /// <param name="isEnabled">True/false is the device enabled</param>
    /// <param name="isMuted">True/false is the device muted</param>
    /// <param name="isPresent">True/false is the device present</param>
    /// <param name="role">The role</param>
    [ExcludeFromCodeCoverage]
    public DeviceModel
    (
      uint id,
      DateTime createdDateTime,
      DateTime modifiedDateTime,
      string actualId,
      string name,
      bool isCapture,
      bool? isDefault,
      bool? isEnabled,
      bool? isMuted,
      bool? isPresent,
      bool? isRender,
      string? role
    ) :
      base
      (
        id,
        createdDateTime,
        modifiedDateTime
      )
    {
      this.Id = id;
      this.ActualId = actualId;
      this.Name = name;
      this.IsCapture = isCapture;
      this.IsDefault = (bool)isDefault;
      this.IsEnabled = (bool)isEnabled;
      this.IsMuted = (bool)isMuted;
      this.IsPresent = (bool)isPresent;
      this.Role = role;
    }

    [ExcludeFromCodeCoverage]
    public void Deconstruct
    (
      out uint id,
      out DateTime createdDateTime,
      out DateTime modifiedDateTime,
      out string actualId,
      out string name,
      out bool isCapture,
      out bool? isDefault,
      out bool? isEnabled,
      out bool? isMuted,
      out bool? isPresent,
      out string role
    )
    {
      base.Deconstruct
      (
        out id,
        out createdDateTime,
        out modifiedDateTime
      );

      actualId = this.ActualId;
      name = this.Name;
      isCapture = this.IsCapture;
      isDefault = this.IsDefault;
      isEnabled = this.IsEnabled;
      isMuted = this.IsMuted;
      isPresent = this.IsPresent;
      role = this.Role;
    }

    #endregion
  }
}