using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace VACARM.Domain.Models
{
  /// <summary>
  /// Object with primary key.
  /// </summary>
  public class BaseModel :
    IBaseModel
  {
    #region Parameters

    public virtual uint Id { get; set; }
    public virtual DateTime CreatedDateTime { get; private set; }
    public virtual DateTime ModifiedDateTime { get; set; }

    #endregion

    #region Logic

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="id">The ID</param>

    [ExcludeFromCodeCoverage]
    public BaseModel(uint id)
    {
      this.Id = id;
      this.CreatedDateTime = DateTime.UtcNow;
      this.ModifiedDateTime = CreatedDateTime;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="id">The ID</param>
    /// <param name="createdDateTime">The construct date-time</param>
    /// <param name="modifiedDateTime">The last date-time a change occurred</param>
    [ExcludeFromCodeCoverage]
    public BaseModel
    (
      uint id,
      DateTime createdDateTime,
      DateTime modifiedDateTime
    )
    {
      this.Id = id;
      this.CreatedDateTime = createdDateTime;
      this.ModifiedDateTime = modifiedDateTime;
    }

    [ExcludeFromCodeCoverage]
    public virtual void Deconstruct
    (
      out uint id,
      out DateTime createdDateTime,
      out DateTime modifiedDateTime
    )
    {
      id = this.Id;
      createdDateTime = this.CreatedDateTime;
      modifiedDateTime = this.ModifiedDateTime;
    }

    #endregion
  }
}