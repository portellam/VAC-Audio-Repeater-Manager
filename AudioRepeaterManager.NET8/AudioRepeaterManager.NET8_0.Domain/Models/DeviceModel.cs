using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using AudioRepeaterManager.NET8_0.Backend.Structs;

namespace AudioRepeaterManager.NET8_0.Backend.Models
{
  public class DeviceModel :
    IDeviceModel,
    INotifyPropertyChanged
  {
    #region Parameters

    private uint id { get; set; }
    private string actualId { get; set; }
    private bool? isInput { get; set; }
    private bool? isOutput { get; set; }
    private bool? isPresent { get; set; }
    private string name { get; set; }

    /// <summary>
    /// Primary Key
    /// </summary>
    public uint Id
    {
      get
      {
        return id;
      }
      set
      {
        id = value;
        OnPropertyChanged(nameof(id));
      }
    }

    /// <summary>
    /// Foreign key
    /// </summary>
    public string ActualId
    {
      get
      {
        return actualId;
      }
      set
      {
        actualId = value;
        OnPropertyChanged(nameof(actualId));
      }
    }

    /// <summary>
    /// Is both Input and Output.
    /// </summary>
    public bool IsDuplex
    {
      get
      {
        return IsInput == IsOutput;
      }
    }

    public bool IsInput
    {
      get
      {
        if (isInput is null)
        {
          return false;
        }

        return (bool)isInput;
      }
      set
      {
        if ((bool?)value is null)
        {
          return;
        }

        isInput = value;
        OnPropertyChanged(nameof(ChannelConfig));
      }
    }

    public bool IsOutput
    {
      get
      {
        if (isOutput is null)
        {
          return false;
        }

        return (bool)isOutput;
      }
      set
      {
        if ((bool?)value is null)
        {
          return;
        }

        isOutput = value;
        OnPropertyChanged(nameof(ChannelConfig));
      }
    }

    public bool IsPresent
    {
      get
      {
        if (isPresent is null)
        {
          return false;
        }

        return (bool)isPresent;
      }
      set
      {
        if ((bool?)value is null)
        {
          return;
        }

        isPresent = value;
        OnPropertyChanged(nameof(ChannelConfig));
      }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public string Name
    {
      get
      {
        return name;
      }
      set
      {
        name = value;
        OnPropertyChanged(nameof(ChannelConfig));
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

    #endregion

    #region Logic

    /// <summary>
    /// Abstract of the actual audio device.
    /// </summary>
    /// <param name="id">The device ID</param>
    /// <param name="actualId">The actual device ID</param>
    /// <param name="name">The device name</param>
    /// <param name="isInput">True/false is an input device</param>
    /// <param name="isOutput">True/false is an output device</param>
    /// <param name="isPresent">True/false is the device present</param>
    [ExcludeFromCodeCoverage]
    public DeviceModel
    (
      uint id,
      string actualId,
      string name,
      bool? isInput,
      bool? isOutput,
      bool? isPresent
    )
    {
      Id = id;
      ActualId = actualId;
      Name = name;
      IsInput = (bool)isInput;
      IsOutput = (bool)isOutput;
      IsPresent = (bool)isPresent;
    }

    /// <summary>
    /// Deconstructor
    /// </summary>
    /// <param name="id">The device ID</param>
    /// <param name="actualId">The actual device ID</param>
    /// <param name="name">The device name</param>
    /// <param name="isInput">True/false is an input device</param>
    /// <param name="isOutput">True/false is an output device</param>
    /// <param name="isPresent">True/false is the device present</param>
    [ExcludeFromCodeCoverage]
    public void Deconstruct
    (
      out uint id,
      out string actualId,
      out string name,
      out bool? isInput,
      out bool? isOutput,
      out bool? isPresent
    )
    {
      id = Id;
      actualId = ActualId;
      name = Name;
      isInput = IsInput;
      isOutput = IsOutput;
      isPresent = IsPresent;
    }

    /// <summary>
    /// Logs event when property has changed.
    /// </summary>
    /// <param name="propertyName">The property name</param>
    private void OnPropertyChanged(string propertyName)
    {
      PropertyChanged?.Invoke
      (
        this,
        new PropertyChangedEventArgs(propertyName)
      );

      Debug.WriteLine
      (
        string.Format
        (
          "PropertyChanged: {0}",
          propertyName
        )
      );
    }

    #endregion
  }
}