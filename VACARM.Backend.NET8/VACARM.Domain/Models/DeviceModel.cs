using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace VACARM.Domain.Models
{
  /// <summary>
  /// A snapshot record of a system audio device.
  /// </summary>
  public class DeviceModel :
    BaseModel,
    IDeviceModel,
    INotifyPropertyChanged
  {
    #region Parameters

    private string actualId { get; set; } = string.Empty;
    private bool? isCapture { get; set; }
    private bool? isDefault { get; set; }
    private bool? isEnabled { get; set; }
    private bool? isMuted { get; set; }
    private bool? isPresent { get; set; }
    private bool? isRender { get; set; }
    private string name { get; set; } = string.Empty;
    private string role { get; set; } = string.Empty;

    public override uint Id { get; set; }

    public string ActualId
    {
      get
      {
        return actualId;
      }
      set
      {
        actualId = value;
        base.OnPropertyChanged(nameof(actualId));
      }
    }

    public bool IsCapture
    {
      get
      {
        if (isCapture == null)
        {
          return false;
        }

        return (bool)isCapture;
      }
      set
      {
        isCapture = value;
        base.OnPropertyChanged(nameof(IsCapture));
      }
    }

    public bool IsDefault
    {
      get
      {
        if (isDefault == null)
        {
          return false;
        }

        return (bool)isDefault;
      }
      set
      {
        isDefault = value;
        base.OnPropertyChanged(nameof(IsDefault));
      }
    }

    public bool IsDuplex
    {
      get
      {
        return IsCapture == IsRender;
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

        return (bool)isEnabled;
      }
      set
      {
        isEnabled = value;
        base.OnPropertyChanged(nameof(IsEnabled));
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

        return (bool)isMuted;
      }
      set
      {
        isMuted = value;
        base.OnPropertyChanged(nameof(IsMuted));
      }
    }

    public bool IsPresent
    {
      get
      {
        if (isPresent == null)
        {
          return false;
        }

        return (bool)isPresent;
      }
      set
      {
        isPresent = value;
        base.OnPropertyChanged(nameof(IsPresent));
      }
    }

    public bool IsRender
    {
      get
      {
        if (isRender == null)
        {
          return false;
        }

        return (bool)isRender;
      }
      set
      {
        isRender = value;
        base.OnPropertyChanged(nameof(IsRender));
      }
    }

    public string Name
    {
      get
      {
        return name;
      }
      set
      {
        name = value;
        base.OnPropertyChanged(nameof(Name));
      }
    }

    public string Availability
    {
      get
      {
        if
        (
          isPresent is null
          || isPresent.Value
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
        return role;
      }
      set
      {
        if (string.IsNullOrWhiteSpace(value))
        {
          value = string.Empty;
        }

        role = value;
        base.OnPropertyChanged(nameof(Role));
      }
    }

    #endregion

    #region Logic

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
    /// <param name="isRender">True/false is a render device</param>
    [ExcludeFromCodeCoverage]
    public DeviceModel
    (
      uint id,
      string actualId,
      string name,
      bool? isCapture,
      bool? isDefault,
      bool? isEnabled,
      bool? isMuted,
      bool? isRender,
      bool? isPresent,
      string? role
    ) :
      base(id)
    {
      Id = id;
      ActualId = actualId;
      Name = name;
      IsCapture = (bool)isCapture;
      IsDefault = (bool)isDefault;
      IsEnabled = (bool)isEnabled;
      IsMuted = (bool)isMuted;
      IsPresent = (bool)isPresent;
      IsRender = (bool)isRender;
      Role = role;
    }

    [ExcludeFromCodeCoverage]
    public void Deconstruct
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
    )
    {
      id = Id;
      actualId = ActualId;
      name = Name;
      isCapture = IsCapture;
      isDefault = IsDefault;
      isEnabled = IsEnabled;
      isMuted = IsMuted;
      isRender = IsRender;
      isPresent = IsPresent;
      role = Role;
    }

    #endregion
  }
}