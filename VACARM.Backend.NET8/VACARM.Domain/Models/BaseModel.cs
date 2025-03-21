using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace VACARM.Domain.Models
{
  /// <summary>
  /// Object with primary key.
  /// </summary>
  public partial class BaseModel :
    IBaseModel
  {
    #region Parameters

    private uint id { get; set; }

    public uint Id
    {
      get
      {
        return id;
      }
      set
      {
        id = value;
        OnPropertyChanged(nameof(this.Id));
      }
    }

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>

    [ExcludeFromCodeCoverage]
    public BaseModel()
    {
      Id = uint.MinValue;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="id">The ID</param>

    [ExcludeFromCodeCoverage]
    public BaseModel(uint id)
    {
      Id = id;
    }

    #endregion
  }
}