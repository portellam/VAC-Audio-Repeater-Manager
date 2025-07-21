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
    private string name { get; set; } = string.Empty;
    private string role { get; set; } = string.Empty;

    public bool IsCapture { get; set; }

    public bool? IsDefault
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

    public bool? IsEnabled
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

    public bool? IsMuted
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

    public bool? IsPresent
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

    public bool? IsRender
    {
      get
      {
        return !this.IsCapture;
      }
    }

    public string ActualId { get; set; }

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

    public string Name
    {
      get
      {
        return this.name;
      }
      set
      {
        if (string.IsNullOrWhiteSpace(value))
        {
          value = string.Empty;
        }

        this.name = value;
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

    public ICollection<uint> LinkIdCollection
    { get; set; } = Array.Empty<uint>();

    public ICollection<RepeaterDeviceLinkModel> LinkModelCollection
    { get; set; } = Array.Empty<RepeaterDeviceLinkModel>();

    #endregion

    #region Logic

    /// <summary>
    /// Abstract of the actual audio device.
    /// </summary>
    /// <param name="id">The ID</param>
    /// <param name="actualId">The actual ID</param>
    /// <param name="isCapture">True/false is a capture device</param>
    [ExcludeFromCodeCoverage]
    public DeviceModel
    (
      int id,
      string actualId,
      bool isCapture
    ) :
      base(id)
    {
      this.ActualId = actualId;
      this.IsCapture = isCapture;
    }

    /// <summary>
    /// Abstract of the actual audio device.
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
    /// <param name="isRender">True/false is a render device</param>
    /// <param name="name">The name</param>
    /// <param name="role">The role</param>
    [ExcludeFromCodeCoverage]
    public DeviceModel
    (
      int id,
      ICollection<uint> linkIdCollection,
      string actualId,
      DateTime createdDateTime,
      DateTime modifiedDateTime,
      ICollection<RepeaterDeviceLinkModel> linkModelCollection,
      bool isCapture,
      bool? isDefault = null,
      bool? isEnabled = null,
      bool? isMuted = null,
      bool? isPresent = null,
      bool? isRender = null,
      string? name = null,
      string? role = null
    ) :
      base
      (
        id,
        createdDateTime,
        modifiedDateTime
      )
    {
      this.LinkIdCollection = linkIdCollection;
      this.ActualId = actualId;
      this.LinkModelCollection = linkModelCollection;
      this.IsCapture = isCapture;
      this.IsDefault = isDefault;
      this.IsEnabled = isEnabled;
      this.IsMuted = isMuted;
      this.IsPresent = isPresent;
      this.Name = name;
      this.Role = role;
    }

    [ExcludeFromCodeCoverage]
    public void Deconstruct
    (
      out int id,
      out ICollection<uint> linkIdCollection,
      out string actualId,
      out DateTime createdDateTime,
      out DateTime modifiedDateTime,
      out ICollection<RepeaterDeviceLinkModel> linkModelCollection,
      out bool isCapture,
      out bool? isDefault,
      out bool? isEnabled,
      out bool? isMuted,
      out bool? isPresent,
      out string? name,
      out string? role
    )
    {
      base.Deconstruct
      (
        out id,
        out createdDateTime,
        out modifiedDateTime
      );

      linkIdCollection = this.LinkIdCollection;
      actualId = this.ActualId;
      linkModelCollection = this.LinkModelCollection;
      isCapture = this.IsCapture;
      isDefault = this.IsDefault;
      isEnabled = this.IsEnabled;
      isMuted = this.IsMuted;
      isPresent = this.IsPresent;
      name = this.Name;
      role = this.Role;
    }

    #endregion
  }
}